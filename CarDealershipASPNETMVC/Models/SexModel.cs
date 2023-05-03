using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models;
public class SexModel : IEntityIntBase
{
    [Key]
    [Display(Name = "Geschlecht Id")]
    [Column("Id")]
    public int Id { get; set; }

    [Display(Name = "Geschlechtsname")]
    [Required(ErrorMessage = "Bitte eingeben die Geschlechtsname")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Geschlechtsname muss zwischen 3 und 50 Charakter sein")]
    [Column("SexName")]
    public string SexName { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU
}
