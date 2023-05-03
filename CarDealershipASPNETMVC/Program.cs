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
// Dienste zum Container hinzuf�gen.
// HU
// Szolg�ltat�sok hozz�ad�sa a t�rol�hoz.
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
//get-help about_entityframeworkcore	Stellt Kernhilfe f�r das Entit�tsframework bereit
//Add-Migration	                        F�gt eine neue Migration hinzu
//Update - Database                     Aktualisiert die Datenbank auf eine angegebene Migration
// pl.: Add-Migration InitialCreate
// Update-Database
// Remove-Migration
// Remove-Migration Wenn wir ein vorheriges Tierfoto wiederherstellen m�chten, verwenden Sie den Befehl "Update-Database" und den Namen der vorherigen Version
// HU
//Command                               C�lja
//get-help about_entityframeworkcore	Az entit�skeretrendszer alapvet� seg�ts�get ny�jt
//Add-Migration	                        �j �ttelep�t�st ad hozz�
//Update - Database                     Friss�ti az adatb�zist egy megadott �ttelep�t�sre
// pl.: Add-Migration InitialCreate
// Update-Database
// Remove-Migration
// Remove-Migration ha el�z� �llatpoto szeretn�nk vissza�ll�tani akkor "Update-Database" parancs �s a kor�bbi verzi� neve

// EN
// Wir m�ssen es zusammen mit dem Namensraum und den Optionen hinzuf�gen, weil ich es wie in einer SQL-Server-Datenbank verwenden m�chte
// dieser AppDbContext
// Mit dieser Methode nimmt der SQL-Server eine Verbindungszeichenfolge
// gute Praxis, dies in "appsettings.json" zu speichern.
// definiere den Verbindungstyp des Datenbankservers "UseSqlServer"
// GE
// Wir m�ssen es zusammen mit dem Namensraum und den Optionen hinzuf�gen, weil ich es wie in einer SQL-Server-Datenbank verwenden m�chte
// dieser AppDbContext
// Mit dieser Methode nimmt der SQL-Server eine Verbindungszeichenfolge
// gute Praxis, dies in "appsettings.json" zu speichern.
// definiere den Verbindungstyp des Datenbankservers "UseSqlServer"
// HU
// hozz� kell adnunk a n�vt�rrel egy�tt �s az opci�kat, mert �gy szeretn�m haszn�lni mint egy sql server adatb�zisban
// ezt az AppDbContext
// A met�dus haszn�lata az sql szerver egy kapcsolati karakterl�ncot vesz fel
// j� gyakorlat, hogy ezt az "appsettings.json"-ban t�rolom
// meghat�rozom az adatbank kiszolg�l�i kapcsolat t�pus�t "UseSqlServer"
builder.Services.AddDbContextPool<AppDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringsDell")));



// Identity
// EN
// the migration must be done before this, so we can change the default settings for data validation
// GE
// Die Migration muss vorher durchgef�hrt werden, damit wir die Standardeinstellungen f�r die Datenvalidierung �ndern k�nnen
// HU
// a migr�ci�t meg kell csin�lni el�tte, �gy meg tudjuk v�ltoztatnia az alapbe�ll�t�sokat az adatellen�rz�sn�l
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 3;
    option.Password.RequiredUniqueChars = 0;

    option.SignIn.RequireConfirmedEmail = true;

    // EN
    // Custom Email Confirmation Time
    // EN
    // Benutzerdefinierte E-Mail-Best�tigungszeit
    // HU
    // Egy�ni e-mail meger�s�t�si id�
    option.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";

    // EN
    // MaxFailedAccessAttempts - Specifies the number of failed logon attempts allowed
    // before the account is locked out. The default is 5.
    // DE
    // MaxFailedAccessAttempts - Gibt die zul�ssige Anzahl fehlgeschlagener Anmeldeversuche an
    // bevor das Konto gesperrt wird. Der Standardwert ist 5.
    // HU
    // MaxFailedAccessAttempts � A fi�k z�rol�sa el�tt enged�lyezett sikertelen bejelentkez�si
    // k�s�rletek sz�m�t adja meg. Az alap�rtelmezett �rt�k 5.
    option.Lockout.MaxFailedAccessAttempts = 5;

    // EN
    // DefaultLockoutTimeSpan - Specifies the amount of the time the account
    // should be locked. The default it 5 minutes.
    // DE
    // DefaultLockoutTimeSpan - Gibt die Zeitdauer des Kontos an
    // sollte gesperrt sein. Der Standardwert betr�gt 5 Minuten.
    // HU
    // DefaultLockoutTimeSpan � Megadja, hogy mennyi ideig tart a fi�k
    // le kell z�rni. Az alap�rtelmezett 5 perc.
    // DefaultLockoutTimeSpan � A fi�k z�rol�s�nak id�tartam�t adja meg. Az alap�rtelmezett 5 perc.
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()

    // EN
    // Custom Email Confirmation Time
    // EN
    // Benutzerdefinierte E-Mail-Best�tigungszeit
    // HU
    // Egy�ni e-mail meger�s�t�si id�
    .AddTokenProvider<CustomEmailConfirmationTokenProvider
            <ApplicationUser>>("CustomEmailConfirmation");

