using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Models;
using System.Configuration;

namespace CarDealershipASPNETMVC.Global
{
    /// <summary>
    /// EN
    /// Short-term storage of global variables
    /// GE
    /// Kurzzeitspeicherung globaler Variablen
    /// HU
    /// Globális változók rövid távú tárolása
    /// </summary>
    public static class GlobalData
    {
        /// <summary>
        /// EN
        /// allows display of shopping cart states, initial value "in shopping cart", "Saved for later", "In transit", "shipped"
        /// GE
        /// ermöglicht die Anzeige von Warenkorbzuständen, Ausgangswert "Im Einkaufswagen", "Für später gespeichert", "Unterwegs", "Zugestellt"
        /// HU
        /// lehetővé teszi a bevásárlókosár állapotainak megjelenítését, kezdeti értéke "a bevásárlókosárban", "elmentve későbbre", "úton", "kiszállított"
        /// </summary>
        public static string ShoppingCartStatus { get; set; }

    }
}
