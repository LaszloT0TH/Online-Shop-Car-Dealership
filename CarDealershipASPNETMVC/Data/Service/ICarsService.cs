using CarDealershipASPNETMVC.Models;
using CarDealershipASPNETMVC.ViewModels;

namespace CarDealershipASPNETMVC.Data.Service
{
    public interface ICarsService : IEntityStringBaseRepository<CarModel>
    {
        Task<string> AddNewCarAsync(CarCreateViewModel data);
        Task<CarModel> GetCarByIdAsync(string id);
        Task<CarDropdownsModel> GetCarsDropdownsValues();
        Task UpdateCarAsync(CarEditViewModel data);
    }
}
