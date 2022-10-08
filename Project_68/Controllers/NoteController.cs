using Microsoft.AspNetCore.Mvc;
using Project_68_Library.Models;
using Project_68_Library.Repositories;
using System.Collections.Generic;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

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
        public async Task<ActionResult> GetAll(string token, string owner)
        {
            Task<ActionResult> task = new Task<ActionResult>(() =>
            {
                try
                {
                    if (UserRepository.CheckUser(owner, token))
                    {
                        List<Note> list = new();
                        foreach (var item in NoteRepository.GetAll())
                        {
                            if (item.Owner == owner) list.Add(item);
                        }
                        if (list.Count > 0) return Ok(list);
                        else return NoContent();
                    }
                    else return NotFound();
                }
                catch
                {
                    return BadRequest();
                }
            });
            task.Start();
            return await task;
        }

        // Get id note
        [HttpGet("Get {token}, {owner}, {id}")]
        public async Task<ActionResult> Get(string token, string owner, int id)
        {
            Task<ActionResult> task = new Task<ActionResult>(() =>
            {
                try
                {
                    if (UserRepository.CheckUser(owner, token))
                    {
                        Note note = NoteRepository.Get(id);
                        if (note != null && note.Owner == owner) return Ok(note);
                        else return NoContent();
                    }
                    else return NotFound();
                }
                catch
                {
                    return BadRequest();
                }
            });
            task.Start();
            return await task;
        }
        // Get notes between start time and end time
        [HttpGet("{token}, {owner}, {startTime}, {endTime}")]
        public async Task<ActionResult> Get(string token, string owner, DateTime startTime, DateTime endTime)
        {
            Task<ActionResult> task = new Task<ActionResult>(() =>
            {
                try
                {
                    if (UserRepository.CheckUser(owner, token))
                    {
                        List<Note> list = new();
                        foreach (var item in NoteRepository.GetAll())
                        {
                            if (item.Owner == owner && item.CreateTime >= startTime && item.CreateTime <= endTime) list.Add(item);
                        }
                        if (list.Count > 0) return Ok(list);
                        else return NoContent();
                    }
                    else return NotFound();
                }
                catch
                {
                    return BadRequest();
                }
            });
            task.Start();
            return await task;
        }

        // Insert note
        [HttpPut("Insert {token}, {owner}, {title}, {text}")]
        public async Task<ActionResult> Insert(string token, string owner, string title, string text)
        {
            Task<ActionResult> task = new Task<ActionResult>(() =>
            {
                try
                {
                    if (UserRepository.CheckUser(owner, token)) return Ok(NoteRepository.Insert(new Note() { Owner = owner, Title = title, Text = text, CreateTime = DateTime.Now }));
                    else return NotFound();
                }
                catch
                {
                    return BadRequest();
                }
            });
            task.Start();
            return await task;
        }

        // Archive note
        [HttpPut("ArchiveNote {token}, {owner}, {id}")]
        public async Task<ActionResult> ArchiveNote(string token, string owner, int id)
        {
            Task<ActionResult> task = new Task<ActionResult>(()=>
            {
                try
                {
                    if (UserRepository.CheckUser(owner, token))
                    {
                        Note note = NoteRepository.Get(id);
                        if (note != null && note.Owner == owner)
                        {
                            if (NoteRepository.Delete(id))
                            {
                                ArchiveRepository.Insert(note);
                                return Ok(true);
                            }
                            else return Ok(false);
                        }
                        else return NoContent();
                    }
                    else return NotFound();
                }
                catch
                {
                    return BadRequest();
                }
            });
            task.Start();
            return await task;
        }

        // Delete note
        [HttpDelete("Delete {token}, {owner}, {id}")]
        public async Task<ActionResult> Delete(string token, string owner, int id)
        {
            Task<ActionResult> task = new Task<ActionResult>(() =>
            {
                try
                {
                    if (UserRepository.CheckUser(owner, token))
                    {

                        Note note = NoteRepository.Get(id);
                        if (note != null && note.Owner == owner) return Ok(NoteRepository.Delete(id));
                        else return NoContent();
                    }
                    else return NotFound();
                }
                catch
                {
                    return BadRequest();
                }
            });
            task.Start();
            return await task;
        }
    }
}
