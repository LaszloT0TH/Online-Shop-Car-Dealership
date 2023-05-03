using CarDealershipASPNETMVC.Data.Service;
using CarDealershipASPNETMVC.Models;
using CarDealershipASPNETMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarDealershipASPNETMVC.Controllers
{
    /// <summary>
    /// EN
    /// To create a user in asp.net core we use UserManager class. Similarly, to create a role, we use RoleManager class provided by asp.net core
    /// The built-in IdentityRole class represents a Role RoleManager class performs all the CRUD operations i.e Creating, Reading, Updating 
    /// and Deleting roles from the underlying database table AspNetRoles
    /// To tell the RoleManager class to work with IdentityRole class, we specify IdentityRole class as the generic argument to RoleManager
    /// RoleManager is made available to any controller or view by asp.net core dependency injection system
    /// DE
    /// Um einen Benutzer im asp.net-Kern zu erstellen, verwenden wir die UserManager-Klasse. In ähnlicher Weise verwenden wir zum Erstellen einer Rolle die RoleManager-Klasse, die vom asp.net-Kern bereitgestellt wird
    /// Die eingebaute IdentityRole-Klasse stellt eine Role RoleManager-Klasse dar, die alle CRUD-Vorgänge durchführt, d. h. Erstellen, Lesen, Aktualisieren
    /// und Löschen von Rollen aus der zugrunde liegenden Datenbanktabelle AspNetRoles
    /// Um der RoleManager-Klasse mitzuteilen, dass sie mit der IdentityRole-Klasse arbeiten soll, geben wir die IdentityRole-Klasse als generisches Argument für RoleManager an
    /// RoleManager wird jedem Controller oder View durch das asp.net Core Dependency Injection System zur Verfügung gestellt
    /// HU
    /// Felhasználó létrehozásához az asp.net magban a UserManager osztályt használjuk. Hasonlóképpen egy szerep létrehozásához az asp.net core által biztosított RoleManager osztályt használjuk
    /// A beépített IdentityRole osztály egy Role RoleManager osztályt képvisel, amely végrehajtja az összes CRUD műveletet, azaz a létrehozást, az olvasást, a frissítést
    /// és szerepek törlése az alapul szolgáló adatbázis-táblából AspNetRoles
    /// Ahhoz, hogy a RoleManager osztály működjön együtt az IdentityRole osztállyal, az IdentityRole osztályt adjuk meg a RoleManager általános argumentumaként
    /// A RoleManager elérhetővé válik bármely vezérlő vagy nézet számára az asp.net alapvető függőségi befecskendezési rendszerén keresztül
    /// </summary>

    // EN
    //If you want to create a user in asp.net core, we use the UserManager class. Similarly, to create a role, we use the RoleManager class provided by asp.net core
    //The built-in IdentityRole class represents a role
    //RoleManager class performs all CRUD operations i.e. create, read, update and delete roles from the underlying AspNetRoles database table
    //In order for the RoleManager class to work with the IdentityRole class, we pass the IdentityRole class as a generic argument to RoleManager.
    //RoleManager is accessible to any controller or view by asp.net core dependency injection system
    // DE
    //Wenn Sie einen Benutzer im asp.net-Kern erstellen möchten, verwenden wir die UserManager-Klasse. In ähnlicher Weise verwenden wir zum Erstellen einer Rolle die RoleManager-Klasse, die vom asp.net-Kern bereitgestellt wird
    //Die eingebaute IdentityRole-Klasse repräsentiert eine Rolle
    //Die RoleManager-Klasse führt alle CRUD-Operationen aus, d. h. Rollen aus der zugrunde liegenden AspNetRoles-Datenbanktabelle erstellen, lesen, aktualisieren und löschen
    //Damit die RoleManager-Klasse mit der IdentityRole-Klasse funktioniert, übergeben wir die IdentityRole-Klasse als generisches Argument an RoleManager.
    //RoleManager ist für jeden Controller oder jede Ansicht über das asp.net Core Dependency Injection System zugänglich
    // HU
    //Ha asp.net magban szeretne felhasználót létrehozni, a UserManager osztályt használjuk. Hasonlóképpen, egy szerepkör létrehozásához asp.net mag által biztosított RoleManager osztályt használjuk
    //A beépített IdentityRole osztály egy szerepkört képvisel
    //A RoleManager osztály végrehajtja az összes CRUD-műveletet, azaz a szerepkörök létrehozását, olvasását, frissítését és törlését az alapul szolgáló AspNetRoles adatbázistáblából
    //Annak érdekében, hogy a RoleManager osztály működjön együtt az IdentityRole osztállyal, az IdentityRole osztályt adjuk meg a RoleManager általános argumentumaként.
    //A RoleManager bármely vezérlő vagy nézet számára elérhető asp.net alapvető függőséginjektáló rendszer által

    // EN
    // Multiple roles can be specified by separating them with a comma. The actions in this controller are accessible
    // only to those users who are members of either Administrator or
    // GE
    // Mehrere Rollen können angegeben werden, indem sie durch ein Komma getrennt werden. Die Aktionen in diesem Controller sind zugänglich
    // nur für die Benutzer, die entweder Mitglieder von Administrator oder sind
    // HU
    // Több szerep is megadható vesszővel elválasztva. A vezérlőben végrehajtott műveletek elérhetők
    // csak azoknak a felhasználóknak, akik az Adminisztrátor vagy az Adminisztrátor tagjai

    // EN
    // Authorize: if there is no role parameter, only the logged-in status is checked
    // GE
    // Autorisieren: Wenn kein Rollenparameter vorhanden ist, wird nur der eingeloggte Status geprüft
    // HU
    // Authorize: ha nincs szerepkör paramétere, akkor csak a bejelentkezve státuszt ellenőrzi
    [Authorize(Roles = "Admin")]

    // EN
    //To be able to access the actions in this controller, users have to be members of both - the Administrator role and the User role.
    // GE
    //Um auf die Aktionen in diesem Controller zugreifen zu können, müssen Benutzer Mitglieder sowohl der Administratorrolle als auch der Benutzerrolle sein.
    // HU
    //Ahhoz, hogy a vezérlőben hozzáférhessenek a műveletekhez, a felhasználóknak mindkét - a rendszergazdai és a felhasználói szerepkör - tagjainak kell lenniük.
    // Ebben az esetben User és Admin szerepkörrel is rendelkezni kell a hozzáféréshez

    //[Authorize(Roles = "User")]
    //[Authorize(Roles = "Admin")]
    // EN
    // If we override the actions, the class rule does not apply to them
    // GE
    // Wenn wir die Aktionen überschreiben, gilt die Klassenregel nicht für sie
    // HU
    // Ha felülírjük a műveleteket akkor azokra nem vonatkozik az osztályszabály
    public class AdministrationController : Controller
    {
        private readonly IAccountService service;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<AdministrationController> logger;

        public AdministrationController(IAccountService service,
                                        RoleManager<IdentityRole> roleManager,
                                        UserManager<ApplicationUser> userManager,
                                        ILogger<AdministrationController> logger)
        {
            this.service = service;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            // EN
            // UserManager service GetClaimsAsync method gets all the current claims of the user
            // GE
            // GetClaimsAsync-Methode des UserManager-Dienstes ruft alle aktuellen Ansprüche des Benutzers ab
            // HU
            // UserManager szolgáltatás A GetClaimsAsync metódus lekéri a felhasználó összes aktuális követelését
            var existingUserClaims = await userManager.GetClaimsAsync(user);

            var model = new UserClaimsViewModel
            {
                UserId = userId
            };

            // EN
            // Loop through each claim we have in our application
            // GE
            // Durchlaufen Sie jeden Anspruch, den wir in unserer Anwendung haben
            // HU
            // Végezze el az alkalmazásunkban szereplő összes követelést
            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };

                // EN
                // If the user has the claim, set IsSelected property to true, so the checkbox
                // next to the claim is checked on the UI
                // GE
                // Wenn der Benutzer den Anspruch hat, setze die Eigenschaft IsSelected auf true, also das Kontrollkästchen
                // neben dem Anspruch wird auf der Benutzeroberfläche überprüft
                // HU
                // Ha a felhasználó rendelkezik a követeléssel, állítsa az IsSelected tulajdonságot igazra, így a jelölőnégyzetet
                // a követelés mellett be van jelölve a felhasználói felületen
                if (existingUserClaims.Any(c => c.Type == claim.Type && c.Value == "true"))
                {
                    userClaim.IsSelected = true;
                }

                model.Cliams.Add(userClaim);
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                // EN
                //ViewBag.ErrorMessage = $"User with Id = {model.UserId} cannot be found";
                // GE
                ViewBag.ErrorMessage = $"Benutzer mit ID = {model.UserId} kann nicht gefunden werden";
                // HU
                //ViewBag.ErrorMessage = $"A(z) {model.UserId} azonosítójú felhasználó nem található";
                return View("NotFound");
            }

            // EN
            // Get all the user existing claims and delete them
            // GE
            // Alle vorhandenen Ansprüche des Benutzers abrufen und löschen
            // HU
            // Szerezze be a felhasználó összes meglévő követelését, és törölje őket
            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing claims");
                return View(model);
            }

            // EN
            // Add all the claims that are selected on the UI
            // GE
            // Alle Ansprüche hinzufügen, die auf der Benutzeroberfläche ausgewählt sind
            // HU
            // Adja hozzá a felhasználói felületen kiválasztott összes követelést
            //result = await userManager.AddClaimsAsync(user,
            //    model.Cliams.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType)));

            // EN
            // Store true false values instead of ClaimType
            // GE
            // True-False-Werte anstelle von ClaimType speichern
            // HU
            // Eltároljuk az igaz hamis értékeket a ClaimType helyett
            result = await userManager.AddClaimsAsync(user,
                model.Cliams.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.IsSelected ? "true" : "false")));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected claims to user");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = model.UserId });

        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                // EN
                //ViewBag.ErrorMessage = $"User with Id = {model.UserId} cannot be found";
                // GE
                ViewBag.ErrorMessage = $"Benutzer mit ID = {userId} kann nicht gefunden werden";
                // HU
                //ViewBag.ErrorMessage = $"A(z) {model.UserId} azonosítójú felhasználó nem található";
                return View("NotFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var role in roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                // EN
                // appsettings.json ConnectionStrings + MultipleActiveResultSets = true
                // If you execute a query while iterating over the results from another query.
                // One thing that can cause this is lazy loading triggered when iterating over the results of some query.
                // This can be easily solved by allowing MARS in your connection string.Add to the provider part of your connection
                // string(where Data Source, Initial Catalog, etc.are specified). MultipleActiveResultSets = true
                // GE
                // appsettings.json ConnectionStrings + MultipleActiveResultSets = true
                // Wenn Sie eine Abfrage ausführen, während Sie über die Ergebnisse einer anderen Abfrage iterieren.
                // Eine Sache, die dies verursachen kann, ist verzögertes Laden, das ausgelöst wird, wenn die Ergebnisse einer Abfrage durchlaufen werden.
                // Dies kann leicht gelöst werden, indem Sie MARS in Ihrer Verbindungszeichenfolge zulassen. Fügen Sie den Provider-Teil Ihrer Verbindung hinzu
                // Zeichenfolge (wobei Datenquelle, Anfangskatalog usw. angegeben sind). MultipleActiveResultSets = wahr
                // HU
                // Ha egy lekérdezést úgy hajt végre, hogy közben egy másik lekérdezés eredményein iterál.
                // Az egyik dolog, ami ezt okozhatja, a lusta betöltés, amely akkor aktiválódik, amikor valamilyen
                // lekérdezés eredményeit iterálja. Ez könnyen megoldható, ha engedélyezi a MARS-t a kapcsolati sztringben.
                // Adja hozzá a szolgáltatóhoz a kapcsolati sztring egy részét(ahol az adatforrás, a kezdeti katalógus stb.meg van adva).
                // MultipleActiveResultSets = true
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                // EN
                //ViewBag.ErrorMessage = $"User with Id = {model.UserId} cannot be found";
                // GE
                ViewBag.ErrorMessage = $"Benutzer mit ID = {userId} kann nicht gefunden werden";
                // HU
                //ViewBag.ErrorMessage = $"A(z) {model.UserId} azonosítójú felhasználó nem található";
                return View("NotFound");
            }

            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                // EN
                // Wrap the code in a try/catch block
                // GE
                // Wrap the code in a try/catch block
                // HU
                // Csomagolja be a kódot egy try/catch blokkba
                try
                {
                    //throw new Exception("Test Exception");

                    var result = await roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View("ListRoles");
                }

                // EN
                // If the exception is DbUpdateException, we know we are not able to
                // delete the role as there are users in the role being deleted
                // GE
                // Wenn die Ausnahme DbUpdateException ist, wissen wir, dass wir das nicht können
                // Lösche die Rolle, da es Benutzer in der zu löschenden Rolle gibt
                // HU
                // Ha a kivétel a DbUpdateException, tudjuk, hogy erre nem vagyunk képesek
                // törli a szerepkört, mivel a törlendő szerepkörben vannak felhasználók
                catch (DbUpdateException ex)
                {
                    // EN
                    //Log the exception to a file. 
                    // GE
                    //Die Ausnahme in einer Datei protokollieren.
                    // HU
                    //A kivétel naplózása egy fájlba. Bejelentkezünk egy fájlba
                    // az Nlog használatával
                    logger.LogError($"Exception Occured : {ex}");

                    // EN
                    // Pass the ErrorTitle and ErrorMessage that you want to show to
                    // the user using ViewBag. The Error view retrieves this data
                    // from the ViewBag and displays to the user.
                    // GE
                    // Übergeben Sie den ErrorTitle und die ErrorMessage, die Sie anzeigen möchten
                    // der Benutzer, der ViewBag verwendet. Die Fehleransicht ruft diese Daten ab
                    // aus dem ViewBag und wird dem Benutzer angezeigt.
                    // HU
                    // Adja át a megjeleníteni kívánt ErrorTitle-t és ErrorMessage-t
                    // a ViewBagot használó felhasználó. A Hiba nézet lekéri ezeket az adatokat
                    // a ViewBagból és megjeleníti a felhasználónak.
                    ViewBag.ErrorTitle = $"{role.Name} role is in use";
                    // EN
                    //ViewBag.ErrorMessage = $"{role.Name} role cannot be deleted as there are users in this role. If you want to delete this role, please remove the users from the role and then try to delete";
                    // GE
                    ViewBag.ErrorMessage = $"{role.Name} Rolle kann nicht gelöscht werden, da es Benutzer in dieser Rolle gibt. Wenn Sie diese Rolle löschen möchten, entfernen Sie bitte die Benutzer aus der Rolle und versuchen Sie dann, sie zu löschen";
                    // HU
                    //ViewBag.ErrorMessage = $"{role.Name} A szerepkör nem törölhető, mivel vannak felhasználók ebben a szerepkörben. Ha törölni szeretné ezt a szerepkört, kérjük, távolítsa el a felhasználókat a szerepkörből, majd próbálja meg törölni";
                    return View("Error");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                // EN
                //ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                // GE
                ViewBag.ErrorMessage = $"Benutzer mit Id = {id} kann nicht gefunden werden";
                // HU
                //ViewBag.ErrorMessage = $"A felhasználó azonosítóval = {id} nem található";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                // EN
                //ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                // GE
                ViewBag.ErrorMessage = $"Benutzer mit Id = {id} kann nicht gefunden werden";
                // HU
                //ViewBag.ErrorMessage = $"A felhasználó azonosítóval = {id} nem található";
                return View("NotFound");
            }

            // EN
            // GetClaimsAsync retunrs the list of user Claims
            // GE
            // GetClaimsAsync gibt die Liste der Benutzeransprüche zurück
            // HU
            // A GetClaimsAsync visszaadja a felhasználói követelések listáját
            var userClaims = await userManager.GetClaimsAsync(user);
            // GetRolesAsync returns the list of user Roles
            var userRoles = await userManager.GetRolesAsync(user);

            var userSpL = await service.GetSpokenLangues(id);

            var DropdownsData = await service.GetAccountDropdownsValues();

            ViewBag.Country = new SelectList(DropdownsData.Countries, "Id", "CountryName");
            ViewBag.Sex = new SelectList(DropdownsData.Sexs, "Id", "SexName");
            ViewBag.SpokenLanguesName = new SelectList(DropdownsData.SpokenLangues, "Id", "SpokenLanguesName");

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                SexId = user.SexId,
                Street = user.Street,
                HouseNumber = user.HouseNumber,
                PostalCode = user.PostalCode,
                Location = user.Location,
                CountryId = user.CountryId,
                SpokenLanguesId = userSpL,
                DateOfBirth = user.DateOfBirth,
                ManagerId = user.ManagerId,
                EntryDate = user.EntryDate,
                Claims = userClaims.Select(c => c.Type + " : " + c.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                // EN
                //ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                // GE
                ViewBag.ErrorMessage = $"Benutzer mit Id = {model.Id} kann nicht gefunden werden";
                // HU
                //ViewBag.ErrorMessage = $"A felhasználó azonosítóval = {model.Id} nem található";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.SexId = model.SexId;
                user.Street = model.Street;
                user.HouseNumber = model.HouseNumber;
                user.PostalCode = model.PostalCode;
                user.Location = model.Location;
                user.CountryId = model.CountryId;
                user.DateOfBirth = model.DateOfBirth;
                user.ManagerId = model.ManagerId;
                user.EntryDate = model.EntryDate;

                var result = await userManager.UpdateAsync(user);

                await service.UpdateSpokenLanguesAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // EN
                // We just need to specify a unique role name to create a new role
                // EN
                // Wir müssen nur einen eindeutigen Rollennamen angeben, um eine neue Rolle zu erstellen
                // HU
                // Csak meg kell adnunk egy egyedi szerepnevet egy új szerepkör létrehozásához
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // EN
                // Saves the role in the underlying AspNetRoles table
                // GE
                // Speichert die Rolle in der zugrunde liegenden AspNetRoles-Tabelle
                // HU
                // Menti a szerepet az alapul szolgáló AspNetRoles táblában
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        // EN
        // Role ID is passed from the URL to the action
        // GE
        // Rollen-ID wird von der URL an die Aktion übergeben
        // HU
        // A szerepazonosítót az URL-ből adják át a műveletnek
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            // EN
            // Find the role by Role ID
            // GE
            // Suchen Sie die Rolle anhand der Rollen-ID
            // HU
            // Keresse meg a szerepet szerepazonosító alapján
            var role = await roleManager.FindByIdAsync(id);
            
            if (role == null)
            {
                // EN
                //ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                // GE
                ViewBag.ErrorMessage = $"Rolle mit Id = {id} kann nicht gefunden werden";
                // HU
                //ViewBag.ErrorMessage = $"A szerep azonosítóval = {id} nem található";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            // EN
            // Retrieve all the Users
            // GE
            // Alle Benutzer abrufen
            // HU
            // Az összes felhasználó lekérése
            foreach (var user in userManager.Users)
            {
                // EN
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                // appsettings.json ConnectionStrings + MultipleActiveResultSets = true
                // If you execute a query while iterating over the results from another query.
                // One thing that can cause this is lazy loading triggered when iterating over the results of some query.
                // This can be easily solved by allowing MARS in your connection string.Add to the provider part of your connection
                // string(where Data Source, Initial Catalog, etc.are specified). MultipleActiveResultSets = true
                // GE
                // Wenn der Benutzer diese Rolle hat, fügen Sie den Benutzernamen hinzu
                // Benutzereigenschaft von EditRoleViewModel. Dieses Model
                // Das Objekt wird dann zur Anzeige an die Ansicht übergeben
                // appsettings.json ConnectionStrings + MultipleActiveResultSets = true
                // Wenn Sie eine Abfrage ausführen, während Sie über die Ergebnisse einer anderen Abfrage iterieren.
                // Eine Sache, die dies verursachen kann, ist verzögertes Laden, das ausgelöst wird, wenn die Ergebnisse einer Abfrage durchlaufen werden.
                // Dies kann leicht gelöst werden, indem Sie MARS in Ihrer Verbindungszeichenfolge zulassen. Fügen Sie den Provider-Teil Ihrer Verbindung hinzu
                // Zeichenfolge (wobei Datenquelle, Anfangskatalog usw. angegeben sind). MultipleActiveResultSets = wahr
                // HU
                // Ha a felhasználó ebben a szerepkörben van, adja hozzá a felhasználónevet a következőhöz
                // Az EditRoleViewModel felhasználói tulajdonsága. Ez a modell
                // objektum ezután a nézetbe kerül megjelenítésre
                // appsettings.json ConnectionStrings + MultipleActiveResultSets = igaz
                // Ha egy lekérdezést hajt végre, miközben egy másik lekérdezés eredményeit iterálja.
                // Egy dolog, ami ezt okozhatja, az a lusta betöltés, amely akkor vált ki, amikor egy lekérdezés eredményeit iteráljuk.
                // Ez könnyen megoldható, ha engedélyezi a MARS-t a kapcsolati karakterláncban. Adja hozzá a kapcsolat szolgáltatói részét
                // string (ahol az adatforrás, a kezdeti katalógus stb. meg van adva). MultipleActiveResultSets = igaz
                // Ha egy lekérdezést úgy hajt végre, hogy közben egy másik lekérdezés eredményein iterál.
                // Az egyik dolog, ami ezt okozhatja, a lusta betöltés, amely akkor aktiválódik, amikor valamilyen
                // lekérdezés eredményeit iterálja. Ez könnyen megoldható, ha engedélyezi a MARS-t a kapcsolati sztringben.
                // Adja hozzá a szolgáltatóhoz a kapcsolati sztring egy részét(ahol az adatforrás, a kezdeti katalógus stb.meg van adva).
                // MultipleActiveResultSets = true
                // Működik az ellenőrzés a feltétel kiértékelése nélkül is
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        // EN
        // This action responds to HttpPost and receives EditRoleViewModel
        // GE
        // Diese Aktion antwortet auf HttpPost und empfängt EditRoleViewModel
        // HU
        // Ez a művelet válaszol a HttpPost parancsra, és megkapja az EditRoleViewModel-t
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                // EN
                //ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                // GE
                ViewBag.ErrorMessage = $"Rolle mit Id = {model.Id} kann nicht gefunden werden";
                // HU
                //ViewBag.ErrorMessage = $"A szerep azonosítóval = {model.Id} nem található";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // EN
                // Update the Role using UpdateAsync
                // GE
                // Aktualisieren Sie die Rolle mit UpdateAsync
                // HU
                // Frissítse a szerepkört az UpdateAsync használatával
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                // EN
                //ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                // GE
                ViewBag.ErrorMessage = $"Rolle mit Id = {roleId} kann nicht gefunden werden";
                // HU
                //ViewBag.ErrorMessage = $"A szerep azonosítóval = {roleId} nem található";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            string errorMessage = string.Empty;
            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                // EN
                // appsettings.json ConnectionStrings + MultipleActiveResultSets = true
                // If you execute a query while iterating over the results from another query.
                // One thing that can cause this is lazy loading triggered when iterating over the results of some query.
                // This can be easily solved by allowing MARS in your connection string.Add to the provider part of your connection
                // string(where Data Source, Initial Catalog, etc.are specified). MultipleActiveResultSets = true
                // GE
                // appsettings.json ConnectionStrings + MultipleActiveResultSets = true
                // Wenn Sie eine Abfrage ausführen, während Sie über die Ergebnisse einer anderen Abfrage iterieren.
                // Eine Sache, die dies verursachen kann, ist verzögertes Laden, das ausgelöst wird, wenn die Ergebnisse einer Abfrage durchlaufen werden.
                // Dies kann leicht gelöst werden, indem Sie MARS in Ihrer Verbindungszeichenfolge zulassen. Fügen Sie den Provider-Teil Ihrer Verbindung hinzu
                // Zeichenfolge (wobei Datenquelle, Anfangskatalog usw. angegeben sind). MultipleActiveResultSets = wahr
                // HU
                // Ha egy lekérdezést úgy hajt végre, hogy közben egy másik lekérdezés eredményein iterál.
                // Az egyik dolog, ami ezt okozhatja, a lusta betöltés, amely akkor aktiválódik, amikor valamilyen
                // lekérdezés eredményeit iterálja. Ez könnyen megoldható, ha engedélyezi a MARS-t a kapcsolati sztringben.
                // Adja hozzá a szolgáltatóhoz a kapcsolati sztring egy részét(ahol az adatforrás, a kezdeti katalógus stb.meg van adva).
                // MultipleActiveResultSets = true
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }


                model.Add(userRoleViewModel);
            }

            return View(model);
        }

       
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                // EN
                //ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                // GE
                ViewBag.ErrorMessage = $"Rolle mit Id = {roleId} kann nicht gefunden werden";
                // HU
                //ViewBag.ErrorMessage = $"A szerep azonosítóval = {roleId} nem található";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null!;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
