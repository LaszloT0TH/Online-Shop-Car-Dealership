using CarDealershipASPNETMVC.Models;
using CarDealershipASPNETMVC.ViewModels;

namespace CarDealershipASPNETMVC.Data.Service
{
    public interface ICarAccessoriesService : IEntityStringBaseRepository<CarAccessoriesModel>
    {
        Task<CarAccessoriesDropdownsModel> GetCarAccessoriesDropdownsValues();
        Task<CarAccessoriesModel> GetCarAccessoriesByIdAsync(string id);
        Task UpdateCarAccessoriesAsync(CarAccessoriesEditViewModel data);
        Task<string> AddNewCarAccessoriesAsync(CarAccessoriesCreateViewModel data);
    }
}
