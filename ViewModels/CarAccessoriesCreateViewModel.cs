﻿using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.ViewModels
{
    public class CarAccessoriesCreateViewModel
    {
        [Display(Name = "Produkt")]
        [Required(ErrorMessage = "Bitte eingeben den Produkt Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Produkt Name muss zwischen 3 und 50 Charakter sein")]
        [Column("ProductName")]
        public string ProductName { get; set; }

        // Car Accessories Product Group
        //https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application
        [Display(Name = "Produktgruppe")]
        [Required(ErrorMessage = "Bitte eingeben die Produktgruppe Name")]
        [Column("CAPGId")]
        public int CAPGId { get; set; }

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

        [Display(Name = "Marke")]
        [Required(ErrorMessage = "Bitte eingeben den Marke")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Marke muss zwischen 1 und 50 Charakter sein")]
        [Column("Brand")]
        public string Brand { get; set; }

        [Display(Name = "Erstellungsdatum")]
        [Required(ErrorMessage = "Bitte eingeben den Erstellungsdatum")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column("CreationDate")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Beschreibung")]
        [Required(ErrorMessage = "Bitte eingeben die Beschreibung")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Description muss zwischen 5 und 500 Charakter sein")]
        [Column("Description")]
        public string Description { get; set; }

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

        public IFormFile? Photo { get; set; }

    }
}
