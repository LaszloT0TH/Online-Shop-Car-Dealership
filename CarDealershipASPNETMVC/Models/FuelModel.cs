using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models
{
    public class FuelModel : IEntityIntBase
    {
        [Key]
        [Display(Name = "Kraftstoff Id")]
        [Column("Id")]
        public int Id { get; set; }

        
        [Display(Name = "Kraftstoff Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Kraftstoff Name must be between 3 and 50 chars")]
        [Required(ErrorMessage = "Bitte eingeben die Kraftstoff Name")]
        [Column("FuelName")]
        public string FuelName { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU
    }
}
