using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models;

public class StockReplenishmentListModel : IEntityIntBase
{
    [Key]
    [Display(Name = "Id")]
    public int Id { get; set; }

    // Car Accessories cannot be deleted if it is on the Stock Replenishment list
    [Display(Name = "Produkt ID")]
    public string ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public CarAccessoriesModel CarAccessories { get; set; }

    [Display(Name = "Produkt Name")]
    public string ProductName { get; set; }

    [Display(Name = "Bestellstatus")]
    public bool OrderedStatus { get; set; }

    [Display(Name = "Zeitstempel")]
    public DateTime SRLTimeStamp { get; set; }
}
