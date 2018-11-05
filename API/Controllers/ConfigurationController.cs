using Config.Net;
using Database;
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

        [HttpPost("changeQuality")]
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
        public IActionResult ChangeAudioPath([FromBody] string path, CancellationToken t)
        {
            try
            {
                settings.AudioStoragePath = path;
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ClearImages")]
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
