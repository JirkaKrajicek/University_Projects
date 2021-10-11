using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eshop.Models.DatabaseFake;
using eshop.Models;
using Microsoft.EntityFrameworkCore.Storage;
using eshop.Models.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using eshop.Controllers;
using eshop.Models.Identity;

namespace eshop.Areas.user.Controllers
{

    [Area("User")]
    public class ProductsController : Controller
    {
        IHostingEnvironment Env;
        EshopDBContext EshopDBContext;
        private ILogger _logger;

        public ProductsController(EshopDBContext eshopDBContext, IHostingEnvironment env, ILoggerFactory logger)
        {
            this.EshopDBContext = eshopDBContext;
            this.Env = env;
            _logger = logger.CreateLogger(typeof(ProductsController));
        }

        public async Task<IActionResult> Products()
        {
            _logger.LogInformation("ProductsController > Products() was called.");
            ProductViewModel productVM = new ProductViewModel();
            productVM.Products = await EshopDBContext.Products.ToListAsync();
            return View(productVM);
        }

        public async Task<IActionResult> Detail(int id)
        {
            _logger.LogInformation("ProductsController > Detail() was called.");
            Product productItem = EshopDBContext.Products.Where(cr => cr.ID == id).FirstOrDefault();
            IList<Rating> ratings = await EshopDBContext.Ratings.Where(r => r.ProductID == id).ToArrayAsync();
            int count = 0;
            decimal sum =0;
            foreach (Rating rating in ratings)
            {
                if (rating.Rated)
                {
                    sum += rating.Stars;
                    count++;
                }
            }
            if (count < 1) count = 1;
            double averageStar = Convert.ToDouble(sum) / count;


            if (productItem != null)
            {
                ViewBag.Message = $"{averageStar}/5";
                return View(productItem);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Buy(int id)
        {
            _logger.LogInformation("AccountController > Buy() was called.");


                //Product product = EshopDBContext.Products.Where(cr => cr.ProductName == Request.Form["productName"]).FirstOrDefault();
                Product product = EshopDBContext.Products.Where(cr => cr.ID == id).FirstOrDefault();

                Random r1 = new Random();
                int i1 = r1.Next(1, 13);
                char c2 = Convert.ToChar(r1.Next(13, 26));

                Order o = new Order();
                o.OrderNumber = c2 + i1.ToString(); //random nazev obsahujici znak+cislo (admin i manager mohou pozdeji upravit)
                EshopDBContext.Orders.Add(o);
                await EshopDBContext.SaveChangesAsync();

                DateTime date = DateTime.Now;
                OrderItem orderItem = new OrderItem()
                {
                    OrderID = o.ID,
                    ProductID = product.ID,
                    Amount = 1,
                    Price = Convert.ToDecimal(product.Price),
                    Product = product,
                    Order = o,
                    DateCreated = date
                };
                EshopDBContext.OrderItems.Add(orderItem);
                await EshopDBContext.SaveChangesAsync();


            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", String.Empty), new { area = "" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rating(string star1, string star2, string star3, string star4, string star5, string NameUser, int idProduct)
        {
           /* try
            {*/
                int starRating;

                if (star1 == "on") starRating = 1;
                else if (star2 == "on") starRating = 2;
                else if (star3 == "on") starRating = 3;
                else if (star4 == "on") starRating = 4;
                else if (star5 == "on") starRating = 5;
                else return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", String.Empty), new { area = "" });

                User user = EshopDBContext.Accounts.Where(u => u.UserName == NameUser).FirstOrDefault();
                Rating[] rats = await EshopDBContext.Ratings.Where(r => r.ProductID == idProduct).Where(r => r.Rated == true).ToArrayAsync();
                bool iDidNotDoThat = true;

                if (rats.Count() != 0)
                {
                    foreach (Rating rat in rats)
                    {
                        if (rat.UserID == user.Id)
                        {
                            DateTime dateNow = DateTime.Now;
                            rat.Stars = starRating;
                            rat.DateCreated = dateNow;
                            await EshopDBContext.SaveChangesAsync();

                            iDidNotDoThat = false;
                            break;
                        }
                    }
                    if (iDidNotDoThat)
                    {
                        DateTime dateNow = DateTime.Now;
                        Rating newRating = new Rating()
                        {
                            ProductID = idProduct,
                            UserID = user.Id,
                            Stars = starRating,
                            Rated = true,
                            Product = EshopDBContext.Products.Where(p => p.ID == idProduct).FirstOrDefault(),
                            User = user,
                            DateCreated = dateNow
                        };

                        EshopDBContext.Add(newRating);
                        await EshopDBContext.SaveChangesAsync();
                    }
                }
                else
                {
                DateTime dateNow = DateTime.Now;

                Rating newRating = new Rating()
                {
                    ProductID = idProduct,
                    UserID = user.Id,
                    Stars = starRating,
                    Rated = true,
                    Product = EshopDBContext.Products.Where(p => p.ID == idProduct).FirstOrDefault(),
                    User = user, 
                    DateCreated = dateNow
                };
                    
                    EshopDBContext.Add(newRating);
                    await EshopDBContext.SaveChangesAsync();
                    
                }
            /* }
             catch(Exception e)
             {
                 return NotFound();
             }*/

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", String.Empty), new { area = "" });
        }
    }
}