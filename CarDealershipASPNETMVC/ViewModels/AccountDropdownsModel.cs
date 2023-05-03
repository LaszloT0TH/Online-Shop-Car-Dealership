using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.ViewModels
{
    public class AccountDropdownsModel
    {
        public AccountDropdownsModel()
        {
            Countries = new List<CountryModel>();
            Sexs = new List<SexModel>();
            SpokenLangues = new List<SpokenLanguesModel>();
        }

        public List<CountryModel> Countries { get; set; }
        public List<SexModel> Sexs { get; set; }
        public List<SpokenLanguesModel> SpokenLangues { get; set; }

    }
}