// EN
//The built-in DataProtectorTokenProvider can generate different types of tokens
//like Email Confirmation Token, Password Reset Token for example. 
//The default lifespan for all these token types is 1 day.
//One way to change the default lifespan is by using the built-in DataProtectionTokenProviderOptions.
// GE
//Der integrierte DataProtectorTokenProvider kann verschiedene Arten von Token generieren
//wie z. B. E-Mail-Best�tigungs-Token, Passwort-Reset-Token.
//Die Standardlebensdauer f�r alle diese Tokentypen betr�gt 1 Tag.
//Eine M�glichkeit, die Standardlebensdauer zu �ndern, ist die Verwendung der integrierten DataProtectionTokenProviderOptions.
// HU
//A be�p�tett DataProtectorTokenProvider k�l�nb�z� t�pus� jogkivonatokat hozhat l�tre,
//p�ld�ul e-mail meger�s�t� tokent, jelsz�-vissza�ll�t�si tokent.
//Az �sszes ilyen jogkivonatt�pus alap�rtelmezett �lettartama 1 nap.
//Az alap�rtelmezett �lettartam m�dos�t�s�nak egyik m�dja a be�p�tett DataProtectionTokenProviderOptions.
//Ez az oszt�ly az �sszes jogkivonatt�pus �lettartam�t ugyanarra az �rt�kre �ll�tja be.

// EN
// Changes token lifespan of all token types
// GE
// �ndert die Token-Lebensdauer aller Token-Typen
// HU
// M�dos�tja az �sszes token �lettartam�t
builder.Services.Configure<DataProtectionTokenProviderOptions>(option => option.TokenLifespan = TimeSpan.FromHours(5));

