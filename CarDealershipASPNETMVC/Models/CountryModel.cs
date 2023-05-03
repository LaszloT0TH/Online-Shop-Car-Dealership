using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models;
public class CountryModel : IEntityIntBase
{
    [Key]
    [Display(Name = "Land Id")]
    [Column("Id")]
    public int Id { get; set; }

    [Display(Name = "Landname")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Landname must be between 3 and 50 chars")]
    [Required(ErrorMessage = "Bitte eingeben die Landname")]
    [Column("CountryName")]
    public string CountryName { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

    [Display(Name = "Steuerprozentsatz des Landes")]
    [Column("CountryTaxPercentageValue")]
    public double CountryTaxPercentageValue { get; set; }

}

