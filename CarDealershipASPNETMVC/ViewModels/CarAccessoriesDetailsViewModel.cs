using CarDealershipASPNETMVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.ViewModels
{

    public class CarAccessoriesDetailsViewModel
    {
        public CarAccessoriesModel CarAccessories { get; set; }

        [NotMapped]
        public string? EncryptedId { get; set; }
        
        public IFormFile? Photo { get; set; }

        [Required]
        [Display(Name = "Einkaufsmenge")]
        public int Quantity { get; set; }

    }

}
