using System.ComponentModel.DataAnnotations;

namespace CarDealershipASPNETMVC.ViewModels
{
    public class CarAccessoriesEditViewModel : CarAccessoriesCreateViewModel
    {
        [Display(Name = "Auto Zubehör Id")]
        public string Id { get; set; }

        public string? ExistingPhotoPath { get; set; }
    }
}
 