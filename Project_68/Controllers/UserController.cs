using Microsoft.AspNetCore.Mvc;
using Project_68.Models;
using Project_68.Models.Repositories;

namespace Project_68.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        [HttpGet()]
        public IEnumerable<User> Get(){
            return UserRepository.GetAll();
        }
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return UserRepository.Get(id);
        }
        [HttpPut("{name}, {password}")]
        public long Set(string name, string password)
        {
            return UserRepository.Set(new User() { Name = name, Password = password});
        }
    }
}
