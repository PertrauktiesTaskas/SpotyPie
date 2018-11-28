using Database;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {

        private readonly SpotyPieIDbContext _ctx;
        private readonly CancellationTokenSource cts;
        private CancellationToken ct;
        private readonly IDb _ctd;

        public PlaylistController(SpotyPieIDbContext ctx, IDb ctd)
        {
            _ctx = ctx;
            _ctd = ctd;
            cts = new CancellationTokenSource();
            ct = cts.Token;
        }

        ////Search for playlists with specified name
        //[HttpPost("/search")]
        //[EnableCors("AllowSpecificOrigin")]
        //public async Task<IActionResult> Search([FromBody] string query)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(query))
        //            return BadRequest("Bad search query");

        //        var albums = await Task.Factory.StartNew(() =>
        //        {
        //            return _ctx.Playlist
        //            .AsNoTracking()
        //            .Include(x => x.Items)
        //            .Where(x => x..Contains(query));
        //        });

        //        return Ok(albums);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return StatusCode(500);
        //    }
        //}
    }
}
