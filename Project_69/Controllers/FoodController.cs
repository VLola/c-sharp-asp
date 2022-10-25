using Microsoft.AspNetCore.Mvc;
using Project_69_Library.Models;
using Project_69_Library.Repositories;

namespace Project_69.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodController : Controller
    {
        [HttpGet("GetFoods")]
        public IEnumerable<Food> Get()
        {
            return FoodRepository.GetAll();
        }

        [HttpPost("AddFood")]
        public IActionResult Add([FromForm] Data data)
        {
            FoodRepository.Add(data.AnimalName, data.FoodName, data.Price, data.UrlImage);
            return Ok();
        }
        public class Data
        {
            public string AnimalName { get; set; }
            public string FoodName { get; set; }
            public decimal Price { get; set; }
            public string UrlImage { get; set; }
        }
    }
}
