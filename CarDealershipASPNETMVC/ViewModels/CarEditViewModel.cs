using System.ComponentModel.DataAnnotations;

namespace CarDealershipASPNETMVC.ViewModels
{
    public class CarEditViewModel : CarCreateViewModel
    {
        [Display(Name = "Auto Id")]
        public string Id { get; set; }

        public string? ExistingPhotoPath { get; set; }
    }
}
