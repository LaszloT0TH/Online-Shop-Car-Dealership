using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models
{
    public class ShoppingCartStatusModel : IEntityIntBase
    {
        [Key]
        [Display(Name = "Status Id des Einkaufswagens")]
        [Column("Id")]
        public int Id { get; set; }

        [Display(Name = "Statusname des Einkaufswagens")]
        [Required(ErrorMessage = "Bitte eingeben den Status des Einkaufswagens Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Status des Einkaufswagens Name muss zwischen 3 und 50 Charakter sein")]
        [Column("ShoppingCartStatusName")]
        public string ShoppingCartStatusName { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

    }
}
