using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipASPNETMVC.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// EN
        /// For the DbContext class to be able to do any useful work, it needs an instance of the DbContextOptions class. 
        /// The DbContextOptions instance carries configuration information such as the connection string, database provider to use etc.
        /// GE
        /// Damit die DbContext-Klasse sinnvoll arbeiten kann, benötigt sie eine Instanz der DbContextOptions-Klasse.
        /// Die DbContextOptions-Instanz enthält Konfigurationsinformationen wie Verbindungszeichenfolge, zu verwendender Datenbankanbieter usw.
        /// HU
        /// Ahhoz, hogy a DbContext osztály bármilyen hasznos munkát végezhessen, szüksége van a DbContextOptions osztály egy példányára.
        /// A DbContextOptions példány konfigurációs információkat tartalmaz, például a kapcsolati karakterláncot, a használandó adatbázis-szolgáltatót stb.
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // EN
        // built-in methods can be overridden
        // GE
        // eingebaute Methoden können überschrieben werden
        // HU
        // felülírhatóak a beépített metódusok
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // EN
            // Definition of intermediate tables
            // GE
            // Definition von Zwischentabellen
            // HU
            // Köztes táblázatok definíciója
            modelBuilder.Entity<ApplicationUser_SpokenLangues>().HasKey(s_sp => new
            {
                s_sp.UserId,
                s_sp.SpokenLanguesId
            });

            modelBuilder.Entity<ApplicationUser_SpokenLangues>().HasOne(s => s.ApplicationUser)
                .WithMany(s_sp => s_sp.ApplicationUser_SpokenLangues).HasForeignKey(u => u.UserId);

            modelBuilder.Entity<ApplicationUser_SpokenLangues>().HasOne(sp => sp.SpokenLanguesModel)
                .WithMany(s_sp => s_sp.ApplicationUser_SpokenLangues).HasForeignKey(s => s.SpokenLanguesId);






            //https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application
            //modelBuilder.Entity<Course>()
            //             .HasMany(c => c.Instructors).WithMany(i => i.Courses)
            //             .Map(t => t.MapLeftKey("CourseID")
            //                 .MapRightKey("InstructorID")
            //                 .ToTable("CourseInstructor"));

            // EN
            // If you want to add the new entities to the data model and perform a database mapping that
            // which you did using non-attributes, replace it with the following code:
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // GE
            // Wenn Sie die neuen Entitäten zum Datenmodell hinzufügen und eine Datenbankzuordnung durchführen möchten
            // was Sie mit Nicht-Attributen getan haben, ersetzen Sie es durch den folgenden Code:
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // HU
            // Ha hozzá szeretné adni az új entitásokat az adatmodellhez, és olyan adatbázis-leképezést szeretne végrehajtani,
            // amelyet nem attribútumok használatával végzett, cserélje le a következő kódra:
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<Course>()
            //    .HasMany(c => c.Instructors).WithMany(i => i.Courses)
            //    .Map(t => t.MapLeftKey("CourseID")
            //        .MapRightKey("InstructorID")
            //        .ToTable("CourseInstructor"));

            // EN
            // The code below shows an example of how you could use liquid
            // API to specify relationship between entities instead of attributes: InstructorOfficeAssignment
            //modelBuilder.Entity<Instructor>()
            //.HasOptional(p => p.OfficeAssignment).WithRequired(p => p.Instructor);
            // GE
            // Der folgende Code zeigt ein Beispiel dafür, wie Sie liquid verwenden könnten
            // API zur Angabe der Beziehung zwischen Entitäten anstelle von Attributen: InstructorOfficeAssignment
            //modelBuilder.Entity<Ausbilder>()
            //.HasOptional(p => p.OfficeAssignment).WithRequired(p => p.Instructor);
            // HU
            // Az alábbi kód egy példát mutat be arra, hogyan használhatott volna folyékony
            // API-t attribútumok helyett az entitások közötti kapcsolat megadásához:InstructorOfficeAssignment
            //modelBuilder.Entity<Instructor>()
            //.HasOptional(p => p.OfficeAssignment).WithRequired(p => p.Instructor);

            // EN
            // Data upload
            // GE
            // Daten hochladen
            // HU
            // Adatfeltöltés
            modelBuilder.Seed();
            // Migration
            // https://csharp-video-tutorials.blogspot.com/2019/05/keeping-domain-models-and-database.html
            // EN
            //Command                               Purpose
            //get-help about_entityframeworkcore	Provides entity framework core help
            //Add-Migration	                        Adds a new migration
            //Update - Database                     Updates the database to a specified migration
            // e.g.: Add-Migration InitialCreate
            // Update-Database
            // Remove-Migration
            // GE
            //Befehl                                Zweck
            //get-help about_entityframeworkcore	Stellt Kernhilfe für das Entitätsframework bereit
            //Add-Migration	                        Fügt eine neue Migration hinzu
            //Update - Database                     Aktualisiert die Datenbank auf eine angegebene Migration
            // z.B.: Add-Migration InitialCreate
            // Update-Database
            // Remove-Migration
            // HU
            //Parancs                               Célja
            //get-help about_entityframeworkcore	Az entitáskeretrendszer alapvető segítséget nyújt
            //Add-Migration	                        Új áttelepítést ad hozzá
            //Update - Database                     Frissíti az adatbázist egy megadott áttelepítésre
            // pl.: Add-Migration InitialCreate
            // Update-Database
            // Remove-Migration

            // EN
            // Remove-Migration = if we want to restore the previous animal photo, the command "Update-Database" and the name of the previous version
            // GE
            // Remove-Migration = wenn wir das vorherige Tierfoto wiederherstellen wollen, den Befehl „Update-Database“ und den Namen der vorherigen Version
            // HU
            // Remove-Migration = ha előző állatpoto szeretnénk visszaállítani akkor "Update-Database" parancs és a korábbi verzió neve




            base.OnModelCreating(modelBuilder);

            // EN
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
            // GE
            //Benutzer werden in der AspNetUsers-Tabelle gespeichert
            //Rollen werden in der AspNetRoles-Tabelle gespeichert
            //Die Benutzer- und Rollenzuordnungsdaten werden in der AspNetUserRoles-Tabelle gespeichert
            //Diese Tabelle enthält nur 2 Spalten: UserId & RoleId
            //Beide sind Fremdschlüssel
            //Die kaskadierende Einschränkung der referenziellen Integrität ermöglicht Ihnen die Definition
            // wie Microsoft SQL Server funktionieren soll, wenn ein Benutzer
            //versucht, einen Schlüssel zu löschen oder zu aktualisieren, auf den ein vorhandener Fremdschlüssel zeigt.
            //Fremdschlüssel kaskadiert DELETE In Entity Framework Core ist dies standardmäßig der Fall
            //Fremdschlüssel der AspNetUserRoles-Tabelle haben kaskadierendes DELETE-Verhalten.
            //Das bedeutet, wenn ein Datensatz der übergeordneten Tabelle (AspNetRoles) gelöscht wird,
            //dann werden die entsprechenden Datensätze der untergeordneten Tabelle (AspNetUserRoles) automatisch gelöscht. Unbekannter Schlüssel
            //KEINE OPERATION BEIM LÖSCHEN
            //Was zu tun ist, wenn Sie dieses Standardverhalten anpassen möchten.
            //Wir wollen das Löschen einer Rolle nicht zulassen, wenn sie in der untergeordneten Tabelle (AspNetUserRoles) ist
            // Es gibt Zeilen, die auf eine der Rollen in der übergeordneten Tabelle (AspNetRoles) verweisen.
            // Dann müssen wir eine neue Migration NO OPERATION ON DELETE hinzufügen
            // HU
            //A felhasználók az AspNetUsers táblában vannak tárolva
            //A szerepkörök az AspNetRoles táblában vannak tárolva
            //A felhasználó-és szerepkör - leképezési adatok az AspNetUserRoles táblában vannak tárolva
            //Ez a táblázat mindössze 2 oszlopot tartalmaz: UserId & RoleId
            //Mindkettő idegen kulcs
            //A lépcsőzetes hivatkozási integritási korlátozás lehetővé teszi annak meghatározását,
            //hogy a Microsoft SQL Servernek milyen műveleteket kell végrehajtania, amikor egy felhasználó
            //megpróbál törölni vagy frissíteni egy olyan kulcsot, amelyre egy meglévő idegen kulcs mutat.
            //Idegen kulcs kaszkádolt DELETE Az Entity Framework Core-ban alapértelmezés szerint az
            //AspNetUserRoles tábla idegen kulcsai kaszkádolt DELETE viselkedéssel rendelkeznek.
            //Ez azt jelenti, hogy ha a szülőtábla(AspNetRoles) egy rekordja törlődik,
            //akkor a gyermektábla(AspNetUserRoles) megfelelő rekordjai automatikusan törlődnek. Idegen kulcs
            //NINCS MŰVELET TÖRLÉSKOR
            //Mi a teendő, ha testre szeretné szabni ezt az alapértelmezett viselkedést.
            //Nem szeretnénk engedélyezni egy szerepkör törlését, ha a gyermektáblában(AspNetUserRoles)
            //vannak olyan sorok, amelyek a szülőtábla(AspNetRoles) egyik szerepkörére mutatnak.
            // Ezután új migrációt kell hozzáadnunk Package Manager Console: Add-Migration NO_OPERATION_ON_DELETE & Update-Database
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }


        }

        // EN
        // the Interface only designates the set of supported operations that the class contains
        //public DbSet<AppUser> Users { get; set; }
        // GE
        // Die Schnittstelle bezeichnet nur die Menge der unterstützten Operationen, die die Klasse enthält
        //public DbSet<AppUser> Benutzer { erhalten; Satz; }
        // HU
        // az Interface csak a támogatott műveletek halmazát jelöli ki, amelyeket az osztály tartalmaz
        //public DbSet<AppUser> Users { get; set; }

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


        // EN
        // Orders related tables
        // DE
        // Bestellt verwandte Tabellen
        // HU
        // Kapcsolódó táblák megrendelése
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }
        public DbSet<ShoppingCartModel> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItemModel> ShoppingCartItems { get; set; }
        public DbSet<StockReplenishmentListModel> StockReplenishmentList { get; set; }

    }
}
