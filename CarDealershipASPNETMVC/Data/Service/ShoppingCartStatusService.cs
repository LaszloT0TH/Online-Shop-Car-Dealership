using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data.Service
{
    public class ShoppingCartStatusService : EntityIntBaseRepository<ShoppingCartStatusModel>, IShoppingCartStatusService
    {
        private readonly AppDbContext context;

        public ShoppingCartStatusService(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
