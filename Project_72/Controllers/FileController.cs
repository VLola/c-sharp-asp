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

        [HttpGet("GetAll")]
        public IEnumerable<Models.File> GetAll(DateTime start, DateTime end)
        {
            return repo.GetAll(start, end.AddDays(1));
        }

        [HttpGet("GetFile")]
        public object GetFile(string name)
        {
            return repo.GetFile(name);
        }

        [HttpPost("Add")]
        public HttpStatusCode OnPostUploadAsync(IFormFile file)
        {
            if (!TryValidateModel(file, nameof(IFormFile)))
                return HttpStatusCode.BadRequest;
            ModelState.ClearValidationState(nameof(IFormFile));
            if (repo.Add(file)) return HttpStatusCode.Created;
            else return HttpStatusCode.BadRequest;
        }

        [HttpDelete("Delete")]
        public HttpStatusCode Delete(string name)
        {
            if (repo.Delete(name)) return HttpStatusCode.OK;
            else return HttpStatusCode.NotFound;
        }
    }
}
