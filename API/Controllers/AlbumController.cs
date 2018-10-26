using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.BackEnd;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
                if (id <= 0)
                    return BadRequest("Id can't be " + id);

                //Need includes
                var album = await _ctx.Albums
                    .Include(x => x.Images)
                    .Include(x => x.Songs)
                    .Select(x => new { x.Id, x.Artists, x.Name, x.Images, x.ReleaseDate, x.TotalTracks, x.Songs })
                    .FirstOrDefaultAsync(x => x.Id == id);

                return Ok(album);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistAlbums(int id)
        {
            try
            {
                var data = await _ctx.Artists.Include(x => x.Albums).Select(x => new { x.Id, x.Albums }).FirstOrDefaultAsync(x => x.Id == id);
                return Ok(data.Albums);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
