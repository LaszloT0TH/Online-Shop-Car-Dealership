using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Data.Service;
using CarDealershipASPNETMVC.Models;
using CarDealershipASPNETMVC.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
 

// Add services to the container.
builder.Services.AddControllersWithViews();


// Migration
// https://csharp-video-tutorials.blogspot.com/2019/05/keeping-domain-models-and-database.html

builder.Services.AddDbContextPool<AppDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringsDell")));



// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 3;
    option.Password.RequiredUniqueChars = 0;

    option.SignIn.RequireConfirmedEmail = true;

    // Custom Email Confirmation Time
    option.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";

    // MaxFailedAccessAttempts - Specifies the number of failed logon attempts allowed
    // before the account is locked out. The default is 5.
    option.Lockout.MaxFailedAccessAttempts = 5;

    // DefaultLockoutTimeSpan - Specifies the amount of the time the account
    // should be locked. The default it 5 minutes.
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    // Custom Email Confirmation time
    .AddTokenProvider<CustomEmailConfirmationTokenProvider
            <ApplicationUser>>("CustomEmailConfirmation");


//The built-in DataProtectorTokenProvider can generate different types of tokens
//like Email Confirmation Token, Password Reset Token for example. 
//The default lifespan for all these token types is 1 day.
//One way to change the default lifespan is by using the built-in DataProtectionTokenProviderOptions.
// Changes token lifespan of all token types
builder.Services.Configure<DataProtectionTokenProviderOptions>(option => option.TokenLifespan = TimeSpan.FromHours(5));
// Changes token lifespan of just the Email Confirmation Token type
builder.Services.Configure<CustomEmailConfirmationTokenProviderOptions>(option => option.TokenLifespan = TimeSpan.FromDays(3));





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


// Register purpose string class with DI container
// Register the class that contains purpose strings with the asp.net core dependency injection container.
// This allows us to inject an instance of this class into any controller throughout our application.
builder.Services.AddSingleton<DataProtectionPurposeStrings>();


// Please note: If you apply [AllowAnonymous] attribute at the controller level, any [Authorize]
// attribute attribute on the same controller actions is ignored.
// Apply Authorize attribute globally To apply [Authorize] attribute globally on all controllers and
// controller actions throughout your application modify the code in ConfigureServices method of the Startup class.
builder.Services.AddMvc(config => {
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});


// Change the default Access Denied route
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
});



//Claims are policy based. We create a policy and include one or more claims in that policy.
//We then need to register the policy. 
//The options parameter type is AuthorizationOptions
//Use AddPolicy() method to create the policy
//The first parameter is the name of the policy and the second parameter is the policy itself
//To satisfy this policy requirements, the logged-in user must have Delete Role claim
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreateRolePolicy",
        policy => policy.RequireClaim("Create Role", "true"));

    options.AddPolicy("EditRolePolicy",
        policy => policy.RequireClaim("Edit Role", "true"));

    options.AddPolicy("DeleteRolePolicy",
        policy => policy.RequireClaim("Delete Role", "true"));

    options.AddPolicy("AllRolePolicy",
        policy => policy.RequireClaim("Create Role", "true")
                        .RequireClaim("Edit Role", "true")
                        .RequireClaim("Delete Role", "true")

            );

    // Use the RequireAssertion method on the AuthorizationPolicyBuilder instance instead of RequireClaim or RequireRole
    // The RequireAssertion() method takes Func<AuthorizationHandlerContext, bool> as a parameter
    // This Func returns AuthorizationHandlerContext as an input parameter and returns a boolean.
    // The AuthorizationHandlerContext instance provides access to user roles and entitlements
    // Func embeds a method, so the code above can be rewritten as follows. We have created a separate method,
    // and referenced it instead of creating the method inline.
    // We can use func to create a custom policy that meets our authorization needs.
    options.AddPolicy("EditRolePolicy", policy => policy.RequireAssertion(context =>
        context.User.IsInRole("Admin") &&
        context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
        context.User.IsInRole("Super Admin")
    ));

    // Authorization handler registration
    // We register custom authorization handler in ConfigureServices() method of the Startup class
    options.AddPolicy("EditRolePolicy", policy =>
            policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));
});


// Authorization handler registration
// We register custom authorization handler in ConfigureServices() method of the Startup class
builder.Services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
