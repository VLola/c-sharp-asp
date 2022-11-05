using Microsoft.AspNetCore.Mvc;
using Project_71_Library.Models;
using Project_71_Library.UnitOfWorks;
using System.Net;

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
        public HttpStatusCode Add([FromForm] User user)
        {
            if (!TryValidateModel(user, nameof(User)))
                return HttpStatusCode.BadRequest;
            ModelState.ClearValidationState(nameof(User));
            if (work.UserRepo.CheckName(user.Name))
            {
                return HttpStatusCode.Conflict;
            }
            else
            {
                if (work.UserRepo.Add(user) > 0)
                {
                    return HttpStatusCode.Created;
                }
                else
                {
                    return HttpStatusCode.NoContent;
                }
            }
        }

        [HttpPost("Login")]
        public HttpStatusCode Login([FromForm] User user)
        {
            if (!TryValidateModel(user, nameof(User)))
                return HttpStatusCode.BadRequest;
            ModelState.ClearValidationState(nameof(User));
            if (work.UserRepo.CheckUser(user.Name, user.Password)) return HttpStatusCode.OK;
            else return HttpStatusCode.NoContent;
        }

        [HttpPost("Repassword")]
        public HttpStatusCode Repassword([FromForm] User user)
        {
            if (!TryValidateModel(user, nameof(User)))
                return HttpStatusCode.BadRequest;
            ModelState.ClearValidationState(nameof(User));
            if (work.UserRepo.Update(user.Name, user.Password)) {
                return HttpStatusCode.OK;
            }
            else return HttpStatusCode.NoContent;
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(int id)
        {
            if (work.UserRepo.Delete(id)) return Ok();
            else return NotFound();
        }
    }
}
