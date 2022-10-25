using Microsoft.AspNetCore.Mvc;
using Project_70_Library.UnitOfWorks;

namespace Project_70.Controllers
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
    }
}
