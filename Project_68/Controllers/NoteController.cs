using Microsoft.AspNetCore.Mvc;
using Project_68.Models;
using Project_68.Models.Repositories;

namespace Project_68.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : Controller
    {
        private readonly ILogger<NoteController> _logger;
        public NoteController(ILogger<NoteController> logger)
        {
            _logger = logger;
        }
        [HttpGet()]
        public IEnumerable<Note> Get()
        {
            return NoteRepository.GetAll();
        }
        [HttpGet("{id}")]
        public Note Get(int id)
        {
            return NoteRepository.Get(id);
        }
        [HttpPut("{owner}, {title}, {text}")]
        public long Set(string owner, string title, string text)
        {
            return NoteRepository.Set(new Note() { Owner = owner, Title = title, Text = text, CreateTime = DateTime.Now});
        }
    }
}
