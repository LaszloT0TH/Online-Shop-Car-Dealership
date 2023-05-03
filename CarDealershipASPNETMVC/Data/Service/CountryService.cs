using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data.Service
{
    public class CountryService : EntityIntBaseRepository<CountryModel>, ICountryService
    {
        private readonly AppDbContext context;

        public CountryService(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
