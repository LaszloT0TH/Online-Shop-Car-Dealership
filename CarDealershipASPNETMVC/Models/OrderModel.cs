using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models;
public class OrderModel : IEntityIntBase
{
    [Key]
    [Display(Name = "Auftragsnummer")]
    public int Id { get; set; }

    public string Email { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

    public string UserId { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

    public List<OrderItemModel> OrderItems { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

}
