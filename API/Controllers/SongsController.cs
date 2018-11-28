using Database;
using Microsoft.AspNetCore.Cors;
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
        [EnableCors("AllowSpecificOrigin")]
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
        [EnableCors("AllowSpecificOrigin")]
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


        [HttpGet("GetSongAlbum/{id}")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> GetSongAlbum(int id)
        {
            try
            {

                string query = "Select AlbumId as Id from Item where id = " + id;
                var albumid = await _ctx.Items.FromSql(query).Select(x => new { x.Id }).ToListAsync();

                Album al = await _ctx.Albums.AsNoTracking().Include(x => x.Images).FirstAsync(x => x.Id == albumid.First().Id);

                return Ok(al);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
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
        [EnableCors("AllowSpecificOrigin")]
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
