using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models;

public class StockReplenishmentListModel : IEntityIntBase
{
    [Key]
    [Display(Name = "Id")]
    public int Id { get; set; }

    // EN
    // Car Accessories cannot be deleted if it is on the Stock Replenishment list
    // GE
    // Az autótartozékok nem törölhetők, ha szerepelnek a Készletfeltöltés listán
    // HU
    // Az autótartozékok nem törölhetők, ha szerepelnek a Készletfeltöltés listán
    [Display(Name = "Produkt ID")]
    public string ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public CarAccessoriesModel CarAccessories { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

    [Display(Name = "Produkt Name")]
    public string ProductName { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

    [Display(Name = "Bestellstatus")]
    public bool OrderedStatus { get; set; }

    [Display(Name = "Zeitstempel")]
    public DateTime SRLTimeStamp { get; set; }
}
