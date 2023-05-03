using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data.Service
{
    public class FuelService : EntityIntBaseRepository<FuelModel>, IFuelService
    {
        private readonly AppDbContext context;

        public FuelService(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
