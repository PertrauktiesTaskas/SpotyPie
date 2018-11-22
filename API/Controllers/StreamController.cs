﻿using Config.Net;
using Database;
using Microsoft.AspNetCore.Mvc;
using Service.Settings;
using System;
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
        private readonly IDb _ctd;
        private ISettings settings;

        public StreamController(SpotyPieIDbContext ctx, IDb ctd)
        {
            _ctx = ctx;
            _ctd = ctd;
            cts = new CancellationTokenSource();
            ct = cts.Token;
            settings = new ConfigurationBuilder<ISettings>()
                .UseJsonFile(Environment.CurrentDirectory + @"/settings.json")
                .Build();
        }

        [HttpGet("test")]
        public IActionResult Test(CancellationToken t)
        {
            return Ok(Environment.CurrentDirectory);
        }

        [HttpGet("play/{id}")]
        public async Task<IActionResult> GetMusic(CancellationToken t, int id)
        {
            try
            {
                t.ThrowIfCancellationRequested();
                var path = await _ctd.GetAudioPathById(id);

                if (!string.IsNullOrWhiteSpace(path))
                {
                    if (_ctd.SetAudioPlaying(id))
                    {
                        return _ctd.OpenFile(path, out FileStream fs)
                            ? File(fs, new MediaTypeHeaderValue("audio/mpeg").MediaType, true)
                            : (IActionResult)BadRequest();
                    }

                    return BadRequest("Cannot find path specified/File not playable");
                }

                return BadRequest("Cannot find path specified/File not playable");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.InnerException);
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
                string aPath = settings != null ? settings.AudioStoragePath : "/root/Music/";
                return _ctd.OpenFile(aPath + file + ".flac", out FileStream fs)
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
