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

namespace CarDealershipASPNETMVC.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarsService service;
        private readonly AppDbContext appDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<CarController> logger;
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

        public CarController(ICarsService service,
                             AppDbContext appDbContext,
                             IWebHostEnvironment webHostEnvironment,
                             UserManager<ApplicationUser> userManager,
                             ILogger<CarController> logger,
                             IDataProtectionProvider dataProtectionProvider,
                             DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            this.service = service;
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
            ViewData["Title"] = "Cars";

            var data = await service.GetAllAsync(f => f.Fuels,
                                                 g => g.Gearbox);

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

            ViewData["Title"] = "Car Details";

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
            var carDetails = await service.GetByIdAsync(id);

            if (carDetails == null) return View("NotFound");

            CarDetailsViewModel CarDetailsViewModel = new CarDetailsViewModel()
            {
                Car = await service.GetCarByIdAsync(id)
            };

            return View(CarDetailsViewModel);
        }


        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var car = await service.GetAllAsync(c => c.Model);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResultNew = car.Where(c =>
                string.Equals(c.Model, searchString, StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(c.Color, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", car);
        }


        [HttpGet]
        [ActionName("Create")]
        [Authorize]
        //[Authorize(Policy = "CreateRolePolicy")]
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Car Create";

            var carDropdownsData = await service.GetCarsDropdownsValues();

            ViewBag.CarFuels = new SelectList(carDropdownsData.Fuels, "Id", "FuelName");

            ViewBag.CarGearboxs = new SelectList(carDropdownsData.Gearboxs, "Id", "GearboxName");

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [Authorize]
        //[Authorize(Policy = "CreateRolePolicy")]
        public async Task<IActionResult> Create(CarCreateViewModel createdCar)
        {
            // hibák esetén hagyom ezen az oldalon
            //https://csharp-video-tutorials.blogspot.com/2019/05/edit-view-in-aspnet-core-mvc.html
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(createdCar);

                // EN
                // Store the file name in PhotoPath property of the employee object
                // which gets saved to the Employees database table
                // GE
                // Speichern Sie den Dateinamen in der PhotoPath-Eigenschaft des Mitarbeiterobjekts
                // die in der Employees-Datenbanktabelle gespeichert wird
                // HU
                // Tárolja a fájl nevét az alkalmazotti objektum PhotoPath tulajdonságában
                // amely az Alkalmazottak adatbázistáblájába kerül mentésre
                createdCar.PhotoPath = uniqueFileName;

                string newId = await service.AddNewCarAsync(createdCar);

                return RedirectToAction("Details", new { id = newId });
            }
            else
            {
                ViewData["Title"] = "Car Create";

                var carDropdownsData = await service.GetCarsDropdownsValues();
                ViewBag.CarFuels = new SelectList(carDropdownsData.Fuels, "Id", "FuelName");
                ViewBag.CarGearboxs = new SelectList(carDropdownsData.Gearboxs, "Id", "GearboxName");

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

            var carDetails = await service.GetByIdAsync(id);

            if (carDetails == null) return View("NotFound");

            CarEditViewModel carEditViewModel = new CarEditViewModel()
            {
                Id = carDetails.Id,
                Model = carDetails.Model,
                Color = carDetails.Color,
                NumberOfSeats = carDetails.NumberOfSeats,
                YearOfProduction = carDetails.YearOfProduction,
                FuelId = carDetails.FuelId,
                GearboxId = carDetails.GearboxId,
                CubicCapacity = carDetails.CubicCapacity,
                Mileage = carDetails.Mileage,
                ChassisNumber = carDetails.ChassisNumber,
                OwnWeight = carDetails.OwnWeight,
                EnginePower = carDetails.EnginePower,
                Sold = carDetails.Sold,
                NettoPrice = carDetails.NettoPrice,
                LastUpdateTime = carDetails.LastUpdateTime,
                PhotoPath = carDetails.PhotoPath
            };

            var carDropdownsData = await service.GetCarsDropdownsValues();
            ViewBag.CarFuels = new SelectList(carDropdownsData.Fuels, "Id", "FuelName");
            ViewBag.CarGearboxs = new SelectList(carDropdownsData.Gearboxs, "Id", "GearboxName");

            return View(carEditViewModel);
        }
        [HttpPost]
        [ActionName("Edit")]
        [Authorize]
        //[Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> Edit(string id, CarEditViewModel modelCar)
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
            if (modelCar.Photo != null)
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
                if (modelCar.ExistingPhotoPath != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                        "images", modelCar.ExistingPhotoPath);
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
                modelCar.PhotoPath = ProcessUploadedFile(modelCar);
            }

            await TryUpdateModelAsync(modelCar);

            // EN
            // leave it on this page in case of errors
            // GE
            // im Fehlerfall auf dieser Seite belassen
            // HU
            // hibák esetén hagyom ezen az oldalon
            if (ModelState.IsValid)
            {
                await service.UpdateCarAsync(modelCar);

                return RedirectToAction("Details", new { id = modelCar.Id });
            }
            else
            {
                var carDropdownsData = await service.GetCarsDropdownsValues();
                ViewBag.CarFuels = new SelectList(carDropdownsData.Fuels, "Id", "FuelName");
                ViewBag.CarGearboxs = new SelectList(carDropdownsData.Gearboxs, "Id", "GearboxName");

                return View(modelCar);
            }
        }

        // Delete
        [HttpPost]
        [Authorize]
        //[Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> Delete(string id)
        {

            //int decryptedIntId = UrlDecryption(id);
            //detailsCarAccessories.Id = decryptedIntId;

            var carDetails = await service.GetByIdAsync(id);

            if (carDetails == null)
            {// EN
                //ViewBag.ErrorMessage = $"Auto not found with id={id}";
                // GE
                ViewBag.ErrorMessage = $"Auto ist mit Id = {id} nicht gefunden";
                // HU
                //ViewBag.ErrorMessage = $"Az autó nem található az id={id} azonosítóval";
                return View("NotFound");
            }
            else
            {
                await service.DeleteAsync(id);
                return RedirectToAction("Index");
            }
        }

        private string ProcessUploadedFile(CarCreateViewModel modelCar)
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
            if (modelCar.Photo != null)
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
                uniqueFileName = Guid.NewGuid().ToString() + "_" + modelCar.Photo.FileName;
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
                    modelCar.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
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
