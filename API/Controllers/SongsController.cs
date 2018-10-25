using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly SpotyPieIDbContext _ctx;
        private readonly CancellationTokenSource cts;
        private CancellationToken ct;
        private Service.Service _service;

        public SongsController(SpotyPieIDbContext ctx, Service.Service service)
        {
            _ctx = ctx;
            _service = service;
            cts = new CancellationTokenSource();
            ct = cts.Token;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                //Need includes
                var song = await _ctx.Items.FirstOrDefaultAsync(x => x.Id == id);
                return new JsonResult(song);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSongs()
        {
            try
            {
                var songs = await _service.GetSongList();
                return new JsonResult(songs);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
