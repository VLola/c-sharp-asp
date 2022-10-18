using Microsoft.AspNetCore.Mvc;
using Project_69_Library.Models;
using Project_69_Library.Repositories;

namespace Project_69.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalController : Controller
    {
        [HttpGet]
        public IEnumerable<Animal> Get()
        {
            return AnimalRepository.GetAll();
        }

        [HttpPost("{name}")]
        public void Add(string name)
        {
            AnimalRepository.Add(name);
        }
    }
}
