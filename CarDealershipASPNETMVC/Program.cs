using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Data.Service;
using CarDealershipASPNETMVC.Models;
using CarDealershipASPNETMVC.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// EN
// Add services to the container.
// GE
// Dienste zum Container hinzufügen.
// HU
// Szolgáltatások hozzáadása a tárolóhoz.
builder.Services.AddControllersWithViews();

// Package Manager Console
// Migration
// https://csharp-video-tutorials.blogspot.com/2019/05/keeping-domain-models-and-database.html
// EN
//Command                               Purpose
//get-help about_entityframeworkcore	Provides entity framework core help
//Add-Migration	                        Adds a new migration
//Update - Database                     Updates the database to a specified migration
// pl.: Add-Migration InitialCreate
// Update-Database
// Remove-Migration
// Remove-Migration if we want to restore a previous animal photo, use the "Update-Database" command and the name of the previous version
// EN
//Command                               Zweck
//get-help about_entityframeworkcore	Stellt Kernhilfe für das Entitätsframework bereit
//Add-Migration	                        Fügt eine neue Migration hinzu
//Update - Database                     Aktualisiert die Datenbank auf eine angegebene Migration
// pl.: Add-Migration InitialCreate
// Update-Database
// Remove-Migration
// Remove-Migration Wenn wir ein vorheriges Tierfoto wiederherstellen möchten, verwenden Sie den Befehl "Update-Database" und den Namen der vorherigen Version
// HU
//Command                               Célja
//get-help about_entityframeworkcore	Az entitáskeretrendszer alapvetõ segítséget nyújt
//Add-Migration	                        Új áttelepítést ad hozzá
//Update - Database                     Frissíti az adatbázist egy megadott áttelepítésre
// pl.: Add-Migration InitialCreate
// Update-Database
// Remove-Migration
// Remove-Migration ha elõzõ állatpoto szeretnénk visszaállítani akkor "Update-Database" parancs és a korábbi verzió neve

// EN
// Wir müssen es zusammen mit dem Namensraum und den Optionen hinzufügen, weil ich es wie in einer SQL-Server-Datenbank verwenden möchte
// dieser AppDbContext
// Mit dieser Methode nimmt der SQL-Server eine Verbindungszeichenfolge
// gute Praxis, dies in "appsettings.json" zu speichern.
// definiere den Verbindungstyp des Datenbankservers "UseSqlServer"
// GE
// Wir müssen es zusammen mit dem Namensraum und den Optionen hinzufügen, weil ich es wie in einer SQL-Server-Datenbank verwenden möchte
// dieser AppDbContext
// Mit dieser Methode nimmt der SQL-Server eine Verbindungszeichenfolge
// gute Praxis, dies in "appsettings.json" zu speichern.
// definiere den Verbindungstyp des Datenbankservers "UseSqlServer"
// HU
// hozzá kell adnunk a névtérrel együtt és az opciókat, mert úgy szeretném használni mint egy sql server adatbázisban
// ezt az AppDbContext
// A metódus használata az sql szerver egy kapcsolati karakterláncot vesz fel
// jó gyakorlat, hogy ezt az "appsettings.json"-ban tárolom
// meghatározom az adatbank kiszolgálói kapcsolat típusát "UseSqlServer"
builder.Services.AddDbContextPool<AppDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringsDell")));



// Identity
// EN
// the migration must be done before this, so we can change the default settings for data validation
// GE
// Die Migration muss vorher durchgeführt werden, damit wir die Standardeinstellungen für die Datenvalidierung ändern können
// HU
// a migrációt meg kell csinálni elõtte, így meg tudjuk változtatnia az alapbeállításokat az adatellenõrzésnél
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 3;
    option.Password.RequiredUniqueChars = 0;

    option.SignIn.RequireConfirmedEmail = true;

    // EN
    // Custom Email Confirmation Time
    // EN
    // Benutzerdefinierte E-Mail-Bestätigungszeit
    // HU
    // Egyéni e-mail megerõsítési idõ
    option.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";

    // EN
    // MaxFailedAccessAttempts - Specifies the number of failed logon attempts allowed
    // before the account is locked out. The default is 5.
    // DE
    // MaxFailedAccessAttempts - Gibt die zulässige Anzahl fehlgeschlagener Anmeldeversuche an
    // bevor das Konto gesperrt wird. Der Standardwert ist 5.
    // HU
    // MaxFailedAccessAttempts – A fiók zárolása elõtt engedélyezett sikertelen bejelentkezési
    // kísérletek számát adja meg. Az alapértelmezett érték 5.
    option.Lockout.MaxFailedAccessAttempts = 5;

    // EN
    // DefaultLockoutTimeSpan - Specifies the amount of the time the account
    // should be locked. The default it 5 minutes.
    // DE
    // DefaultLockoutTimeSpan - Gibt die Zeitdauer des Kontos an
    // sollte gesperrt sein. Der Standardwert beträgt 5 Minuten.
    // HU
    // DefaultLockoutTimeSpan – Megadja, hogy mennyi ideig tart a fiók
    // le kell zárni. Az alapértelmezett 5 perc.
    // DefaultLockoutTimeSpan – A fiók zárolásának idõtartamát adja meg. Az alapértelmezett 5 perc.
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()

    // EN
    // Custom Email Confirmation Time
    // EN
    // Benutzerdefinierte E-Mail-Bestätigungszeit
    // HU
    // Egyéni e-mail megerõsítési idõ
    .AddTokenProvider<CustomEmailConfirmationTokenProvider
            <ApplicationUser>>("CustomEmailConfirmation");

