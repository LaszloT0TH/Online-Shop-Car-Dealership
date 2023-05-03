using CarDealershipASPNETMVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.ViewModels
{
    public class CarDropdownsModel
    {
        public CarDropdownsModel()
        {
            Fuels = new List<FuelModel>();
            Gearboxs = new List<GearboxModel>();
        }

        public List<FuelModel> Fuels { get; set; }
        public List<GearboxModel> Gearboxs { get; set; }
    }
}