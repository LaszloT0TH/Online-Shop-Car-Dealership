using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CarDealershipASPNETMVC.Models
{
    public class CarAccessoriesModel : IEntityStringBase
    {
        //[NotMapped]
        //public string? EncryptedId { get; set; }

        [Key]
        [Display(Name = "Auto Zubehör Id")]
        [Column("Id")]
        public string Id { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

        [Display(Name = "Produkt")]
        [Required(ErrorMessage = "Bitte eingeben den Produkt Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Produkt Name muss zwischen 3 und 50 Charakter sein")]
        [Column("ProductName")] 
        public string ProductName { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

        // Car Accessories Product Group
        //https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application
        // EN
        // Foreign key properties and navigation properties reflect the following relationships:
        // The registration record is for a single course, so there is a foreign key property and
        // a navigation property:CAPGId
        // GE
        // Fremdschlüsseleigenschaften und Navigationseigenschaften spiegeln die folgenden Beziehungen wider:
        // Der Registrierungsdatensatz gilt für einen einzelnen Kurs, daher gibt es eine Fremdschlüsseleigenschaft und
        // eine Navigationseigenschaft:CAPGId
        // HU
        // Az idegenkulcs tulajdonságai és a navigációs tulajdonságok a következő kapcsolatokat tükrözik:
        // A regisztrációs rekord egyetlen tanfolyamra vonatkozik, így van egy külföldi kulcstulajdonság és
        // egy navigációs tulajdonság:CAPGId
        [Display(Name = "Produktgruppe")]
        [Required(ErrorMessage = "Bitte eingeben die Produktgruppe Name")]
        [Column("CAPGId")] 
        public int CAPGId { get; set; }
        [ForeignKey("CAPGId")]
        public virtual CarAccessoriesProductGroupModel carAccessoriesProductGroupModel { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

        [Display(Name = "Lagerbestand")]
        [Required(ErrorMessage = "Bitte eingeben den Lagerbestand")]
        [Column("QuantityOfStock")]
        public int QuantityOfStock { get; set; }

        [Display(Name = "Mindestbestandsmenge")]
        [Required(ErrorMessage = "Bitte eingeben die Mindestbestandsmenge")]
        [Column("MinimumStockQuantity")]
        public int MinimumStockQuantity { get; set; }

        [Display(Name = "Netto-Verkaufspreis")]
        [Required(ErrorMessage = "Bitte eingeben den Netto-Verkaufspreis")]
        [Column("NetSellingPrice")]
        public double NetSellingPrice { get; set; }

        [Display(Name = "Verkaufseinheit")]
        [Required(ErrorMessage = "Bitte eingeben die Verkaufseinheit")]
        [Column("SalesUnit")]
        public double SalesUnit { get; set; }

        // EN
        // Display the gross price by country
        // GE
        // Display the gross price by country
        // HU
        // A bruttó ár megjelenítése ország szerint
        // Brutto price = (double)(NetSellingPrice * SalesUnit * (1 + (CountryTaxPercentageValue / 100)));
        [Display(Name = "Einheit Preis")]
        [Column("UnitPrice")]
        public double UnitPrice
        {
            get
            {
                return (double)(NetSellingPrice * SalesUnit);
            }
        }

        // Car Accessories Unit
        [Display(Name = "Einheit Name")]
        [Required(ErrorMessage = "Bitte eingeben den Einheit Name")]
        [Column("UnitNameId")]
        public int UnitNameId { get; set; }
        [ForeignKey("UnitNameId")]
        public virtual CarAccessoriesUnitModel carAccessoriesUnitModel { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

        [Display(Name = "Marke")]
        [Required(ErrorMessage = "Bitte eingeben den Marke")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Marke muss zwischen 1 und 50 Charakter sein")]
        [Column("Brand")]
        public string Brand { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

        [Display(Name = "Erstellungsdatum")]
        [Required(ErrorMessage = "Bitte eingeben den Erstellungsdatum")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column("CreationDate")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Beschreibung")]
        [Required(ErrorMessage = "Bitte eingeben die Beschreibung")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Description muss zwischen 5 und 500 Charakter sein")]
        [Column("Description")]
        public string Description { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

        [Display(Name = "Version")]
        [Required(ErrorMessage = "Bitte eingeben den Version")]
        [Column("Version")]
        public int Version { get; set; }

        [Display(Name = "Letzte Aktualisierungszeit")]
        [Required(ErrorMessage = "Bitte eingeben den Letzte Aktualisierungszeit")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column("LastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }

        [Column("PhotoPath")]
        public string? PhotoPath { get; set; }
    }
}
