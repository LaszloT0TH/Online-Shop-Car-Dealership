using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data.Service
{
    public class CarAccessoriesProductGroupService : EntityIntBaseRepository<CarAccessoriesProductGroupModel>, ICarAccessoriesProductGroupService
    {
        private readonly AppDbContext context;

        public CarAccessoriesProductGroupService(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
