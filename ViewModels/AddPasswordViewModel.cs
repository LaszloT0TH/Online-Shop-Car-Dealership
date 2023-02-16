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
        //[Compare("NewPassword", ErrorMessage =
        //    "The new password and confirmation password do not match.")]
        [Compare("NewPassword", ErrorMessage =
            "Das neue Passwort und das Bestätigungspasswort stimmen nicht überein.")]
        public string ConfirmPassword { get; set; }
    }
}
