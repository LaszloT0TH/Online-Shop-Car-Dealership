using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models
{
    public class OrderItemModel
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Verkäufer Id")]
        public string? SalesPersonId { get; set; }


        public string? CarAccessoriesId { get; set; }
        [ForeignKey("CarAccessoriesId")]
        public virtual CarAccessoriesModel? CarAccessories { get; set; }


        public string? CarId { get; set; }
        [ForeignKey("CarId")]
        public virtual CarModel? Cars { get; set; }


        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public OrderModel Order { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU


        [Display(Name = "Menge")]
        public int Quantity { get; set; }

        [Display(Name = "Bestelldatum")]
        public DateTime OrderDate { get; set; }


        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Bestellstatus Id")]
        public int OrderStatusId { get; set; }
        // kulcs
        [ForeignKey("OrderStatusId")]
        public OrderStatusModel OrderStatus { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU


        [Display(Name = "Rabatt")]
        public double Discount { get; set; }

        [Display(Name = "Versanddatum")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ShippedDate { get; set; }

        [Display(Name = "Netto Verkaufsbetrag")]
        public double SaleAmount { get; set; }

        [Display(Name = "Brutto Verkaufsbetrag")]
        public double GrossSaleAmount { get { return (SaleAmount * (1 + CountryTaxPercentageValue / 100)); } }

        [Display(Name = "Bezahlter Verkaufsbetrag")]
        public double? SaleAmountPaid { get; set; }

        [Display(Name = "Steuerprozentsatz")]
        public double CountryTaxPercentageValue { get; set; }

        [Display(Name = "Verkaufszeit")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SaleTime { get; set; }


        [HiddenInput(DisplayValue = false)]
        public int ShoppingCartId { get; set; }


        [Display(Name = "Statusname des Einkaufswagens")]
        public int ShoppingCartStatusId { get; set; }
        [ForeignKey("ShoppingCartStatusId")]
        public ShoppingCartStatusModel ShoppingCartStatus { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

    }
}