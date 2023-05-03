using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models
{    
    public class GearboxModel : IEntityIntBase
    {
        [Key]
        [Display(Name = "Getriebe Id")]
        [Column("Id")]
        public int Id { get; set; }

        [Display(Name = "Getriebename")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Getriebe Name must be between 3 and 50 chars")]
        [Required(ErrorMessage = "Bitte eingeben die Getriebe Name")]
        [Column("GearboxName")]
        public string GearboxName { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU
    }
}
