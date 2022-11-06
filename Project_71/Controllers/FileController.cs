using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Project_71.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : Controller
    {
        string path = Directory.GetCurrentDirectory() + "/www/Files/";
        [HttpPost("Add")]
        public HttpStatusCode OnPostUploadAsync(IFormFile file)
        {
            FileInfo fi = new FileInfo(file.FileName);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (file.Length > 0)
            {
                using (var stream = System.IO.File.Create(RandomName(fi.Extension)))
                {
                    file.CopyToAsync(stream);
                }
            }
            return HttpStatusCode.OK;
        }
        private string RandomName(string extenc)
        {
            while (true)
            {
                string name = Path.GetRandomFileName();
                string fileName = path + name + extenc;
                if (!System.IO.File.Exists(fileName))
                {
                    return fileName;
                }
            }
        }
    }
}
