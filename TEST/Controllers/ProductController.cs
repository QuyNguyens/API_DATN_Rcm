using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEST.Helper;
namespace TEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        public ProductController(IWebHostEnvironment environment) { 
            this._environment = environment;
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile formfile) {
            ApiReponse reponse = new ApiReponse();
            Guid productcode = Guid.NewGuid();
            try
            {
                string Filepath = this.GetFilePath(productcode.ToString());
                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }
                string imagepath = Filepath + "\\" + productcode + ".png";
                if(!System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                using (FileStream stream=System.IO.File.Create(imagepath))
                {
                    await formfile.CopyToAsync(stream);
                    reponse.ResponseCode = 200;
                    reponse.Result = productcode.ToString()+".png";
                }
            }catch(Exception ex)
            {
                reponse.ErroreMessage = ex.Message;
            }
            return Ok(reponse);
            
        }

        [NonAction]
        private string GetFilePath(string productcode)
        {
            return this._environment.WebRootPath + "\\Upload\\" ;

        }

        [HttpGet("get-image")]
        public async Task<IActionResult> GetImage(string productcode)
        {
            string imageUrl = string.Empty;
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string filePath = GetFilePath(productcode);
                string imagepath = filePath + "\\" + productcode + ".png";
                if (System.IO.File.Exists(imagepath))
                {
                    imageUrl = hosturl + "/Upload/" + productcode + ".png";
                }
                else
                {
                    return NotFound();
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(imageUrl);
        }

    }
}
