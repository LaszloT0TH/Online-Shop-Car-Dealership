using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models
{
    public class CarAccessoriesProductGroupModel : IEntityIntBase
    {
        [Key]
        [Display(Name = "Produktgruppe Autozubehör Id")]
        [Column("Id")]
        public int Id { get; set; }

        [Display(Name = "Produktgruppe Autozubehör")]
        [Required(ErrorMessage = "Bitte eingeben die Produktgruppe Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Produktgruppe Name muss zwischen 3 und 50 Charakter sein")]
        [Column("CAPGName")]
        public string CAPGName { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU
    }
}
