using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models
{
    //[Table("ShoppingCart")]
    public class ShoppingCartModel : IEntityIntBase
    {
        // https://learn.microsoft.com/en-us/aspnet/web-forms/overview/getting-started/getting-started-with-aspnet-45-web-forms/shopping-cart
        [Key]
        public int Id { get; set; }

        public string Email { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

        public string UserId { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

        // több 
        public List<ShoppingCartItemModel> ShoppingCartItem { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

    }

}
