using CarDealershipASPNETMVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
            SpokenLanguesId = new List<int>();
        }

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Vorname")]
        [Required(ErrorMessage = "Bitte eingeben die Vorname")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name muss zwischen 3 und 50 Charakter sein")]
        public string FirstName { get; set; }

        [Display(Name = "Nachname")]
        [Required(ErrorMessage = "Bitte eingeben die Nachname")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nachname muss zwischen 3 und 50 Charakter sein")]
        public string LastName { get; set; }

        // Sex
        [Display(Name = "Geschlecht")]
        [Required(ErrorMessage = "Bitte eingeben die Geschlecht")]
        [Column("SexId")]
        public int SexId { get; set; }
        [ForeignKey("SexId")]
        public virtual SexModel sexModel { get; set; }

        [Display(Name = "Straße")]
        [Required(ErrorMessage = "Bitte eingeben die Straße Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Straße Name muss zwischen 3 und 50 Charakter sein")]
        public string Street { get; set; }

        [Display(Name = "Hausnummer")]
        [Required(ErrorMessage = "Bitte eingeben die Hausnummer")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Hausnummer muss zwischen 3 und 50 Charakter sein")]
        public string HouseNumber { get; set; }

        [Display(Name = "Postleitzahl")]
        [Required(ErrorMessage = "Bitte eingeben den Postleitzahl")]
        public int PostalCode { get; set; }

        [Display(Name = "Ort")]
        [Required(ErrorMessage = "Bitte eingeben den Ort")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ort muss zwischen 3 und 50 Charakter sein")]
        public string Location { get; set; }

        // Country
        [Display(Name = "Land")]
        [Required(ErrorMessage = "Bitte eingeben die Land")]
        [Column("CountryId")]
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual CountryModel countryModel { get; set; }

        [Display(Name = "Geburtsdatum")]
        [Required(ErrorMessage = "Bitte eingeben den Geburtsdatum")]
        [Column("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Manager")]
        [Column("ManagerId")]
        public Guid? ManagerId { get; set; }

        public List<int>? SpokenLanguesId { get; set; }


        [Display(Name = "Eintrittsdatum")]
        [Required(ErrorMessage = "Bitte eingeben den Eintrittsdatum")]
        [Column("EntryDate")]
        public DateTime? EntryDate { get; set; }

        public List<string> Claims { get; set; }

        public IList<string> Roles { get; set; }
    }
}
