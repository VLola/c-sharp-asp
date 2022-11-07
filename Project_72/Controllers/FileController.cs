using Microsoft.AspNetCore.Mvc;
using Project_72.Repositories;
using System.Net;

namespace Project_72.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : Controller
    {

        FileRepo repo;
        public FileController()
        {
            repo = new FileRepo();
        }
        [HttpPost("Add")]
        public HttpStatusCode OnPostUploadAsync(IFormFile file)
        {
            FileInfo fi = new FileInfo(file.FileName);
            if (file.Length > 0)
            {
                using (var stream = System.IO.File.Create(repo.RandomName(fi.Extension)))
                {
                    file.CopyToAsync(stream);
                }
            }
            return HttpStatusCode.OK;
        }
    }
}
