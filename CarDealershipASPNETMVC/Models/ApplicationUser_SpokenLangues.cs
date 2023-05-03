using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models
{
    /// <summary>
    /// EN
    /// switch table between the application user table and the spoken languages table
    /// GE
    /// Umschalttabelle zwischen der Anwendungsbenutzertabelle und der Tabelle der gesprochenen Sprachen
    /// HU
    /// táblázat váltása az alkalmazás felhasználói táblája és a beszélt nyelvek táblája között
    /// </summary>
    public class ApplicationUser_SpokenLangues
    {
        public string UserId { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public int SpokenLanguesId { get; set; }
        [ForeignKey("SpokenLanguesId")]
        public SpokenLanguesModel SpokenLanguesModel { get; set; } = null!;

    }
}
