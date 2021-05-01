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
        /// <summary>
        /// Returns the list of events upon a GET request on api/Event
        /// </summary>
        /// <returns>The list of all events in the database.</returns>
        // GET: api/Event
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
                
        }
        /// <summary>
        /// Returns an event upon receiving a GET request on api/Event/{id}
        /// </summary>
        /// <param name="id">The id of the event</param>
        /// <returns>The event and it's information</returns>
        // GET: api/Event/1
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var Event = await _context.Events.FindAsync(id);

            if (Event == null)
            {
                return NotFound();
            }

            return Event;
        }
        /// <summary>
        /// Modifies an event upon receiving a PUT request on api/Event/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="events"></param>
        /// <returns>Produces a status 200 OK response</returns>
        // PUT: api/Event/2
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

            return Ok();
        }
        /// <summary>
        /// Deletes an event upon receiving a DELETE request on api/Event/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A status 200 OK response if an event if found or 404 if not found</returns>
        // DELETE: api/Event/1
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var events = await _context.Events.FindAsync(id);
            if (events == null)
            {
                return NotFound();
            }

            _context.Events.Remove(events);
            await _context.SaveChangesAsync();

            return Ok();
        }
        /// <summary>
        /// Creates a new event upon receiving a POST request to api/Event
        /// </summary>
        /// <param name="events">The event to be created</param>
        /// <returns>A status 201 response if all parameters are okay.</returns>
        // POST: api/Event
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Event>> PostEvent(Event events)
        {
            _context.Events.Add(events);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetEvent), new { id = events.Id}, events);
        }
    }
}
