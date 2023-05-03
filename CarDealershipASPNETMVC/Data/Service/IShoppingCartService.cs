using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data.Service
{
    public interface IShoppingCartService : IEntityIntBaseRepository<ShoppingCartModel>
    {
        Task AddCarAccessoriesItemToCart(CarAccessoriesModel carAccessoriesItem, int quantity, string userId, string userRole);
        Task AddCarItemToCart(CarModel carItem, string userId, string userRole);
        Task AddToCart(int id);
        Task EditQuantityAsync(ShoppingCartItemModel shoppingCartItem);
        Task<ShoppingCartItemModel> GetByItemIdAsync(int id);
        Task<List<ShoppingCartModel>> GetShoppingCartByUserIdAndRoleAsync(string userId, string userRole);
        Task<List<ShoppingCartItemModel>> GetShoppingCartItems();
        Task Remove(int id);
        Task SaveForLater(int id);
    }
}