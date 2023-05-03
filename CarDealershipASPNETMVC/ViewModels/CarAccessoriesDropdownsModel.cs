using CarDealershipASPNETMVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.ViewModels
{

    public class CarAccessoriesDropdownsModel
    {
        public CarAccessoriesDropdownsModel()
        {
            CarAccessoriesProductGroups = new List<CarAccessoriesProductGroupModel>();
            CarAccessoriesUnits = new List<CarAccessoriesUnitModel>();
        }

        public List<CarAccessoriesProductGroupModel> CarAccessoriesProductGroups { get; set; }
        public List<CarAccessoriesUnitModel> CarAccessoriesUnits { get; set; }
    }

}
