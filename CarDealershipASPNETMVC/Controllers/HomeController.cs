using CarDealershipASPNETMVC.Models;
using CarDealershipASPNETMVC.Security;
using CarDealershipASPNETMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarDealershipASPNETMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataProtector protector;

        public HomeController(ILogger<HomeController> logger,
                                IDataProtectionProvider dataProtectionProvider,
                              DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _logger = logger;
            
            this.protector = dataProtectionProvider.CreateProtector(
                dataProtectionPurposeStrings.IdRouteValue);
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home";

            return View();
        }

        public IActionResult Login()
        {
            ViewData["Title"] = "Login";

            return RedirectToAction("Edit", "Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData["Title"] = "Error";

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}