// EN
//The built-in DataProtectorTokenProvider can generate different types of tokens
//like Email Confirmation Token, Password Reset Token for example. 
//The default lifespan for all these token types is 1 day.
//One way to change the default lifespan is by using the built-in DataProtectionTokenProviderOptions.
// GE
//Der integrierte DataProtectorTokenProvider kann verschiedene Arten von Token generieren
//wie z. B. E-Mail-Bestätigungs-Token, Passwort-Reset-Token.
//Die Standardlebensdauer für alle diese Tokentypen beträgt 1 Tag.
//Eine Möglichkeit, die Standardlebensdauer zu ändern, ist die Verwendung der integrierten DataProtectionTokenProviderOptions.
// HU
//A beépített DataProtectorTokenProvider különbözõ típusú jogkivonatokat hozhat létre,
//például e-mail megerõsítõ tokent, jelszó-visszaállítási tokent.
//Az összes ilyen jogkivonattípus alapértelmezett élettartama 1 nap.
//Az alapértelmezett élettartam módosításának egyik módja a beépített DataProtectionTokenProviderOptions.
//Ez az osztály az összes jogkivonattípus élettartamát ugyanarra az értékre állítja be.

// EN
// Changes token lifespan of all token types
// GE
// Ändert die Token-Lebensdauer aller Token-Typen
// HU
// Módosítja az összes token élettartamát
builder.Services.Configure<DataProtectionTokenProviderOptions>(option => option.TokenLifespan = TimeSpan.FromHours(5));

// EN
// Changes token lifespan of just the Email Confirmation Token type
// GE
// Ändert die Token-Lebensdauer nur des E-Mail-Bestätigungs-Token-Typs
// HU
// Csak az e-mail megerõsítési token típusának a token élettartamát módosítja
builder.Services.Configure<CustomEmailConfirmationTokenProviderOptions>(option => option.TokenLifespan = TimeSpan.FromDays(3));


