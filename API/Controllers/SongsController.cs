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
    public class SongsController : ControllerBase
    {
        private readonly SpotyPieIDbContext _ctx;
        private readonly CancellationTokenSource cts;
        private CancellationToken ct;
        private readonly IDb _ctd;

        public SongsController(SpotyPieIDbContext ctx, IDb ctd)
        {
            _ctx = ctx;
            _ctd = ctd;
            cts = new CancellationTokenSource();
            ct = cts.Token;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var song = await _ctx.Items
                    .FirstOrDefaultAsync(x => x.Id == id);

                return new JsonResult(new
                {
                    Artist = JsonConvert.DeserializeObject<List<Artist>>(song.Artists)[0].Name,
                    song.DurationMs,
                    song.IsPlayable,
                    song.Name
                });
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
                var songs = await _ctd.GetSongList();
                return Ok(songs);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
