using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_73.Cache;
using Project_73.Data;
using Project_73.Models;

namespace Project_73.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DbContextClass _context;
        private readonly ICacheService _cacheService;
        public ProductController(DbContextClass context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }
        [HttpGet]
        [Route("ProductsList")]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var productCache = new List<Product>();
            productCache = _cacheService.GetData<List<Product>>("Product");
            if (productCache == null)
            {
                var product = await _context.Products.ToListAsync();
                if (product.Count > 0)
                {
                    productCache = product;
                    var expirationTime = DateTimeOffset.Now.AddMinutes(3.0);
                    _cacheService.SetData("Product", productCache, expirationTime);
                }
            }
            return productCache;
        }

        [HttpGet]
        [Route("ProductDetail")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var productCache = new Product();
            var productCacheList = new List<Product>();
            productCacheList = _cacheService.GetData<List<Product>>("Product");
            productCache = productCacheList.Find(x => x.ProductId == id);
            if (productCache == null)
            {
                Product? product = await _context.Products.FindAsync(id);
                if (product == null) return NotFound();
                else return product;
            }
            else return productCache;
        }

        [HttpPost, Authorize]
        [Route("CreateProduct")]
        public async Task<ActionResult> POST([FromForm]Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            _cacheService.RemoveData("Product");
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
            _cacheService.RemoveData("Product");
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
            _cacheService.RemoveData("Product");
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
