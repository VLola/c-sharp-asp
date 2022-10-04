using Microsoft.AspNetCore.Mvc;
using Project_68_Library.Models;
using Project_68_Library.Repositories;

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

        // Get all archives
        [HttpGet("GetAll {token}, {owner}")]
        public IEnumerable<Archive>? Get(string token, string owner)
        {
            if (UserRepository.CheckUser(owner, token))
            {
                List<Archive> list = new();
                foreach (var item in ArchiveRepository.GetAll())
                {
                    if (item.Owner == owner) list.Add(item);
                }
                return list;
            }
            else return null;
        }
    }
}
