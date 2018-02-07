using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.io.Data;
using Library.io.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Library.io.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger _logger;

        public RentalsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<RentalsController> logger)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            //return all rentals when current user is admin
            if (await GetCurrentUserRole() == "Admin")
            {
                return View(await _context.Rental
                    .Include(r => r.Book)
                    .Include(r => r.User)
                    .OrderByDescending(r => r.Expiration)
                    .ToListAsync());
            }
            else
            {
                string CurrentUserId = await GetCurrentUserId();
                return View(await _context.Rental
                    .Where(r => r.ApplicationUserId== CurrentUserId)
                    .Include(r => r.Book)
                    .Include(r => r.User)
                    .OrderByDescending(r => r.Expiration)
                    .ToListAsync());
            }
        }


        // GET: Rentals/Rent/{id} - where id=book id
        public async Task<IActionResult> Rent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .SingleOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            if (RentalExistsForUser(book.ID, await GetCurrentUserId()))
            {
                return View(null);
            }

            return View(book);
        }

        [HttpPost, ActionName("Rent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rent(int id)
        {
            //TODO check if the user already rented the book
            _logger.LogInformation("_______Renting book with id: " + id);
            Book book = await _context.Book.SingleOrDefaultAsync(m => m.ID == id);
            if (book.Available > 0)
            {
                Rental rental = new Rental() { Book = book, User = await GetCurrentUserAsync(), Expiration = DateTime.Now.AddDays(30) };
                _context.Add(rental);
                //decrease number of available books
                book.Available--;
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .SingleOrDefaultAsync(m => m.ID == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rental.SingleOrDefaultAsync(m => m.ID == id);
            //increase number of available books after returning it
            var book = await _context.Book.SingleOrDefaultAsync(m => m.ID == rental.BookId);
            book.Available += 1;
            _context.Rental.Remove(rental);
            _context.Book.Update(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExistsForUser(int BookId, string UserId)
        {
            return _context.Rental.Any(e => e.BookId == BookId && e.ApplicationUserId == UserId);
        }

        private bool RentalExists(int id)
        {
            return _context.Rental.Any(e => e.ID == id);
        }

        private async Task<string> GetCurrentUserRole()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return userManager.GetRolesAsync(usr).Result.Single();
        }

        public async Task<string> GetCurrentUserId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
    }
}
