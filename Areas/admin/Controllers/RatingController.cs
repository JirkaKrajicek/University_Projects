using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Models;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshop.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class RatingController : Controller
    {
        private readonly EshopDBContext _context;
        private ILogger _logger;

        public RatingController(EshopDBContext context, ILoggerFactory logger)
        {
            _context = context;
            _logger = logger.CreateLogger(typeof(RatingController));
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("RatingController > Index() was called.");
            var eshopDBContext = _context.Ratings.Include(o => o.User).Include(o => o.Product);
            return View(await eshopDBContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            _logger.LogInformation("RatingController > Details() was called.");
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Ratings
                .Include(o => o.User)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        public IActionResult Create()
        {
            _logger.LogInformation("RatingController > Create() was called.");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "UserNumber");
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Product");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,ProductID,Stars,Rated,ID,DateCreated")] Rating rating)
        {
            _logger.LogInformation("RatingController > Create() was called.");
            DateTime date = DateTime.Now;
            if (ModelState.IsValid)
            {
                rating.DateCreated = date;
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "UserID", rating.UserID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ProductID", rating.ProductID);
            return View(rating);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogInformation("RatingController > Edit() was called.");
            if (id == null)
            {
                return NotFound();
            }

            // var rating = await _context.Ratings.FindAsync(id);
            var rating = await _context.Ratings
              .Include(o => o.User)
              .Include(o => o.Product)
              .FirstOrDefaultAsync(m => m.ID == id);

            if (rating == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "UserID", rating.UserID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ProductID", rating.ProductID);
            return View(rating);
        }


        private bool RatingExists(int id)
        {
            _logger.LogInformation("RatingController > RatingExists() was called.");
            return _context.Ratings.Any(e => e.ID == id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,ProductID,Stars,Rated,ID,DateCreated")] Rating rating)
        {
            _logger.LogInformation("RatingController > Edit() was called.");
            if (id != rating.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    decimal stars = rating.Stars;
                    bool rated = rating.Rated;

                    rating = await _context.Ratings
                          .Include(o => o.User)
                          .Include(o => o.Product)
                          .FirstOrDefaultAsync(m => m.ID == id);

                    rating.Stars = stars;
                    rating.Rated = rated;

                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "UserID", rating.UserID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ProductID", rating.ProductID);
            return View(rating);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            _logger.LogInformation("RatingController > Delete() was called.");
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Ratings
                .Include(o => o.User)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation("RatingController > DeleteConfirmed() was called.");
            var rating = await _context.Ratings.FindAsync(id);
            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

    
    /***********************************************************************/

