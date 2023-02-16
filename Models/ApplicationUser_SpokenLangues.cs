using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.Models
{
    /// <summary>
    /// switch table between the application user table and the spoken languages table
    /// </summary>
    // Umschalttabelle zwischen der Anwendungsbenutzertabelle und der Tabelle der gesprochenen Sprachen
    public class ApplicationUser_SpokenLangues
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int SpokenLanguesId { get; set; }
        [ForeignKey("SpokenLanguesId")]
        public SpokenLanguesModel SpokenLanguesModel { get; set; }

    }
}
