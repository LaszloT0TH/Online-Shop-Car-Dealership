using CarDealershipASPNETMVC.Models;
namespace CarDealershipASPNETMVC.Data.Service
{
    public class CarAccessoriesUnitService : EntityIntBaseRepository<CarAccessoriesUnitModel>, ICarAccessoriesUnitService
    {
        private readonly AppDbContext context;

        public CarAccessoriesUnitService(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
