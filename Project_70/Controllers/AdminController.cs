using Microsoft.AspNetCore.Mvc;
using Project_70_Library.Models;
using Project_70_Library.UnitOfWorks;

namespace Project_70.Controllers
{
    public class AdminController : UserController
    {
        AdminWork work;
        public AdminController()
        {
            work = new AdminWork();
        }

        [HttpPost("Add")]
        public object Add([FromForm] Note note) { 
            return work.AdminRepo.Add(note);
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(int id)
        {
            if (work.AdminRepo.Delete(id)) return Ok();
            else return NotFound();
        }
    }
}