// EN
// Changes token lifespan of just the Email Confirmation Token type
// GE
// �ndert die Token-Lebensdauer nur des E-Mail-Best�tigungs-Token-Typs
// HU
// Csak az e-mail meger�s�t�si token t�pus�nak a token �lettartam�t m�dos�tja
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
// Die IEmployeeRepository-Schnittstelle hat 2 Implementierungen. Woher wei� die Anwendung, welche Implementierung zu verwenden ist.
// Die Antwort darauf finden Sie in der Startup.cs-Datei der Startup-Klasse. ASP.NET Core SQLEmployeeRepository mit der folgenden Codezeile
// stellt eine Instanz der Klasse bereit, wenn eine Instanz von IEmployeeRepository angefordert wird.
// Wir verwenden die Methode AddScoped(), weil wir wollen, dass die Instanz aktiv ist,
// und im vollen Umfang dieser HTTP-Anforderung verf�gbar sein. F�r eine weitere neue HTTP-Anforderung eine neue Instanz der Klasse SQLEmployeeRepository
// wird angegeben und steht im vollen Umfang der HTTP-Anfrage zur Verf�gung.
// AddSingleton, also h�ngt "CarAccessoriesController" davon ab
// Der <IDataAccess, DataAccess>-Konstruktor ruft den ConnectionStrings-Wert von �appsettings.json� ab.
// HU
// Az IEmployeeRepository fel�letnek 2 implement�ci�ja van. Honnan tudja az alkalmaz�s, hogy melyik implement�ci�t kell haszn�lnia.
// A v�lasz erre az Ind�t�si oszt�ly Startup.cs f�jlban tal�lhat�. A k�vetkez� k�dsorral a ASP.NET Core az SQLEmployeeRepository
// oszt�ly egy p�ld�ny�t biztos�tja, amikor az IEmployeeRepository egy p�ld�ny�t k�rik.
// Az�rt haszn�ljuk az AddScoped() met�dust, mert azt szeretn�nk, hogy a p�ld�ny �letben legyen,
// �s el�rhet� legyen az adott HTTP-k�r�s teljes hat�k�r�ben. Egy m�sik �j HTTP-k�r�shez az SQLEmployeeRepository oszt�ly �j p�ld�nya
// lesz megadva, �s az a HTTP-k�r�s teljes hat�k�r�ben el�rhet� lesz.
// AddSingleton �gy ett�l f�gg a "CarAccessoriesController"
// <IDataAccess, DataAccess> konstruktora lek�ri az "appsettings.json" ConnectionStrings �rt�k�t  
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
// Registrieren Sie die Klasse, die Zweck-Strings enth�lt, mit dem asp.net Core Dependency Injection-Container.
// Dies erm�glicht es uns, eine Instanz dieser Klasse in einen beliebigen Controller in unserer gesamten Anwendung einzuf�gen.
// HU
// C�lsztringoszt�ly regisztr�l�sa DI-t�rol�val Regisztr�lja a c�lsztringeket tartalmaz� oszt�lyt
// az asp.net alapvet� f�gg�s�ginjekt�l�si t�rol�val.
// Ez lehet�v� teszi sz�munkra, hogy ennek az oszt�lynak egy p�ld�ny�t b�rmely vez�rl�be befecskendezz�k az alkalmaz�sunk sor�n.
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
// Controller-Aktionen in Ihrer gesamten Anwendung �ndern den Code in der ConfigureServices-Methode der Startup-Klasse.
// HU
// Megjegyz�s: Ha az [AllowAnonymous] attrib�tumot a vez�rl� szintj�n alkalmazza, a rendszer figyelmen k�v�l
// hagyja az ugyanazon vez�rl�m�veletekre vonatkoz� [Authorize] attrib�tumattrib�tumokat.
// Attrib�tum glob�lis alkalmaz�sa Az [Authorize] attrib�tum glob�lis alkalmaz�s�hoz az alkalmaz�s
// �sszes vez�rl�j�re �s vez�rl�m�velet�re m�dos�tania kell a k�dot az ind�t�si oszt�ly ConfigureServices met�dus�ban.
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
//    // Id-kat a google regisztr�ci� ut�n kapjuk meg
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
//    // Id-kat a facebook regisztr�ci� ut�n kapjuk meg
//    facebookOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//    facebookOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//});

