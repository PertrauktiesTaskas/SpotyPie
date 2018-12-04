using Config.Net;
using Database;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly CancellationTokenSource cts;
        private CancellationToken ct;
        private readonly IDb _ctd;
        private ISettings settings;

        public ConfigurationController(IDb ctd)
        {
            _ctd = ctd;
            cts = new CancellationTokenSource();
            ct = cts.Token;
            settings = new ConfigurationBuilder<ISettings>()
                .UseJsonFile(Environment.CurrentDirectory + @"/settings.json")
                .Build();
        }

        [HttpGet("rebindAudio")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> RebindAudioFiles(CancellationToken t)
        {
            try
            {
                var r = await _ctd.BindAudioFiles();
                return Ok(r);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("changeQuality")]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult ChangeQuality([FromBody] int bits, CancellationToken t)
        {
            try
            {
                settings.StreamQuality = bits;
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("configFinshed")]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult FinishConfig(CancellationToken t)
        {
            try
            {
                settings.FirstUse = false;
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("changeCachePath")]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult ChangeCachePath([FromBody] string path, CancellationToken t)
        {
            try
            {
                settings.CachePath = path;
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("changeAudioPath")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> ChangeAudioPath([FromBody] string path, CancellationToken t)
        {
            try
            {
                settings.AudioStoragePath = path;
                return Ok(await _ctd.BindAudioFiles());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ClearImages")]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult ClearImages(CancellationToken t)
        {
            try
            {
                _ctd.RemoveCache();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("SyncImages")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> SyncronizeImages(CancellationToken t)
        {
            try
            {
                var result = await _ctd.CacheImages();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
