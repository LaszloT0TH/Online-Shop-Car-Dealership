using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipASPNETMVC.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// For the DbContext class to be able to do any useful work, it needs an instance of the DbContextOptions class. 
        /// The DbContextOptions instance carries configuration information such as the connection string, database provider to use etc.
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser_SpokenLangues>().HasKey(s_sp => new
            {
                s_sp.UserId,
                s_sp.SpokenLanguesId
            });

            modelBuilder.Entity<ApplicationUser_SpokenLangues>().HasOne(s => s.ApplicationUser)
                .WithMany(s_sp => s_sp.ApplicationUser_SpokenLangues).HasForeignKey(u => u.UserId);

            modelBuilder.Entity<ApplicationUser_SpokenLangues>().HasOne(sp => sp.SpokenLanguesModel)
                .WithMany(s_sp => s_sp.ApplicationUser_SpokenLangues).HasForeignKey(s => s.SpokenLanguesId);

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);


            //Users are stored in the AspNetUsers table
            //Roles are stored in the AspNetRoles table
            //The user and role mapping data are stored in the AspNetUserRoles table
            //This table contains only 2 columns: UserId & RoleId
            //Both are foreign keys
            //The cascading referential integrity constraint allows you to define
            //how Microsoft SQL Server should perform when a user
            //attempts to delete or update a key pointed to by an existing foreign key.
            //Foreign key cascaded DELETE In Entity Framework Core, it is by default
            //Foreign keys of AspNetUserRoles table have cascading DELETE behavior.
            //This means that if a record of the parent table (AspNetRoles) is deleted,
            //then the corresponding records of the child table(AspNetUserRoles) are automatically deleted. Foreign key
            //NO OPERATION ON DELETE
            //What to do if you want to customize this default behavior.
            //We don't want to allow deleting a role if in the child table(AspNetUserRoles)
            //there are rows that point to one of the roles in the parent table(AspNetRoles).
            // Then we need to add a new migration NO OPERATION ON DELETE

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }


        }

        public DbSet<SpokenLanguesModel> SpokenLangues { get; set; }
        public DbSet<ApplicationUser_SpokenLangues> ApplicationUser_Spokens { get; set; }

        public DbSet<OrderStatusModel> OrderStatus { get; set; }
        public DbSet<ShoppingCartStatusModel> ShoppingCartStatus { get; set; }

        public DbSet<CountryModel> Country { get; set; }
        public DbSet<SexModel> Sex { get; set; }


        public DbSet<FuelModel> Fuel { get; set; }
        public DbSet<GearboxModel> Gearbox { get; set; }
        public DbSet<CarModel> Cars { get; set; }


        public DbSet<CarAccessoriesProductGroupModel> CarAccessoriesProductGroup { get; set; }
        public DbSet<CarAccessoriesUnitModel> CarAccessoriesUnit { get; set; }
        public DbSet<CarAccessoriesModel> CarAccessories { get; set; }



        //Orders related tables
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }
        public DbSet<ShoppingCartModel> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItemModel> ShoppingCartItems { get; set; }
        public DbSet<StockReplenishmentListModel> StockReplenishmentList { get; set; }

    }
}
