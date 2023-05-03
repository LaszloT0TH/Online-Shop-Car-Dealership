using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data.Service
{
    public class SexService : EntityIntBaseRepository<SexModel>, ISexService
    {
        private readonly AppDbContext context;

        public SexService(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
