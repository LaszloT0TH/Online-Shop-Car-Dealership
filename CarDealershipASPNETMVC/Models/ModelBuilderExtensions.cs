using CarDealershipASPNETMVC.Data.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipASPNETMVC.Models
{
    // https://stackoverflow.com/questions/34343599/how-to-seed-users-and-roles-with-code-first-migration-using-identity-asp-net-cor
    // https://stackoverflow.com/questions/52708439/how-do-i-seed-user-roles-in-identity-asp-net-core-2-1-identity-scaffolded/52709902#52709902
    // https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
    /// <summary>
    /// EN
    /// To keep the DbContext class clean, you may move the seeding code from the DbContext class 
    /// into an extension method on the ModelBuilder class.
    /// GE
    /// Um die DbContext-Klasse sauber zu halten, können Sie den Seeding-Code aus der DbContext-Klasse verschieben
    /// in eine Erweiterungsmethode der ModelBuilder-Klasse.
    /// HU
    /// A DbContext osztály tisztán tartásához áthelyezheti a betöltőkódot a DbContext osztályból
    /// a ModelBuilder osztály egy bővítménymetódusába.
    /// </summary>
    public static class ModelBuilderExtensions
    {
       
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Application Admin & User Data
            // Seed Roles
            // Table [dbo].[AspNetRoles]
            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" },
                new IdentityRole { Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole { Name = "Salasperson", NormalizedName = "SALESPERSON" }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // -----------------------------------------------------------------------------

            // Seed Users

            var passwordHasher = new PasswordHasher<ApplicationUser>();

            // Table [dbo].[AspNetUsers]
            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                // imporant: don't forget NormalizedUserName, NormalizedEmail 
                new ApplicationUser
                {
                    UserName = "admin@car.com",
                    NormalizedUserName = "ADMIN@CAR.COM",
                    Email = "admin@car.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "AppAdmin",
                    SexId = 1,
                    Street = "Street",
                    HouseNumber = "HouseNumber8",
                    PostalCode = 1010,
                    Location = "Wien",
                    CountryId = 32,
                    DateOfBirth = Convert.ToDateTime("1982-09-08 15:09:12.1234567")
                },

                new ApplicationUser
                {
                    UserName = "user@car.com",
                    NormalizedUserName = "USER@CAR.COM",
                    Email = "user@car.com",
                    EmailConfirmed = true,
                    FirstName = "User",
                    LastName = "AppUser",
                    SexId = 1,
                    Street = "Street",
                    HouseNumber = "HouseNumber10",
                    PostalCode = 1010,
                    Location = "Wien",
                    CountryId = 32,
                    DateOfBirth = Convert.ToDateTime("2000-10-20 15:09:12.1234567")
                },

                new ApplicationUser
                {
                    UserName = "manager@car.com",
                    NormalizedUserName = "MANAGER@CAR.COM",
                    Email = "manager@car.com",
                    EmailConfirmed = true,
                    FirstName = "Manager",
                    LastName = "AppManager",
                    SexId = 1,
                    Street = "Street",
                    HouseNumber = "HouseNumber2",
                    PostalCode = 1010,
                    Location = "Wien",
                    CountryId = 32,
                    DateOfBirth = Convert.ToDateTime("1990-10-20 15:09:12.1234567")
                },

                new ApplicationUser
                {
                    UserName = "salasperson@car.com",
                    NormalizedUserName = "SALESPERSON@CAR.COM",
                    Email = "salasperson@car.com",
                    EmailConfirmed = true,
                    FirstName = "Salasperson",
                    LastName = "AppSalasperson",
                    SexId = 1,
                    Street = "Street",
                    HouseNumber = "HouseNumber50",
                    PostalCode = 1010,
                    Location = "Wien",
                    CountryId = 32,
                    DateOfBirth = Convert.ToDateTime("2003-10-20 15:09:12.1234567")
                },
            };


            modelBuilder.Entity<ApplicationUser>().HasData(users);

            ///----------------------------------------------------

            // Seed UserRoles


            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

            // Add Password For All Users

            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "Admin.123");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "User.123");
            users[2].PasswordHash = passwordHasher.HashPassword(users[2], "Manager.123");
            users[3].PasswordHash = passwordHasher.HashPassword(users[3], "Salasperson.123");

            // Table [dbo].[AspNetRoles]
            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == "Admin").Id
            });

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[1].Id,
                RoleId = roles.First(q => q.Name == "User").Id
            });

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[2].Id,
                RoleId = roles.First(q => q.Name == "Manager").Id
            });

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[3].Id,
                RoleId = roles.First(q => q.Name == "Salasperson").Id
            });


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);


            // Table [dbo].[AspNetUserClaims]
            List <IdentityUserClaim<string>> claims = new List<IdentityUserClaim<string>>();
            //  for Admin
            claims.Add(new IdentityUserClaim<string>
            {
                Id = 1,
                UserId = users[0].Id,
                ClaimType = "Create Role",
                ClaimValue = "true"
            });
            claims.Add(new IdentityUserClaim<string>
            {
                Id = 2,
                UserId = users[0].Id,
                ClaimType = "Edit Role",
                ClaimValue = "true"
            });
            claims.Add(new IdentityUserClaim<string>
            {
                Id = 3,
                UserId = users[0].Id,
                ClaimType = "Delete Role",
                ClaimValue = "true"
            });
            //  for Salasperson
            claims.Add(new IdentityUserClaim<string>
            {
                Id = 4,
                UserId = users[3].Id,
                ClaimType = "Create Role",
                ClaimValue = "true"
            });
            //  for Manager
            claims.Add(new IdentityUserClaim<string>
            {
                Id = 5,
                UserId = users[2].Id,
                ClaimType = "Create Role",
                ClaimValue = "true"
            });
            claims.Add(new IdentityUserClaim<string>
            {
                Id = 6,
                UserId = users[2].Id,
                ClaimType = "Edit Role",
                ClaimValue = "true"
            });
            claims.Add(new IdentityUserClaim<string>
            {
                Id = 7,
                UserId = users[2].Id,
                ClaimType = "Delete Role",
                ClaimValue = "true"
            });

            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(claims);

            // Table [dbo].[OrderItems]
            //List <OrderItemModel> orderItem = new List<OrderItemModel>()
            //{
            //    new OrderItemModel
            //    {
            //        OrderId = 1,
            //        SalesPersonId = users[0].Id
            //    }
            //};

            //modelBuilder.Entity<OrderItemModel>().HasData(orderItem);

            #endregion

            #region Shopping Cart Status Data
            modelBuilder.Entity<ShoppingCartStatusModel>().HasData(
                    new ShoppingCartStatusModel
                    {
                        Id = 1,
                        ShoppingCartStatusName = "Im Einkaufswagen"
                    },
                    new ShoppingCartStatusModel
                    {
                        Id = 2,
                        ShoppingCartStatusName = "Bestellt"
                    },
                    new ShoppingCartStatusModel
                    {
                        Id = 3,
                        ShoppingCartStatusName = "Für später gespeichert"
                    },
                    new ShoppingCartStatusModel
                    {
                        Id = 4,
                        ShoppingCartStatusName = "Unterwegs"
                    },
                    new ShoppingCartStatusModel
                    {
                        Id = 5,
                        ShoppingCartStatusName = "Zugestellt"
                    }
                );
            #endregion

            #region Spoken Langues Data
            modelBuilder.Entity<SpokenLanguesModel>().HasData(
                    new SpokenLanguesModel
                    {
                        Id = 1,
                        SpokenLanguesName = "Albanisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 2,
                        SpokenLanguesName = "Arabisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 3,
                        SpokenLanguesName = "Deutsch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 4,
                        SpokenLanguesName = "Englisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 5,
                        SpokenLanguesName = "Finnisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 6,
                        SpokenLanguesName = "Französisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 7,
                        SpokenLanguesName = "Griechisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 8,
                        SpokenLanguesName = "Italienisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 9,
                        SpokenLanguesName = "Irisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 10,
                        SpokenLanguesName = "Niederländisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 11,
                        SpokenLanguesName = "Ukrainisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 12,
                        SpokenLanguesName = "Ungarisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 13,
                        SpokenLanguesName = "Portugiesisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 14,
                        SpokenLanguesName = "Rumänisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 15,
                        SpokenLanguesName = "Russisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 16,
                        SpokenLanguesName = "Schwedisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 17,
                        SpokenLanguesName = "Slowakisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 18,
                        SpokenLanguesName = "Slowenisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 19,
                        SpokenLanguesName = "Weißrussisch"
                    },
                    new SpokenLanguesModel
                    {
                        Id = 20,
                        SpokenLanguesName = "Andere Sprache"
                    }
                );
            #endregion

            #region Spoken Langues Level Data
            //modelBuilder.Entity<SpokenLanguesLevelModel>().HasData(
            //        new SpokenLanguesLevelModel
            //        {
            //            Id = 1,
            //            SpokenLanguesLevelName = "A1"
            //        },
            //        new SpokenLanguesLevelModel
            //        {
            //            Id = 2,
            //            SpokenLanguesLevelName = "A2"
            //        },
            //        new SpokenLanguesLevelModel
            //        {
            //            Id = 3,
            //            SpokenLanguesLevelName = "B1"
            //        },
            //        new SpokenLanguesLevelModel
            //        {
            //            Id = 4,
            //            SpokenLanguesLevelName = "B2"
            //        },
            //        new SpokenLanguesLevelModel
            //        {
            //            Id = 5,
            //            SpokenLanguesLevelName = "C1"
            //        },
            //        new SpokenLanguesLevelModel
            //        {
            //            Id = 6,
            //            SpokenLanguesLevelName = "C2 oder Muttersprache"
            //        }
            //    );
            #endregion

            #region Admin Spoken Langues

            modelBuilder.Entity<ApplicationUser_SpokenLangues>().HasData(
                    new ApplicationUser_SpokenLangues
                    {
                        UserId = users[0].Id,
                        SpokenLanguesId = 3
                    },
                    new ApplicationUser_SpokenLangues
                    {
                        UserId = users[0].Id,
                        SpokenLanguesId = 4
                    },
                    new ApplicationUser_SpokenLangues
                    {
                        UserId = users[0].Id,
                        SpokenLanguesId = 6
                    },
                    new ApplicationUser_SpokenLangues
                    {
                        UserId = users[0].Id,
                        SpokenLanguesId = 12
                    });
            #endregion

            #region Orderstatus Data
            modelBuilder.Entity<OrderStatusModel>().HasData(
                    new OrderStatusModel
                    {
                        Id = 1,
                        OrderStatusName = "Ausstehend"
                    },
                    new OrderStatusModel
                    {
                        Id = 2,
                        OrderStatusName = "Verarbeitung"
                    },
                    new OrderStatusModel
                    {
                        Id = 3,
                        OrderStatusName = "Abgelehnt"
                    },
                    new OrderStatusModel
                    {
                        Id = 4,
                        OrderStatusName = "Abgeschlossen"
                    }
                );
            #endregion

            #region Sex Data
            modelBuilder.Entity<SexModel>().HasData(
                    new SexModel
                    {
                        Id = 1,
                        SexName = "männlich"
                    },
                    new SexModel
                    {
                        Id = 2,
                        SexName = "weiblich"
                    },
                    new SexModel
                    {
                        Id = 3,
                        SexName = "divers"
                    },
                    new SexModel
                    {
                        Id = 4,
                        SexName = "inter"
                    },
                    new SexModel
                    {
                        Id = 5,
                        SexName = "offen"
                    },
                    new SexModel
                    {
                        Id = 6,
                        SexName = "kein Eintrag"
                    }
                );
            #endregion

            #region Country Data
            modelBuilder.Entity<CountryModel>().HasData(
                    new CountryModel
                    {
                        Id = 1,
                        CountryName = "Albanien",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 2,
                        CountryName = "Andorra",
                        CountryTaxPercentageValue = 4.5
                    },
                    new CountryModel
                    {
                        Id = 3,
                        CountryName = "Armenia",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 4,
                        CountryName = "Belarus",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 5,
                        CountryName = "Belgien",
                        CountryTaxPercentageValue = 21
                    },
                    new CountryModel
                    {
                        Id = 6,
                        CountryName = "Bosnien und Herzegowina",
                        CountryTaxPercentageValue = 17
                    },
                    new CountryModel
                    {
                        Id = 7,
                        CountryName = "Bulgarien",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 8,
                        CountryName = "Dänemark",
                        CountryTaxPercentageValue = 25
                    },
                    new CountryModel
                    {
                        Id = 9,
                        CountryName = "Deutschland",
                        CountryTaxPercentageValue = 19
                    },
                    new CountryModel
                    {
                        Id = 10,
                        CountryName = "Estland",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 11,
                        CountryName = "Finnland",
                        CountryTaxPercentageValue = 24
                    },
                    new CountryModel
                    {
                        Id = 12,
                        CountryName = "Frankreich",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 13,
                        CountryName = "Georgien",
                        CountryTaxPercentageValue = 18
                    },
                    new CountryModel
                    {
                        Id = 14,
                        CountryName = "Griechenland",
                        CountryTaxPercentageValue = 24
                    },
                    new CountryModel
                    {
                        Id = 15,
                        CountryName = "Irland",
                        CountryTaxPercentageValue = 23
                    },
                    new CountryModel
                    {
                        Id = 16,
                        CountryName = "Island",
                        CountryTaxPercentageValue = 24
                    },
                    new CountryModel
                    {
                        Id = 17,
                        CountryName = "Italien",
                        CountryTaxPercentageValue = 22
                    },
                    new CountryModel
                    {
                        Id = 18,
                        CountryName = "Kasachstan",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 19,
                        CountryName = "Kosovo",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 20,
                        CountryName = "Kroatien",
                        CountryTaxPercentageValue = 25
                    },
                    new CountryModel
                    {
                        Id = 21,
                        CountryName = "Lettland",
                        CountryTaxPercentageValue = 21
                    },
                    new CountryModel
                    {
                        Id = 22,
                        CountryName = "Liechtenstein",
                        CountryTaxPercentageValue = 8
                    },
                    new CountryModel
                    {
                        Id = 23,
                        CountryName = "Litauen",
                        CountryTaxPercentageValue = 21
                    },
                    new CountryModel
                    {
                        Id = 24,
                        CountryName = "Luxemburg",
                        CountryTaxPercentageValue = 17
                    },
                    new CountryModel
                    {
                        Id = 25,
                        CountryName = "Malta",
                        CountryTaxPercentageValue = 18
                    },
                    new CountryModel
                    {
                        Id = 26,
                        CountryName = "Moldau",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 27,
                        CountryName = "Monaco",
                        CountryTaxPercentageValue = 0
                    },
                    new CountryModel
                    {
                        Id = 28,
                        CountryName = "Montenegro",
                        CountryTaxPercentageValue = 21
                    },
                    new CountryModel
                    {
                        Id = 29,
                        CountryName = "Niederlande",
                        CountryTaxPercentageValue = 21
                    },
                    new CountryModel
                    {
                        Id = 30,
                        CountryName = "Nordmazedonien",
                        CountryTaxPercentageValue = 18
                    },
                    new CountryModel
                    {
                        Id = 31,
                        CountryName = "Norwegen",
                        CountryTaxPercentageValue = 25
                    },
                    new CountryModel
                    {
                        Id = 32,
                        CountryName = "Österreich",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 33,
                        CountryName = "Polen",
                        CountryTaxPercentageValue = 23
                    },
                    new CountryModel
                    {
                        Id = 34,
                        CountryName = "Portugal",
                        CountryTaxPercentageValue = 23
                    },
                    new CountryModel
                    {
                        Id = 35,
                        CountryName = "Rumänien",
                        CountryTaxPercentageValue = 19
                    },
                    new CountryModel
                    {
                        Id = 36,
                        CountryName = "Russland",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 37,
                        CountryName = "San Marino",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 38,
                        CountryName = "Schweden",
                        CountryTaxPercentageValue = 25
                    },
                    new CountryModel
                    {
                        Id = 39,
                        CountryName = "Schweiz",
                        CountryTaxPercentageValue = 8
                    },
                    new CountryModel
                    {
                        Id = 40,
                        CountryName = "Serbien",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 41,
                        CountryName = "Slowakei",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 42,
                        CountryName = "Slowenien",
                        CountryTaxPercentageValue = 21
                    },
                    new CountryModel
                    {
                        Id = 43,
                        CountryName = "Tschechien",
                        CountryTaxPercentageValue = 21
                    },
                    new CountryModel
                    {
                        Id = 44,
                        CountryName = "Türkei",
                        CountryTaxPercentageValue = 18
                    },
                    new CountryModel
                    {
                        Id = 45,
                        CountryName = "Ukraine",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 46,
                        CountryName = "Ungarn",
                        CountryTaxPercentageValue = 27
                    },
                    new CountryModel
                    {
                        Id = 47,
                        CountryName = "Vereinigtes Königreich",
                        CountryTaxPercentageValue = 20
                    },
                    new CountryModel
                    {
                        Id = 48,
                        CountryName = "Zypern",
                        CountryTaxPercentageValue = 19
                    },
                    new CountryModel
                    {
                        Id = 49,
                        CountryName = "Andere",
                        CountryTaxPercentageValue = 20
                    }
                );
            #endregion

            #region Cars Data
            modelBuilder.Entity<FuelModel>().HasData(
                    new FuelModel
                    {
                        Id = 1,
                        FuelName = "Strom"
                    },
                    new FuelModel
                    {
                        Id = 2,
                        FuelName = "Benzin"
                    },
                    new FuelModel
                    {
                        Id = 3,
                        FuelName = "Diesel"
                    },
                    new FuelModel
                    {
                        Id = 4,
                        FuelName = "Erdgas"
                    }
                );

            modelBuilder.Entity<GearboxModel>().HasData(
                    new GearboxModel
                    {
                        Id = 1,
                        GearboxName = "manual"
                    },
                    new GearboxModel
                    {
                        Id = 2,
                        GearboxName = "automat"
                    },
                    new GearboxModel
                    {
                        Id = 3,
                        GearboxName = "Es gibt keine"
                    }
                );

            modelBuilder.Entity<CarModel>().HasData(
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Ford",
                        Color = "silber",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 1765,
                        Mileage = 287355,
                        ChassisNumber = "XFOW3AT152HhWNS9FF",
                        EnginePower = 85,
                        OwnWeight = 1270,
                        Sold = false,
                        NettoPrice = 2700,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Opel",
                        Color = "Weis",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2014-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 1670,
                        Mileage = 298603,
                        ChassisNumber = "XFOW3AT612G54WNS9FF",
                        EnginePower = 75,
                        OwnWeight = 1170,
                        Sold = false,
                        NettoPrice = 3500,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Mercedes",
                        Color = "Grün",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2012-10-20 15:09:12.1234567"),
                        FuelId = 2,
                        GearboxId = 1,
                        CubicCapacity = 2002,
                        Mileage = 350355,
                        ChassisNumber = "XFOW323EG56692GWNS9FF",
                        EnginePower = 110,
                        OwnWeight = 1670,
                        Sold = false,
                        NettoPrice = 5100,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "BMW",
                        Color = "Blau",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2016-10-20 15:09:12.1234567"),
                        FuelId = 2,
                        GearboxId = 2,
                        CubicCapacity = 2500,
                        Mileage = 156478,
                        ChassisNumber = "XFOW5GH125GW65NS9FF",
                        EnginePower = 150,
                        OwnWeight = 1450,
                        Sold = false,
                        NettoPrice = 6200,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Suzuki",
                        Color = "Gelb",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2018-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 1040,
                        Mileage = 150355,
                        ChassisNumber = "XFOW3AT12563GWNS9FF",
                        EnginePower = 61,
                        OwnWeight = 1120,
                        Sold = false,
                        NettoPrice = 4500,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Citoen",
                        Color = "Schwarz",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2018-10-20 15:09:12.1234567"),
                        FuelId = 2,
                        GearboxId = 1,
                        CubicCapacity = 950,
                        Mileage = 84335,
                        ChassisNumber = "XFOW3AT98573GWNS9FF",
                        EnginePower = 54,
                        OwnWeight = 988,
                        Sold = false,
                        NettoPrice = 5300,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Volkswagen",
                        Color = "Pink",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2020-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 2,
                        CubicCapacity = 1400,
                        Mileage = 25355,
                        ChassisNumber = "XFOW3AT345253GWNS9FF",
                        EnginePower = 66,
                        OwnWeight = 972,
                        Sold = false,
                        NettoPrice = 6250,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Ford",
                        Color = "grau",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2003-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 1783,
                        Mileage = 289000,
                        ChassisNumber = "wf0utk1wsd421",
                        EnginePower = 85,
                        OwnWeight = 1300,
                        Sold = false,
                        NettoPrice = 1800,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Tesla",
                        Color = "rot",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2019-10-20 15:09:12.1234567"),
                        FuelId = 1,
                        GearboxId = 3,
                        CubicCapacity = 1900,
                        Mileage = 0,
                        ChassisNumber = "we36xsgvf2656dfs2",
                        EnginePower = 130,
                        OwnWeight = 1000,
                        Sold = false,
                        NettoPrice = 28000,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Skoda",
                        Color = "Weis",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2003-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 1420,
                        Mileage = 214563,
                        ChassisNumber = "vs3daffew8sdf3",
                        EnginePower = 57,
                        OwnWeight = 950,
                        Sold = false,
                        NettoPrice = 1500,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Ford",
                        Color = "grau",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2004-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 1783,
                        Mileage = 298003,
                        ChassisNumber = "wf0utk4wsd4ht21",
                        EnginePower = 85,
                        OwnWeight = 1320,
                        Sold = false,
                        NettoPrice = 1850,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Tesla",
                        Color = "rot",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2019-10-20 15:09:12.1234567"),
                        FuelId = 1,
                        GearboxId = 3,
                        CubicCapacity = 0,
                        Mileage = 298603,
                        ChassisNumber = "we36xsgvf6556dfseh2",
                        EnginePower = 122,
                        OwnWeight = 989,
                        Sold = false,
                        NettoPrice = 35000,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Skoda",
                        Color = "weiss",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2002-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 1408,
                        Mileage = 214563,
                        ChassisNumber = "vsda6ffertew8sdf3",
                        EnginePower = 56,
                        OwnWeight = 950,
                        Sold = false,
                        NettoPrice = 1500,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Audi",
                        Color = "pink",
                        NumberOfSeats = 3,
                        YearOfProduction = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        FuelId = 2,
                        GearboxId = 1,
                        CubicCapacity = 2300, 
                        Mileage = 50000,
                        ChassisNumber = "srg55dfg721dfs4",
                        EnginePower = 190,
                        OwnWeight = 1400,
                        Sold = false,
                        NettoPrice = 5299,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Bmw",
                        Color = "schwarz",
                        NumberOfSeats = 3,
                        YearOfProduction = Convert.ToDateTime("2010-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 4000,
                        Mileage = 160000,
                        ChassisNumber = "dsfg8ds5fssgfsd65",
                        EnginePower = 210,
                        OwnWeight = 1640,
                        Sold = false,
                        NettoPrice = 4500,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Ford",
                        Color = "lila",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2004-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 3726,
                        Mileage = 86958,
                        ChassisNumber = "wf0utkwsd9436",
                        EnginePower = 89,
                        OwnWeight = 1550,
                        Sold = false,
                        NettoPrice = 2300,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Tesla",
                        Color = "grau",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2017-10-20 15:09:12.1234567"),
                        FuelId = 1,
                        GearboxId = 3,
                        CubicCapacity = 0,
                        Mileage = 59676,
                        ChassisNumber = "we36x10sgvf656dfs7",
                        EnginePower = 125,
                        OwnWeight = 1010,
                        Sold = false,
                        NettoPrice = 12999,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Skoda",
                        Color = "hellrot",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("1997-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 2,
                        CubicCapacity = 4693,
                        Mileage = 214563,
                        ChassisNumber = "vsda11ffew8sdf8",
                        EnginePower = 44,
                        OwnWeight = 1750,
                        Sold = false,
                        NettoPrice = 2399,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Audi",
                        Color = "Weiss",
                        NumberOfSeats = 2,
                        YearOfProduction = Convert.ToDateTime("1999-10-20 15:09:12.1234567"),
                        FuelId = 2,
                        GearboxId = 1,
                        CubicCapacity = 5177,
                        Mileage = 512833,
                        ChassisNumber = "srg55d12fg21dfs9",
                        EnginePower = 120,
                        OwnWeight = 1852,
                        Sold = false,
                        NettoPrice = 3000,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Bmw",
                        Color = "dunkelpink",
                        NumberOfSeats = 2,
                        YearOfProduction = Convert.ToDateTime("2005-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 5660,
                        Mileage = 160260,
                        ChassisNumber = "dsfgd13s5fssgfsd710",
                        EnginePower = 142,
                        OwnWeight = 1950,
                        Sold = false,
                        NettoPrice = 2800,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Ford",
                        Color = "schwarz",
                        NumberOfSeats = 2,
                        YearOfProduction = Convert.ToDateTime("2007-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 6143,
                        Mileage = 267003,
                        ChassisNumber = "wf0utk14wsd4411",
                        EnginePower = 103,
                        OwnWeight = 2050,
                        Sold = false,
                        NettoPrice = 4299,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Polo",
                        Color = "lila",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2001-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 1795,
                        Mileage = 356197,
                        ChassisNumber = "wf0utkwsd154312",
                        EnginePower = 86,
                        OwnWeight = 1350,
                        Sold = false,
                        NettoPrice = 2899,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "A4",
                        Color = "grau",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2019-10-20 15:09:12.1234567"),
                        FuelId = 1,
                        GearboxId = 3,
                        CubicCapacity = 1900,
                        Mileage = 10591,
                        ChassisNumber = "we36xsgvf61656dfs13",
                        EnginePower = 132,
                        OwnWeight = 1315,
                        Sold = false,
                        NettoPrice = 5599,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Vectra",
                        Color = "hellrot",
                        NumberOfSeats = 3,
                        YearOfProduction = Convert.ToDateTime("2003-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 1988,
                        Mileage = 311713,
                        ChassisNumber = "vsdaf17few8sdf14",
                        EnginePower = 95,
                        OwnWeight = 1950,
                        Sold = false,
                        NettoPrice = 2800,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Saxo",
                        Color = "weiss",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2015-10-20 15:09:12.1234567"),
                        FuelId = 2,
                        GearboxId = 1,
                        CubicCapacity = 2315,
                        Mileage = 120890,
                        ChassisNumber = "srg55dfg2118dfs15",
                        EnginePower = 190,
                        OwnWeight = 1406,
                        Sold = false,
                        NettoPrice = 4800,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Civic",
                        Color = "dunkelpink",
                        NumberOfSeats = 5,
                        YearOfProduction = Convert.ToDateTime("2010-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 4133,
                        Mileage = 306806,
                        ChassisNumber = "dsfgds195fssgfsd716",
                        EnginePower = 210,
                        OwnWeight = 1620,
                        Sold = false,
                        NettoPrice = 3900,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Primera",
                        Color = "schwarz",
                        NumberOfSeats = 2,
                        YearOfProduction = Convert.ToDateTime("2004-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 3726,
                        Mileage = 204353,
                        ChassisNumber = "wf0utkw20sd4417",
                        EnginePower = 89,
                        OwnWeight = 1550,
                        Sold = false,
                        NettoPrice = 6600,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Passat",
                        Color = "weiss",
                        NumberOfSeats = 3,
                        YearOfProduction = Convert.ToDateTime("1997-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 4693,
                        Mileage = 9944,
                        ChassisNumber = "vsdaffew8s22df19",
                        EnginePower = 44,
                        OwnWeight = 1778,
                        Sold = false,
                        NettoPrice = 5900,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "C Class",
                        Color = "pink",
                        NumberOfSeats = 2,
                        YearOfProduction = Convert.ToDateTime("1999-10-20 15:09:12.1234567"),
                        FuelId = 2,
                        GearboxId = 1,
                        CubicCapacity = 5196,
                        Mileage = 363636,
                        ChassisNumber = "srg55dfg2123dfs20",
                        EnginePower = 122,
                        OwnWeight = 1860,
                        Sold = false,
                        NettoPrice = 2900,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        Model = "Mondeo",
                        Color = "schwarz",
                        NumberOfSeats = 2,
                        YearOfProduction = Convert.ToDateTime("2005-10-20 15:09:12.1234567"),
                        FuelId = 3,
                        GearboxId = 1,
                        CubicCapacity = 5660,
                        Mileage = 309455,
                        ChassisNumber = "dsfgds5fssgf24sd821",
                        EnginePower = 142,
                        OwnWeight = 1667,
                        Sold = false,
                        NettoPrice = 3999,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    }
                );
            #endregion

            #region Car Accessories Data
            modelBuilder.Entity<CarAccessoriesProductGroupModel>().HasData(
                    new CarAccessoriesProductGroupModel
                    {
                        Id = 1,
                        CAPGName = "Autoinnenraum"
                    },
                    new CarAccessoriesProductGroupModel
                    {
                        Id = 2,
                        CAPGName = "Autoreinigungs"
                    },
                    new CarAccessoriesProductGroupModel
                    {
                        Id = 3,
                        CAPGName = "Winter-Autozubehör"
                    },
                    new CarAccessoriesProductGroupModel
                    {
                        Id = 4,
                        CAPGName = "Straßennotfälle und Erste Hilfe"
                    },
                    new CarAccessoriesProductGroupModel
                    {
                        Id = 5,
                        CAPGName = "Zubehör für Autotelefone"
                    },
                    new CarAccessoriesProductGroupModel
                    {
                        Id = 6,
                        CAPGName = "Schutzausrüstung"
                    },
                    new CarAccessoriesProductGroupModel
                    {
                        Id = 7,
                        CAPGName = "Unterhaltung im Auto"
                    }
                );

            modelBuilder.Entity<CarAccessoriesUnitModel>().HasData(
                    new CarAccessoriesUnitModel
                    {
                        Id = 1,
                        UnitName = "€uro/Stück"
                    },
                    new CarAccessoriesUnitModel
                    {
                        Id = 2,
                        UnitName = "€uro/Liter"
                    },
                    new CarAccessoriesUnitModel
                    {
                        Id = 3,
                        UnitName = "€uro/Kilogram"
                    }
                );

            modelBuilder.Entity<CarAccessoriesModel>().HasData(
                    // Autoinnenraum
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Sitzbezüge",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 16.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "LIONSTRONG",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Sitzbezüge Sitzschoner für Autositze, Sitzbezug Werkstatt Auto, " +
                        "universal Autositzschoner, Sitzbezüge, wasserdichter Stoff (Polyester)",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Gepäckraumschalen",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 49.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "M MOTOS",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Kofferraumwanne Kofferraummatte passt für Audi Q3 II, " +
                        "unterer Kofferraumboden 2018 Verbessern Sie Ihren Reisekomfort mit Antirutschmatte Auto",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Kühlbox",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 62.84,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "AEG",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "AEG Automotive Thermoelektrische Kühl- und Warmhaltebox KK 14 Liter, 12 Volt für Auto",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Lenkradschutz",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 29.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Oneil",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Upgrade4cars Lenkradbezug Rot Schwarz Leder Optik Lenkradschutz in Universal Größe 37-39 cm " +
                        "Lenkradhülle für Sommer & Winter Autozubehör Innenraum",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Lenkschloss",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 89,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Oneil",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Lenkschloss",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Kühltasche",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 17.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Oneil",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Kühltasche",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Auto-Staubsauger",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 49.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Oneil",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Auto-Staubsauger",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Windschutzscheibendecke",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 18.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Oneil",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Windschutzscheibendecke",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Autositzschutz",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 22.97,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Oneil",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Upgrade4cars Autositzschutz",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Nackenkissen",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 29.95,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Oneil",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Upgrade4cars Nackenkissen",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Anti-Rutsch-Armaturenbrett",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 10.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Oneil",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Upgrade4cars Anti-Rutsch-Armaturenbrett",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Beheizter Sitzbezug",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 37.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Oneil",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Upgrade4cars Beheizter Sitzbezug",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    }, 
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Armlehne",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 20.50,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Oneil",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Upgrade4cars Armlehne Autozubehör Innenraum",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Fahrradträger",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 278.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Oneil",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Upgrade4cars Fahrradträger",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Kindersitz",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 142.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Oneil",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Upgrade4cars Kindersitz Autozubehör Innenraum",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Nummernschildhalterung",
                        CAPGId = 1,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 23.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "GENMAG",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Nummernschildhalterung Autozubehör",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },


                    // Autoreinigungs
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Hirschleder Schal",
                        CAPGId = 2,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 20.90,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "LINDENMANN",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Hirschleder Schal Autozubehör Innenraum",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Mikrofasertücher",
                        CAPGId = 2,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 14.99,
                        SalesUnit = 5,
                        UnitNameId = 1,
                        Brand = "GLART",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Mikrofasertücher Autozubehör",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Schwämme",
                        CAPGId = 2,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 8.95,
                        SalesUnit = 5,
                        UnitNameId = 1,
                        Brand = "SONAX",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Schwämme",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Auto Lufterfrischer",
                        CAPGId = 2,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 59.95,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Febreze",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Auto Lufterfrischer Autozubehör Innenraum",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Auto Luftentfeuchter",
                        CAPGId = 2,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 19.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "LICARGO",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Auto Luftentfeuchter Autozubehör Innenraum",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Reinigungsbürste",
                        CAPGId = 2,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 13.99,
                        SalesUnit = 3,
                        UnitNameId = 1,
                        Brand = "SONAX",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Reinigungsbürste Autozubehör",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Gummiwischer",
                        CAPGId = 2,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 32.99,
                        SalesUnit = 2,
                        UnitNameId = 1,
                        Brand = "COFIT",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Gummiwischer Autozubehör",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Hochdruckreiniger",
                        CAPGId = 2,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 78.82,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "BOSCH",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Hochdruckreiniger Autozubehör",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Händedesinfektionsmittel",
                        CAPGId = 2,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 20.90,
                        SalesUnit = 0.5,
                        UnitNameId = 2,
                        Brand = "SONAX",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Händedesinfektionsmittel",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Papierhandtuch",
                        CAPGId = 2,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 41.95,
                        SalesUnit = 8,
                        UnitNameId = 1,
                        Brand = "Tork",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Papierhandtuch Autozubehör",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Autowaschbürste",
                        CAPGId = 2,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 13.99,
                        SalesUnit = 2,
                        UnitNameId = 1,
                        Brand = "WillingHeart",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Autowaschbürste Autozubehör",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Felgenreinigungsbürste",
                        CAPGId = 2,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 19.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "LICARGO",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Felgenreinigungsbürste Autozubehör",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Reinigungstücher",
                        CAPGId = 2,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 8.32,
                        SalesUnit = 3,
                        UnitNameId = 1,
                        Brand = "SONAX",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Reinigungstücher Autozubehör",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },


                    // Winter-Autozubehör

                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Glasreiniger",
                        CAPGId = 3,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 29.99,
                        SalesUnit = 0.7,
                        UnitNameId = 1,
                        Brand = "LICARGO",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Glasreiniger Autozubehör",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Eiskratzer",
                        CAPGId = 3,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 11.98,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Oneil",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Eiskratzer Autozubehör",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Schneeketten",
                        CAPGId = 3,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 107.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "MICHELIN",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Schneeketten Autozubehör",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Dachbox",
                        CAPGId = 3,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 76.49,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "NABIYE",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Dachbox Autozubehör",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Skitasche",
                        CAPGId = 3,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 8.32,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Brubaker",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Skitasche Autozubehör",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },


                    // Straßennotfälle und Erste Hilfe

                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Arbeitshandschuhe",
                        CAPGId = 4,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 19.98,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Uvex",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Arbeitshandschuhe",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Erste-Hilfe-Kit für das Auto",
                        CAPGId = 4,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 22.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Oneil",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Erste-Hilfe-Kit für das Auto",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Warndreieck",
                        CAPGId = 4,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 16.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Brubaker",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Warndreieck",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Hammer zum Glasbrechen",
                        CAPGId = 4,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 18.49,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Heldenwerk",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Hammer zum Glasbrechen",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Feuerlöscher",
                        CAPGId = 4,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 29.39,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Brandengel",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Feuerlöscher Brandengel",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Warnweste",
                        CAPGId = 4,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 5.94,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "SULWZM",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Warnweste SULWZM",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Bull-Kabel",
                        CAPGId = 4,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 53.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "EBROM",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Bull-Kabel",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Taschenlampe",
                        CAPGId = 4,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 34.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "LE LED",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Taschenlampe",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Abschleppseil",
                        CAPGId = 4,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 24.95,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Tadussi",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Abschleppseil",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Hebegurt",
                        CAPGId = 4,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 19.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Tubayia",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Hebegurt",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Benzinkanister",
                        CAPGId = 4,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 64.75,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "CHARON",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Benzinkanister",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    // Zubehör für Autotelefone

                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Freisprecheinrichtung im Auto",
                        CAPGId = 5,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 59.97,
                        SalesUnit = 5,
                        UnitNameId = 1,
                        Brand = "VeoPulse",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Freisprecheinrichtung im Auto",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Auto-Ladegerät für Handy",
                        CAPGId = 5,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 15.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Syncwire",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Auto-Ladegerät für Handy",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Autotelefonhalter",
                        CAPGId = 5,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 8.32,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "VANMASS",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Autotelefonhalter",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },


                    //Schutzausrüstung

                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Augenschutz",
                        CAPGId = 6,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 19.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "SolidWork",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Augenschutz",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Arbeitshosen und Overalls",
                        CAPGId = 6,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 59.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Uvex",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Arbeitshosen und Overalls",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Ohrschutz",
                        CAPGId = 6,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 15.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Senner",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Ohrschutz",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Staubmasken und Atemschutzmasken",
                        CAPGId = 6,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 26.89,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Vanderfields",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Staubmasken und Atemschutzmasken",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Sicheres Schuhwerk",
                        CAPGId = 6,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 49.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "TAERGU",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Sicheres Schuhwerk",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Schutz vor Schweißen",
                        CAPGId = 6,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 45.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "ANUNU",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Schutz vor Schweißen Vollmaske mit Filter",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Arbeitsjacken und -westen",
                        CAPGId = 6,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 45.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "ADAC",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Arbeitsjacken und -westen",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Arbeitshemden",
                        CAPGId = 6,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 25.90,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "LUCKY",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Arbeitshemden Thermojacke Arbeitshemd Herren Holzfäller Langarm",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },


                    // Unterhaltung im Auto
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Schallschutzplatte",
                        CAPGId = 7,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 79.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "GMP Tech",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Akustikschaumstoff Selbstklebend Pyramide Matte 200x100 x 8 cm von GMP Tech beauty of sound - Dämmung",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Autoradio",
                        CAPGId = 7,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 164.80,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Pioneer",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Autoradio Pioneer 15,2 cm (6,2 Zoll) 2-DIN-Display mit Bluetooth",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Auto Lautsprecher",
                        CAPGId = 7,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 82.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Herdio",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Auto Lautsprecher Herdio 6,5 Zoll Deckenlautsprecher, 160 Watt Bluetooth Einbaulautsprecher, " +
                        "Bündige Montage Sound, für Zuhause Badezimmer Küche Büro mit Full Range Sound",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Auto Verstärker",
                        CAPGId = 7,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 99.9,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Sony",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Auto Verstärker Sony XMN1004 Kfz-Verstärker (1000 Watt)",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Multimedia-Haupteinheit",
                        CAPGId = 7,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 289.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "BOOYES",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Multimedia-Haupteinheit BOOYES für Mercedes-Benz W169 W245 B160 B170 B180 B200 W639 Vito Viano W906 Sprinter",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Subwoofer",
                        CAPGId = 7,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 75.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "JBL",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Subwoofer JBL Stadium 102SSI 10 Zoll Subwoofer Auto Set von Harman Kardon - Leistungsstarke " +
                        "1350 Watt Kfz Bassbox Autolautsprecher - 30Hz – 175Hz - 250mm mit SSI Impedanzschalter",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Autoradio-Kondensator",
                        CAPGId = 7,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 14.99,
                        SalesUnit = 1,
                        UnitNameId = 1,
                        Brand = "Hama",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Autoradio-Kondensator Hama Hochleistungs-Entstörfilter, 10 Amp",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    },
                    new CarAccessoriesModel
                    {
                        Id = Convert.ToString(Guid.NewGuid()),
                        ProductName = "Autoradio-Kabelsatz",
                        CAPGId = 7,
                        QuantityOfStock = 1000000,
                        MinimumStockQuantity = 100000,
                        NetSellingPrice = 15.49,
                        SalesUnit = 1,
                        UnitNameId = 2,
                        Brand = "Adapter-Universe",
                        CreationDate = Convert.ToDateTime("2013-10-20 15:09:12.1234567"),
                        Description = "Autoradio-Kabelsatz Adapter Kabel Auto Radio aktiv System ISO kompatibel mit Audi VW Seat Bose DSP",
                        Version = 1,
                        LastUpdateTime = DateTime.Now,
                        PhotoPath = null
                    }
                );
            #endregion
    }

        public static async Task SeedUsersAndRolesAsync(WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@car.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        FirstName = "Laszlo",
                        LastName = "Toth",
                        SexId = 1,
                        Street = "Street",
                        HouseNumber = "HouseNumber8",
                        PostalCode = 1010,
                        Location = "Wien",
                        CountryId = 32
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@car.com");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@car.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        FirstName = "Laszlo",
                        LastName = "Toth",
                        SexId = 1,
                        Street = "Street",
                        HouseNumber = "HouseNumber8",
                        PostalCode = 1010,
                        Location = "Wien",
                        CountryId = 32
                    };
                    await userManager.CreateAsync(newAppUser, "User@car.com");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
