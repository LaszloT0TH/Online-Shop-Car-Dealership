using CarDealershipASPNETMVC.Data.Service;
using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CarDealershipASPNETMVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly ICarAccessoriesService carAccessoriesService;
        private readonly ICarsService carsService;

        public ShoppingCartController(IShoppingCartService shoppingCartService,
                                      ICarAccessoriesService carAccessoriesService,
                                      ICarsService carsService)
        {
            this.shoppingCartService = shoppingCartService;
            this.carAccessoriesService = carAccessoriesService;
            this.carsService = carsService;
        }

        static bool First_Entry = true;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (First_Entry) { First_Entry = false; GlobalData.ShoppingCartStatus = "Im Einkaufswagen"; }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var shoppingCart = await shoppingCartService.GetShoppingCartByUserIdAndRoleAsync(userId, userRole);
            return View(shoppingCart);
        }

        [HttpGet]
        public async Task<IActionResult> SaveForLaterFromShoppingCart(int id)
        {
            await shoppingCartService.SaveForLater(id);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> AddToShoppingCart(int id)
        {
            await shoppingCartService.AddToCart(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditQuantityFromItemShoppingCart(int id)
        {
            var shoppingCartItem = await shoppingCartService.GetByItemIdAsync(id);
            ViewBag.Quantity = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "1", Value = "1", Selected = true},
                new SelectListItem {Text = "2", Value = "2"},
                new SelectListItem {Text = "3", Value = "3"},
                new SelectListItem {Text = "4", Value = "4"},
                new SelectListItem {Text = "5", Value = "5"},
                new SelectListItem {Text = "6", Value = "6"},
                new SelectListItem {Text = "7", Value = "7"},
                new SelectListItem {Text = "8", Value = "8"},
                new SelectListItem {Text = "9", Value = "9"},
                new SelectListItem {Text = "10", Value = "10"},
                new SelectListItem {Text = "11", Value = "11"},
                new SelectListItem {Text = "12", Value = "12"},
                new SelectListItem {Text = "13", Value = "13"},
                new SelectListItem {Text = "14", Value = "14"},
                new SelectListItem {Text = "15", Value = "15"},
            }, "Value", "Text");
            return View(shoppingCartItem);
        }

        [HttpPost]
        public async Task<IActionResult> EditQuantityFromItemShoppingCart(ShoppingCartItemModel shoppingCartItem)
        {
            await shoppingCartService.EditQuantityAsync(shoppingCartItem);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToShoppingCart(string id, int quantity)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var carAccessoriesItem = await carAccessoriesService.GetCarAccessoriesByIdAsync(id);

            if (carAccessoriesItem != null)
            {
                await shoppingCartService.AddCarAccessoriesItemToCart(carAccessoriesItem, quantity, userId, userRole);
                return RedirectToAction("Index", "CarAccessories");
            }

            var carItem = await carsService.GetCarByIdAsync(id);

            if (carItem != null)
            {
                await shoppingCartService.AddCarItemToCart(carItem, userId, userRole);
                return RedirectToAction("Index", "Car");
            }

            return View("NotFound");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            await shoppingCartService.Remove(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ShoppingCartView()
        {
            GlobalData.ShoppingCartStatus = "Im Einkaufswagen";
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult SaveForLaterView()
        {
            GlobalData.ShoppingCartStatus = "Für später gespeichert";
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult OrderedView()
        {
            GlobalData.ShoppingCartStatus = "Bestellt";
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult InTransitView()
        {
            GlobalData.ShoppingCartStatus = "Unterwegs";
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult DeliveredView()
        {
            GlobalData.ShoppingCartStatus = "Zugestellt";
            return RedirectToAction("Index");
        }
    }
}
