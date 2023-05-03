using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.ViewModels
{
    public class CarCreateViewModel
    {
        [Display(Name = "Modell")]
        [Required(ErrorMessage = "Bitte eingeben den Modell")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Modell Name muss zwischen 3 und 50 Charakter sein")]
        [Column("Model")]
        public string Model { get; set; }

        [Display(Name = "Farbe")]
        [Required(ErrorMessage = "Bitte eingeben den Color")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Color Name muss zwischen 3 und 50 Charakter sein")]
        [Column("Color")]
        public string Color { get; set; }

        [Display(Name = "Anzahl Sitzplätze")]
        [Required(ErrorMessage = "Bitte eingeben den Anzahl Sitzplätze")]
        [Column("NumberOfSeats")]
        public int NumberOfSeats { get; set; }

        [Display(Name = "Baujahr")]
        [Required(ErrorMessage = "Bitte eingeben die Baujahr")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column("YearOfProduction")]
        public DateTime YearOfProduction { get; set; }

        // Fuel
        //https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application
        // EN
        // Foreign key properties and navigation properties reflect the following relationships:
        // The registration record is for a single course, so there is a foreign key property and
        // a navigation property:FuelId
        // GE
        // Fremdschlüsseleigenschaften und Navigationseigenschaften spiegeln die folgenden Beziehungen wider:
        // Der Registrierungsdatensatz gilt für einen einzelnen Kurs, daher gibt es eine Fremdschlüsseleigenschaft und
        // eine Navigationseigenschaft:FuelId
        // HU
        // Az idegenkulcs tulajdonságai és a navigációs tulajdonságok a következő kapcsolatokat tükrözik:
        // A regisztrációs rekord egyetlen tanfolyamra vonatkozik, így van egy külföldi kulcstulajdonság és
        // egy navigációs tulajdonság:FuelId
        [Display(Name = "Kraftstoffname")]
        [Required(ErrorMessage = "Bitte eingeben die Kraftstoff")]
        [ForeignKey("FuelId")]
        [Column("FuelId")]
        public int FuelId { get; set; }       

        // Gearbox
        //https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application
        [Display(Name = "Getriebetyp")]
        [Required(ErrorMessage = "Bitte eingeben die Getriebetyp")]
        [ForeignKey("GearboxId")]
        [Column("GearboxId")]
        public int GearboxId { get; set; }
        
        [Display(Name = "Hubraum")]
        [Required(ErrorMessage = "Bitte eingeben den größe vom Hubraum")]
        [Column("CubicCapacity")]
        public double CubicCapacity { get; set; }

        [Display(Name = "Kilometerstand")]
        [Required(ErrorMessage = "Bitte eingeben den Kilometerstand")]
        [Column("Mileage")]
        public double Mileage { get; set; }

        [Display(Name = "Fahrgestellnummer")]
        [Required(ErrorMessage = "Bitte eingeben die Fahrgestellnummer")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Fahrgestellnummer muss zwischen 10 und 50 Charakter sein")]
        [Column("ChassisNumber")]
        public string ChassisNumber { get; set; }

        [Display(Name = "Motorleistung")]
        [Required(ErrorMessage = "Bitte eingeben die Motorleistung")]
        [Column("EnginePower")]
        public int EnginePower { get; set; }

        [Display(Name = "Eigengewicht")]
        [Required(ErrorMessage = "Bitte eingeben den Eigengewicht")]
        [Column("OwnWeight")]
        public int OwnWeight { get; set; }

        [Display(Name = "Verkauft")]
        [Column("Sold")]
        public bool Sold { get; set; }

        [Display(Name = "Nettopreis")]
        [Required(ErrorMessage = "Bitte eingeben den Nettopreis")]
        [Column("NettoPrice")]
        public double NettoPrice { get; set; }

        [Display(Name = "Letzte Aktualisierungszeit")]
        [Required(ErrorMessage = "Bitte eingeben den Letzte Aktualisierungszeit")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column("LastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }

        [Column("PhotoPath")]
        public string? PhotoPath { get; set; }
        public IFormFile? Photo { get; set; }

    }
}
