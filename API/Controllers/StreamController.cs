using Database;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        private readonly SpotyPieIDbContext _ctx;
        private readonly CancellationTokenSource cts;
        private CancellationToken ct;
        private Service.Service _service;

        public StreamController(SpotyPieIDbContext ctx, Service.Service service)
        {
            _ctx = ctx;
            _service = service;
            cts = new CancellationTokenSource();
            ct = cts.Token;
        }

        [HttpGet("test")]
        public IActionResult Test(CancellationToken t)
        {
            return Ok("Gerai");
        }

        [HttpGet("play/{id}")]
        public async Task<IActionResult> GetMusic(CancellationToken t, int id)
        {
            try
            {
                t.ThrowIfCancellationRequested();
                var path = await _service.GetAudioPathById(id);

                if (!string.IsNullOrWhiteSpace(path))
                {
                    if (_service.SetAudioPlaying(id))
                    {
                        return Service.Service.OpenFile(path, out FileStream fs)
                            ? File(fs, new MediaTypeHeaderValue("audio/mpeg").MediaType, true)
                            : (IActionResult)BadRequest();
                    }

                    return BadRequest("Cannot find path specified/File not playable");
                }

                return BadRequest("Cannot find path specified/File not playable");
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
