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
            //_ctd.Start();
            return Ok();
        }

        //Returns full Album info without songs
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Id can't be " + id);

                //Need includes
                var album = await _ctx.Albums
                    .AsNoTracking()
                    .Include(x => x.Images)
                    .FirstOrDefaultAsync(x => x.Id == id);

                return Ok(album);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500);
            }
        }

        //Updated albums used time and popularity
        [Route("Update/{id}")]
        [HttpGet]
        public void IncreaseAlbumPopularity(int id)
        {
            try
            {
                var album = _ctx.Albums.First(x => x.Id == id);

                album.Popularity++;

                album.LastActiveTime = DateTime.Now;
                _ctx.Entry(album).State = EntityState.Modified;
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Return album with songs
        [Route("{id}/tracks")]
        [HttpGet]
        public async Task<IActionResult> GetAlbumTracks(int id)
        {
            try
            {
                var data = await _ctx.Albums
                    .Include(x => x.Songs).Include(x => x.Images)
                    .FirstOrDefaultAsync(x => x.Id == id);

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Return 6 most recent albums
        [Route("Recent")]
        [HttpGet]
        public async Task<IActionResult> GetRecentAlbums()
        {
            try
            {
                var data = await _ctx.Albums
                    .Include(x => x.Images)
                    .OrderByDescending(x => x.LastActiveTime)
                    .Take(6).ToListAsync();

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Return 6 most popular albums
        [Route("Popular")]
        [HttpGet]
        public async Task<IActionResult> GetPopularAlbums()
        {
            try
            {
                var data = await _ctx.Albums
                    .Include(x => x.Images)
                    .OrderByDescending(x => x.Popularity)
                    .Take(6).ToListAsync();

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        //Return 6 oldes albums
        [Route("Old")]
        [HttpGet]
        public async Task<IActionResult> GetOldAlbums()
        {
            try
            {
                var data = await _ctx.Albums
                    .Include(x => x.Images)
                    .Where(x => x.Popularity >= 1)
                    .OrderByDescending(x => x.LastActiveTime)
                    .Take(6).ToListAsync();

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
