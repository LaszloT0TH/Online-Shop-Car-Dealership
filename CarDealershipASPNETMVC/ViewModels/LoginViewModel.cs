using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace CarDealershipASPNETMVC.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Mich erinnern")]
        //[Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        // EN
        // ReturnUrl is the URL the user was trying to access before authentication.
        // We preserve and pass it between requests using ReturnUrl property, so the user
        // can be redirected to that URL upon successful authentication.
        // GE
        // ReturnUrl ist die URL, auf die der Benutzer vor der Authentifizierung zugreifen wollte.
        // Wir bewahren und übergeben es zwischen Anfragen mit der ReturnUrl-Eigenschaft, also dem Benutzer
        // kann nach erfolgreicher Authentifizierung zu dieser URL umgeleitet werden.
        // HU
        // A ReturnUrl az az URL, amelyet a felhasználó megpróbált elérni a hitelesítés előtt.
        // A ReturnUrl tulajdonság segítségével megőrizzük és a kérések között továbbítjuk,
        // így a sikeres hitelesítés után a felhasználó átirányítható erre az URL-re.
        public string? ReturnUrl { get; set; }

        // AuthenticationScheme is in Microsoft.AspNetCore.Authentication namespace
        // EN
        // ExternalLogins property stores the list of external logins(like Facebook, Google etc) that are enabled in our application.
        // DE
        // Die Eigenschaft ExternalLogins speichert die Liste der externen Logins (wie Facebook, Google usw.), die in unserer Anwendung aktiviert sind.
        // HU
        // Az ExternalLogins tulajdon az alkalmazásunkban engedélyezett külső bejelentkezések (például Facebook, Google stb.) listáját tárolja.
        public IList<AuthenticationScheme>? ExternalLogins { get; set; }
    }
}
