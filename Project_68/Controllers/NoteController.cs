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
        [HttpGet("GetAll")]
        public IEnumerable<Note> Get()
        {
            return NoteRepository.GetAll();
        }
        [HttpGet("Get {id}")]
        public Note Get(int id)
        {
            return NoteRepository.Get(id);
        }
        [HttpGet("Get {owner}, {startTime}, {endTime}")]
        public IEnumerable<Note> Get(string owner, DateTime startTime, DateTime endTime)
        {
            List<Note> list = new();
            foreach (var item in NoteRepository.GetAll())
            {
                if (item.Owner == owner && item.CreateTime >= startTime && item.CreateTime <= endTime) list.Add(item);
            }
            return list;
        }
        [HttpPut("Set {owner}, {title}, {text}")]
        public long Set(string owner, string title, string text)
        {
            return NoteRepository.Set(new Note() { Owner = owner, Title = title, Text = text, CreateTime = DateTime.Now });
        }
        [HttpPut("SetArchive {id}")]
        public bool SetArchive(int id)
        {
            ArchiveRepository.Set(NoteRepository.Get(id));
            return NoteRepository.Del(id);
        }
        [HttpPut("Delete {id}")]
        public bool Delete(int id)
        {
            return NoteRepository.Del(id);
        }
    }
}
