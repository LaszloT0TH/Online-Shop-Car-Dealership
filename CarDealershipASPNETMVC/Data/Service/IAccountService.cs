using CarDealershipASPNETMVC.ViewModels;

namespace CarDealershipASPNETMVC.Data.Service
{
    public interface IAccountService
    {
        Task<AccountDropdownsModel> GetAccountDropdownsValues();
        Task<List<int>> GetSpokenLangues(string id);
        Task UpdateSpokenLanguesAsync(EditUserViewModel data);
    }
}