using Microsoft.EntityFrameworkCore;
using CarDealershipASPNETMVC.ViewModels;
using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data.Service
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext context;

        public AccountService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<AccountDropdownsModel> GetAccountDropdownsValues()
        {
            var response = new AccountDropdownsModel()
            {
                Countries = await context.Country.OrderBy(c => c.CountryName).ToListAsync(),
                Sexs = await context.Sex.OrderBy(s => s.SexName).ToListAsync(),
                SpokenLangues = await context.SpokenLangues.OrderBy(s => s.SpokenLanguesName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateSpokenLanguesAsync(EditUserViewModel data)
        {
            // EN        
            // Remove existing SpokenLangues
            // GE
            // Bestehende SpokenLanguages ​​entfernen
            // HU
            // Meglévő SpokenLangues eltávolítása
            var existingSpokenLanguesDb = context.ApplicationUser_Spokens.Where(u => u.UserId == data.Id).ToList();
            context.ApplicationUser_Spokens.RemoveRange(existingSpokenLanguesDb);
            await context.SaveChangesAsync();

            // Get tbl.SpokenLangues Id Max Value
            //int SPIdMax = Convert.ToInt32(await context.SpokenLangues.OrderByDescending(u => u.Id == null ? 0 : u.Id).FirstOrDefaultAsync());
    
            foreach (var sPLId in data.SpokenLanguesId)
            {
                // EN
                // Add ApplicationUser SpokenLangues tbl.ApplicationUser_SpokenLangues
                // GE
                // ApplicationUser SpokenLangues hinzufügen tbl.ApplicationUser_SpokenLangues
                // HU
                // ApplicationUser SpokenLangues tbl.ApplicationUser_SpokenLangues hozzáadása
                var newApplicationUser_SpokenLangues = new ApplicationUser_SpokenLangues()
                {
                    UserId = data.Id,
                    SpokenLanguesId = sPLId
                };
                await context.ApplicationUser_Spokens.AddAsync(newApplicationUser_SpokenLangues);
            }

            await context.SaveChangesAsync();
        }

        public async Task<List<int>> GetSpokenLangues(string id)
        {
            var response = await context.ApplicationUser_Spokens.Where(u => u.UserId == id).Select(u => u.SpokenLanguesId).ToListAsync();

            return response;
        }
    }
}
