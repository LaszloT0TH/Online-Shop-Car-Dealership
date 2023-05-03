using Microsoft.AspNetCore.Authorization;

namespace CarDealershipASPNETMVC.Security
{
    /// <summary>
    /// EN
    /// Create a custom authorization requirement
    /// create a class that implements the IAuthorizationRequirement interface.
    /// This is an empty marker surface, which means there is nothing on this surface,
    /// which our custom requirements class should implement.
    /// DE
    /// Erstellen Sie eine benutzerdefinierte Autorisierungsanforderung
    /// Erstellen Sie eine Klasse, die die IAuthorizationRequirement-Schnittstelle implementiert.
    /// Dies ist eine leere Markierungsfläche, was bedeutet, dass auf dieser Fläche nichts ist,
    /// die unsere benutzerdefinierte Anforderungsklasse implementieren soll.
    /// HU
    /// Egyéni engedélyezési követelmény létrehozása
    /// hozzon létre egy osztályt, amely megvalósítja az IAuthorizationRequirement felületet.
    /// Ez egy üres jelölőfelület, ami azt jelenti, hogy ezen a felületen nincs semmi,
    /// amelyet egyéni követelményosztályunknak végre kell hajtania.
    /// </summary>
    public class ManageAdminRolesAndClaimsRequirement : IAuthorizationRequirement
    { 

    }
}
