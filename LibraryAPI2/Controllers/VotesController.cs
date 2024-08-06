using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI2.Data;
using LibraryAPI2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace LibraryAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public VotesController(ApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Votes
        [Authorize(Roles = "Employee,Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voting>>> GetVotes()
        {
          if (_context.Votes == null)
          {
              return NotFound();
          }
            return await _context.Votes.ToListAsync();
        }

        // GET: api/Votes/5
        [Authorize(Roles = "Employee,Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Voting>> GetVoting(int id)
        {
          if (_context.Votes == null)
          {
              return NotFound();
          }
            var voting = await _context.Votes.FindAsync(id);

            if (voting == null)
            {
                return NotFound();
            }

            return voting;
        }

        // PUT: api/Votes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoting(int id, Voting voting)
        {
            if (id != voting.Id)
            {
                return BadRequest();
            }

            _context.Entry(voting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VotingExists(id))
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

        // POST: api/Votes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<ActionResult<Voting>> PostVoting(Voting voting)
        {
            
            Book book = _context.Books.FirstOrDefault(b => b.Id == voting.BookId);
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var IsVoted = await _context.Votes.FirstOrDefaultAsync(v => v.MemberId == userId && v.BookId == book.Id);
            
            if (IsVoted != null)
            {
                return BadRequest("You have already voted this book.");
            }


            if (_context.Votes == null)
            {
              return Problem("Entity set 'ApplicationContext.Votes'  is null.");
            }

            book.Rating = (book.Rating * book.TotalVotes + voting.Vote) / (book.TotalVotes + 1);
            book.TotalVotes++;

            if (book.Rating > 5)
            {
                book.Rating = 5;
            }
            voting.MemberId = userId;
            _context.Votes.Add(voting);

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoting", new { id = voting.Id }, voting);
        }

        // DELETE: api/Votes/5
        [Authorize(Roles = "Employee,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoting(int id)
        {
            if (_context.Votes == null)
            {
                return NotFound();
            }
            var voting = await _context.Votes.FindAsync(id);
            if (voting == null)
            {
                return NotFound();
            }

            if (voting.IsDeleted)
            {
                return Problem("Author is already deleted");
            }

            voting.IsDeleted = true;
            _context.Entry(voting).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VotingExists(int id)
        {
            return (_context.Votes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
