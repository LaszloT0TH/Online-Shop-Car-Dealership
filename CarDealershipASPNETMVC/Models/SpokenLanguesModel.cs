using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models;
public class SpokenLanguesModel : IEntityIntBase
{
    [Key]
    [Display(Name = "Sprache Id")]
    [Column("SpokenLanguesId")]
    public int Id { get; set; }

    [Display(Name = "Gesprochene Sprachen")]
    [Required(ErrorMessage = "Bitte eingeben den Gesprochene Sprachen")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Gesprochene Sprachen must be between 3 and 50 chars")]
    [Column("SpokenLanguesName")]
    public string SpokenLanguesName { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

    //Relationships
    public List<ApplicationUser_SpokenLangues> ApplicationUser_SpokenLangues { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU


}
