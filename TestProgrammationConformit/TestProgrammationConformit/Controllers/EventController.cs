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
        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
                
        }
        // GET: api/Event/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var Event = await _context.Events.FindAsync(id);

            if (Event == null)
            {
                return NotFound();
            }

            return Event;
        }
        // PUT: api/Event/2
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event events)
        {
            if (id != events.Id)
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
                if (!EventExists(id))
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

        // DELETE: api/Event/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var events = await _context.Events.FindAsync(id);
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


            return CreatedAtAction(nameof(GetEvent), new { id = events.Id}, events);
        }
    }
}
