using Microsoft.AspNetCore.Mvc;
using Project_71_Library.Models;
using Project_71_Library.UnitOfWorks;

namespace Project_71.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        UserWork work;
        public UserController()
        {
            work = new UserWork();
        }
        [HttpGet("GetAll")]
        public IEnumerable<object> GetAll() => work.UserRepo.GetAll();

        [HttpGet("GetId")]
        public object GetId(int id) => work.UserRepo.Get(id); 
        
        [HttpPost("Add")]
        public object Add([FromForm] User user)
        {
            return work.UserRepo.Add(user);
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(int id)
        {
            if (work.UserRepo.Delete(id)) return Ok();
            else return NotFound();
        }
    }
}
