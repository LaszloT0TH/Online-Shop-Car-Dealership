using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarDealershipASPNETMVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        [Display(Name = "Bestätige das Passwort")]
        //[Compare("Password",
        //    ErrorMessage = "Password and confirmation password do not match.")]
        [Compare("Password",
            ErrorMessage = "Passwort und Bestätigungspasswort stimmen nicht überein.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Vorname")]
        [Required(ErrorMessage = "Bitte eingeben die Vorname")]
        public string FirstName { get; set; }

        [Display(Name = "Nachname")]
        [Required(ErrorMessage = "Bitte eingeben die Nachname")]
        public string LastName { get; set; }

        // Sex
        [Display(Name = "Geschlecht")]
        [Required(ErrorMessage = "Bitte eingeben die Geschlecht")]
        public int SexId { get; set; }

        [Display(Name = "Straße")]
        [Required(ErrorMessage = "Bitte eingeben die Straße Name")]
        public string Street { get; set; }

        [Display(Name = "Hausnummer")]
        [Required(ErrorMessage = "Bitte eingeben die Hausnummer")]
        public string HouseNumber { get; set; }

        [Display(Name = "Postleitzahl")]
        [Required(ErrorMessage = "Bitte eingeben den Postleitzahl")]
        public int PostalCode { get; set; }

        [Display(Name = "Ort")]
        [Required(ErrorMessage = "Bitte eingeben den Ort")]
        public string Location { get; set; }

        // Country
        [Display(Name = "Land")]
        [Required(ErrorMessage = "Bitte eingeben die Land")]
        public int CountryId { get; set; }

    }
}
