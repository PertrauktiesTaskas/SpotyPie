using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.BackEnd;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IDb _ctd;
        private readonly SpotyPieIDbContext _ctx;
        private readonly CancellationTokenSource cts;
        private CancellationToken ct;

        public AlbumController(IDb ctdd, SpotyPieIDbContext ctx)
        {
            _ctd = ctdd;
            _ctx = ctx;
            cts = new CancellationTokenSource();
            ct = cts.Token;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _ctd.Start();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(int id)
        {
            try
            {
                //Need includes
                var album = await _ctx.Albums
                    .Include(x => x.Images)
                    .Include(x => x.Songs)
                    .FirstOrDefaultAsync(x => x.Id == id);

                return new JsonResult(new
                {
                    Artist = JsonConvert.DeserializeObject<List<Artist>>(album.Artists)[0].Name,
                    album.Name,
                    album.Images,
                    album.ReleaseDate,
                    album.TotalTracks,
                    album.Songs
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
