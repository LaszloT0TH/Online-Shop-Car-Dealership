using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data.Service
{
    public class SpokenLanguesService : EntityIntBaseRepository<SpokenLanguesModel>, ISpokenLanguesService
    {
        private readonly AppDbContext context;

        public SpokenLanguesService(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
