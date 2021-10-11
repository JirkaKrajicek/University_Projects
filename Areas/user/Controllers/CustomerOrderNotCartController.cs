using eshop.Areas.Customer.Controllers;
using eshop.Controllers;
using eshop.Models;
using eshop.Models.ApplicationServices;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Customer.Controllers
{
    [Area("User")]
    [Authorize(Roles = nameof(Roles.Customer))]
    public class CustomerOrderNotCartController : Controller
    {
        const string totalPriceString = "TotalPrice";
        const string orderItemsString = "OrderItems";

        private ILogger _logger;
        ISecurityApplicationService iSecure;
        EshopDBContext EshopDBContext;
        public CustomerOrderNotCartController(ISecurityApplicationService iSecure, EshopDBContext eshopDBContext, ILoggerFactory logger)
        {
            this.iSecure = iSecure;
            EshopDBContext = eshopDBContext;
            _logger = logger.CreateLogger(typeof(CustomerOrderNotCartController));
        }


        [HttpPost]
        public double AddOrderItemsToSession(int? productId)
        {
            _logger.LogInformation("CustomerOrderNotCartController > AddOrderItemsToSession() was called.");
            double totalPrice = 0;
            if (HttpContext.Session.IsAvailable)
            {
                totalPrice = HttpContext.Session.GetDouble(totalPriceString).GetValueOrDefault();
            }


            Product product = EshopDBContext.Products.Where(prod => prod.ID == productId).FirstOrDefault();

            if (product != null)
            {
                DateTime date = DateTime.Now;
                OrderItem orderItem = new OrderItem()
                {
                    ProductID = product.ID,
                    Product = product,
                    Amount = 1,
                    Price = (decimal)product.Price,   //zde pozor na datový typ -> pokud máte Price v obou případech double nebo decimal, tak je to OK. Mě se bohužel povedlo mít to jednou jako decimal a jednou jako double. Nejlepší je datový typ změnit v databázi/třídě, tak to prosím udělejte.
                    DateCreated = date
                };

                if (HttpContext.Session.IsAvailable)
                {

                    List<OrderItem> orderItems = HttpContext.Session.GetObject<List<OrderItem>>(orderItemsString);
                    OrderItem orderItemInSession = null;
                    if (orderItems != null)
                        orderItemInSession = orderItems.Find(oi => oi.ProductID == orderItem.ProductID);
                    else
                        orderItems = new List<OrderItem>();


                    if (orderItemInSession != null)
                    {
                        ++orderItemInSession.Amount;
                        orderItemInSession.Price += (decimal)orderItem.Product.Price;   //zde pozor na datový typ -> pokud máte Price v obou případech double nebo decimal, tak je to OK. Mě se bohužel povedlo mít to jednou jako decimal a jednou jako double. Nejlepší je datový typ změnit v databázi/třídě, tak to prosím udělejte.
                    }
                    else
                    {
                        orderItems.Add(orderItem);
                    }


                    HttpContext.Session.SetObject(orderItemsString, orderItems);

                    totalPrice += orderItem.Product.Price;
                    HttpContext.Session.SetDouble(totalPriceString, totalPrice);
                }
            }


            return totalPrice;
        }


        public async Task<IActionResult> ApproveOrderInSession()
        {
            _logger.LogInformation("CustomerOrderNotCartController > ApproveOrderInSession() was called.");
            if (HttpContext.Session.IsAvailable)
            {
                try
                {
                    double totalPrice = 0;
                    List<OrderItem> orderItems = HttpContext.Session.GetObject<List<OrderItem>>(orderItemsString);
                    if (orderItems != null)
                    {
                        foreach (OrderItem orderItem in orderItems)
                        {
                            totalPrice += orderItem.Product.Price * orderItem.Amount;
                            orderItem.Product = null; //zde musime nullovat referenci na produkt, jinak by doslo o pokus jej znovu vlozit do databaze
                        }


                        User currentUser = await iSecure.GetCurrentUser(User);

                        DateTime date = DateTime.Now;
                        Order order = new Order()
                        {
                            OrderNumber = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                            TotalPrice = totalPrice,
                            OrderItems = orderItems,
                            UserId = currentUser.Id,
                            DateCreated = date
                        };



                        //We can add just the order; order items will be added automatically.
                        await EshopDBContext.AddAsync(order);
                        await EshopDBContext.SaveChangesAsync();



                        HttpContext.Session.Remove(orderItemsString);
                        HttpContext.Session.Remove(totalPriceString);

                        return RedirectToAction(nameof(CustomerOrdersController.Index), nameof(CustomerOrdersController).Replace("Controller", ""), new { Area = nameof(User) });

                    }
                }
                catch (Exception e)
                {
                    return RedirectToAction(nameof(ErrorCodeStatusViewModel.StatusCode), nameof(ErrorCodeStatusViewModel).Replace("Controller", ""), new { Area = "" });
                }                
            }
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""), new { Area = "" });

        }
    }
}
