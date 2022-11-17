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

        [HttpPost, Authorize]
        [Route("CreateProduct")]
        public async Task<ActionResult> POST([FromForm]Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete, Authorize]
        [Route("DeleteProduct")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("UpdateProduct")]
        public async Task<ActionResult> Update([FromForm]Product product)
        {
            var productData = await _context.Products.FindAsync(product.ProductId);
            if (productData == null)
            {
                return NotFound();
            }
            productData.ProductDescription = product.ProductDescription;
            productData.ProductName = product.ProductName;
            productData.ProductCost = product.ProductCost;
            productData.ProductStock = product.ProductStock;
            productData.ProductDiscount = product.ProductDiscount;
            productData.ProductImageUrl = product.ProductImageUrl;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
