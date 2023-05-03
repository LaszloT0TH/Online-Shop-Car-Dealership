using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models;
public class CarAccessoriesUnitModel : IEntityIntBase
{
    [Key]
    [Display(Name = "Einheit Id")]
    [Column("Id")]
    public int Id { get; set; }

    [Display(Name = "Einheit")]
    [Required(ErrorMessage = "Bitte eingeben die Einheit Name")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Einheit Name muss zwischen 3 und 50 Charakter sein")]
    [Column("UnitName")]
    public string UnitName { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

}
