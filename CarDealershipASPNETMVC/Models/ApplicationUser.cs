using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models
{
    /// <summary>
    /// EN
    /// Extend IdentityUser Class
    /// The built-in IdentityUser class has very limited set of properties
    /// DE
    /// IdentityUser-Klasse erweitern
    /// Die eingebaute IdentityUser-Klasse hat einen sehr begrenzten Satz von Eigenschaften
    /// HU
    /// Az IdentityUser osztály kiterjesztése
    /// A beépített IdentityUser osztály nagyon korlátozott tulajdonságokkal rendelkezik.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Vorname")]
        [Required(ErrorMessage = "Bitte eingeben die Vorname")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name muss zwischen 3 und 50 Charakter sein")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Nachname")]
        [Required(ErrorMessage = "Bitte eingeben die Nachname")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nachname muss zwischen 3 und 50 Charakter sein")]
        public string LastName { get; set; } = null!;

        // Sex
        [Display(Name = "Geschlecht")]
        [Required(ErrorMessage = "Bitte eingeben die Geschlecht")]
        [Column("SexId")]
        public int SexId { get; set; }
        [ForeignKey("SexId")]
        public virtual SexModel sexModel { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

        [Display(Name = "Straße")]
        [Required(ErrorMessage = "Bitte eingeben die Straße Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Straße Name muss zwischen 3 und 50 Charakter sein")]
        public string Street { get; set; } = null!;

        [Display(Name = "Hausnummer")]
        [Required(ErrorMessage = "Bitte eingeben die Hausnummer")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Hausnummer muss zwischen 3 und 50 Charakter sein")]
        public string HouseNumber { get; set; } = null!;

        [Display(Name = "Postleitzahl")]
        [Required(ErrorMessage = "Bitte eingeben den Postleitzahl")]
        public int PostalCode { get; set; }

        [Display(Name = "Ort")]
        [Required(ErrorMessage = "Bitte eingeben den Ort")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ort muss zwischen 3 und 50 Charakter sein")]
        public string Location { get; set; } = null!;

        // Country
        [Display(Name = "Land")]
        [Required(ErrorMessage = "Bitte eingeben die Land")]
        [Column("CountryId")]
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual CountryModel countryModel { get; set; } = null!;

        [Display(Name = "Geburtsdatum")]
        [Required(ErrorMessage = "Bitte eingeben den Geburtsdatum")]
        [Column("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Manager")]
        [Column("ManagerId")]
        public Guid? ManagerId { get; set; }

        //Relationships
        public List<ApplicationUser_SpokenLangues> ApplicationUser_SpokenLangues { get; set; } = null!;


        [Display(Name = "Eintrittsdatum")]
        [Column("EntryDate")]
        public DateTime? EntryDate { get; set; }
    }
}
