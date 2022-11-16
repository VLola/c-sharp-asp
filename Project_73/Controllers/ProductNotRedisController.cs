using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_73.Data;
using Project_73.Models;

namespace Project_73.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductNotRedisController : Controller
    {
        private readonly DbContextClass _context;
        public ProductNotRedisController(DbContextClass context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("ProductsList")]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet]
        [Route("ProductDetail")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if(product == null) return NotFound();
            else return product;
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<ActionResult<Product>> POST(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = product.ProductId }, product);
        }

        [HttpPost]
        [Route("DeleteProduct")]
        public async Task<ActionResult<IEnumerable<Product>>> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }

        [HttpPost]
        [Route("UpdateProduct")]
        public async Task<ActionResult<IEnumerable<Product>>> Update(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            var productData = await _context.Products.FindAsync(id);
            if (productData == null)
            {
                return NotFound();
            }
            productData.ProductCost = product.ProductCost;
            productData.ProductDescription = product.ProductDescription;
            productData.ProductName = product.ProductName;
            productData.ProductStock = product.ProductStock;
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }
    }
}
