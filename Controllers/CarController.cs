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
        // It is through IDataProtector interface Protect and Unprotect methods,
        // we encrypt and decrypt respectively
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

            //// Decrypt the carAccessories id using Unprotect method
            //string decryptedId = protector.Unprotect(id);
            //int decryptedIntId = Convert.ToInt32(decryptedId);


            // We check if it exists

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

                // Store the file name in PhotoPath property of the employee object
                // which gets saved to the Employees database table
                // Speichern Sie den Dateinamen in der PhotoPath-Eigenschaft des Mitarbeiterobjekts
                // die in der Employees-Datenbanktabelle gespeichert wird
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
            //// Decrypt the carAccessories id using Unprotect method
            //string decryptedId = protector.Unprotect(id);
            //int decryptedIntId = Convert.ToInt32(decryptedId);


            // We check if it exists

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
            // A biztonság érdekében, hogy ne lehessen feltörni az oldalt
            var carAccessoriesDetails = await service.GetByIdAsync(id);
            if (carAccessoriesDetails == null) return View("NotFound");

            // If the user wants to change the photo, a new photo will be
            // uploaded and the Photo property on the model object receives
            // the uploaded photo. If the Photo property is null, user did
            // not upload a new photo and keeps his existing photo
            if (modelCar.Photo != null)
            {
                // If a new photo is uploaded, the existing photo must be
                // deleted. So check if there is an existing photo and delete
                if (modelCar.ExistingPhotoPath != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                        "images", modelCar.ExistingPhotoPath);
                    System.IO.File.Delete(filePath);
                }
                // Save the new photo in wwwroot/images folder and update
                // PhotoPath property of the employee object which will be
                // eventually saved in the database
                modelCar.PhotoPath = ProcessUploadedFile(modelCar);
            }

            await TryUpdateModelAsync(modelCar);

            // leave it on this page in case of errors
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
            {
                ViewBag.ErrorMessage = $"Auto ist mit Id = {id} nicht gefunden";
                return View("NotFound");
            }
            else
            {
                // kódolt id esetén
                //await service.CarAccessoriesDelete(decryptedIntId);

                await service.DeleteAsync(id);
                return RedirectToAction("Index");
            }
        }

        private string ProcessUploadedFile(CarCreateViewModel modelCar)
        {
            string uniqueFileName = null;

            // If the Photo property on the incoming model object is not null, then the user
            // has selected an image to upload.
            // Wenn die Photo-Eigenschaft des eingehenden Modellobjekts nicht null ist, dann der Benutzer
            // hat ein Bild zum Hochladen ausgewählt.
            if (modelCar.Photo != null)
            {
                // The image must be uploaded to the images folder in wwwroot
                // To get the path of the wwwroot folder we are using the inject
                // HostingEnvironment service provided by ASP.NET Core
                // Das Bild muss in den Bilderordner in wwwroot hochgeladen werden
                // Um den Pfad des wwwroot-Ordners zu erhalten, verwenden wir das inject
                // HostingEnvironment-Dienst, bereitgestellt von ASP.NET Core
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                // To make sure the file name is unique we are appending a new
                // GUID value and and an underscore to the file name
                // Um sicherzustellen, dass der Dateiname eindeutig ist, hängen wir eine neue an
                // GUID-Wert und ein Unterstrich für den Dateinamen
                uniqueFileName = Guid.NewGuid().ToString() + "_" + modelCar.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                // Verwenden Sie die CopyTo()-Methode, die von der IFormFile-Schnittstelle bereitgestellt wird
                // Kopieren Sie die Datei in den Ordner wwwroot/images
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    modelCar.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        private int UrlDecryption(string id)
        {
            // Decrypt the employee id using Unprotect method
            string decryptedId = protector.Unprotect(id);
            int decryptedIntId = Convert.ToInt32(decryptedId);
            return decryptedIntId;

        }
    }
}
