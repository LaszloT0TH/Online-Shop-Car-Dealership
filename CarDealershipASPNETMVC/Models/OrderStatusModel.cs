using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models;
public class OrderStatusModel : IEntityIntBase
{
    [Key]
    [Display(Name = "Auftragsstatus Id")]
    [Column("Id")]
    public int Id { get; set; }

    [Display(Name = "Auftragsstatusname")]
    [Required(ErrorMessage = "Bitte eingeben die Auftragsstatusname")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Auftragsstatusname muss zwischen 3 und 50 Charakter sein")]
    [Column("OrderStatusName")]
    public string OrderStatusName { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU
}
