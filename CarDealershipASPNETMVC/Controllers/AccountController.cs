using CarDealershipASPNETMVC.Data.Service;
using CarDealershipASPNETMVC.Models;
using CarDealershipASPNETMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Mail;
using System.Security.Claims;

namespace CarDealershipASPNETMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService service;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(IAccountService service,
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ILogger<AccountController> logger
                                )
        {
            this.service = service;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> AddPassword()
        {
            var user = await userManager.GetUserAsync(User);

            var userHasPassword = await userManager.HasPasswordAsync(user);

            if (userHasPassword)
            {
                return RedirectToAction("ChangePassword");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPassword(AddPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);

                var result = await userManager.AddPasswordAsync(user, model.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                await signInManager.RefreshSignInAsync(user);

                return View("AddPasswordConfirmation");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await userManager.GetUserAsync(User);

            var userHasPassword = await userManager.HasPasswordAsync(user);

            if (!userHasPassword)
            {
                return RedirectToAction("AddPassword");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                // EN
                // ChangePasswordAsync changes the user password
                // GE
                // ChangePasswordAsync ändert das Benutzerkennwort
                // HU
                // A ChangePasswordAsync megváltoztatja a felhasználói jelszót
                var result = await userManager.ChangePasswordAsync(user,
                    model.CurrentPassword, model.NewPassword);

                // EN
                // The new password did not meet the complexity rules or
                // the current password is incorrect. Add these errors to
                // the ModelState and rerender ChangePassword view
                // GE
                // Das neue Passwort entsprach nicht den Komplexitätsregeln oder
                // Das aktuelle Passwort ist falsch. Fügen Sie diese Fehler hinzu
                // den ModelState und die ChangePassword-Ansicht neu rendern
                // HU
                // Az új jelszó nem felelt meg a bonyolultsági szabályoknak ill
                // a jelenlegi jelszó helytelen. Adja hozzá ezeket a hibákat
                // a ModelState és a ChangePassword nézet újrarenderelése
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                // EN
                // Upon successfully changing the password refresh sign-in cookie
                // GE
                // Nach erfolgreicher Änderung des Anmelde-Cookies zur Kennwortaktualisierung
                // HU
                // A jelszó sikeres megváltoztatása után frissítse a bejelentkezési cookie-t
                await signInManager.RefreshSignInAsync(user);
                return View("ChangePasswordConfirmation");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var country_sexDropdownsData = await service.GetAccountDropdownsValues();

            ViewBag.Country = new SelectList(country_sexDropdownsData.Countries, "Id", "CountryName");
            ViewBag.Sex = new SelectList(country_sexDropdownsData.Sexs, "Id", "SexName");

            return View();
        }

        /// <summary>
        ///  EN
        /// This method should respond to both HTTP GET and POST. This is the reason we specified both
        /// the HTTP verbs(Get and Post) using [AcceptVerbs] attribute.
        /// ASP.NET Core MVC uses jQuery remote() method which in turn issues an AJAX call to invoke the server side method.
        /// The jQuery remote() method expects a JSON response, this is the reason we are returning JSON response from
        /// the server-side method(IsEmailInUse)
        ///  EN 
        /// Diese Methode sollte sowohl auf HTTP GET als auch auf POST reagieren. Aus diesem Grund haben wir beide angegeben
        /// die HTTP-Verben (Get und Post) mit dem [AcceptVerbs]-Attribut.
        /// ASP.NET Core MVC verwendet die Methode jQuery remote(), die wiederum einen AJAX-Aufruf zum Aufrufen der serverseitigen Methode ausgibt.
        /// Die Methode jQuery remote() erwartet eine JSON-Antwort, aus diesem Grund geben wir die JSON-Antwort zurück
        /// die serverseitige Methode (IsEmailInUse)
        ///  HU
        /// Ennek a módszernek a HTTP GET-re és a POST-ra is válaszolnia kell. Ez az oka annak, hogy mindkettőt megadtuk
        /// a HTTP igék (Get and Post) az [AcceptVerbs] attribútum használatával.
        /// Az ASP.NET Core MVC a jQuery remote() metódust használja, amely viszont AJAX hívást ad ki a szerveroldali metódus meghívására.
        /// A jQuery remote() metódus JSON választ vár, ezért adunk vissza JSON választ
        /// a szerveroldali metódus (IsEmailInUse)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use.");
            }
        }


        [HttpPost] 
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // EN
                // Copy data from RegisterViewModel to IdentityUser
                // GE
                // Daten von RegisterViewModel nach IdentityUser kopieren
                // HU
                // Adatok másolása a RegisterViewModelből az IdentityUserbe
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    SexId = model.SexId,
                    Street = model.Street,
                    HouseNumber = model.HouseNumber,
                    PostalCode = model.PostalCode,
                    Location = model.Location,
                    CountryId = model.CountryId
                };

                // EN
                // Store user data in AspNetUsers database table
                // GE
                // Benutzerdaten in der AspNetUsers-Datenbanktabelle speichern
                // HU
                // A felhasználói adatok tárolása az AspNetUsers adatbázistáblában
                var result = await userManager.CreateAsync(user, model.Password);
                
                // EN
                // If user is successfully created, sign-in the user using
                // SignInManager and redirect to index action of HomeController
                // GE
                // Wenn der Benutzer erfolgreich erstellt wurde, melden Sie den Benutzer mit an
                // SignInManager und Umleitung zur Indexaktion von HomeController
                // HU
                // Ha a felhasználó létrehozása sikeres volt, jelentkezzen be a felhasználóval a következővel
                // SignInManager és átirányítás a HomeController indexműveletére
                if (result.Succeeded)
                {
                    // EN
                    // In ASP.NET core generating email confirmation token is straight forward.
                    // Use UserManager service GenerateEmailConfirmationTokenAsync() method.
                    // This method takes one parameter. The user for whom we want to generate the email confirmation token.
                    // In ASP.NET Core ist das Generieren eines E-Mail-Bestätigungstokens einfach.
                    // GE
                    // GenerateEmailConfirmationTokenAsync()-Methode des UserManager-Dienstes verwenden.
                    // Diese Methode benötigt einen Parameter. Der Benutzer, für den wir das E-Mail-Bestätigungstoken generieren möchten.
                    // HU
                    // ASP.NET e-mail megerősítő jogkivonat létrehozása egyszerű.
                    // Használja a UserManager szolgáltatás GenerateEmailConfirmationTokenAsync() metódusát.
                    // Ez a módszer egy paramétert vesz igénybe. Az a felhasználó, akinek létre szeretnénk
                    // hozni az e-mailes megerősítő jogkivonatot.
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    // EN
                    // Once we have the token generated, build the email confirmation link.
                    // The user simply clicks this link to confirm his email. This link executes,
                    // ConfirmEmail action in Account controller. The user ID and the email confirmation
                    // token are passed in the query string. Model binding in ASP.NET core maps the values
                    // from the query string parameters to the respective parameters on the ConfirmEmail action.
                    // GE
                    // Sobald wir das Token generiert haben, erstellen Sie den E-Mail-Bestätigungslink.
                    // Der Benutzer klickt einfach auf diesen Link, um seine E-Mail zu bestätigen. Dieser Link führt aus,
                    // ConfirmEmail-Aktion im Account-Controller. Die Benutzer-ID und die E-Mail-Bestätigung
                    // Token werden in der Abfragezeichenfolge übergeben. Die Modellbindung in ASP.NET Core ordnet die Werte zu
                    // von den Parametern der Abfragezeichenfolge zu den entsprechenden Parametern der Aktion ConfirmEmail.
                    // HU
                    // A felhasználó egyszerűen rákattint erre a linkre az e - mail címének megerősítéséhez.
                    // Ez a hivatkozás végrehajtja a ConfirmEmail műveletet a Fiókkezelőben.A felhasználói
                    // azonosító és az e-mail - megerősítő jogkivonat a lekérdezési sztringben lesz átadva.
                    // Az ASP.NET magban lévő modellkötés leképezi a lekérdezési sztring paramétereinek
                    // értékeit a ConfirmEmail művelet megfelelő paramétereire.
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                    // EN
                    // log the confirmation link
                    // GE
                    // den Bestätigungslink protokollieren
                    // HU
                    // naplózzuk a megerősítő hivatkozást
                    logger.Log(LogLevel.Warning, confirmationLink);

                    // EN
                    // If the user is signed in and in the Admin role, then it is
                    // the Admin user that is creating a new user. So redirect the
                    // Admin user to ListRoles action
                    // GE
                    // Wenn der Benutzer angemeldet ist und die Admin-Rolle hat, dann ist er es
                    // der Admin-Benutzer, der einen neuen Benutzer erstellt. Also umleiten
                    // Admin-Benutzer für ListRoles-Aktion
                    // HU
                    // Ha a felhasználó be van jelentkezve és rendszergazdai szerepkörben van, akkor az
                    // az adminisztrátori felhasználó, aki új felhasználót hoz létre. Tehát irányítsd át a
                    // Adminisztrátor felhasználó a ListRoles művelethez
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }

                    //await signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("Index", "Home");

                    // EN
                    //ViewBag.ErrorTitle = "Registration successful";
                    ViewBag.ErrorTitle = "Registrierung erfolgreich";
                    // EN
                    //ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
                    //        "email, by clicking on the confirmation link we have emailed you";
                    ViewBag.ErrorMessage = "Bevor Sie sich einloggen können, bestätigen Sie bitte Ihr " +
                             "E-Mail, indem Sie auf den Bestätigungslink klicken, den wir Ihnen per E-Mail zugesendet haben";
                    return View("Error");
                }

                // EN
                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                // GE
                // Falls Fehler vorhanden sind, fügen Sie diese dem ModelState-Objekt hinzu
                // die vom Tag-Helfer für die Validierungszusammenfassung angezeigt wird
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (returnUrl == "/")
                returnUrl = null;

            // EN
            //Let's say RequireConfirmedEmail property is set to true and the email address is
            //not confirmed yet. If we now use the SignInManager service PasswordSignInAsync() method
            //to sign-in the user we get NotAllowed as the result, even if we supply the correct
            //username and password.
            //The same is true with ExternalLoginSignInAsync() method of SignInManager service.
            //We use ExternalLoginSignInAsync() method to sign -in the user using an external login
            //provider like Facebook, Google etc.If the email address associated with external login
            //account is not confirmed, signin result will be NotAllowed.
            //The following Login action in AccountController, blocks the login and
            //displays Email not confirmed yet error. 
            // GE
            //Nehmen wir an, die Eigenschaft RequireConfirmedEmail ist auf true gesetzt und die E-Mail-Adresse ist
            //noch nicht bestätigt. Verwenden wir nun die PasswordSignInAsync()-Methode des SignInManager-Dienstes
            //Um den Benutzer anzumelden, erhalten wir NotAllowed als Ergebnis, auch wenn wir das Richtige angeben
            //Benutzername und Passwort.
            //Das Gleiche gilt für die Methode ExternalLoginSignInAsync() des SignInManager-Dienstes.
            // Wir verwenden die Methode ExternalLoginSignInAsync(), um den Benutzer mit einem externen Login anzumelden
            //Anbieter wie Facebook, Google etc.Wenn die E-Mail-Adresse mit externem Login verknüpft ist
            //Konto ist nicht bestätigt, Anmeldeergebnis ist NotAllowed.
            //Die folgende Anmeldeaktion in AccountController blockiert die Anmeldung und
            // zeigt E-Mail noch nicht bestätigten Fehler an.
            // HU
            // Tegyük fel, hogy a RequireConfirmedEmail tulajdonság true értékre van állítva,
            // és az e-mail cím még nincs megerősítve. Ha most a SignInManager szolgáltatás
            // PasswordSignInAsync() metódusát használjuk a felhasználó bejelentkezéséhez,
            // akkor a NotAllowed eredményt kapjuk, még akkor is, ha a megfelelő felhasználónevet
            // és jelszót adjuk meg.
            // Ugyanez a helyzet a SignInManager szolgáltatás ExternalLoginSignInAsync() metódusával.
            // Az ExternalLoginSignInAsync() metódust használjuk a felhasználó bejelentkezéséhez
            // egy külső bejelentkezési szolgáltató, például Facebook, Google stb.használatával.
            // Ha a külső bejelentkezési fiókhoz tartozó e - mail - címet nem erősítik meg,
            // a bejelentkezés eredménye NotAllowed lesz.
            // A következő bejelentkezési művelet az AccountControllerben blokkolja a bejelentkezést,
            // és az E - mail még meg nem erősített hibaüzenetet jeleníti meg.
            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            // EN
            //Please note : With the external login the user does not provide username and password to
            //our application.Upon successful authentication, the user is redirected to the ExternalLoginCallback()
            //action in our application. So we know the user is already authenticated and hence we display
            //the validation error -Email not confirmed, without the need to check if the provided username
            //and password combination is correct.
            // GE
            //Bitte beachten Sie: Bei der externen Anmeldung gibt der Benutzer keinen Benutzernamen und kein Passwort an
            //unsere Anwendung. Bei erfolgreicher Authentifizierung wird der Benutzer zum ExternalLoginCallback() umgeleitet.
            //Aktion in unserer Anwendung. Wir wissen also, dass der Benutzer bereits authentifiziert ist, und zeigen ihn daher an
            //der Validierungsfehler -E-Mail nicht bestätigt, ohne dass überprüft werden muss, ob der angegebene Benutzername vorhanden ist
            //und Passwort-Kombination ist korrekt.
            // HU
            //Megjegyzés: A külső bejelentkezéskor a felhasználó nem ad felhasználónevet és jelszót az alkalmazásunkhoz.
            //Sikeres hitelesítés esetén a felhasználót az alkalmazásunkban az ExternalLoginCallback() művelethez irányítjuk.
            //Tehát tudjuk, hogy a felhasználó már hitelesített, és ezért megjelenítjük
            //az érvényesítési hibát - Az e-mail nincs megerősítve, anélkül, hogy ellenőrizni kellene,
            //hogy a megadott felhasználónév és jelszó kombináció helyes-e.

            if (ModelState.IsValid)
            {
                //var result = await signInManager.PasswordSignInAsync(
                //    model.Email, model.Password, model.RememberMe, false);
                var user = await userManager.FindByEmailAsync(model.Email);

                // EN
                // This error message is displayed only, if the Email is not confirmed
                // AND the user has provided correct username and password. 
                // If you are wondering, why do we need to check if the user has provided correct username and password.Well,
                // this is to avoid account enumeration and brute force attacks. 
                //Let's understand, what might happen if we display this validation message - Email not confirmed yet,
                //without checking if the provided email address and password combination is correct.
                //An attacker might try random emails and as soon as he sees the message,
                //Email not confirmed yet, he knows this is a valid email that could be used to login.
                //He waits for couple of days until the email is confirmed by the actual owner,
                //and can then try random passwords with that email address to gain access. 
                //In order to avoid these types of account enumeration and brute force attacks,
                //display the validation error, only upon providing the correct email address and password combination.
                //We also want to block the login if an external login account(like Facebook, Google etc)
                //is used and the email address associated with that external account is not confirmed.
                //The section that blocks the login is commented.
                // GE
                // Diese Fehlermeldung wird nur angezeigt, wenn die E-Mail nicht bestätigt wird
                // UND der Benutzer hat den richtigen Benutzernamen und das richtige Passwort angegeben.
                // Wenn Sie sich fragen, warum müssen wir überprüfen, ob der Benutzer den richtigen Benutzernamen und das richtige Passwort angegeben hat?Nun,
                // Dies dient der Vermeidung von Kontoaufzählungen und Brute-Force-Angriffen.
                //Lassen Sie uns verstehen, was passieren könnte, wenn wir diese Validierungsnachricht anzeigen - E-Mail noch nicht bestätigt,
                //ohne zu prüfen, ob die angegebene Kombination aus E-Mail-Adresse und Passwort korrekt ist.
                //Ein Angreifer könnte zufällige E-Mails versuchen und sobald er die Nachricht sieht,
                //E-Mail noch nicht bestätigt, er weiß, dass dies eine gültige E-Mail ist, die zum Anmelden verwendet werden könnte.
                //Er wartet ein paar Tage, bis die E-Mail vom tatsächlichen Besitzer bestätigt wird,
                //und kann dann zufällige Passwörter mit dieser E-Mail-Adresse ausprobieren, um Zugriff zu erhalten.
                //Um diese Art der Kontoaufzählung und Brute-Force-Angriffe zu vermeiden,
                //den Validierungsfehler anzeigen, nur wenn die richtige Kombination aus E-Mail-Adresse und Passwort angegeben wird.
                //Wir möchten die Anmeldung auch blockieren, wenn ein externes Anmeldekonto (wie Facebook, Google usw.)
                //wird verwendet und die diesem externen Konto zugeordnete E-Mail-Adresse wird nicht bestätigt.
                //Der Abschnitt, der die Anmeldung blockiert, ist auskommentiert.
                // HU
                //Ez a hibaüzenet csak akkor jelenik meg, ha az e - mail nincs megerősítve
                //ÉS a felhasználó helyes felhasználónevet és jelszót adott meg.
                //Ha kíváncsi, miért kell ellenőriznünk, hogy a felhasználó helyes felhasználónevet
                //és jelszót adott - e meg.Nos, ez azért van, hogy elkerüljük a számlaszámlálást és a brutális erőszakos támadásokat.
                //Értsük meg, mi történhet, ha ezt az érvényesítési üzenetet - Az e - mail még nem erősítette meg,
                //anélkül, hogy ellenőriznénk, hogy a megadott e-mail cím és jelszó kombináció helyes - e.
                //A támadó véletlenszerű e - maileket próbálhat ki, és amint meglátja az E-mail még meg nem erősített üzenetet,
                //tudja, hogy ez egy érvényes e - mail, amellyel bejelentkezhet. Vár néhány napot,
                //amíg az e - mailt a tényleges tulajdonos megerősíti, majd véletlenszerű jelszavakat próbálhat ki
                //ezzel az e - mail címmel, hogy hozzáférjen.
                //Az ilyen típusú fiókfelsorolások és a brutális erőszakos támadások elkerülése érdekében
                //csak a megfelelő e - mail cím és jelszó kombináció megadása esetén jelenítse meg az érvényesítési hibát.
                //Azt is szeretnénk blokkolni a bejelentkezést, ha külső bejelentkezési fiókot(például Facebook, Google stb.) használunk,
                //és az ehhez a külső fiókhoz társított e-mail címet nem erősítik meg. A bejelentkezést blokkoló szakasz megjegyzést kap.
                if (user != null && !user.EmailConfirmed && (await userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View(model);
                }

                // EN
                // The last boolean parameter lockoutOnFailure indicates if the account
                // should be locked on failed logon attempt. On every failed logon
                // attempt AccessFailedCount column value in AspNetUsers table is
                // incremented by 1. When the AccessFailedCount reaches the configured
                // MaxFailedAccessAttempts which in our case is 5, the account will be
                // locked and LockoutEnd column is populated. After the account is
                // lockedout, even if we provide the correct username and password,
                // PasswordSignInAsync() method returns Lockedout result and the login
                // will not be allowed for the duration the account is locked.
                // GE
                // Der letzte boolesche Parameter lockoutOnFailure gibt an, ob das Konto
                // sollte bei fehlgeschlagenem Anmeldeversuch gesperrt werden. Bei jeder fehlgeschlagenen Anmeldung
                // Versuch AccessFailedCount-Spaltenwert in AspNetUsers-Tabelle ist
                // um 1 erhöht. Wenn der AccessFailedCount den konfigurierten Wert erreicht
                // MaxFailedAccessAttempts, was in unserem Fall 5 ist, wird das Konto sein
                // gesperrt und die LockoutEnd-Spalte wird ausgefüllt. Nachdem das Konto ist
                // gesperrt, auch wenn wir den richtigen Benutzernamen und das richtige Passwort angeben,
                // Die Methode PasswordSignInAsync() gibt das Lockedout-Ergebnis und die Anmeldung zurück
                // ist nicht erlaubt, solange das Konto gesperrt ist.
                // HU
                // Az utolsó logikai paraméter lockoutOnFailure jelzi, hogy a fiók
                //Kell zárolva a sikertelen bejelentkezési kísérlet esetén.Minden sikertelen bejelentkezéskor
                //kísérlet Az AspNetUsers tábla AccessFailedCount oszlopának értéke a következő:
                //növekményezve: 1.Amikor az AccessFailedCount eléri a konfigurált értéket
                // MaxFailedAccessAttempts amely esetünkben 5, a fiók lesz
                //zárolva és A LockoutEnd oszlop ki van töltve.A fiók után
                //zárva, még akkor is, ha helyes felhasználónevet és jelszót adunk meg,
                // A PasswordSignInAsync() metódus a Lockedout eredményt és a bejelentkezést adja vissza
                //nem lesz A fiók zárolásának időtartamára engedélyezett.
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }

                // EN
                // If account is lockedout send the use to AccountLocked view
                // GE
                // Wenn das Konto gesperrt ist, senden Sie die Verwendung an die AccountLocked-Ansicht
                // HU
                // Ha a fiók zárolva van, küldje el a felhasználást az AccountLocked nézetbe
                if (result.IsLockedOut)
                {
                    // EN
                    //return View("AccountLocked");
                    return View("Konto gesperrt");
                }
                // EN
                //ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                ModelState.AddModelError(string.Empty, "Ungültiger Anmeldeversuch");
            }

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                // EN
                //ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                ViewBag.ErrorMessage = $"Die Benutzer-ID {userId} ist ungültig";
                return View("NotFound");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return View();
            }

            // EN
            //ViewBag.ErrorTitle = "Email cannot be confirmed";
            ViewBag.ErrorTitle = "E-Mail kann nicht bestätigt werden";
            return View("Error");
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                                new { ReturnUrl = returnUrl });
            var properties = signInManager
                .ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult>ExternalLoginCallback(string returnUrl = null!, string remoteError = null!)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins =
                        (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", loginViewModel);
            }

            // EN
            // Get the login information about the user from the external login provider
            // GE
            // Anmeldeinformationen über den Benutzer vom externen Anmeldeanbieter abrufen
            // HU
            // Szerezze be a felhasználó bejelentkezési adatait a külső bejelentkezési szolgáltatótól
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", loginViewModel);
            }

            // EN
            // Get the email claim value
            // Get the email claim from external login provider (Google, Facebook etc)
            // DE
            // Wert des E-Mail-Anspruchs abrufen
            // Holen Sie sich den E-Mail-Anspruch von einem externen Anmeldeanbieter (Google, Facebook usw.)
            // HU
            // Az e-mail követelés értékének lekérése
            // Kérje le az e-mail-igénylést a külső bejelentkezési szolgáltatótól (Google, Facebook stb.)
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            ApplicationUser user = null;
            if (email != null)
            {
                // EN
                // Find the user
                // DE
                // Finde den Benutzer
                // HU
                // Keresse meg a felhasználót
                user = await userManager.FindByEmailAsync(email);

                // EN
                // If email is not confirmed, display login view with validation error
                // DE
                // Wenn E-Mail nicht bestätigt wird, Anmeldeansicht mit Validierungsfehler anzeigen
                // HU
                // Ha az e-mailt nem erősítik meg, jelenítse meg a bejelentkezési nézetet érvényesítési hibával
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View("Login", loginViewModel);
                }
            }

            // EN
            // If the user already has a login (i.e if there is a record in AspNetUserLogins
            // table) then sign-in the user with this external login provider
            // GE
            // Wenn der Benutzer bereits ein Login hat (d. h. wenn es einen Datensatz in AspNetUserLogins
            // table) und melden Sie den Benutzer dann mit diesem externen Anmeldeanbieter an
            // HU
            // Ha a felhasználó már rendelkezik bejelentkezési névvel (azaz ha van rekord az AspNetUserLogins-ban
            // táblázat), majd jelentkezzen be a felhasználóba ezzel a külső bejelentkezési szolgáltatóval
            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            // EN
            // If there is no record in AspNetUserLogins table, the user may not have
            // a local account
            // GE
            // Wenn es keinen Datensatz in der AspNetUserLogins-Tabelle gibt, hat der Benutzer möglicherweise keinen
            // ein lokales Konto
            // HU
            // Ha nincs rekord az AspNetUserLogins táblában, akkor előfordulhat, hogy a felhasználó nem rendelkezik
            // helyi fiók
            else
            {
                if (email != null)
                {
                    // EN
                    // Create a new user without password if we do not have a user already
                    // GE
                    // Erstellen Sie einen neuen Benutzer ohne Passwort, wenn wir noch keinen Benutzer haben
                    // HU
                    // Hozzon létre egy új felhasználót jelszó nélkül, ha még nincs felhasználónk
                    user = await userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };

                        await userManager.CreateAsync(user);

                        // EN
                        // After a local user account is created, generate and log the
                        // email confirmation link
                        // GE
                        // Nachdem ein lokales Benutzerkonto erstellt wurde, generieren und protokollieren Sie die
                        // E-Mail-Bestätigungslink
                        // HU
                        // A helyi felhasználói fiók létrehozása után generálja és naplózza a
                        // email megerősítő link
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                        var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                        new { userId = user.Id, token = token }, Request.Scheme);

                        logger.Log(LogLevel.Warning, confirmationLink);

                        // EN
                        ViewBag.ErrorTitle = "Registration successful";
                        ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
                            "email, by clicking on the confirmation link we have emailed you";

                        // GE
                        ViewBag.ErrorTitle = "Registrierung erfolgreich";
                        ViewBag.ErrorMessage = "Bevor Sie sich einloggen können, bestätigen Sie bitte Ihr " +
                             "E-Mail, indem Sie auf den Bestätigungslink klicken, den wir Ihnen per E-Mail zugesendet haben";
                        return View("Error");
                    }

                    // EN
                    // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                    // GE
                    // Ein Login hinzufügen (d. h. eine Zeile für den Benutzer in die AspNetUserLogins-Tabelle einfügen)
                    // HU
                    // Bejelentkezés hozzáadása (azaz beszúrjon egy sort a felhasználó számára az AspNetUserLogins táblába)
                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                // EN
                // If we cannot find the user email we cannot continue
                //ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                //ViewBag.ErrorMessage = "Please contact support on support@support.com";                
                // GE
                // Wenn wir die Benutzer-E-Mail nicht finden können, können wir nicht fortfahren
                ViewBag.ErrorTitle = $"E-Mail-Anspruch nicht erhalten von: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Bitte kontaktieren Sie den Support unter support@support.com";
                // HU
                // Ha nem találjuk a felhasználói e-mailt, nem tudjuk folytatni
                ViewBag.ErrorTitle = $"E-mail követelés nem érkezett tőle: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Kérjük, lépjen kapcsolatba az ügyfélszolgálattal a support@support.com címen";

                return View("Error");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // EN
                // Find the user by email
                // GE
                // Suchen Sie den Benutzer per E-Mail
                // HU
                // Keresse meg a felhasználót e-mailben
                var user = await userManager.FindByEmailAsync(model.Email);
                // EN
                // If the user is found AND Email is confirmed
                // GE
                // Wenn der Benutzer gefunden wird UND die E-Mail bestätigt wird
                // HU
                // Ha a felhasználó megtalálható ÉS az e-mail megerősítésre kerül
                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    // EN
                    // Generate the reset password token
                    // GE
                    // Das Token zum Zurücksetzen des Passworts generieren
                    // HU
                    // A jelszó visszaállítási tokent generálja
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    // EN
                    // Build the password reset link
                    // GE
                    // Erstellen Sie den Link zum Zurücksetzen des Passworts
                    // HU
                    // A jelszó-visszaállítási hivatkozás létrehozása
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                            new { email = model.Email, token = token }, Request.Scheme);

                    await SendEmail(model.Email, "Bestätigung des Passworts vergessen", passwordResetLink);

                    // EN
                    // Log the password reset link
                    // GE
                    // Link zum Zurücksetzen des Passworts protokollieren
                    // HU
                    // Jelentkezzen be a jelszó-visszaállítási hivatkozásra
                    logger.Log(LogLevel.Warning, passwordResetLink);

                    // EN
                    // Send the user to Forgot Password Confirmation view
                    // GE
                    // Den Benutzer zur Ansicht „Passwort vergessen“ schicken
                    // HU
                    // Elküldi a felhasználót az Elfelejtett jelszó megerősítése nézetbe
                    return View("ForgotPasswordConfirmation");
                }

                // EN
                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist or is not confirmed
                // GE
                // Um Kontoaufzählung und Brute-Force-Angriffe zu vermeiden, tun Sie es nicht
                // enthüllen, dass der Benutzer nicht existiert oder nicht bestätigt ist
                // HU
                // A fiókfelsorolás és a brutális erők elleni támadások elkerülése érdekében ne tegye
                // felfedi, hogy a felhasználó nem létezik, vagy nincs megerősítve
                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }
        private async Task SendEmail(string toEmailAddress, string emailSubject, string emailMessage)
        {
            var message = new MailMessage();
            message.From = new MailAddress("support@mycompany.com");
            message.To.Add(toEmailAddress);
            message.CC.Add(new MailAddress("MyEmailID@gmail.com"));

            message.Subject = emailSubject;
            message.Body = emailMessage;

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(message);
            }
        }
        #region Mailing step by step
        //private static void SendEmail(string emailbody, string to_email)
        //{
        //    // Specify the from and to email address
        //    //MailMessage mailMessage = new MailMessage
        //    //    ("from_email@gmail.com", "to_email@gmail.com"); 
        //    MailMessage mailMessage = new MailMessage
        //        ("from_email@gmail.com", to_email);
        //    // Specify the email body
        //    mailMessage.Body = emailbody;
        //    // Specify the email Subject
        //    mailMessage.Subject = "Bestätigung des Passworts vergessen";

        //    // No need to specify the SMTP settings as these 
        //    // are already specified in appsettings.json
        //    SmtpClient smtpClient = new SmtpClient();
        //    // Finall send the email message using Send() method
        //    if (true)
        //    {
        //        smtpClient.Send(mailMessage);
        //    }

        //    //without appsettings.json settings
        //    //// Specify the SMTP server name and post number
        //    //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        //    //// Specify your gmail address and password
        //    //smtpClient.Credentials = new System.Net.NetworkCredential()
        //    //{
        //    //    UserName = "from_email@gmail.com",
        //    //    Password = "your_password"
        //    //};
        //    //// Gmail works on SSL, so set this property to true
        //    //smtpClient.EnableSsl = true;
        //    //// Finall send the email message using Send() method
        //    //smtpClient.Send(mailMessage);


        //    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        //    //SmtpClient smtpClient = new SmtpClient("mail.MyWebsiteDomainName.com", 25);

        //    //smtpClient.Credentials = new System.Net.NetworkCredential("info@MyWebsiteDomainName.com", "myIDPassword");
        //    //// smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
        //    //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    //smtpClient.EnableSsl = true;
        //    //MailMessage mail = new MailMessage();

        //    ////Setting From , To and CC
        //    //mail.From = new MailAddress("info@MyWebsiteDomainName", "MyWeb Site");
        //    //mail.To.Add(new MailAddress("info@MyWebsiteDomainName"));
        //    //mail.CC.Add(new MailAddress("MyEmailID@gmail.com"));

        //    //smtpClient.Send(mail);
        //}
        #endregion

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            // EN
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            // GE
            // Wenn das Token oder die E-Mail-Adresse zum Zurücksetzen des Passworts null ist, ist höchstwahrscheinlich die
            // Benutzer hat versucht, den Link zum Zurücksetzen des Passworts zu manipulieren
            // HU
            // Ha a jelszó-visszaállítási token vagy e-mail értéke null, akkor valószínűleg a
            // a felhasználó megpróbálta megváltoztatni a jelszó-visszaállítási hivatkozást
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // EN
                    // reset the user password
                    // GE
                    // Benutzerpasswort zurücksetzen
                    // HU
                    // állítsa vissza a felhasználói jelszót
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        // EN
                        // Upon successful password reset and if the account is lockedout, set
                        // the account lockout end date to current UTC date time, so the user
                        // can login with the new password
                        // GE
                        // Nach erfolgreichem Zurücksetzen des Passworts und wenn das Konto gesperrt ist, setzen
                        // das Enddatum der Kontosperrung zur aktuellen UTC-Datumszeit, also der Benutzer
                        // kann sich mit dem neuen Passwort anmelden
                        // HU
                        // Sikeres jelszó-visszaállítás után, és ha a fiók zárolva van, állítsa be
                        // a fiókzárolás befejezési dátuma az aktuális UTC dátumig, így a felhasználó
                        // tud bejelentkezni az új jelszóval
                        if (await userManager.IsLockedOutAsync(user))
                        {
                            await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                        }

                        return View("ResetPasswordConfirmation");
                    }
                    // EN
                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    // GE
                    // Validierungsfehler anzeigen. Zum Beispiel bereits ein Token zum Zurücksetzen des Passworts
                    // Wird verwendet, um das Passwort zu ändern, oder die Regeln für die Passwortkomplexität wurden nicht erfüllt
                    // HU
                    // Érvényesítési hibák megjelenítése. Például már jelszó-visszaállítási token
                    // a jelszó megváltoztatására használt, vagy a jelszó összetettségi szabályai nem teljesülnek
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                // EN
                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                // GE
                // Um Kontoaufzählung und Brute-Force-Angriffe zu vermeiden, tun Sie es nicht
                // enthüllen, dass der Benutzer nicht existiert                
                // HU
                // A fiókfelsorolás és a brutális erők elleni támadások elkerülése érdekében ne tegye
                // felfedi, hogy a felhasználó nem létezik
                return View("ResetPasswordConfirmation");
            }

            // EN
            // Display validation errors if model state is not valid
            // GE
            // Validierungsfehler anzeigen, wenn der Modellstatus nicht gültig ist
            // HU
            // Az érvényesítési hibák megjelenítése, ha a modellállapot nem érvényes
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
