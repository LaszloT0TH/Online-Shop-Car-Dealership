using System.ComponentModel.DataAnnotations;

namespace CarDealershipASPNETMVC.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        //[Display(Name = "Current password")]
        [Display(Name = "Aktuelles Passwort")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        //[Compare("NewPassword", ErrorMessage =
        //    "The new password and confirmation password do not match.")]
        [Compare("NewPassword", ErrorMessage =
            "Das neue Passwort und das Bestätigungspasswort stimmen nicht überein.")]
        public string ConfirmPassword { get; set; }
    }
}
