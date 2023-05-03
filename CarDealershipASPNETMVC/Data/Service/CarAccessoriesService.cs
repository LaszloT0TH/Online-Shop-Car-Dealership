using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Data.Service;
using CarDealershipASPNETMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipASPNETMVC.Models
{
    public class CarAccessoriesService : EntityStringBaseRepository<CarAccessoriesModel>, ICarAccessoriesService
    {
        private readonly AppDbContext context;

        public CarAccessoriesService(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<CarAccessoriesDropdownsModel> GetCarAccessoriesDropdownsValues()
        {
            var response = new CarAccessoriesDropdownsModel()
            {
                CarAccessoriesProductGroups = await context.CarAccessoriesProductGroup.OrderBy(capg => capg.CAPGName).ToListAsync(),
                CarAccessoriesUnits = await context.CarAccessoriesUnit.OrderBy(u => u.UnitName).ToListAsync()
            };

            return response;
        }

        public async Task<CarAccessoriesModel> GetCarAccessoriesByIdAsync(string id)
        {
            var carAccessoriesDetails = await context.CarAccessories
                .Include(capg => capg.carAccessoriesProductGroupModel)
                .Include(u => u.carAccessoriesUnitModel)
                .FirstOrDefaultAsync(n => n.Id == id);

            return carAccessoriesDetails!; // ! = https://www.youtube.com/watch?v=H2sfNnB1QAU
        }

        public async Task UpdateCarAccessoriesAsync(CarAccessoriesEditViewModel data)
        {
            var uptatedCarAccessories = await context.CarAccessories.FirstOrDefaultAsync(n => n.Id == data.Id);

            if(uptatedCarAccessories != null)
            {
                uptatedCarAccessories.ProductName = data.ProductName;
                uptatedCarAccessories.CAPGId = data.CAPGId;
                uptatedCarAccessories.QuantityOfStock = data.QuantityOfStock;
                uptatedCarAccessories.MinimumStockQuantity = data.MinimumStockQuantity;
                uptatedCarAccessories.NetSellingPrice = data.NetSellingPrice;
                uptatedCarAccessories.SalesUnit = data.SalesUnit;
                uptatedCarAccessories.UnitNameId = data.UnitNameId;
                uptatedCarAccessories.Brand = data.Brand;
                uptatedCarAccessories.CreationDate = data.CreationDate;
                uptatedCarAccessories.Description = data.Description;
                uptatedCarAccessories.Version = data.Version;
                uptatedCarAccessories.LastUpdateTime = data.LastUpdateTime;
                uptatedCarAccessories.PhotoPath = data.PhotoPath;

                await context.SaveChangesAsync();
            }
        }

        public async Task<string> AddNewCarAccessoriesAsync(CarAccessoriesCreateViewModel data)
        {
            var createCarAccessories = new CarAccessoriesModel()
            {
                Id = Convert.ToString(Guid.NewGuid())!,
                ProductName = data.ProductName,
                CAPGId = data.CAPGId,
                QuantityOfStock = data.QuantityOfStock,
                MinimumStockQuantity = data.MinimumStockQuantity,
                NetSellingPrice = data.NetSellingPrice,
                SalesUnit = data.SalesUnit,
                UnitNameId = data.UnitNameId,
                Brand = data.Brand,
                CreationDate = data.CreationDate,
                Description = data.Description,
                Version = data.Version,
                LastUpdateTime = data.LastUpdateTime,
                PhotoPath = data.PhotoPath
            };

            await context.CarAccessories.AddAsync(createCarAccessories);
            await context.SaveChangesAsync();

            return createCarAccessories.Id;
        }
    }
}
