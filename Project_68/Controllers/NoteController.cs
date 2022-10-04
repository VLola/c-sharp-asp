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

        // Get all notes
        [HttpGet("GetAll {token}, {owner}")]
        public IEnumerable<Note>? GetAll(string token, string owner)
        {
            if (UserRepository.CheckUser(owner, token)) {
                List<Note> list = new(); 
                foreach (var item in NoteRepository.GetAll())
                {
                    if (item.Owner == owner) list.Add(item);
                }
                return list;
            }
            else return null;
        }

        // Get id note
        [HttpGet("Get {token}, {owner}, {id}")]
        public Note? Get(string token, string owner, int id)
        {
            if (UserRepository.CheckUser(owner, token)) {
                Note note = NoteRepository.Get(id);
                if (note != null && note.Owner == owner) return note;
                else return null;
            }
            else return null;
        }

        // Get all notes (between start time and end time)
        [HttpGet("Get {token}, {owner}, {startTime}, {endTime}")]
        public IEnumerable<Note>? Get(string token, string owner, DateTime startTime, DateTime endTime)
        {
            if (UserRepository.CheckUser(owner, token))
            {
                List<Note> list = new();
                foreach (var item in NoteRepository.GetAll())
                {
                    if (item.Owner == owner && item.CreateTime >= startTime && item.CreateTime <= endTime) list.Add(item);
                }
                return list;
            }
            else return null;
        }

        // Insert note
        [HttpPut("Insert {token}, {owner}, {title}, {text}")]
        public long Insert(string token, string owner, string title, string text)
        {
            if (UserRepository.CheckUser(owner, token)) return NoteRepository.Insert(new Note() { Owner = owner, Title = title, Text = text, CreateTime = DateTime.Now });
            else return -1;
        }

        // Archive note
        [HttpPut("ArchiveNote {token}, {owner}, {id}")]
        public bool ArchiveNote(string token, string owner, int id)
        {
            if (UserRepository.CheckUser(owner, token))
            {
                Note note = NoteRepository.Get(id);
                if (note != null && note.Owner == owner) {
                    ArchiveRepository.Insert(note);
                    return NoteRepository.Delete(id);
                }
                else return false;
            }
            else return false;
        }

        // Delete note
        [HttpPut("Delete {token}, {owner}, {id}")]
        public bool Delete(string token, string owner, int id)
        {
            if (UserRepository.CheckUser(owner, token)) {

                Note note = NoteRepository.Get(id);
                if (note != null && note.Owner == owner) return NoteRepository.Delete(id);
                else return false;
            }
            else return false;
        }
    }
}
