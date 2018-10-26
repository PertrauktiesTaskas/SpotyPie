using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.BackEnd;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly SpotyPieIDbContext _ctx;
        private readonly CancellationTokenSource cts;
        private CancellationToken ct;
        private readonly IDb _ctd;

        public UploadController(SpotyPieIDbContext ctx, IDb ctd)
        {
            _ctx = ctx;
            _ctd = ctd;
            cts = new CancellationTokenSource();
            ct = cts.Token;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Item info, IFormFile file)
        {
            try
            {
                var filePath = @"/root/Music/" + Path.GetRandomFileName();
                info.LocalUrl = filePath;

                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream).ContinueWith((o) =>
                        {
                            _ctd.AddAudioToLibrary(info);
                        });
                    }
                }

                return Ok(new { file.Length, filePath });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