// EN
// The IEmployeeRepository interface has 2 implementations. How does the application know which implementation to use.
// The answer to this can be found in the Startup.cs file of the Startup class. ASP.NET Core SQLEmployeeRepository with the following line of code
// provides an instance of class when an instance of IEmployeeRepository is requested.
// We use the AddScoped() method because we want the instance to be alive,
// and be available in the full scope of that HTTP request. For another new HTTP request, a new instance of the SQLEmployeeRepository class
// will be specified and will be available in the full scope of the HTTP request.
// AddSingleton so "CarAccessoriesController" depends on this
// <IDataAccess, DataAccess> constructor gets the ConnectionStrings value of "appsettings.json".
// GE
// Die IEmployeeRepository-Schnittstelle hat 2 Implementierungen. Woher weiß die Anwendung, welche Implementierung zu verwenden ist.
// Die Antwort darauf finden Sie in der Startup.cs-Datei der Startup-Klasse. ASP.NET Core SQLEmployeeRepository mit der folgenden Codezeile
// stellt eine Instanz der Klasse bereit, wenn eine Instanz von IEmployeeRepository angefordert wird.
// Wir verwenden die Methode AddScoped(), weil wir wollen, dass die Instanz aktiv ist,
// und im vollen Umfang dieser HTTP-Anforderung verfügbar sein. Für eine weitere neue HTTP-Anforderung eine neue Instanz der Klasse SQLEmployeeRepository
// wird angegeben und steht im vollen Umfang der HTTP-Anfrage zur Verfügung.
// AddSingleton, also hängt "CarAccessoriesController" davon ab
// Der <IDataAccess, DataAccess>-Konstruktor ruft den ConnectionStrings-Wert von „appsettings.json“ ab.
// HU
// Az IEmployeeRepository felületnek 2 implementációja van. Honnan tudja az alkalmazás, hogy melyik implementációt kell használnia.
// A válasz erre az Indítási osztály Startup.cs fájlban található. A következõ kódsorral a ASP.NET Core az SQLEmployeeRepository
// osztály egy példányát biztosítja, amikor az IEmployeeRepository egy példányát kérik.
// Azért használjuk az AddScoped() metódust, mert azt szeretnénk, hogy a példány életben legyen,
// és elérhetõ legyen az adott HTTP-kérés teljes hatókörében. Egy másik új HTTP-kéréshez az SQLEmployeeRepository osztály új példánya
// lesz megadva, és az a HTTP-kérés teljes hatókörében elérhetõ lesz.
// AddSingleton így ettõl függ a "CarAccessoriesController"
// <IDataAccess, DataAccess> konstruktora lekéri az "appsettings.json" ConnectionStrings értékét  
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICarAccessoriesProductGroupService, CarAccessoriesProductGroupService>();
builder.Services.AddScoped<ICarAccessoriesService, CarAccessoriesService>();
builder.Services.AddScoped<ICarAccessoriesUnitService, CarAccessoriesUnitService>();
builder.Services.AddScoped<ICarsService, CarsService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IFuelService, FuelService>();
builder.Services.AddScoped<IGearboxService, GearboxService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderStatusService, OrderStatusService>();
builder.Services.AddScoped<ISexService, SexService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IShoppingCartStatusService, ShoppingCartStatusService>();
builder.Services.AddScoped<ISpokenLanguesService, SpokenLanguesService>();

// EN
// Register purpose string class with DI container
// Register the class that contains purpose strings with the asp.net core dependency injection container.
// This allows us to inject an instance of this class into any controller throughout our application.
// GE
// Zweck-String-Klasse beim DI-Container registrieren
// Registrieren Sie die Klasse, die Zweck-Strings enthält, mit dem asp.net Core Dependency Injection-Container.
// Dies ermöglicht es uns, eine Instanz dieser Klasse in einen beliebigen Controller in unserer gesamten Anwendung einzufügen.
// HU
// Célsztringosztály regisztrálása DI-tárolóval Regisztrálja a célsztringeket tartalmazó osztályt
// az asp.net alapvetõ függõséginjektálási tárolóval.
// Ez lehetõvé teszi számunkra, hogy ennek az osztálynak egy példányát bármely vezérlõbe befecskendezzük az alkalmazásunk során.
builder.Services.AddSingleton<DataProtectionPurposeStrings>();

// EN
// Please note: If you apply [AllowAnonymous] attribute at the controller level, any [Authorize]
// attribute attribute on the same controller actions is ignored.
// Apply Authorize attribute globally To apply [Authorize] attribute globally on all controllers and
// controller actions throughout your application modify the code in ConfigureServices method of the Startup class.
// GE
// Bitte beachten Sie: Wenn Sie das Attribut [AllowAnonymous] auf Controller-Ebene anwenden, werden alle [Autorisieren]
// Attribut Attribut auf denselben Controller-Aktionen wird ignoriert.
// Autorisierungsattribut global anwenden Um das [Autorisierungs]-Attribut global auf alle Controller anzuwenden und
// Controller-Aktionen in Ihrer gesamten Anwendung ändern den Code in der ConfigureServices-Methode der Startup-Klasse.
// HU
// Megjegyzés: Ha az [AllowAnonymous] attribútumot a vezérlõ szintjén alkalmazza, a rendszer figyelmen kívül
// hagyja az ugyanazon vezérlõmûveletekre vonatkozó [Authorize] attribútumattribútumokat.
// Attribútum globális alkalmazása Az [Authorize] attribútum globális alkalmazásához az alkalmazás
// összes vezérlõjére és vezérlõmûveletére módosítania kell a kódot az indítási osztály ConfigureServices metódusában.
builder.Services.AddMvc(config => {
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});


//builder.Services.AddAuthentication().AddGoogle(googleOptions =>
//{
//    // EN  
//    // We get IDs after google registration
//    // GE
//    // Wir erhalten IDs nach der Google-Registrierung
//    // HU  
//    // Id-kat a google regisztráció után kapjuk meg
//    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//});

//builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
//{
//    // EN  
//    // We get IDs after facebook registration
//    // GE
//    // Wir erhalten IDs nach der facebook-Registrierung
//    // HU  
//    // Id-kat a facebook regisztráció után kapjuk meg
//    facebookOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//    facebookOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//});

