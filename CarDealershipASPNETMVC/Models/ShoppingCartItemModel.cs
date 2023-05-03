using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models
{
    public class ShoppingCartItemModel
    {
        // https://learn.microsoft.com/en-us/aspnet/web-forms/overview/getting-started/getting-started-with-aspnet-45-web-forms/shopping-cart
        [Key]
        public int Id { get; set; }

        public string? CarAccessoriesId { get; set; }
        [ForeignKey("CarAccessoriesId")]
        public CarAccessoriesModel? CarAccessories { get; set; }

        public string? CarId { get; set; }
        [ForeignKey("CarId")]
        public virtual CarModel? Cars { get; set; }


        [Display(Name = "Menge")]
        public int Quantity { get; set; }

        [Display(Name = "Versanddatum")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ShippedDate { get; set; }

        [Display(Name = "Netto Verkaufsbetrag")]
        public double SaleAmount { get; set; }

        [Display(Name = "Brutto Verkaufsbetrag")]
        public double GrossSaleAmount { get { return (SaleAmount * (1 + TaxPercentageValue / 100)); } } 

        [Display(Name = "Steuerprozentsatz")]
        public double TaxPercentageValue { get; set; }

        [Display(Name = "Verkaufszeit")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SaleTime { get; set; }


        //[HiddenInput(DisplayValue = false)]
        [Display(Name = "Statusname des Auftrag")]
        public int ShoppingCartOrderStatusId { get; set; }
        [ForeignKey("ShoppingCartOrderStatusId")]
        public virtual OrderStatusModel OrderStatus { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

        [Display(Name = "Statusname des Einkaufswagens")]
        public int ShoppingCartStatusId { get; set; }
        [ForeignKey("ShoppingCartStatusId")]
        public virtual ShoppingCartStatusModel ShoppingCartStatus { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU


        public int ShoppingCartId { get; set; }
        [ForeignKey("ShoppingCartId")]
        public virtual ShoppingCartModel ShoppingCart { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

    }
}
