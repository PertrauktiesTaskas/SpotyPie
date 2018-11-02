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
        private CancellationToken ct;
        private HttpClient client;

        public HomeController()
        {
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

                    multiContent.Add(new ByteArrayContent(buffer), "file", file.FileName);
                }

                var response = await client.PostAsync($"http://localhost:51924/api/upload/", multiContent);
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
