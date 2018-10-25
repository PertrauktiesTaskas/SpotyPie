using Database;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        private readonly SpotyPieIDbContext _ctx;
        private readonly CancellationTokenSource cts;
        private CancellationToken ct;

        public StreamController(SpotyPieIDbContext ctx)
        {
            _ctx = ctx;
            cts = new CancellationTokenSource();
            ct = cts.Token;
        }

        [HttpGet("test")]
        public IActionResult Test(CancellationToken t)
        {
            return Ok("Gerai");
        }

        [HttpGet("doSomething")]
        public IActionResult GetMusic(CancellationToken t)
        {
            try
            {
                t.ThrowIfCancellationRequested();
                //"C:\Users\lukas\Source\Repos\SpotyPie\API\music.flac"
                //"/root/Music/" + file + ".flac"
                return Service.Service.OpenFile(@"C:\Users\lukas\Source\Repos\SpotyPie\API\music.flac", out FileStream fs)
                    ? File(fs, new MediaTypeHeaderValue("audio/mpeg").MediaType, true)
                    : (IActionResult)BadRequest();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }

        [HttpGet("{file}")]
        public IActionResult GetMusic(CancellationToken t, string file)
        {
            try
            {
                t.ThrowIfCancellationRequested();
                //"C:\Users\lukas\Source\Repos\SpotyPie\API\music.flac"
                //"/root/Music/" + file + ".flac"
                return Service.Service.OpenFile(@"/root/Music/" + file + ".flac", out FileStream fs)
                    ? File(fs, new MediaTypeHeaderValue("audio/mpeg").MediaType, true)
                    : (IActionResult)BadRequest();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }
    }
}
