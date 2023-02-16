using CarDealershipASPNETMVC.Data.Service;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarDealershipASPNETMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService ordersService;
        private readonly ICarAccessoriesService carAccessoriesService;
        private readonly ICarsService carsService;
        private readonly IShoppingCartService shoppingCartService;

        public OrderController(IOrderService ordersService,
                               ICarAccessoriesService carAccessoriesService,
                               ICarsService carsService,
                               IShoppingCartService shoppingCartService)
        {
            this.ordersService = ordersService;
            this.carAccessoriesService = carAccessoriesService;
            this.carsService = carsService;
            this.shoppingCartService = shoppingCartService;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Processing(int id)
        {
            // update orderStatusId = 2 //Verarbeitung
            // add salesPerson with Id = User.Id
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await ordersService.UpdateOrderStatusAddSalesPersonAsync(id, userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Rejected(int id)
        {
            // update orderStatusId = 3 //Abgelehnt
            // back posting to stock
            // check and if necessary, remove it from the Stock Replenishment list
            await ordersService.UpdateOrderStatusBackPostingToStockAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Completed(int id)
        {
            // update orderStatusId = 4 //Abgeschlossen
            // update shoppingCartStatusId = 4 //Unterwegs
            await ordersService.UpdateOrderStatusAndShoppingCartStatusAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CompleteOrder()
        {
            var items = await shoppingCartService.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await ordersService.StoreOrderAsync(items, userId, userEmailAddress);

            return RedirectToAction("Index", "ShoppingCart");
        }

    }
}
