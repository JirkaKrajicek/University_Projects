using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Controllers;
using eshop.Models;
using eshop.Models.ApplicationServices;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ubiety.Dns.Core;

namespace eshop.Areas.Security.Controllers
{
    [Area("Security")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private ILogger _logger;
        ISecurityApplicationService iSecure;
        EshopDBContext EshopDBContext;
        IHostingEnvironment Env;

        public AccountController(EshopDBContext eshopDBContext, IHostingEnvironment env, ISecurityApplicationService iSecure, ILoggerFactory logger)
        {
            this.Env = env;
            this.EshopDBContext = eshopDBContext;
            this.iSecure = iSecure;
            _logger = logger.CreateLogger(typeof(AccountController));
        }

        public IActionResult Login()
        {
            _logger.LogInformation("AccountController > Login() was called.");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            _logger.LogInformation("AccountController > Login() was called.");
            vm.LoginFailed = false;

            if (ModelState.IsValid)
            {
                bool isLogged = await iSecure.Login(vm);
                if (isLogged)
                {                    
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", String.Empty), new { area = "" });
                }
                else
                {
                    vm.LoginFailed = true;
                    
                }
            }
            return View(vm);
        }

        public IActionResult Logout()
        {
            _logger.LogInformation("AccountController > Logout() was called.");
            iSecure.Logout();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Register()
        {
            _logger.LogInformation("AccountController > Register() was called.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            _logger.LogInformation("AccountController > Register() was called.");
            vm.ErrorsDuringRegister = null;
            if (ModelState.IsValid)
            {
                vm.ErrorsDuringRegister = await iSecure.Register(vm, Models.Identity.Roles.Customer);

                if(vm.ErrorsDuringRegister == null)
                {
                    var lvM = new LoginViewModel()
                    {
                        Username = vm.Username,
                        Password = vm.Password,
                        RememberMe = true,
                        LoginFailed = false
                    };

                    return await Login(lvM);
                }

            }
            return View(vm);
        }

        public async Task<IActionResult> Accounts()
        {
            _logger.LogInformation("AccountController > Accounts() was called.");
            AccountsViewModel accountsVM = new AccountsViewModel();
            accountsVM.Accounts = await EshopDBContext.Accounts.ToListAsync();
            return View(accountsVM);
        }



        public IActionResult Edit(int id)
        {
            _logger.LogInformation("AccountController > Edit() was called.");
            User userItem = EshopDBContext.Accounts.Where(cr => cr.Id == id).FirstOrDefault();
            if (userItem != null)
            {
                return View(userItem);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            _logger.LogInformation("AccountController > Edit() was called.");
            User userItem = EshopDBContext.Accounts.Where(cr => cr.Id == user.Id).FirstOrDefault();
            if (userItem != null)
            {
                userItem.UserName = user.UserName;
                userItem.Name = user.Name;
                userItem.LastName = user.LastName;
                userItem.PhoneNumber = user.PhoneNumber;
                userItem.Email = user.Email;

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