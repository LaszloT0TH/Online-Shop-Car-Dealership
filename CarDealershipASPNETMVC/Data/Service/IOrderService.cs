using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data.Service
{
    public interface IOrderService : IEntityIntBaseRepository<OrderModel>
    {
        Task<List<OrderModel>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
        Task StoreOrderAsync(List<ShoppingCartItemModel> items, string userId, string userEmailAddress);
        Task UpdateOrderStatusAddSalesPersonAsync(int id, string userId);
        Task UpdateOrderStatusAndShoppingCartStatusAsync(int id);
        Task UpdateOrderStatusBackPostingToStockAsync(int id);
    }
}