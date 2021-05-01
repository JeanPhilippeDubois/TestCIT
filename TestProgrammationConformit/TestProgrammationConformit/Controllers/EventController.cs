using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammationConformit.Infrastructures;
using TestProgrammationConformit.Models;

namespace TestProgrammationConformit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ConformitContext _context;

        public EventController(ConformitContext context)
        {
            _context = context;
        }
        private bool EventExists(string title)
        {
            return _context.Events.Any(e => e.Title == title);
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
                
        }
        // GET: api/Event/the_title
        [HttpGet("{title}")]
        public async Task<ActionResult<Event>> GetEvent(string title)
        {
            var Event = await _context.Events.FindAsync(title);

            if (Event == null)
            {
                return NotFound();
            }

            return Event;
        }
        // PUT: api/Event/the_title
        [HttpPut("{title}")]
        public async Task<IActionResult> PutEvent(string title, Event events)
        {
            if (title != events.Title)
            {
                return BadRequest();
            }

            _context.Entry(events).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(title))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteEvent(string title)
        {
            var events = await _context.Events.FindAsync(title);
            if (events == null)
            {
                return NotFound();
            }

            _context.Events.Remove(events);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Event
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event events)
        {
            _context.Events.Add(events);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetEvent), new { title = events.Title }, events);
        }
    }
}
