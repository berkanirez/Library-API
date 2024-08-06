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
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationContext _context;
       

        public AuthorsController(ApplicationContext context)
        {
            _context = context;
           
        }

        // GET: api/Authors
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
          if (_context.Authors == null)
          {
              return NotFound();
          }
            return await _context.Authors.ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Author>> GetAuthor(long id)
        {
          if (_context.Authors == null)
          {
              return NotFound();
          }
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(long id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }
            Methods _methods = new Methods();
            if (_methods.YearChecker(author.BirthYear) == false)
            {
                return BadRequest("The year that entered should be less than current year!");
            }

            if (_methods.YearChecker(author.DeathYear) == false)
            {
                return BadRequest("The year that entered should be less than current year!");
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee,Admin")]
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
          if (_context.Authors == null)
          {
              return Problem("Entity set 'ApplicationContext.Authors'  is null.");
          }

            Methods _methods = new Methods();
            if (_methods.YearChecker(author.BirthYear) == false)
            {
                return BadRequest("The year that entered should be less than current year!");
            }

            if (_methods.YearChecker(author.DeathYear) == false)
            {
                return BadRequest("The year that entered should be less than current year!");
            }

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [Authorize(Roles = "Employee,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(long id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            if (author.IsDeleted)
            {
                return Problem("Author is already deleted");
            }

            author.IsDeleted = true;
            _context.Entry(author).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool AuthorExists(long id)
        {
            return (_context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
