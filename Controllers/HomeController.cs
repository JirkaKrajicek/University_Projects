using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eshop.Models;
using eshop.Models.DatabaseFake;
using eshop.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace eshop.Controllers
{
    public class HomeController : Controller
    {

        EshopDBContext EshopDBContext;
        private ILogger _logger;

        public HomeController(EshopDBContext eshopDBContext, ILoggerFactory logger)
        {
            _logger = logger.CreateLogger(typeof(HomeController));
            this.EshopDBContext = eshopDBContext; 
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("HomeController > Index() was called.");
            dynamic mymodel = new ExpandoObject();

            var cvm = new CarouselViewModel();
            cvm.Carousels = await EshopDBContext.Carousels.ToListAsync();
            mymodel.Carousels = cvm.Carousels;

            var pvm = new ProductViewModel();
            pvm.Products = await EshopDBContext.Products.ToListAsync();
            mymodel.Products = pvm.Products;


            return View(mymodel);

         /*   var cvm = new CarouselViewModel();
            cvm.Carousels = await EshopDBContext.Carousels.ToListAsync();
            return View(vm);*/
        }

        public IActionResult About()
        {
            _logger.LogInformation("HomeController > About() was called.");
            ViewData["Message"] = "Your application description page.";
            //throw new Exception("Něco se stalo");

            return View();
        }

        public IActionResult Contact()
        {
            _logger.LogInformation("HomeController > Contact() was called.");
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Products()
        {
            _logger.LogInformation("HomeController > Products() was called.");
            ViewData["Message"] = "Your product content page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation("HomeController > Error() was called.");
            var featureException = HttpContext.Features.Get<IExceptionHandlerPathFeature>();            
             _logger.LogError("Exception occured: " + featureException.Error.ToString());

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ErrorCodeStatus(int? statusCode = null)
        {
            var statCode = statusCode.HasValue ? statusCode.Value : 0;
            _logger.LogWarning("Status Code: " + statCode);
            string originalURL = String.Empty;
            var features = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if(features != null)
            {
                originalURL = features.OriginalPathBase + features.OriginalPath + features.OriginalQueryString;
            }

            if(statCode == 404)
            {
                View404ViewModel vm404 = new View404ViewModel()
                {
                    StatusCode = statCode
                };

                return View("View"+statusCode.ToString(), vm404);
            }
            else if (statusCode.HasValue && statusCode.Value == 505)
            {
                View505ViewModel vm505 = new View505ViewModel()
                {
                    StatusCode = statusCode.HasValue ? statusCode.Value : 0
                };

                return View("View" + statusCode.ToString(), vm505);
            }

            ErrorCodeStatusViewModel vm = new ErrorCodeStatusViewModel()
            {
                StatusCode = statusCode.HasValue ? statusCode.Value : 0,
                OriginalURL = originalURL
            };

            return View(vm);
        }
    }
}