// EN
// Change the default Access Denied route
// GE
// Ändern Sie die Standardroute "Zugriff verweigert".
// HU
// Az alapértelmezett hozzáférés megtagadva útvonalának módosítása
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
});


// EN
//Claims are policy based. We create a policy and include one or more claims in that policy.
//We then need to register the policy. 
//The options parameter type is AuthorizationOptions
//Use AddPolicy() method to create the policy
//The first parameter is the name of the policy and the second parameter is the policy itself
//To satisfy this policy requirements, the logged-in user must have Delete Role claim
// GE
//Ansprüche basieren auf Richtlinien. Wir erstellen eine Police und nehmen einen oder mehrere Ansprüche in diese Police auf.
//Wir müssen dann die Police registrieren.
//Der Optionsparametertyp ist AuthorizationOptions
//AddPolicy()-Methode verwenden, um die Richtlinie zu erstellen
//Der erste Parameter ist der Name der Richtlinie und der zweite Parameter ist die Richtlinie selbst
//Um diese Richtlinienanforderungen zu erfüllen, muss der angemeldete Benutzer über den Anspruch „Rolle löschen“ verfügen
// HU
// létrehozása A jogcímek házirenden alapulnak. Létrehozunk egy szabályzatot, és egy vagy több követelést
// belefoglalunk az irányelvbe. Ezután regisztrálnunk kell a politikát.
// Az options paraméter típusa AuthorizationOptions
//Az AddPolicy() metódus használata a szabályzat létrehozásához
//Az elsõ paraméter a szabályzat neve, a második paraméter pedig maga a szabályzat
//A házirend-követelmények teljesítéséhez a bejelentkezett felhasználónak Szerepkör törlése jogcímmel kell rendelkeznie
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreateRolePolicy",
        policy => policy.RequireClaim("Create Role", "true"));

    options.AddPolicy("EditRolePolicy",
        policy => policy.RequireClaim("Edit Role", "true"));

    options.AddPolicy("DeleteRolePolicy",
        policy => policy.RequireClaim("Delete Role", "true"));

    // EN
    // "AllRolePolicy" is met if all requirements must meet "RequireClaim".
    // GE
    // "AllRolePolicy" ist erfüllt, wenn alle Anforderungen "RequireClaim" erfüllen müssen.
    // HU
    // akkor teljesül a "AllRolePolicy" ha minden követelménynek "RequireClaim" meg kell felelnie
    options.AddPolicy("AllRolePolicy",
        policy => policy.RequireClaim("Create Role", "true")
                        .RequireClaim("Edit Role", "true")
                        .RequireClaim("Delete Role", "true")

            );
    // EN
    // Use the RequireAssertion method on the AuthorizationPolicyBuilder instance instead of RequireClaim or RequireRole
    // The RequireAssertion() method takes Func<AuthorizationHandlerContext, bool> as a parameter
    // This Func returns AuthorizationHandlerContext as an input parameter and returns a boolean.
    // The AuthorizationHandlerContext instance provides access to user roles and entitlements
    // Func embeds a method, so the code above can be rewritten as follows. We have created a separate method,
    // and referenced it instead of creating the method inline.
    // We can use func to create a custom policy that meets our authorization needs.
    // GE
    // Verwenden Sie die RequireAssertion-Methode für die AuthorizationPolicyBuilder-Instanz anstelle von RequireClaim oder RequireRole
    // Die Methode RequireAssertion() nimmt Func<AuthorizationHandlerContext, bool> als Parameter
    // Diese Funktion gibt AuthorizationHandlerContext als Eingabeparameter und einen booleschen Wert zurück.
    // Die AuthorizationHandlerContext-Instanz bietet Zugriff auf Benutzerrollen und -berechtigungen
    // Func bettet eine Methode ein, sodass der obige Code wie folgt umgeschrieben werden kann. Wir haben eine separate Methode erstellt,
    // und darauf verwiesen, anstatt die Methode inline zu erstellen.
    // Wir können func verwenden, um eine benutzerdefinierte Richtlinie zu erstellen, die unseren Autorisierungsanforderungen entspricht.
    // HU
    // Az AuthorizationPolicyBuilder példányon használja a RequireAssertion metódust a RequireClaim vagy a RequireRole helyett
    // A RequireAssertion() metódus paraméterként a Func<AuthorizationHandlerContext, bool> paramétert veszi
    // Ez a Func bemeneti paraméterként AuthorizationHandlerContext ad vissza, és egy logikai értéket ad vissza.
    // A AuthorizationHandlerContext példány hozzáférést biztosít a felhasználói szerepkörökhöz és jogcímekhez
    // A Func egy metódust ágyaz be, így a fenti kód is átírható az alábbiak szerint. Létrehoztunk egy külön metódust, 
    // és hivatkoztunk rá, ahelyett, hogy a metódust inline hoztuk volna létre.
    // A func használatával létrehozhatunk egy egyéni szabályzatot, amely megfelel az engedélyezési igényeinknek. 
    options.AddPolicy("EditRolePolicy", policy => policy.RequireAssertion(context =>
        context.User.IsInRole("Admin") &&
        context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
        context.User.IsInRole("Super Admin")
    ));

    // EN
    // Authorization handler registration
    // We register custom authorization handler in ConfigureServices() method of the Startup class
    // GE
    // Registrierung des Autorisierungs-Handlers
    // Wir registrieren einen benutzerdefinierten Autorisierungs-Handler in der ConfigureServices()-Methode der Startup-Klasse
    // HU
    // Engedélyezéskezelõ regisztrációja
    // Egyéni engedélyezési kezelõt regisztrálunk a Startup osztály ConfigureServices() metódusában
    options.AddPolicy("EditRolePolicy", policy =>
            policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));
});

