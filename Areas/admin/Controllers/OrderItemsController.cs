using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eshop.Models;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace eshop.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class OrderItemsController : Controller
    {
        private readonly EshopDBContext _context;
        private ILogger _logger;

        public OrderItemsController(EshopDBContext context, ILoggerFactory logger)
        {
            _context = context;
            _logger = logger.CreateLogger(typeof(OrderItemsController));
        }

        // GET: admin/OrderItems
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("OrderItemsController > Index() was called.");
            var eshopDBContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product);
            return View(await eshopDBContext.ToListAsync());
        }

        // GET: admin/OrderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            _logger.LogInformation("OrderItemsController > Details() was called.");
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // GET: admin/OrderItems/Create
        public IActionResult Create()
        {
            _logger.LogInformation("OrderItemsController > Create() was called.");
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "OrderNumber");
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ImageAlt");
            return View();
        }

        // POST: admin/OrderItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,ProductID,Amount,Price,ID,DateCreated")] OrderItem orderItem)
        {
            _logger.LogInformation("OrderItemsController > Create() was called.");
            if (ModelState.IsValid)
            {
                DateTime date = DateTime.Now;
                orderItem.DateCreated = date;
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "OrderNumber", orderItem.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ImageAlt", orderItem.ProductID);
            return View(orderItem);
        }

        // GET: admin/OrderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogInformation("OrderItemsController > Edit() was called.");
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "OrderNumber", orderItem.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ImageAlt", orderItem.ProductID);
            return View(orderItem);
        }

        // POST: admin/OrderItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,ProductID,Amount,Price,ID,DateCreated")] OrderItem orderItem)
        {
            _logger.LogInformation("OrderItemsController > Edit() was called.");
            if (id != orderItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemExists(orderItem.ID))
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
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "OrderNumber", orderItem.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ImageAlt", orderItem.ProductID);
            return View(orderItem);
        }

        // GET: admin/OrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            _logger.LogInformation("OrderItemsController > Delete() was called.");
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // POST: admin/OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation("OrderItemsController > DeleteConfirmed() was called.");
            var orderItem = await _context.OrderItems.FindAsync(id);
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemExists(int id)
        {
            _logger.LogInformation("OrderItemsController > OrderItemExists() was called.");
            return _context.OrderItems.Any(e => e.ID == id);
        }
    }
}
