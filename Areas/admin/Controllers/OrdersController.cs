using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eshop.Models;
using eshop.Models.Database;
using Microsoft.AspNetCore.Authorization;
using eshop.Models.Identity;
using Microsoft.Extensions.Logging;

namespace eshop.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class OrdersController : Controller
    {
        private readonly EshopDBContext _context;
        private ILogger _logger;

        public OrdersController(EshopDBContext context, ILoggerFactory logger)
        {
            _logger = logger.CreateLogger(typeof(OrdersController));
            _context = context;
        }

        // GET: admin/Orders
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("OrdersController > Index() was called.");
            return View(await _context.Orders.ToListAsync());
        }

        // GET: admin/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            _logger.LogInformation("OrdersController > Details() was called.");
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: admin/Orders/Create
        public IActionResult Create()
        {
            _logger.LogInformation("OrdersController > Create() was called.");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: admin/Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderNumber,TotalPrice,UserId,ID,DateCreated")] Order order)
        {
            _logger.LogInformation("OrdersController > Create() was called.");
            if (ModelState.IsValid)
            {
                DateTime date = DateTime.Now;
                order.DateCreated = date;
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId); /**/
            return View(order);
        }

        // GET: admin/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogInformation("OrdersController > Edit() was called.");
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId); /**/
            return View(order);
        }

        // POST: admin/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderNumber,TotalPrice,UserId,ID,Condition,DateCreated")] Order order)
        {
            _logger.LogInformation("OrdersController > Edit() was called.");
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Order editOrder = order;

                     order = await _context.Orders
                               .Include(o => o.User)
                               .FirstOrDefaultAsync(m => m.ID == id);

                    order.OrderNumber = editOrder.OrderNumber;
                    order.TotalPrice = editOrder.TotalPrice;
                    order.UserId = editOrder.UserId;
                    order.Condition = editOrder.Condition;

                  
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: admin/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            _logger.LogInformation("OrdersController > Delete() was called.");
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation("OrdersController > DeleteConfirmed() was called.");
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }
    }
}
