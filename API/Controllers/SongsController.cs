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

        [HttpGet("Songs")]
        public async Task<IActionResult> GetSongs()
        {
            try
            {
                var songs = await _ctx.Items.AsNoTracking().ToListAsync();
                return Ok(songs);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var song = await _ctx.Items.AsNoTracking()  ///.Select(x => new { x.Id, x.Artists, x.DurationMs, x.IsPlayable, x.Name })
                    .FirstOrDefaultAsync(x => x.Id == id);

                return Ok(song);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("{id}/Update")]
        [HttpGet]
        public async Task Update(int id)
        {
            try
            {
                var song = _ctx.Items  ///.Select(x => new { x.Id, x.Artists, x.DurationMs, x.IsPlayable, x.Name })
                    .First(x => x.Id == id);

                song.LastActiveTime = DateTime.Now;
                _ctx.Entry(song).State = EntityState.Modified;
                _ctx.SaveChanges();
            }
            catch (System.Exception ex)
            {
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