// EN
// Authorization handler registration
// We register custom authorization handler in ConfigureServices() method of the Startup class
// GE
// Registrierung des Autorisierungs-Handlers
// Wir registrieren einen benutzerdefinierten Autorisierungs-Handler in der ConfigureServices()-Methode der Startup-Klasse
// HU
// Engedélyezéskezelõ regisztrációja
// Egyéni engedélyezési kezelõt regisztrálunk az indítási osztály ConfigureServices() metódusában
builder.Services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();

// EN
//Schließlich die Verwendung einer benutzerdefinierten Richtlinie zum Schutz von Ressourcen wie Controller-Aktionsmethoden
// GE
//Schließlich die Verwendung einer benutzerdefinierten Richtlinie zum Schutz von Ressourcen wie Controller-Aktionsmethoden
// HU
//Végül az egyéni szabályzat használata az erõforrások, például a vezérlõ mûveleti módszereinek védelméhez
//[HttpGet][Authorize(Policy = "EditRolePolicy")]
//public asynk Task<IActionResult> ManageUserRoles(string userId)
//{
//    // Implement
//}



// EN
//Adding Multiple Claims to Policy
// GE
//Hinzufügen mehrerer Ansprüche zur Richtlinie
// HU
//Több követelés hozzáadása a szabályzathoz
// EN
//To add multiple claims to a given policy, chain RequireClaim() method
// GE
//Um einer bestimmten Richtlinie mehrere Ansprüche hinzuzufügen, verketten Sie die RequireClaim()-Methode
// HU
//Több jogcím hozzáadásához egy adott házirendhez láncolja a RequireClaim() metódust
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("DeleteRolePolicy",
//        policy => policy.RequireClaim("Delete Role")
//                        .RequireClaim("Create Role")

//        );
//});





//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminRolePolicy", policy => policy.RequireRole("Admin"));

//    // EN
//    // If you want to include multiple roles in the policy simply separate them with a comma
//    // GE
//    // Wenn Sie mehrere Rollen in die Richtlinie aufnehmen möchten, trennen Sie sie einfach durch ein Komma
//    // HU
//    // Ha több szerepet szeretne belefoglalni a szabályzatba, egyszerûen válassza el õket vesszõvel
//    //options.AddPolicy("SuperAdminPolicy", policy =>
//    //              policy.RequireRole("Admin", "User", "Manager"));
//    //[HttpPost]
//    //[Authorize(Policy = "SuperAdminPolicy")]
//    //public async Task<IActionResult> DeleteRole(string id)
//    //{
//    //    // Delete Role
//    //}

//});





var app = builder.Build();

// EN
// Configure the HTTP request pipeline.
// GE
// Konfigurieren Sie die HTTP-Anforderungspipeline.
// HU
// Konfigurálja a HTTP kérés folyamatát.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // EN
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // GE
    // Der HSTS-Standardwert beträgt 30 Tage. Möglicherweise möchten Sie dies für Produktionsszenarien ändern, siehe https://aka.ms/aspnetcore-hsts.
    // HU
    // Az alapértelmezett HSTS-érték 30 nap. Elõfordulhat, hogy ezt módosítani szeretné az éles forgatókönyvek esetében, lásd: https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Identity
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
