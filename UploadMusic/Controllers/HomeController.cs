using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [RequestSizeLimit(500000000)]
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
                //http://spotypie.deveim.com
                var response = await client.PostAsync($"http://spotypie.deveim.com/api/upload/", multiContent);
                if (response.IsSuccessStatusCode)
                {
                    var rResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);
                    ViewBag.Message = "Success!";
                    ViewBag.Results = rResult;
                }
                else
                {
                    var rResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);
                    ViewBag.Message = "Failed!";
                    ViewBag.Results = rResult;
                }

                return View();
            }
            catch (System.Exception ex)
            {
                ViewBag.Message = "Failed to upload file(s) " + ex.Message;
                ViewBag.Results = null;
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
