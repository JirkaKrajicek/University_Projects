using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eshop.Models;
using eshop.Models.Database;
using eshop.Models.DatabaseFake;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace eshop.Areas.admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class CarouselController : Controller
    {
        IHostingEnvironment Env;
        EshopDBContext EshopDBContext;
        private ILogger _logger;

        public CarouselController(EshopDBContext eshopDBContext, IHostingEnvironment env, ILoggerFactory logger)
        {
            _logger = logger.CreateLogger(typeof(CarouselController));
            this.EshopDBContext = eshopDBContext;
            this.Env = env;
        }

        public async Task<IActionResult> Carousel()
        {
            _logger.LogInformation("CarouselController > Carousel() was called.");
           
            CarouselViewModel carouselVM = new CarouselViewModel();
            carouselVM.Carousels =await EshopDBContext.Carousels.ToListAsync();
            return View(carouselVM);
        }

        public IActionResult Create()
        {
            _logger.LogInformation("CarouselController > Create() was called.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Carousel carousel)
        {
            _logger.LogInformation("CarouselController > Create() was called.");
            if (ModelState.IsValid)
            {
                carousel.ImageSrc = String.Empty;

                FileUpload fup = new FileUpload(Env.WebRootPath, "Carousels", "image");
                carousel.ImageSrc = await fup.FileUploadAsync(carousel.Image);
                DateTime date = DateTime.Now;
                carousel.DateCreated = date;

                EshopDBContext.Carousels.Add(carousel);
                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Carousel));
            }
            else
            {
                return View(carousel);
            }
        }

        public IActionResult Edit(int id)
        {
            _logger.LogInformation("CarouselController > Edit() was called.");
            Carousel carouselItem = EshopDBContext.Carousels.Where(cr => cr.ID == id).FirstOrDefault();
            if(carouselItem != null)
            {
                return View(carouselItem);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Carousel carousel)
        {
            _logger.LogInformation("CarouselController > Edit() was called.");
            Carousel carouselItem = EshopDBContext.Carousels.Where(cr => cr.ID == carousel.ID).FirstOrDefault();
            if (carouselItem != null)
            {
                carouselItem.DataTarget = carousel.DataTarget;
                carouselItem.ImageAlt = carousel.ImageAlt;
                carouselItem.CarouselContent = carousel.CarouselContent;

                FileUpload fup = new FileUpload(Env.WebRootPath, "Carousels", "image");
                if (String.IsNullOrWhiteSpace( carousel.ImageSrc = await fup.FileUploadAsync(carousel.Image)) == false) carouselItem.ImageSrc = carousel.ImageSrc;

                await EshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(Carousel));
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("CarouselController > Delete() was called.");
            Carousel carouselItem = EshopDBContext.Carousels.Where(cr => cr.ID == id).FirstOrDefault();
            if (carouselItem != null)
            {
                EshopDBContext.Carousels.Remove(carouselItem);
                await EshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(Carousel));
            }
            else
            {
                return NotFound();
            }
        }

        
    }
}
