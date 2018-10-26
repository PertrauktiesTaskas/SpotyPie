using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {

        private readonly SpotyPieIDbContext _ctx;
        private readonly CancellationTokenSource cts;
        private CancellationToken ct;
        private readonly IDb _ctd;

        public ArtistsController(SpotyPieIDbContext ctx, IDb ctd)
        {
            _ctx = ctx;
            _ctd = ctd;
            cts = new CancellationTokenSource();
            ct = cts.Token;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtist(int id)
        {
            try
            {
                var artist = await _ctx.Artists.Include(x => x.Images)
                    .FirstOrDefaultAsync(x => x.Id == id);

                return Ok(artist);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Route("ArtistWithSongs/")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistWithSongs(int id)
        {
            try
            {
                var artist = await _ctx.Artists.Include(x => x.Images).Include(x => x.Songs)
                    .FirstOrDefaultAsync(x => x.Id == id);

                return Ok(artist);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArtists()
        {
            try
            {
                var artists = await _ctd.GetArtistList();
                return Ok(artists);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
