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
    public class CommentController : ControllerBase
    {
        private readonly ConformitContext _context;

        public CommentController(ConformitContext context)
        {
            _context = context;
        }
        private bool CommentExists(long id)
        {
            return _context.Comments.Any(c => c.Id == id);
        }

        // GET: api/Comment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _context.Comments.ToListAsync();
                
        }
        // GET: api/Comment/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(long id)
        {
            var Comment = await _context.Comments.FindAsync(id);

            if (Comment == null)
            {
                return NotFound();
            }

            return Comment;
        }
        // PUT: api/Comment/2

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(long id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // DELETE: api/Comment/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(long id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // POST: api/Event
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(comment), new { id = comment.Id }, comment);
        }

    }
}
