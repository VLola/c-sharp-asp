using Microsoft.AspNetCore.Mvc;
using Project_69_Library.Models;
using Project_69_Library.Repositories;

namespace Project_69.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodController : Controller
    {
        [HttpGet]
        public IEnumerable<Food> Get()
        {
            return FoodRepository.GetAll();
        }

        [HttpPost("{animalName}, {foodName}, {price}, {urlImage}")]
        public void Add(string animalName, string foodName, decimal price, string urlImage)
        {
            FoodRepository.Add(animalName, foodName, price, urlImage);
        }
    }
}
