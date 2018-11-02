using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
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

        [DisableFormValueModelBinding]
        [HttpPost]
        public async Task<IActionResult> Post(/*IList<IFormFile> files*/)
        {
            try
            {
                if (Request.HasFormContentType)
                {

                    var form = Request.Form;
                    foreach (var formFile in form.Files)
                    {

                        var filePath = @"/root/Music/" + formFile.FileName;

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(fileStream);
                        }

                        if (System.IO.File.Exists(filePath))
                        {
                            await _ctd.AddAudioToLibrary(filePath, formFile.FileName, null);
                        }
                    }
                }
                return Ok("Visk gera");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class DisableFormValueModelBindingAttribute : Attribute, IResourceFilter
        {
            public void OnResourceExecuting(ResourceExecutingContext context)
            {
                var factories = context.ValueProviderFactories;
                factories.RemoveType<FormValueProviderFactory>();
                factories.RemoveType<JQueryFormValueProviderFactory>();
            }

            public void OnResourceExecuted(ResourceExecutedContext context)
            {
            }
        }
    }
}
