using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data.Service
{
    public class OrderStatusService : EntityIntBaseRepository<OrderStatusModel>, IOrderStatusService
    {
        private readonly AppDbContext context;

        public OrderStatusService(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
