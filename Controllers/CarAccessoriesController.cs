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
        // It is through IDataProtector interface Protect and Unprotect methods,
        // we encrypt and decrypt respectively
        private readonly IDataProtector protector;

        // It is the CreateProtector() method of IDataProtectionProvider interface
        // that creates an instance of IDataProtector. CreateProtector() requires
        // a purpose string. So both IDataProtectionProvider and the class that
        // contains our purpose strings are injected using the contructor
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
            //    // Encrypt the ID value and store in EncryptedId property
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

            //// Decrypt the carAccessories id using Unprotect method
            //string decryptedId = protector.Unprotect(id);
            //int decryptedIntId = Convert.ToInt32(decryptedId);


            // We check if it exists

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

        [HttpPost]
        [ActionName("Create")]
        [Authorize]
        //[Authorize(Policy = "CreateRolePolicy")]
        public async Task<IActionResult> Create(CarAccessoriesCreateViewModel createdcarAccessories)
        {
            //https://csharp-video-tutorials.blogspot.com/2019/05/edit-view-in-aspnet-core-mvc.html
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(createdcarAccessories);

                // Store the file name in PhotoPath property of the employee object
                // which gets saved to the Employees database table
                // Speichern Sie den Dateinamen in der PhotoPath-Eigenschaft des Mitarbeiterobjekts
                // die in der Employees-Datenbanktabelle gespeichert wird
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
            //// Decrypt the carAccessories id using Unprotect method
            //string decryptedId = protector.Unprotect(id);
            //int decryptedIntId = Convert.ToInt32(decryptedId);


            // We check if it exists

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
            var carAccessoriesDetails = await service.GetByIdAsync(id);
            if (carAccessoriesDetails == null) return View("NotFound");

            // If the user wants to change the photo, a new photo will be
            // uploaded and the Photo property on the model object receives
            // the uploaded photo. If the Photo property is null, user did
            // not upload a new photo and keeps his existing photo
            if (modelCarAccessories.Photo != null)
            {
                // If a new photo is uploaded, the existing photo must be
                // deleted. So check if there is an existing photo and delete
                if (modelCarAccessories.ExistingPhotoPath != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                        "images", modelCarAccessories.ExistingPhotoPath);
                    System.IO.File.Delete(filePath);
                }
                // Save the new photo in wwwroot/images folder and update
                // PhotoPath property of the employee object which will be
                // eventually saved in the database
                modelCarAccessories.PhotoPath = ProcessUploadedFile(modelCarAccessories);
            }

            await TryUpdateModelAsync(modelCarAccessories);

            // leave it on this page in case of errors
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
 
            // If the Photo property on the incoming model object is not null, then the user
            // has selected an image to upload.
            // Wenn die Photo-Eigenschaft des eingehenden Modellobjekts nicht null ist, dann der Benutzer
            // hat ein Bild zum Hochladen ausgewählt.
            if (modelCarAccessories.Photo != null)
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
                uniqueFileName = Guid.NewGuid().ToString() + "_" + modelCarAccessories.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                // Verwenden Sie die CopyTo()-Methode, die von der IFormFile-Schnittstelle bereitgestellt wird
                // Kopieren Sie die Datei in den Ordner wwwroot/images
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    modelCarAccessories.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        /// <summary>
        /// Car Accessories cannot be deleted if it is on the Stock Replenishment list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        //[Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> Delete(string id)
        {
            //int decryptedIntId = UrlDecryption(id);
            //detailsCarAccessories.Id = decryptedIntId;

            var carAccessoriesDetails = await service.GetByIdAsync(id);

            if (carAccessoriesDetails == null)
            {
                ViewBag.ErrorMessage = $"Auto Zubehör ist mit Id = {id} nicht gefunden";
                return View("NotFound");
            }
            else
            {
                //await service.CarAccessoriesDelete(decryptedIntId);

                await service.DeleteAsync(id);
                return RedirectToAction("Index");

            }
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
