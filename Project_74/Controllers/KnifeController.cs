using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_74.Data;
using Project_74.Models;

namespace Project_74.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnifeController : Controller
    {
        private readonly DbContextClass _context;
        public KnifeController(DbContextClass context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("KnifesList")]
        public async Task<ActionResult<IEnumerable<Knife>>> Get(int page, string category, int min, int max)
        {
            List<Knife> list = await _context.Knifes.ToListAsync();
            list = list.Where(item=>item.Name.ToLower().Contains(category.ToLower()) && item.Cost >= min && item.Cost <= max).ToList();
            List<Knife> pageList = new();

            for (int i = 0; i < list.Count; i++)
            {
                if(i < (page * 6) && i >= ((page * 6) - 6))
                {
                    pageList.Add(list[i]);
                }
            }
            return pageList;
        }
        [HttpGet]
        [Route("GetPages")]
        public async Task<ActionResult<int>> GetPages(string category, int min, int max)
        {
            List<Knife> list = await _context.Knifes.ToListAsync();
            list = list.Where(item => item.Name.ToLower().Contains(category.ToLower()) && item.Cost >= min && item.Cost <= max).ToList();
            int products = list.Count;
            if (products % 6 == 0) return products / 6;
            else return products / 6 + 1;
        }

        [HttpGet]
        [Route("KnifeDetail")]
        public async Task<ActionResult<Knife>> KnifeDetail(int id)
        {
            Knife? knife = await _context.Knifes.FindAsync(id);
            if (knife == null) return NotFound();
            else return knife;
        }

        [HttpPost, Authorize]
        [Route("CreateKnife")]
        public async Task<ActionResult> POST([FromForm] Knife knife)
        {
            if (!TryValidateModel(knife, nameof(Knife)))
                return BadRequest();
            ModelState.ClearValidationState(nameof(Knife));
            await _context.Knifes.AddAsync(knife);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete, Authorize]
        [Route("DeleteKnife")]
        public async Task<ActionResult> Delete(int id)
        {
            var knife = await _context.Knifes.FindAsync(id);
            if (knife == null)
            {
                return NotFound();
            }
            _context.Knifes.Remove(knife);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("UpdateKnife")]
        public async Task<ActionResult> Update([FromForm] Knife knife)
        {
            if (!TryValidateModel(knife, nameof(Knife)))
                return BadRequest();
            ModelState.ClearValidationState(nameof(Knife));
            var knifeData = await _context.Knifes.FindAsync(knife.Id);
            if (knifeData == null)
            {
                return NotFound();
            }
            knifeData.Name = knife.Name;
            knifeData.Cost = knife.Cost;
            knifeData.Stock = knife.Stock;
            knifeData.SteelHardness = knife.SteelHardness;
            knifeData.SteelGrade = knife.SteelGrade;
            knifeData.LiningMaterial = knife.LiningMaterial;
            knifeData.Description = knife.Description;
            knifeData.ImgUrl = knife.ImgUrl;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
