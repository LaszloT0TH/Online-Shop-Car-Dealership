using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Data.Service;
using CarDealershipASPNETMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipASPNETMVC.Models
{
    public class CarsService : EntityStringBaseRepository<CarModel>, ICarsService
    {
        private readonly AppDbContext context;
        public CarsService(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<CarDropdownsModel> GetCarsDropdownsValues()
        {
            var response = new CarDropdownsModel()
            {
                Fuels = await context.Fuel.OrderBy(f => f.FuelName).ToListAsync(),
                Gearboxs = await context.Gearbox.OrderBy(g => g.GearboxName).ToListAsync()
            };

            return response;
        }

        public async Task<CarModel> GetCarByIdAsync(string id)
        {
            var carDetails = await context.Cars
                .Include(f => f.Fuels)
                .Include(g => g.Gearbox)
                .FirstOrDefaultAsync(n => n.Id == id);

            return carDetails;
        }

        public async Task UpdateCarAsync(CarEditViewModel data)
        {
            var uptatedCar = await context.Cars.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (uptatedCar != null)
            {
                uptatedCar.Model = data.Model;
                uptatedCar.Color = data.Color;
                uptatedCar.NumberOfSeats = data.NumberOfSeats;
                uptatedCar.YearOfProduction = data.YearOfProduction;
                uptatedCar.FuelId = data.FuelId;
                uptatedCar.GearboxId = data.GearboxId;
                uptatedCar.CubicCapacity = data.CubicCapacity;
                uptatedCar.Mileage = data.Mileage;
                uptatedCar.ChassisNumber = data.ChassisNumber;
                uptatedCar.OwnWeight = data.OwnWeight;
                uptatedCar.EnginePower = data.EnginePower;
                uptatedCar.Sold = data.Sold;
                uptatedCar.NettoPrice = data.NettoPrice;
                uptatedCar.LastUpdateTime = data.LastUpdateTime;
                uptatedCar.PhotoPath = data.PhotoPath;

                await context.SaveChangesAsync();
            }
        }

        public async Task<string> AddNewCarAsync(CarCreateViewModel data)
        {
            var createCar = new CarModel()
            {
                Id = Convert.ToString(Guid.NewGuid())!,
                Model = data.Model,
                Color = data.Color,
                NumberOfSeats = data.NumberOfSeats,
                YearOfProduction = data.YearOfProduction,
                FuelId = data.FuelId,
                GearboxId = data.GearboxId,
                CubicCapacity = data.CubicCapacity,
                Mileage = data.Mileage,
                ChassisNumber = data.ChassisNumber,
                OwnWeight = data.OwnWeight,
                EnginePower = data.EnginePower,
                Sold = data.Sold,
                NettoPrice = data.NettoPrice,
                LastUpdateTime = data.LastUpdateTime,
                PhotoPath = data.PhotoPath
            };

            await context.Cars.AddAsync(createCar);
            await context.SaveChangesAsync();

            return createCar.Id;
        }
    }
}
