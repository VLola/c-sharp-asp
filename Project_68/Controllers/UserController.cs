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
            return UserRepository.Set(new User() { 
                Name = name, 
                Password = BCrypt.Net.BCrypt.HashPassword(password), 
                Token = NewToken()
            });
        }

        private static string NewToken()
        {
            string token;
            while (true)
            {
                token = RandomToken();
                if (!UserRepository.CheckToken(token)) break;
            }
            return token;
        }
        private static string RandomToken()
        {
            string alphabet = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            string token = "";
            for (int i = 0; i < 8; i++)
            {
                token += alphabet[new Random().Next(0, alphabet.Length - 1)];
            }
            return token;
        }
        //public static string Generate(string pass)
        //{
        //    return BCrypt.Net.BCrypt.HashPassword(pass);
        //}
        //public static bool Veryfy(string pass, string hash)
        //{
        //    return BCrypt.Net.BCrypt.Verify(pass, hash);
        //}
    }
}
