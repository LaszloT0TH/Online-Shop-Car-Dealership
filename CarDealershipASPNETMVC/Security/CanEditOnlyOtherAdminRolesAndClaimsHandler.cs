using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CarDealershipASPNETMVC.Security
{
    /// <summary>
    /// EN
    /// Creating a custom authorization handler 
    /// It is in the authorization handler that we write our logic to allow or deny access to a resource like a controller action for example.
    /// To implement a handler you inherit from AuthorizationHandler<T>, and implement the HandleRequirementAsync() method. 
    /// The generic parameter<T> on the AuthorizationHandler<T> is the type of requirement.
    /// GE
    /// Erstellen eines benutzerdefinierten Autorisierungs-Handlers
    /// In den Autorisierungshandler schreiben wir unsere Logik, um den Zugriff auf eine Ressource wie beispielsweise eine Controller-Aktion zuzulassen oder zu verweigern.
    /// Um einen Handler zu implementieren, erben Sie von AuthorizationHandler<T> und implementieren die Methode HandleRequirementAsync().
    /// Der generische Parameter<T> im AuthorizationHandler<T> ist der Anforderungstyp.
    /// HU
    /// Egyéni jogosultságkezelő létrehozása
    /// Az engedélyezéskezelőben írjuk meg a logikánkat,
    /// hogy engedélyezzük vagy megtagadjuk a hozzáférést egy erőforráshoz, például egy vezérlőművelethez.
    /// Az AuthorizationHandler<T>-től örökölt kezelő megvalósításához és a HandleRequirementAsync() metódus megvalósításához.
    /// Az AuthorizationHandler<T> általános paramétere a<T> a követelmény típusa.
    /// </summary>

    public class CanEditOnlyOtherAdminRolesAndClaimsHandler :
        AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ManageAdminRolesAndClaimsRequirement requirement)
        {
            // EN
            // Explanation of authorization handler code
            // The AuthorizationHandlerContext resource property returns the protected resource.
            // In our case, we use this custom requirement to protect a controller's action method.
            // So the next line returns the control action protected as AuthorizationFilterContext,
            // and provides access to HttpContext, RouteData, and everything else provided by MVC and Razor Pages.
            // GE
            // Erläuterung des Autorisierungs-Handler-Codes
            // Die Ressourceneigenschaft AuthorizationHandlerContext gibt die geschützte Ressource zurück.
            // In unserem Fall verwenden wir diese benutzerdefinierte Anforderung, um die Aktionsmethode eines Controllers zu schützen.
            // Die nächste Zeile gibt also die als AuthorizationFilterContext geschützte Kontrollaktion zurück,
            // und bietet Zugriff auf HttpContext, RouteData und alles andere, was von MVC und Razor Pages bereitgestellt wird.
            // HU
            // Az engedélyezéskezelő kódjának magyarázata
            // Az AuthorizationHandlerContext erőforrás-tulajdonsága a védett erőforrást adja vissza.
            // Esetünkben ezt az egyéni követelményt használjuk egy vezérlő műveleti módszerének védelmére.
            // Így a következő sor visszaadja a vezérlőműveletet, amely AuthorizationFilterContext néven védett,
            // és hozzáférést biztosít a HttpContext, a RouteData és minden máshoz, amelyet az MVC és a Razor Pages biztosít.
            var authFilterContext = context.Resource as AuthorizationFilterContext;

            // EN
            // If AuthorizationFilterContext is NULL, we cannot check if the requirement is met or not, so we return Task.
            // CompletedTask and the access is not authorised.
            // GE
            // Wenn AuthorizationFilterContext NULL ist, können wir nicht prüfen, ob die Anforderung erfüllt ist oder nicht, also geben wir Task zurück.
            // CompletedTask und der Zugriff ist nicht autorisiert.
            // HU
            // Ha az AuthorizationFilterContext értéke NULL, nem tudjuk ellenőrizni, hogy a követelmény teljesül-e vagy sem,
            // ezért visszaadjuk a Feladatot.CompletedTask és a hozzáférés nincs engedélyezve.
            if (authFilterContext == null)
            {
                return Task.CompletedTask;
            }

            string loggedInAdminId =
                context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            string adminIdBeingEdited = authFilterContext.HttpContext.Request.Query["userId"];

            // EN
            // Our requirement is met and the authorization succeeds 
            // If the user is in the Admin role AND has Edit Role claim type with a claim value of true
            // AND the logged -in user Id is NOT EQUAL TO the Id of the Admin user being edited
            // GE
            // Unsere Anforderung ist erfüllt und die Autorisierung ist erfolgreich
            // Wenn der Benutzer die Admin-Rolle hat UND den Anspruchstyp „Rolle bearbeiten“ mit dem Anspruchswert „true“ hat
            // UND die angemeldete Benutzer-ID ist NICHT GLEICH DER ID des bearbeiteten Admin-Benutzers
            // HU
            // A követelményünk teljesül, és az engedélyezés sikeres
            // Ha a felhasználó rendszergazdai szerepkörben van, ÉS Szerepkör szerkesztése jogcímtípussal rendelkezik,
            // amelynek jogcímértéke true ÉS a bejelentkezett felhasználói azonosító NEM EGYENLŐ a szerkesztett
            // rendszergazda felhasználó azonosítójával
            if (context.User.IsInRole("Admin") &&
                context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") &&
                adminIdBeingEdited.ToLower() != loggedInAdminId.ToLower())
            {
                // EN
                // Succeed() method specifies that the requirement is successfully evaluated.
                // GE
                // Succeed()-Methode gibt an, dass die Anforderung erfolgreich evaluiert wurde.
                // HU
                // A Succeed() metódus azt adja meg, hogy a követelmény kiértékelése sikeresen megtörtént.
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
