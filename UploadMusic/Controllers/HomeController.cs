using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UploadMusic.Models;

namespace UploadMusic.Controllers
{
    public class HomeController : Controller
    {

        private readonly SpotyPieIDbContext _ctx;
        private readonly CancellationTokenSource cts;
        private CancellationToken ct;
        private readonly IDb _ctd;
        private HttpClient client;

        public HomeController(SpotyPieIDbContext ctx, IDb ctd)
        {
            _ctx = ctx;
            _ctd = ctd;
            cts = new CancellationTokenSource();
            ct = cts.Token;
            client = new HttpClient();
        }

        public IActionResult Upload()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IList<IFormFile> files)
        {
            try
            {
                MultipartFormDataContent multiContent = new MultipartFormDataContent();

                foreach (var file in files)
                {
                    var fileStream = file.OpenReadStream();

                    fileStream.Position = 0;
                    byte[] buffer = new byte[fileStream.Length];
                    for (int totalBytesCopied = 0; totalBytesCopied < fileStream.Length;)
                        totalBytesCopied += fileStream.Read(buffer, totalBytesCopied, Convert.ToInt32(fileStream.Length) - totalBytesCopied);

                    multiContent.Add(new ByteArrayContent(buffer), "files", file.FileName);
                }

                var response = await client.PostAsync($"music.pertrauktiestaskas.lt/upload/", multiContent).ConfigureAwait(false);
                //return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                ViewBag.Message = "File(s) uploaded succesfully!";
                return View();
            }
            catch (System.Exception ex)
            {
                ViewBag.Message = "Failed to upload file(s) " + ex.Message;
                return View();
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
