using System.ComponentModel.DataAnnotations;

namespace CarDealershipASPNETMVC.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        // EN
        //[Display(Name = "Confirm password")]
        // GE
        [Display(Name = "Bestätige das Passwort")]
        // EN
        //[Compare("Password",
        //    ErrorMessage = "Password and Confirm Password must match")]
        // GE
        [Compare("Password",
            ErrorMessage = "Passwort und Passwort bestätigen müssen übereinstimmen")]
        // HU
        //[Compare("Password",
        //    ErrorMessage = "A jelszónak és a Jelszó megerősítésének meg kell egyeznie")]

        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
