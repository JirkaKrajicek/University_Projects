using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eshop.Models.DatabaseFake;
using eshop.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Hosting;
using eshop.Models.Database;
using Microsoft.EntityFrameworkCore;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace eshop.Areas.admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class ProductsController : Controller
    {
        IHostingEnvironment Env;
        EshopDBContext EshopDBContext;
        private ILogger _logger;

        public ProductsController(EshopDBContext eshopDBContext, IHostingEnvironment env, ILoggerFactory logger)
        {
            _logger = logger.CreateLogger(typeof(ProductsController));
            this.EshopDBContext = eshopDBContext;
            this.Env = env;
        }

        public async Task<IActionResult> Products()
        {
            _logger.LogInformation("ProductsController > Products() was called.");
            ProductViewModel productVM = new ProductViewModel();
            productVM.Products = await EshopDBContext.Products.ToListAsync();
            return View(productVM);
        }

        public IActionResult Create()
        {
            _logger.LogInformation("ProductsController > Create() was called.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            _logger.LogInformation("ProductsController > Create() was called.");
            product.ImageSrc = String.Empty;

            FileUpload fup = new FileUpload(Env.WebRootPath, "Products", "image");
            product.ImageSrc = await fup.FileUploadAsync(product.Image);
            DateTime date = DateTime.Now;
            product.DateCreated = date;
            EshopDBContext.Products.Add(product);
            await EshopDBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Products));
        }

        public IActionResult Edit(int id)
        {
            _logger.LogInformation("ProductsController > Edit() was called.");
            Product productItem = EshopDBContext.Products.Where(p => p.ID == id).FirstOrDefault();
            if (productItem != null)
            {
                return View(productItem);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            _logger.LogInformation("ProductsController > Edit() was called.");
            Product productItem = EshopDBContext.Products.Where(p => p.ID == product.ID).FirstOrDefault();
            if (productItem != null)
            {
                productItem.ProductName = product.ProductName;
                productItem.ImageAlt = product.ImageAlt;
                productItem.Price = product.Price;
                productItem.Description = product.Description;

                FileUpload fup = new FileUpload(Env.WebRootPath, "Products", "image");
                if (String.IsNullOrWhiteSpace(product.ImageSrc = await fup.FileUploadAsync(product.Image)) == false) productItem.ImageSrc = product.ImageSrc;

                await EshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(Products));
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("ProductsController > Delete() was called.");
            Product productItem = EshopDBContext.Products.Where(p => p.ID == id).FirstOrDefault();
            if (productItem != null)
            {
                EshopDBContext.Products.Remove(productItem);
                await EshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(Products));
            }
            else
            {
                return NotFound();
            }
        }

    }
}