using Microsoft.AspNetCore.Mvc;
using Project_68_Library.Models;
using Project_68_Library.Repositories;

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

        // Get all users
        [HttpGet("GetAll")]
        public IEnumerable<User> GetAll(){
            return UserRepository.GetAll();
        }

        // Get user id
        [HttpGet("Get {id}")]
        public User Get(int id)
        {
            return UserRepository.Get(id);
        }

        // Insert user
        [HttpPut("Insert {name}, {password}")]
        public string Insert(string name, string password)
        {
            string token = NewToken();
            UserRepository.Insert(new User() { 
                Name = name, 
                Password = BCrypt.Net.BCrypt.HashPassword(password), 
                Token = token
            });
            return token;
        }

        // Login user
        [HttpGet("Login {token}, {name}, {password}")]
        public bool Login(string token, string name, string password)
        {
            if (UserRepository.CheckUser(name, token))
            {
                if (UserRepository.CheckPassword(name, password)) return true;
                else return false;
            }
            else return false;
        }

        // Delete user
        [HttpPut("Delete {id}")]
        public bool Delete(int id)
        {
            return UserRepository.Delete(id);
        }

        // New token
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

        // Random token
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

    }
}
