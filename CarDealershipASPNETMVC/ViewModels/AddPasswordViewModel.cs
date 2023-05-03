using System.ComponentModel.DataAnnotations;

namespace CarDealershipASPNETMVC.ViewModels
{
    public class AddPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        // EN
        //[Compare("NewPassword", ErrorMessage =
        //    "The new password and confirmation password do not match.")]
        // GE
        [Compare("NewPassword", ErrorMessage =
            "Das neue Passwort und das Bestätigungspasswort stimmen nicht überein.")]
        // HU
        //[Compare("NewPassword", ErrorMessage =
        //    "Az új jelszó és a megerősítő jelszó nem egyezik.")]
        public string ConfirmPassword { get; set; }
    }
}
