using Microsoft.AspNetCore.Mvc;
using Project_67.Models;

namespace Project_67.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private static readonly ProductModel[] _products = new[]
        {
            new ProductModel(){ Id = 0, Name = "Milk"},
            new ProductModel(){ Id = 1, Name = "Fish"},
            new ProductModel(){ Id = 2, Name = "Tea"},
            new ProductModel(){ Id = 3, Name = "Apple"},
            new ProductModel(){ Id = 4, Name = "Carrot"},
            new ProductModel(){ Id = 5, Name = "Meat"},
            new ProductModel(){ Id = 6, Name = "Bread"}
        };
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return _products;
        }
        [HttpGet("{id}")]
        public ProductModel GetProduct(int id)
        {
            foreach (var product in _products)
            {
                if(product.Id == id) return product;
            }
            return null;
        }
    }
}
