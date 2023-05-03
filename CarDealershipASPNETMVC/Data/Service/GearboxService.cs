using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data.Service
{
    public class GearboxService : EntityIntBaseRepository<GearboxModel>, IGearboxService
    {
        private readonly AppDbContext context;

        public GearboxService(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
