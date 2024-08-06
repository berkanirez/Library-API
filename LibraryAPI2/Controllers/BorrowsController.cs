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

namespace LibraryAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public BorrowsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Borrows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Borrow>>> GetBorrow()
        {
          if (_context.Borrow == null)
          {
              return NotFound();
          }
            return await _context.Borrow.ToListAsync();
        }

        // GET: api/Borrows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Borrow>> GetBorrow(int id)
        {
          if (_context.Borrow == null)
          {
              return NotFound();
          }
            var borrow = await _context.Borrow.FindAsync(id);

            if (borrow == null)
            {
                return NotFound();
            }

            return borrow;
        }

        // PUT: api/Borrows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorrow(int id, Borrow borrow)
        {
            
            BookCopy bookCopy = _context.BookCopy.FirstOrDefault(b => b.Id == borrow.BookId);

            if (id != borrow.Id)
            {
                return BadRequest("You have not took with this book before");
            }

            if (borrow.ReceiveDate > borrow.ReturnDate)
            {

                TimeSpan overduePeriod = (TimeSpan)(borrow.ReceiveDate - borrow.ReturnDate);
                if(overduePeriod.TotalDays>30)
                {

                    int overdueMonths = (int)Math.Ceiling(overduePeriod.TotalDays / 30.0);

                    float penaltyAmount = (overdueMonths * 100);

                    borrow.PunishmentTaken = penaltyAmount;
                }
            }

            if(borrow.IsHarmed == true)
            {
                borrow.PunishmentTaken += 500;
            }

            bookCopy.IsBorrowed = false;
            _context.BookCopy.Update(bookCopy);

            
            _context.Entry(borrow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowExists(id))
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

        // POST: api/Borrows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee,Admin")]
        [HttpPost]
        public async Task<ActionResult<Borrow>> PostBorrow(Borrow borrow)
        {
            
            var employeeExists = await _context.Employees.AnyAsync(e => e.Id == borrow.EmployeeId);
            if (!employeeExists)
            {
                return BadRequest("Invalid EmployeeId.");
            }

            BookCopy bookCopy = _context.BookCopy.FirstOrDefault(b => b.Id == borrow.BookId);
          if (_context.Borrow == null)
          {
              return Problem("Entity set 'ApplicationContext.Borrow'  is null.");
          }
          if (bookCopy.IsBorrowed == true)
            {
                return BadRequest("Book is already borrowed.");
            }
          if(bookCopy.IsHarmed == true)
            {
                return Problem("Book is harmed.");
            }
            
            _context.Borrow.Add(borrow);
            bookCopy.IsBorrowed = true;
            _context.BookCopy.Update(bookCopy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBorrow", new { id = borrow.Id }, borrow);
        }

        // DELETE: api/Borrows/5
        [Authorize(Roles = "Employee,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrow(int id)
        {
            if (_context.Borrow == null)
            {
                return NotFound();
            }
            var borrow = await _context.Borrow.FindAsync(id);
            if (borrow == null)
            {
                return NotFound();
            }

            if (borrow.IsDeleted)
            {
                return Problem("Author is already deleted");
            }

            borrow.IsDeleted = true;
            _context.Entry(borrow).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BorrowExists(int id)
        {
            return (_context.Borrow?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
