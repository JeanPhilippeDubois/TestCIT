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
        /// <summary>
        /// Context for the database.
        /// </summary>
        private readonly ConformitContext _context;
        public CommentController(ConformitContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Verifies if a comment exists by comparing an id against every comment in the database.
        /// </summary>
        /// <param name="id">The id of the comment.</param>
        /// <returns>True or false, if the comment exists or not.</returns>
        private bool CommentExists(int id)
        {
            return _context.Comments.Any(c => c.Id == id);
        }
        /// <summary>
        /// Returns the list of all comments in the database upon a GET request on api/Comment/
        /// </summary>
        /// <returns>All comments from the database.</returns>
        // GET: api/Comment
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Comment>>> GetListComments()
        {
            return await _context.Comments.ToListAsync();

        }
        /// <summary>
        /// Returns a comment depending on it's id upon a GET request using api/Comment/GetComment/{id}
        /// </summary>
        /// <param name="id">The id of the comment</param>
        /// <returns>The comment</returns>
        // GET: api/Comment/GetComment/1
        [HttpGet("{action}/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var Comment = await _context.Comments.FindAsync(id);

            if (Comment == null)
            {
                return NotFound();
            }

            return Comment;
        }
        /// <summary>
        /// Modifies a comment upon receiving a PUT request on api/Comment/{id}
        /// </summary>
        /// <param name="id">The id of the comment</param>
        /// <param name="comment">The updated comment</param>
        /// <returns>Returns a status 200 response</returns>
        // PUT: api/Comment/2
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutComment(int id, [FromBody] Comment comment)
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

            return Ok();
        }
        /// <summary>
        /// Deletes a comment upon receiving a DELETE request on api/Comment/{id}.
        /// </summary>
        /// <param name="id">The id of the comment.</param>
        /// <returns>Returns a status 200 response.</returns>
        // DELETE: api/Comment/3
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok();
        }
        /// <summary>
        /// Adds a comment to the database upon receiving a POST request on api/Event
        /// </summary>
        /// <param name="comment">The comment from the JSON request</param>
        /// <returns>Returns a status 201 response</returns>
        // POST: api/Event
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Comment>> PostComment([FromBody] Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(comment), new { id = comment.Id }, comment);
        }


    }
}
