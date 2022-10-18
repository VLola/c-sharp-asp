using Microsoft.AspNetCore.Mvc;
using Project_69_Library.Models;
using Project_69_Library.Repositories;
using Project_69_Library.Context;
using System.Numerics;

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

        [HttpPost("{animalName}, {foodName}, {price}")]
        public void Add(string animalName, string foodName, decimal price)
        {
            FoodRepository.Add(animalName, foodName, price);
        }
    }
}