// EN
// Change the default Access Denied route
// GE
// �ndern Sie die Standardroute "Zugriff verweigert".
// HU
// Az alap�rtelmezett hozz�f�r�s megtagadva �tvonal�nak m�dos�t�sa
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
//Anspr�che basieren auf Richtlinien. Wir erstellen eine Police und nehmen einen oder mehrere Anspr�che in diese Police auf.
//Wir m�ssen dann die Police registrieren.
//Der Optionsparametertyp ist AuthorizationOptions
//AddPolicy()-Methode verwenden, um die Richtlinie zu erstellen
//Der erste Parameter ist der Name der Richtlinie und der zweite Parameter ist die Richtlinie selbst
//Um diese Richtlinienanforderungen zu erf�llen, muss der angemeldete Benutzer �ber den Anspruch �Rolle l�schen� verf�gen
// HU
// l�trehoz�sa A jogc�mek h�zirenden alapulnak. L�trehozunk egy szab�lyzatot, �s egy vagy t�bb k�vetel�st
// belefoglalunk az ir�nyelvbe. Ezut�n regisztr�lnunk kell a politik�t.
// Az options param�ter t�pusa AuthorizationOptions
//Az AddPolicy() met�dus haszn�lata a szab�lyzat l�trehoz�s�hoz
//Az els� param�ter a szab�lyzat neve, a m�sodik param�ter pedig maga a szab�lyzat
//A h�zirend-k�vetelm�nyek teljes�t�s�hez a bejelentkezett felhaszn�l�nak Szerepk�r t�rl�se jogc�mmel kell rendelkeznie
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
    // "AllRolePolicy" ist erf�llt, wenn alle Anforderungen "RequireClaim" erf�llen m�ssen.
    // HU
    // akkor teljes�l a "AllRolePolicy" ha minden k�vetelm�nynek "RequireClaim" meg kell felelnie
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
    // Verwenden Sie die RequireAssertion-Methode f�r die AuthorizationPolicyBuilder-Instanz anstelle von RequireClaim oder RequireRole
    // Die Methode RequireAssertion() nimmt Func<AuthorizationHandlerContext, bool> als Parameter
    // Diese Funktion gibt AuthorizationHandlerContext als Eingabeparameter und einen booleschen Wert zur�ck.
    // Die AuthorizationHandlerContext-Instanz bietet Zugriff auf Benutzerrollen und -berechtigungen
    // Func bettet eine Methode ein, sodass der obige Code wie folgt umgeschrieben werden kann. Wir haben eine separate Methode erstellt,
    // und darauf verwiesen, anstatt die Methode inline zu erstellen.
    // Wir k�nnen func verwenden, um eine benutzerdefinierte Richtlinie zu erstellen, die unseren Autorisierungsanforderungen entspricht.
    // HU
    // Az AuthorizationPolicyBuilder p�ld�nyon haszn�lja a RequireAssertion met�dust a RequireClaim vagy a RequireRole helyett
    // A RequireAssertion() met�dus param�terk�nt a Func<AuthorizationHandlerContext, bool> param�tert veszi
    // Ez a Func bemeneti param�terk�nt AuthorizationHandlerContext ad vissza, �s egy logikai �rt�ket ad vissza.
    // A AuthorizationHandlerContext p�ld�ny hozz�f�r�st biztos�t a felhaszn�l�i szerepk�r�kh�z �s jogc�mekhez
    // A Func egy met�dust �gyaz be, �gy a fenti k�d is �t�rhat� az al�bbiak szerint. L�trehoztunk egy k�l�n met�dust, 
    // �s hivatkoztunk r�, ahelyett, hogy a met�dust inline hoztuk volna l�tre.
    // A func haszn�lat�val l�trehozhatunk egy egy�ni szab�lyzatot, amely megfelel az enged�lyez�si ig�nyeinknek. 
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
    // Enged�lyez�skezel� regisztr�ci�ja
    // Egy�ni enged�lyez�si kezel�t regisztr�lunk a Startup oszt�ly ConfigureServices() met�dus�ban
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
// Enged�lyez�skezel� regisztr�ci�ja
// Egy�ni enged�lyez�si kezel�t regisztr�lunk az ind�t�si oszt�ly ConfigureServices() met�dus�ban
builder.Services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();

// EN
//Schlie�lich die Verwendung einer benutzerdefinierten Richtlinie zum Schutz von Ressourcen wie Controller-Aktionsmethoden
// GE
//Schlie�lich die Verwendung einer benutzerdefinierten Richtlinie zum Schutz von Ressourcen wie Controller-Aktionsmethoden
// HU
//V�g�l az egy�ni szab�lyzat haszn�lata az er�forr�sok, p�ld�ul a vez�rl� m�veleti m�dszereinek v�delm�hez
//[HttpGet][Authorize(Policy = "EditRolePolicy")]
//public asynk Task<IActionResult> ManageUserRoles(string userId)
//{
//    // Implement
//}



// EN
//Adding Multiple Claims to Policy
// GE
//Hinzuf�gen mehrerer Anspr�che zur Richtlinie
// HU
//T�bb k�vetel�s hozz�ad�sa a szab�lyzathoz
// EN
//To add multiple claims to a given policy, chain RequireClaim() method
// GE
//Um einer bestimmten Richtlinie mehrere Anspr�che hinzuzuf�gen, verketten Sie die RequireClaim()-Methode
// HU
//T�bb jogc�m hozz�ad�s�hoz egy adott h�zirendhez l�ncolja a RequireClaim() met�dust
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
//    // Wenn Sie mehrere Rollen in die Richtlinie aufnehmen m�chten, trennen Sie sie einfach durch ein Komma
//    // HU
//    // Ha t�bb szerepet szeretne belefoglalni a szab�lyzatba, egyszer�en v�lassza el �ket vessz�vel
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
// Konfigur�lja a HTTP k�r�s folyamat�t.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // EN
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // GE
    // Der HSTS-Standardwert betr�gt 30 Tage. M�glicherweise m�chten Sie dies f�r Produktionsszenarien �ndern, siehe https://aka.ms/aspnetcore-hsts.
    // HU
    // Az alap�rtelmezett HSTS-�rt�k 30 nap. El�fordulhat, hogy ezt m�dos�tani szeretn� az �les forgat�k�nyvek eset�ben, l�sd: https://aka.ms/aspnetcore-hsts.
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
