using Microsoft.AspNetCore.Mvc;
using Project_68.Models.Repositories;
using Project_68.Models;

namespace Project_68.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArchiveController : Controller
    {
        private readonly ILogger<ArchiveController> _logger;
        public ArchiveController(ILogger<ArchiveController> logger)
        {
            _logger = logger;
        }
        [HttpGet("GetAll")]
        public IEnumerable<Archive> Get()
        {
            return ArchiveRepository.GetAll();
        }
    }
}
