using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Services.Contracts;

namespace StudyBuddy.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UploadController(IServiceManager serviceManager, IWebHostEnvironment webHostEnvironment)
        {
            this.serviceManager = serviceManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                string filepath = webHostEnvironment.WebRootPath + "\\Images";

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                string ImagePath = filepath + "\\" + file.FileName;
                if (System.IO.File.Exists(ImagePath))
                {
                    System.IO.File.Delete(ImagePath);
                }
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    using (FileStream stream = System.IO.File.Create(ImagePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    //using (var stream = new FileStream(fullPath, FileMode.Create))
                    //{
                    //    file.CopyTo(stream);
                    //}
                    var ImageUrl = Request.Scheme + "://" + Request.Host + "/Images/" + file.FileName;
                    return Ok(ImageUrl);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
