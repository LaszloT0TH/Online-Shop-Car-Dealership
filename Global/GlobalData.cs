using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Models;
using System.Configuration;

namespace CarDealershipASPNETMVC.Global
{
    /// <summary>
    /// Short-term storage of global variables
    /// Kurzzeitspeicherung globaler Variablen
    /// </summary>
    public static class GlobalData
    {
        /// <summary>
        /// allows display of shopping cart states, initial value "in shopping cart", "Saved for later", "In transit", "shipped"
        /// ermöglicht die Anzeige von Warenkorbzuständen, Ausgangswert "Im Einkaufswagen", "Für später gespeichert", "Unterwegs", "Zugestellt"
        /// </summary>
        public static string ShoppingCartStatus { get; set; }

    }
}
