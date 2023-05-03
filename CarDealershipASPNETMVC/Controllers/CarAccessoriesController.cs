using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Data.Service;
using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using CarDealershipASPNETMVC.Security;
using CarDealershipASPNETMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace CarDealershipASPNETMVC.Controllers
{
    [Authorize]
    public class CarAccessoriesController : Controller
    {
        private readonly ICarAccessoriesService service;
        private readonly IShoppingCartService shoppingCartService;
        private readonly AppDbContext appDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<CarAccessoriesController> logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        
        // EN
        // It is through IDataProtector interface Protect and Unprotect methods,
        // we encrypt and decrypt respectively
        // EN
        // Es ist durch die Protect- und Unprotect-Methoden der IDataProtector-Schnittstelle,
        // wir verschlüsseln bzw. entschlüsseln
        // HU
        // Az IDataProtector interfészen keresztül Protect és Unprotect módszerek,
        // titkosítjuk és visszafejtjük
        private readonly IDataProtector protector;

        // EN
        // It is the CreateProtector() method of IDataProtectionProvider interface
        // that creates an instance of IDataProtector. CreateProtector() requires
        // a purpose string. So both IDataProtectionProvider and the class that
        // contains our purpose strings are injected using the contructor
        // GE
        // Dies ist die CreateProtector()-Methode der IDataProtectionProvider-Schnittstelle
        // die eine Instanz von IDataProtector erstellt. CreateProtector() erfordert
        // ein Zweck-String. Also sowohl IDataProtectionProvider als auch die Klasse that
        // Enthält unsere Zweckzeichenfolgen, die mit dem Konstruktor eingefügt werden
        // HU
        // Ez az IDataProtectionProvider felület CreateProtector() metódusa
        // , amely létrehozza az IDataProtector egy példányát. A CreateProtector() használatához az szükséges.
        // egy célkarakterlánc. Tehát mind az IDataProtectionProvider, mind az osztály, amely
        // tartalmazza a célhúrjainkat a contructor segítségével injektáljuk
        // a program.cs "builder.Services.AddSingleton<IDataAccess, DataAccess>();" sora hívódik meg, ami beállítja az Interface értékét
        public CarAccessoriesController(ICarAccessoriesService service,
                                        IShoppingCartService shoppingCartService,
                                        AppDbContext appDbContext, 
                                        IWebHostEnvironment webHostEnvironment,
                                        UserManager<ApplicationUser> userManager,
                                        ILogger<CarAccessoriesController> logger,
                                        IDataProtectionProvider dataProtectionProvider,
                                        DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            this.service = service;
            this.shoppingCartService = shoppingCartService;
            this.appDbContext = appDbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.logger = logger;
            this.protector = dataProtectionProvider.CreateProtector(
                dataProtectionPurposeStrings.IdRouteValue);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            ViewData["Title"] = "Car Accessories";

            var data = await service.GetAllAsync(capg => capg.carAccessoriesProductGroupModel,
                                                 cau => cau.carAccessoriesUnitModel);

            //.Select(e =>
            //{
            //    // EN
            //    // Encrypt the ID value and store in EncryptedId property
            //    // GE
            //    // Den ID-Wert verschlüsseln und in der EncryptedId-Eigenschaft speichern
            //    // HU
            //    // Az azonosító érték titkosítása és tárolása az EncryptedId tulajdonságban
            //    e.EncryptedId = protector.Protect(e.Id.ToString());
            //    return e;
            //});

            return View(data);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");

            ViewData["Title"] = "Car Accessories Details";

            //// EN
            //// Decrypt the carAccessories id using Unprotect method
            //// GE
            //// Entschlüsseln Sie die carAccessories-ID mit der Unprotect-Methode
            //// HU
            //// A carAccessories azonosító visszafejtése Unprotect metódussal
            //string decryptedId = protector.Unprotect(id);
            //int decryptedIntId = Convert.ToInt32(decryptedId);

            // EN
            // We check if it exists
            // GE
            // Wir prüfen, ob es existiert
            // HU
            // Ellenőrizzük, hogy létezik-e
            var carAccessoriesDetails = await service.GetByIdAsync(id);

            if (carAccessoriesDetails == null) return View("NotFound");

            CarAccessoriesDetailsViewModel CarAccessoriesDetailsViewModel = new CarAccessoriesDetailsViewModel()
            {
                CarAccessories = await service.GetCarAccessoriesByIdAsync(id)
            };

            ViewBag.Quantity = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "1", Value = "1", Selected = true},
                new SelectListItem {Text = "2", Value = "2"},
                new SelectListItem {Text = "3", Value = "3"},
                new SelectListItem {Text = "4", Value = "4"},
                new SelectListItem {Text = "5", Value = "5"},
                new SelectListItem {Text = "6", Value = "6"},
                new SelectListItem {Text = "7", Value = "7"},
                new SelectListItem {Text = "8", Value = "8"},
                new SelectListItem {Text = "9", Value = "9"},
                new SelectListItem {Text = "10", Value = "10"},
                new SelectListItem {Text = "11", Value = "11"},
                new SelectListItem {Text = "12", Value = "12"},
                new SelectListItem {Text = "13", Value = "13"},
                new SelectListItem {Text = "14", Value = "14"},
                new SelectListItem {Text = "15", Value = "15"},
            }, "Value", "Text");

            return View(CarAccessoriesDetailsViewModel);
        }

        //[HttpPost]
        ////[Authorize]
        //public async Task<IActionResult> Details(string id, int quantity)
        //{
        //    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    string userRole = User.FindFirstValue(ClaimTypes.Role);

        //    var carAccessoriesItem = await service.GetCarAccessoriesByIdAsync(id);

        //    if (carAccessoriesItem != null)
        //    {
        //        await shoppingCartService.AddCarAccessoriesItemToCart(carAccessoriesItem, quantity, userId, userRole);
        //        return RedirectToAction("Index", "CarAccessories");
        //    }
        //    return RedirectToAction("Index");
        //}

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var carAccessories = await service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResultNew = carAccessories.Where(c => 
                string.Equals(c.ProductName, searchString, StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(c.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", carAccessories);
        }

        // EN
        // HttpGet allows the page to be called, the data entered there is sent to HttpPost when clicked, both must be created
        // GE
        // HttpGet ermöglicht den Aufruf der Seite, die dort eingetragenen Daten werden beim Klick an HttpPost gesendet, beides muss erstellt werden
        // HU
        // A HttpGet az oldal hívását teszi lehetővé, az ott megadott adatok a HttpPost-ba kerülnek kattintáskor, létre kell hozni mindkettőt
        [HttpGet]
        [ActionName("Create")]
        [Authorize]
        //[Authorize(Policy = "CreateRolePolicy")]
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Car Accessories Create";

            var carAccessoriesDropdownsData = await service.GetCarAccessoriesDropdownsValues();

            ViewBag.CarAccessoriesProductGroup = new SelectList(carAccessoriesDropdownsData.CarAccessoriesProductGroups, "Id", "CAPGName");

            ViewBag.CarAccessoriesUnit = new SelectList(carAccessoriesDropdownsData.CarAccessoriesUnits, "Id", "UnitName");

            return View(); 
        }
        // EN
        // this is what happens to the data collected in HttpGet
        // properties can be entered as parameters in the constructor, which can be used later, so writing is less
        // or pass the object directly
        // GE
        // this is what happens to the data collected in HttpGet
        // properties can be entered as parameters in the constructor, which can be used later, so writing is less
        // or pass the object directly
        // HU
        // ez történik a HttpGet-ben gyűjtott adatokkal
        // a konstrukrotban lehet paraméterként megadni a tulajdonságokat, amiket később fel lehet használni így kevesebb az írás
        // vagy közvetlenül az objektumot átadni
        [HttpPost]
        [ActionName("Create")]
        [Authorize]
        //[Authorize(Policy = "CreateRolePolicy")]
        public async Task<IActionResult> Create(CarAccessoriesCreateViewModel createdcarAccessories)
        {
            // hibák esetén hagyom ezen az oldalon
            //https://csharp-video-tutorials.blogspot.com/2019/05/edit-view-in-aspnet-core-mvc.html
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(createdcarAccessories);

                // EN
                // Store the file name in PhotoPath property of the employee object
                // which gets saved to the Employees database table
                // GE
                // Speichern Sie den Dateinamen in der PhotoPath-Eigenschaft des Mitarbeiterobjekts
                // die in der Employees-Datenbanktabelle gespeichert wird
                // HU
                // Tárolja a fájl nevét az alkalmazotti objektum PhotoPath tulajdonságában
                // amely az Alkalmazottak adatbázistáblájába kerül mentésre
                createdcarAccessories.PhotoPath = uniqueFileName;

                string newId = await service.AddNewCarAccessoriesAsync(createdcarAccessories);

                return RedirectToAction("Details", new { id = newId });
            }
            else
            {
                ViewData["Title"] = "Car Accessories Create";

                var carAccessoriesDropdownsData = await service.GetCarAccessoriesDropdownsValues();
                ViewBag.CarAccessoriesProductGroup = new SelectList(carAccessoriesDropdownsData.CarAccessoriesProductGroups, "Id", "CAPGName");
                ViewBag.CarAccessoriesUnit = new SelectList(carAccessoriesDropdownsData.CarAccessoriesUnits, "Id", "UnitName");

                return View();
            }
        }

        // Update
        [HttpGet]
        [ActionName("Edit")]
        [Authorize]
        //[Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> Edit(string id)
        {
            //// EN
            //// Decrypt the carAccessories id using Unprotect method
            //// GE
            //// Entschlüsseln Sie die carAccessories-ID mit der Unprotect-Methode
            //// HU
            //// A carAccessories azonosító visszafejtése Unprotect metódussal
            //string decryptedId = protector.Unprotect(id);
            //int decryptedIntId = Convert.ToInt32(decryptedId);

            // EN
            // We check if it exists
            // GE
            // Wir prüfen, ob es existiert
            // HU
            // Ellenőrizzük, hogy létezik-e

            var carAccessoriesDetails = await service.GetByIdAsync(id);

            if (carAccessoriesDetails == null) return View("NotFound");

            CarAccessoriesEditViewModel carAccessoriesEditViewModel = new CarAccessoriesEditViewModel()
            {
                Id = carAccessoriesDetails.Id,
                ProductName = carAccessoriesDetails.ProductName,
                CAPGId = carAccessoriesDetails.CAPGId,
                QuantityOfStock = carAccessoriesDetails.QuantityOfStock,
                MinimumStockQuantity = carAccessoriesDetails.MinimumStockQuantity,
                NetSellingPrice = carAccessoriesDetails.NetSellingPrice,
                SalesUnit = carAccessoriesDetails.SalesUnit,
                UnitNameId = carAccessoriesDetails.UnitNameId,
                Brand = carAccessoriesDetails.Brand,
                CreationDate = carAccessoriesDetails.CreationDate,
                Description = carAccessoriesDetails.Description,
                Version = carAccessoriesDetails.Version,
                LastUpdateTime = carAccessoriesDetails.LastUpdateTime,
                PhotoPath = carAccessoriesDetails.PhotoPath
            };

            var carAccessoriesDropdownsData = await service.GetCarAccessoriesDropdownsValues();
            ViewBag.CarAccessoriesProductGroup = new SelectList(carAccessoriesDropdownsData.CarAccessoriesProductGroups, "Id", "CAPGName");
            ViewBag.CarAccessoriesUnit = new SelectList(carAccessoriesDropdownsData.CarAccessoriesUnits, "Id", "UnitName");

            return View(carAccessoriesEditViewModel);
        }
        [HttpPost]
        [ActionName("Edit")]
        [Authorize]
        //[Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> Edit(string id, CarAccessoriesEditViewModel modelCarAccessories)
        {
            // EN
            // For security, so the page can't be hacked
            // GE
            // Aus Sicherheitsgründen, damit die Seite nicht gehackt werden kann
            // HU
            // A biztonság érdekében, hogy ne lehessen feltörni az oldalt
            var carAccessoriesDetails = await service.GetByIdAsync(id);
            if (carAccessoriesDetails == null) return View("NotFound");

            // EN
            // takes the parameters from the completed page, the overloaded version must be used for security
            // in order to exclude null values based on specified properties in a string array
            // contents of the array are included
            // GE
            // übernimmt die Parameter von der fertigen Seite, zur Sicherheit muss die überladene Version verwendet werden
            // um Nullwerte basierend auf angegebenen Eigenschaften in einem String-Array auszuschließen
            // Inhalte des Arrays werden eingeschlossen
            // HU
            // átveszi a kitöltött oldalról a paramétereket, a tulterhelt változatát kell használni a biztonság
            // érdekében egy string tömbben történik a kizárás meghatározott tulajdonságok alapján null értékek nélkül
            // a tömbben foglaltak tartoznak bele
            //      A Fiddler programmal lehetne feltörni weboldalakat
            //await TryUpdateModelAsync(findUpdatedCarAccessories, null, null, new string[] { "" });
            // lehet még hasnálni Edit_Post([Bind(Include="Id, Name" )]CarAccessoriesModel carAccessories) konstruktort is "Exclude" paraméterrel is
            //await TryUpdateModelAsync(modelCarAccessories);

            // EN
            // If the user wants to change the photo, a new photo will be
            // uploaded and the Photo property on the model object receives
            // the uploaded photo. If the Photo property is null, user did
            // not upload a new photo and keeps his existing photo
            // GE
            // Wenn der Benutzer das Foto ändern möchte, wird ein neues Foto angezeigt
            // hochgeladen und die Photo-Eigenschaft des Modellobjekts erhält
            // das hochgeladene Foto. Wenn die Photo-Eigenschaft null ist, hat der Benutzer dies getan
            // lädt kein neues Foto hoch und behält sein vorhandenes Foto
            // HU
            // Ha a felhasználó meg akarja változtatni a fényképet, akkor új fotó lesz
            // feltöltve, és a modellobjektum Photo tulajdonságát megkapja
            // a feltöltött fotó. Ha a Photo tulajdonság nulla, a felhasználó ezt tette
            // nem tölt fel új fényképet, és megtartja a meglévő fényképét
            if (modelCarAccessories.Photo != null)
            {
                // EN
                // If a new photo is uploaded, the existing photo must be
                // deleted. So check if there is an existing photo and delete
                // GE
                // Wenn ein neues Foto hochgeladen wird, muss das vorhandene Foto hochgeladen werden
                // gelöscht. Überprüfen Sie also, ob ein vorhandenes Foto vorhanden ist, und löschen Sie es
                // HU
                // Új fénykép feltöltése esetén a meglévőnek kell lennie
                // törölve. Tehát ellenőrizze, hogy van-e fénykép, és törölje
                if (modelCarAccessories.ExistingPhotoPath != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                        "images", modelCarAccessories.ExistingPhotoPath);
                    System.IO.File.Delete(filePath);
                }

                // EN
                // Save the new photo in wwwroot/images folder and update
                // PhotoPath property of the employee object which will be
                // eventually saved in the database
                // GE
                // Das neue Foto im Ordner wwwroot/images speichern und aktualisieren
                // PhotoPath-Eigenschaft des Mitarbeiterobjekts, das sein wird
                // schließlich in der Datenbank gespeichert
                // HU
                // Mentse el az új fényképet a wwwroot/images mappába, és frissítse
                // Az alkalmazott objektum PhotoPath tulajdonsága, amely lesz
                // végül elmentve az adatbázisba
                modelCarAccessories.PhotoPath = ProcessUploadedFile(modelCarAccessories);
            }

            await TryUpdateModelAsync(modelCarAccessories);

            // EN
            // leave it on this page in case of errors
            // GE
            // im Fehlerfall auf dieser Seite belassen
            // HU
            // hibák esetén hagyom ezen az oldalon
            if (ModelState.IsValid)
            {
                await service.UpdateCarAccessoriesAsync(modelCarAccessories);

                return RedirectToAction("Details", new { id = modelCarAccessories.Id });
            }
            else
            {
                var carAccessoriesDropdownsData = await service.GetCarAccessoriesDropdownsValues();
                ViewBag.CarAccessoriesProductGroup = new SelectList(carAccessoriesDropdownsData.CarAccessoriesProductGroups, "Id", "CAPGName");
                ViewBag.CarAccessoriesUnit = new SelectList(carAccessoriesDropdownsData.CarAccessoriesUnits, "Id", "UnitName");

                return View(modelCarAccessories);
            }
        }

        private string ProcessUploadedFile(CarAccessoriesCreateViewModel modelCarAccessories)
        {
            string uniqueFileName = null;
 
            // EN
            // If the Photo property on the incoming model object is not null, then the user
            // has selected an image to upload.
            // GE
            // Wenn die Photo-Eigenschaft des eingehenden Modellobjekts nicht null ist, dann der Benutzer
            // hat ein Bild zum Hochladen ausgewählt.
            // HU
            // Ha a bejövő modellobjektum Photo tulajdonsága nem null, akkor a felhasználó
            // kiválasztotta a feltöltendő képet.
            if (modelCarAccessories.Photo != null)
            {
                // EN
                // The image must be uploaded to the images folder in wwwroot
                // To get the path of the wwwroot folder we are using the inject
                // HostingEnvironment service provided by ASP.NET Core
                // GE
                // Das Bild muss in den Bilderordner in wwwroot hochgeladen werden
                // Um den Pfad des wwwroot-Ordners zu erhalten, verwenden wir das inject
                // HostingEnvironment-Dienst, bereitgestellt von ASP.NET Core
                // HU
                // A képet fel kell tölteni a wwwroot images mappájába
                // A wwwroot mappa elérési útjának lekéréséhez az injekciót használjuk
                // Az ASP.NET Core által biztosított tárhelykörnyezeti szolgáltatás
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                // EN
                // To make sure the file name is unique we are appending a new
                // GUID value and and an underscore to the file name
                // GE
                // Um sicherzustellen, dass der Dateiname eindeutig ist, hängen wir eine neue an
                // GUID-Wert und ein Unterstrich für den Dateinamen
                // HU
                // Annak érdekében, hogy a fájlnév egyedi legyen, egy újat fűzünk hozzá
                // GUID érték és és aláhúzás a fájlnévhez
                uniqueFileName = Guid.NewGuid().ToString() + "_" + modelCarAccessories.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                // EN
                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                // GE
                // Verwenden Sie die CopyTo()-Methode, die von der IFormFile-Schnittstelle bereitgestellt wird
                // Kopieren Sie die Datei in den Ordner wwwroot/images
                // HU
                // Használja az IFormFile felület által biztosított CopyTo() metódust
                // másolja a fájlt a wwwroot/images mappába
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    modelCarAccessories.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        /// <summary>
        /// EN
        /// Car Accessories cannot be deleted if it is on the Stock Replenishment list
        /// /// DE
        /// Autozubehör kann nicht gelöscht werden, wenn es auf der Bestandsauffüllungsliste steht
        /// HU
        /// Az autótartozékok nem törölhetők, ha a Készletfeltöltés listán szerepelnek
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        //[Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> Delete(string id)
        {
            var carAccessoriesDetails = await service.GetByIdAsync(id);

            if (carAccessoriesDetails == null)
            {
                // EN
                //ViewBag.ErrorMessage = $"Auto accessory not found with id={id}";
                // GE
                ViewBag.ErrorMessage = $"Auto Zubehör ist mit Id = {id} nicht gefunden";
                // HU
                //ViewBag.ErrorMessage = $"Az autós kiegészítő nem található az id={id} azonosítóval";
                return View("NotFound");
            }
            else
            {
                // EN
                // for coded id
                // GE
                // für codierte ID
                // HU
                // kódolt id esetén
                //await service.CarAccessoriesDelete(decryptedIntId);

                await service.DeleteAsync(id);
                return RedirectToAction("Index");

            }
        }
        
        private int UrlDecryption(string id)
        {
            // EN
            // Decrypt the employee id using Unprotect method
            // GE
            // Entschlüsseln Sie die Mitarbeiter-ID mit der Unprotect-Methode
            // HU
            // Az alkalmazotti azonosító visszafejtése a Unprotect metódussal
            string decryptedId = protector.Unprotect(id);
            int decryptedIntId = Convert.ToInt32(decryptedId);
            return decryptedIntId;

        }
    }
}
