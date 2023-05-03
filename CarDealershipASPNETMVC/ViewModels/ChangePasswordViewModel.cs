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
        // EN
        //[Compare("NewPassword", ErrorMessage =
        //    "The new password and confirmation password do not match.")]
        // DE
        [Compare("NewPassword", ErrorMessage =
            "Das neue Passwort und das Bestätigungspasswort stimmen nicht überein.")]
        // HU
        //[Compare("NewPassword", ErrorMessage =
        //    "Az új jelszó és a megerősítő jelszó nem egyezik.")]

        public string ConfirmPassword { get; set; }
    }
}
