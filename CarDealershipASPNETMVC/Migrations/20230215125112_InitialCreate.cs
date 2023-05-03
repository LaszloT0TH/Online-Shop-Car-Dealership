using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarDealershipASPNETMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarAccessoriesProductGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CAPGName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarAccessoriesProductGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarAccessoriesUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarAccessoriesUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryTaxPercentageValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fuel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuelName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gearbox",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GearboxName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gearbox", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderStatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sex",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SexName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sex", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingCartStatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpokenLangues",
                columns: table => new
                {
                    SpokenLanguesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpokenLanguesName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpokenLangues", x => x.SpokenLanguesId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarAccessories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CAPGId = table.Column<int>(type: "int", nullable: false),
                    QuantityOfStock = table.Column<int>(type: "int", nullable: false),
                    MinimumStockQuantity = table.Column<int>(type: "int", nullable: false),
                    NetSellingPrice = table.Column<double>(type: "float", nullable: false),
                    SalesUnit = table.Column<double>(type: "float", nullable: false),
                    UnitNameId = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarAccessories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarAccessories_CarAccessoriesProductGroup_CAPGId",
                        column: x => x.CAPGId,
                        principalTable: "CarAccessoriesProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarAccessories_CarAccessoriesUnit_UnitNameId",
                        column: x => x.UnitNameId,
                        principalTable: "CarAccessoriesUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false),
                    YearOfProduction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FuelId = table.Column<int>(type: "int", nullable: false),
                    GearboxId = table.Column<int>(type: "int", nullable: false),
                    CubicCapacity = table.Column<double>(type: "float", nullable: false),
                    Mileage = table.Column<double>(type: "float", nullable: false),
                    ChassisNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnginePower = table.Column<int>(type: "int", nullable: false),
                    OwnWeight = table.Column<int>(type: "int", nullable: false),
                    Sold = table.Column<bool>(type: "bit", nullable: false),
                    NettoPrice = table.Column<double>(type: "float", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Fuel_FuelId",
                        column: x => x.FuelId,
                        principalTable: "Fuel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cars_Gearbox_GearboxId",
                        column: x => x.GearboxId,
                        principalTable: "Gearbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SexId = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostalCode = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Sex_SexId",
                        column: x => x.SexId,
                        principalTable: "Sex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockReplenishmentList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderedStatus = table.Column<bool>(type: "bit", nullable: false),
                    SRLTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReplenishmentList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockReplenishmentList_CarAccessories_ProductId",
                        column: x => x.ProductId,
                        principalTable: "CarAccessories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser_Spokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SpokenLanguesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser_Spokens", x => new { x.UserId, x.SpokenLanguesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUser_Spokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_Spokens_SpokenLangues_SpokenLanguesId",
                        column: x => x.SpokenLanguesId,
                        principalTable: "SpokenLangues",
                        principalColumn: "SpokenLanguesId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesPersonId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarAccessoriesId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CarId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaleAmount = table.Column<double>(type: "float", nullable: false),
                    SaleAmountPaid = table.Column<double>(type: "float", nullable: true),
                    CountryTaxPercentageValue = table.Column<double>(type: "float", nullable: false),
                    SaleTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_CarAccessories_CarAccessoriesId",
                        column: x => x.CarAccessoriesId,
                        principalTable: "CarAccessories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_OrderStatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_ShoppingCartStatus_ShoppingCartStatusId",
                        column: x => x.ShoppingCartStatusId,
                        principalTable: "ShoppingCartStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarAccessoriesId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CarId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaleAmount = table.Column<double>(type: "float", nullable: false),
                    TaxPercentageValue = table.Column<double>(type: "float", nullable: false),
                    SaleTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShoppingCartOrderStatusId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartStatusId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_CarAccessories_CarAccessoriesId",
                        column: x => x.CarAccessoriesId,
                        principalTable: "CarAccessories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_OrderStatus_ShoppingCartOrderStatusId",
                        column: x => x.ShoppingCartOrderStatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ShoppingCartStatus_ShoppingCartStatusId",
                        column: x => x.ShoppingCartStatusId,
                        principalTable: "ShoppingCartStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f154c36-490e-4f6b-baea-5d573a237311", "42e63dfb-590a-4a62-998b-fbed975b18d0", "Manager", "MANAGER" },
                    { "79ce4d97-90ed-4e0d-a99a-7d9b491345f5", "1ec07faf-dd0a-4dd6-af66-a52cf6f667b5", "Admin", "ADMIN" },
                    { "7cc51bee-8c9a-4cdf-ba5b-8bce93d6cede", "f526124a-0a70-4f51-8e40-00952e6a6756", "Salasperson", "SALESPERSON" },
                    { "a38f9a3e-7108-4f0e-98af-70d2636d20c6", "76984178-7bce-41f8-90f9-add937d0610c", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "CarAccessoriesProductGroup",
                columns: new[] { "Id", "CAPGName" },
                values: new object[,]
                {
                    { 1, "Autoinnenraum" },
                    { 2, "Autoreinigungs" },
                    { 3, "Winter-Autozubehör" },
                    { 4, "Straßennotfälle und Erste Hilfe" },
                    { 5, "Zubehör für Autotelefone" },
                    { 6, "Schutzausrüstung" },
                    { 7, "Unterhaltung im Auto" }
                });

            migrationBuilder.InsertData(
                table: "CarAccessoriesUnit",
                columns: new[] { "Id", "UnitName" },
                values: new object[,]
                {
                    { 1, "€uro/Stück" },
                    { 2, "€uro/Liter" },
                    { 3, "€uro/Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryName", "CountryTaxPercentageValue" },
                values: new object[,]
                {
                    { 1, "Albanien", 20.0 },
                    { 2, "Andorra", 4.5 },
                    { 3, "Armenia", 20.0 },
                    { 4, "Belarus", 20.0 },
                    { 5, "Belgien", 21.0 },
                    { 6, "Bosnien und Herzegowina", 17.0 },
                    { 7, "Bulgarien", 20.0 },
                    { 8, "Dänemark", 25.0 },
                    { 9, "Deutschland", 19.0 },
                    { 10, "Estland", 20.0 },
                    { 11, "Finnland", 24.0 },
                    { 12, "Frankreich", 20.0 },
                    { 13, "Georgien", 18.0 },
                    { 14, "Griechenland", 24.0 },
                    { 15, "Irland", 23.0 },
                    { 16, "Island", 24.0 },
                    { 17, "Italien", 22.0 },
                    { 18, "Kasachstan", 20.0 },
                    { 19, "Kosovo", 20.0 },
                    { 20, "Kroatien", 25.0 },
                    { 21, "Lettland", 21.0 },
                    { 22, "Liechtenstein", 8.0 },
                    { 23, "Litauen", 21.0 },
                    { 24, "Luxemburg", 17.0 },
                    { 25, "Malta", 18.0 },
                    { 26, "Moldau", 20.0 },
                    { 27, "Monaco", 0.0 },
                    { 28, "Montenegro", 21.0 },
                    { 29, "Niederlande", 21.0 },
                    { 30, "Nordmazedonien", 18.0 },
                    { 31, "Norwegen", 25.0 },
                    { 32, "Österreich", 20.0 },
                    { 33, "Polen", 23.0 },
                    { 34, "Portugal", 23.0 },
                    { 35, "Rumänien", 19.0 },
                    { 36, "Russland", 20.0 },
                    { 37, "San Marino", 20.0 },
                    { 38, "Schweden", 25.0 },
                    { 39, "Schweiz", 8.0 },
                    { 40, "Serbien", 20.0 },
                    { 41, "Slowakei", 20.0 },
                    { 42, "Slowenien", 21.0 },
                    { 43, "Tschechien", 21.0 },
                    { 44, "Türkei", 18.0 },
                    { 45, "Ukraine", 20.0 },
                    { 46, "Ungarn", 27.0 },
                    { 47, "Vereinigtes Königreich", 20.0 },
                    { 48, "Zypern", 19.0 },
                    { 49, "Andere", 20.0 }
                });

            migrationBuilder.InsertData(
                table: "Fuel",
                columns: new[] { "Id", "FuelName" },
                values: new object[,]
                {
                    { 1, "Strom" },
                    { 2, "Benzin" },
                    { 3, "Diesel" },
                    { 4, "Erdgas" }
                });

            migrationBuilder.InsertData(
                table: "Gearbox",
                columns: new[] { "Id", "GearboxName" },
                values: new object[,]
                {
                    { 1, "manual" },
                    { 2, "automat" },
                    { 3, "Es gibt keine" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "Id", "OrderStatusName" },
                values: new object[,]
                {
                    { 1, "Ausstehend" },
                    { 2, "Verarbeitung" },
                    { 3, "Abgelehnt" },
                    { 4, "Abgeschlossen" }
                });

            migrationBuilder.InsertData(
                table: "Sex",
                columns: new[] { "Id", "SexName" },
                values: new object[,]
                {
                    { 1, "männlich" },
                    { 2, "weiblich" },
                    { 3, "divers" },
                    { 4, "inter" },
                    { 5, "offen" },
                    { 6, "kein Eintrag" }
                });

            migrationBuilder.InsertData(
                table: "ShoppingCartStatus",
                columns: new[] { "Id", "ShoppingCartStatusName" },
                values: new object[,]
                {
                    { 1, "Im Einkaufswagen" },
                    { 2, "Bestellt" },
                    { 3, "Für später gespeichert" },
                    { 4, "Unterwegs" },
                    { 5, "Zugestellt" }
                });

            migrationBuilder.InsertData(
                table: "SpokenLangues",
                columns: new[] { "SpokenLanguesId", "SpokenLanguesName" },
                values: new object[,]
                {
                    { 1, "Albanisch" },
                    { 2, "Arabisch" },
                    { 3, "Deutsch" },
                    { 4, "Englisch" },
                    { 5, "Finnisch" },
                    { 6, "Französisch" },
                    { 7, "Griechisch" },
                    { 8, "Italienisch" },
                    { 9, "Irisch" },
                    { 10, "Niederländisch" },
                    { 11, "Ukrainisch" },
                    { 12, "Ungarisch" },
                    { 13, "Portugiesisch" },
                    { 14, "Rumänisch" },
                    { 15, "Russisch" },
                    { 16, "Schwedisch" },
                    { 17, "Slowakisch" },
                    { 18, "Slowenisch" },
                    { 19, "Weißrussisch" },
                    { 20, "Andere Sprache" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CountryId", "DateOfBirth", "Email", "EmailConfirmed", "EntryDate", "FirstName", "HouseNumber", "LastName", "Location", "LockoutEnabled", "LockoutEnd", "ManagerId", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "SexId", "Street", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "02e69598-6679-4435-8b99-1bea500c87de", 0, "d253c242-1b6b-44c6-9e74-02a162a8868f", 32, new DateTime(2003, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "salasperson@car.com", true, null, "Salasperson", "HouseNumber50", "AppSalasperson", "Wien", false, null, null, null, "SALESPERSON@CAR.COM", "AQAAAAEAACcQAAAAEFo6b1eZDj53kroFWFmikefM3x4hUJv/2zkKq3n3FlPrUfyT/f/bayYpO/SOVn9YNA==", null, false, 1010, "e2f5b9ec-b2e3-4361-a8b7-282b41e21b50", 1, "Street", false, "salasperson@car.com" },
                    { "96ddf804-0261-4d83-821a-c87c37cc87f7", 0, "b0f329ad-9586-4667-8ba0-cda1c77c9ab1", 32, new DateTime(1990, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "manager@car.com", true, null, "Manager", "HouseNumber2", "AppManager", "Wien", false, null, null, null, "MANAGER@CAR.COM", "AQAAAAEAACcQAAAAEO2W9ITnJh4tx9pd/12Kr1XEohQ+Cp0WEMNGWONwisSU6zQfP/dooYiB7Bt9nOfDog==", null, false, 1010, "dcae1578-9311-49a1-a3f5-55eddc47eb0b", 1, "Street", false, "manager@car.com" },
                    { "9e8b5c56-46f5-4ffd-828f-aacf965d17d5", 0, "abdc3825-881e-49c1-9582-a8e5622f1930", 32, new DateTime(2000, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "user@car.com", true, null, "User", "HouseNumber10", "AppUser", "Wien", false, null, null, null, "USER@CAR.COM", "AQAAAAEAACcQAAAAEDcdL+ZtnAJOlfJWsX4+gEoIRAsDFrXHa8JIwLnzhwWM0fpEl5DkNv1WXFw8qiey+A==", null, false, 1010, "4f914fe9-56c1-49ef-bde2-c6255cc01f61", 1, "Street", false, "user@car.com" },
                    { "f8f19bd4-fba0-4a81-8ad1-4ecef9060dc6", 0, "7b524824-796c-4671-8044-5c3a4de92d89", 32, new DateTime(1982, 9, 8, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "admin@car.com", true, null, "Admin", "HouseNumber8", "AppAdmin", "Wien", false, null, null, null, "ADMIN@CAR.COM", "AQAAAAEAACcQAAAAEOichy/adhcdhduthO2GBw9uAABGdWFLhvkXKMBYbedrFn9d0/RHA7q5+JbW3T0CbA==", null, false, 1010, "28556286-840f-4427-8960-3274880ef1f7", 1, "Street", false, "admin@car.com" }
                });

            migrationBuilder.InsertData(
                table: "CarAccessories",
                columns: new[] { "Id", "Brand", "CAPGId", "CreationDate", "Description", "LastUpdateTime", "MinimumStockQuantity", "NetSellingPrice", "PhotoPath", "ProductName", "QuantityOfStock", "SalesUnit", "UnitNameId", "Version" },
                values: new object[,]
                {
                    { "00cdffaf-265c-4d61-97aa-4ae013ed8b79", "SONAX", 2, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Händedesinfektionsmittel", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1025), 100000, 20.899999999999999, null, "Händedesinfektionsmittel", 1000000, 0.5, 2, 1 },
                    { "02c470bf-952b-4a55-89a1-bea7fcb9647c", "EBROM", 4, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Bull-Kabel", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1268), 100000, 53.990000000000002, null, "Bull-Kabel", 1000000, 1.0, 1, 1 },
                    { "035efe2c-732f-4b43-9d59-e6ee29bd10f6", "COFIT", 2, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Gummiwischer Autozubehör", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(992), 100000, 32.990000000000002, null, "Gummiwischer", 1000000, 2.0, 1, 1 },
                    { "04fb6bc8-8e3d-4dcf-b657-73503e5fc627", "Oneil", 4, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Erste-Hilfe-Kit für das Auto", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1191), 100000, 22.989999999999998, null, "Erste-Hilfe-Kit für das Auto", 1000000, 1.0, 1, 1 },
                    { "05fda083-82fd-4b80-beba-f54133b8b424", "Heldenwerk", 4, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Hammer zum Glasbrechen", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1220), 100000, 18.489999999999998, null, "Hammer zum Glasbrechen", 1000000, 1.0, 1, 1 },
                    { "0dcbed03-f220-4c75-989a-51131bf90219", "Uvex", 4, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Arbeitshandschuhe", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1177), 100000, 19.98, null, "Arbeitshandschuhe", 1000000, 1.0, 1, 1 },
                    { "0ef4212e-2e7c-4c4d-b38b-271a8745b3b2", "LE LED", 4, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Taschenlampe", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1285), 100000, 34.990000000000002, null, "Taschenlampe", 1000000, 1.0, 1, 1 },
                    { "13acb8a3-ae21-4d91-865a-1aace267d678", "TAERGU", 6, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Sicheres Schuhwerk", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1508), 100000, 49.990000000000002, null, "Sicheres Schuhwerk", 1000000, 1.0, 1, 1 },
                    { "16bc0bd1-d791-4c65-8de4-a5020f139f61", "VeoPulse", 5, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Freisprecheinrichtung im Auto", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1400), 100000, 59.969999999999999, null, "Freisprecheinrichtung im Auto", 1000000, 5.0, 1, 1 },
                    { "1baacb8a-5e78-46b1-bfeb-f114c0045be1", "Syncwire", 5, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Auto-Ladegerät für Handy", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1414), 100000, 15.99, null, "Auto-Ladegerät für Handy", 1000000, 1.0, 1, 1 },
                    { "1f480591-8089-4e94-8695-c5bf6cbfa391", "Oneil", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Windschutzscheibendecke", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(689), 100000, 18.989999999999998, null, "Windschutzscheibendecke", 1000000, 1.0, 1, 1 },
                    { "1f71b5c9-2c9b-4325-8075-49c2f9a08cd7", "SULWZM", 4, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Warnweste SULWZM", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1253), 100000, 5.9400000000000004, null, "Warnweste", 1000000, 1.0, 1, 1 },
                    { "203e6a60-58d9-4bc4-b8cc-06950c5f61db", "Oneil", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Lenkschloss", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(640), 100000, 89.0, null, "Lenkschloss", 1000000, 1.0, 1, 1 },
                    { "36c1e9cb-6028-4a7e-b8ca-182a7cd519bb", "Oneil", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Kühltasche", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(655), 100000, 17.989999999999998, null, "Kühltasche", 1000000, 1.0, 1, 1 },
                    { "3b497f56-957a-4a50-82ef-4d731282a62b", "LINDENMANN", 2, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Hirschleder Schal Autozubehör Innenraum", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(902), 100000, 20.899999999999999, null, "Hirschleder Schal", 1000000, 1.0, 1, 1 },
                    { "3f2d0472-770f-4ebd-adeb-f27576371553", "LICARGO", 2, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Auto Luftentfeuchter Autozubehör Innenraum", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(961), 100000, 19.989999999999998, null, "Auto Luftentfeuchter", 1000000, 1.0, 1, 1 },
                    { "3f3d61ea-ff53-4822-9db3-6b19061e5047", "GMP Tech", 7, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Akustikschaumstoff Selbstklebend Pyramide Matte 200x100 x 8 cm von GMP Tech beauty of sound - Dämmung", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1571), 100000, 79.989999999999995, null, "Schallschutzplatte", 1000000, 1.0, 1, 1 },
                    { "4a0f71d2-0610-442b-8ca9-051b0f5abb1d", "GENMAG", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Nummernschildhalterung Autozubehör", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(886), 100000, 23.989999999999998, null, "Nummernschildhalterung", 1000000, 1.0, 1, 1 },
                    { "4d2fa68f-2c3d-4ef1-b300-4ba541511617", "Oneil", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Upgrade4cars Beheizter Sitzbezug", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(823), 100000, 37.990000000000002, null, "Beheizter Sitzbezug", 1000000, 1.0, 1, 1 },
                    { "5353d541-550c-48b7-ac37-a3d346d019b4", "LICARGO", 3, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Glasreiniger Autozubehör", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1100), 100000, 29.989999999999998, null, "Glasreiniger", 1000000, 0.69999999999999996, 1, 1 },
                    { "5383182a-22fc-48de-832f-bee542d93f2a", "AEG", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "AEG Automotive Thermoelektrische Kühl- und Warmhaltebox KK 14 Liter, 12 Volt für Auto", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(611), 100000, 62.840000000000003, null, "Kühlbox", 1000000, 1.0, 1, 1 },
                    { "5481de3e-2900-42a9-b86d-b79f15463a6a", "Adapter-Universe", 7, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Autoradio-Kabelsatz Adapter Kabel Auto Radio aktiv System ISO kompatibel mit Audi VW Seat Bose DSP", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1676), 100000, 15.49, null, "Autoradio-Kabelsatz", 1000000, 1.0, 2, 1 },
                    { "5d838a5a-5778-47e1-8954-46720de1ace9", "Pioneer", 7, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Autoradio Pioneer 15,2 cm (6,2 Zoll) 2-DIN-Display mit Bluetooth", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1585), 100000, 164.80000000000001, null, "Autoradio", 1000000, 1.0, 1, 1 },
                    { "62cbc78c-fc64-400d-ad5a-a02e4e5328f4", "SONAX", 2, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Schwämme", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(933), 100000, 8.9499999999999993, null, "Schwämme", 1000000, 5.0, 1, 1 },
                    { "64e7589a-1896-4a46-8f1e-1b0a2df99bf3", "LIONSTRONG", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Sitzbezüge Sitzschoner für Autositze, Sitzbezug Werkstatt Auto, universal Autositzschoner, Sitzbezüge, wasserdichter Stoff (Polyester)", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(576), 100000, 16.989999999999998, null, "Sitzbezüge", 1000000, 1.0, 1, 1 },
                    { "6ecc866a-0a07-408b-9364-9891d4f19968", "MICHELIN", 3, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Schneeketten Autozubehör", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1133), 100000, 107.98999999999999, null, "Schneeketten", 1000000, 1.0, 1, 1 },
                    { "75896f45-f64b-4e96-bbb5-fac02842b116", "LUCKY", 6, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Arbeitshemden Thermojacke Arbeitshemd Herren Holzfäller Langarm", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1555), 100000, 25.899999999999999, null, "Arbeitshemden", 1000000, 1.0, 1, 1 },
                    { "7a77d02d-39ac-4f2f-b14f-b764c6e874f4", "BOSCH", 2, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Hochdruckreiniger Autozubehör", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1011), 100000, 78.819999999999993, null, "Hochdruckreiniger", 1000000, 1.0, 1, 1 },
                    { "8084ee05-f8df-410d-9fb8-dc895fa105d8", "LICARGO", 2, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Felgenreinigungsbürste Autozubehör", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1072), 100000, 19.989999999999998, null, "Felgenreinigungsbürste", 1000000, 1.0, 1, 1 },
                    { "81eec0b4-9282-4614-a9a4-dfcec84fe08b", "Tork", 2, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Papierhandtuch Autozubehör", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1040), 100000, 41.950000000000003, null, "Papierhandtuch", 1000000, 8.0, 1, 1 },
                    { "835fbfc1-c2d0-4ec6-b638-dd2e0279b4ae", "Senner", 6, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Ohrschutz", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1478), 100000, 15.99, null, "Ohrschutz", 1000000, 1.0, 1, 1 },
                    { "838b6baa-a60a-46e1-8d4e-1ef7dc338259", "WillingHeart", 2, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Autowaschbürste Autozubehör", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1056), 100000, 13.99, null, "Autowaschbürste", 1000000, 2.0, 1, 1 },
                    { "861b7af2-d136-4328-9682-91a0a22196cd", "Oneil", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Upgrade4cars Fahrradträger", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(852), 100000, 278.99000000000001, null, "Fahrradträger", 1000000, 1.0, 1, 1 },
                    { "877ba85c-389b-40b3-8598-ce3f6f84cd69", "Tubayia", 4, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Hebegurt", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1371), 100000, 19.989999999999998, null, "Hebegurt", 1000000, 1.0, 1, 1 },
                    { "89917765-defc-40a1-9257-bcd2b280a3a1", "Oneil", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Upgrade4cars Anti-Rutsch-Armaturenbrett", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(805), 100000, 10.99, null, "Anti-Rutsch-Armaturenbrett", 1000000, 1.0, 1, 1 },
                    { "8a663fc5-75ce-4b4c-a2c5-56fcdda466f8", "Brandengel", 4, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Feuerlöscher Brandengel", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1235), 100000, 29.390000000000001, null, "Feuerlöscher", 1000000, 1.0, 1, 1 },
                    { "8a6cc604-2540-4a07-b475-0aa48ae8f6e0", "BOOYES", 7, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Multimedia-Haupteinheit BOOYES für Mercedes-Benz W169 W245 B160 B170 B180 B200 W639 Vito Viano W906 Sprinter", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1629), 100000, 289.99000000000001, null, "Multimedia-Haupteinheit", 1000000, 1.0, 1, 1 },
                    { "8ae22845-d424-430d-ae07-40e3037528b7", "Febreze", 2, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Auto Lufterfrischer Autozubehör Innenraum", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(947), 100000, 59.950000000000003, null, "Auto Lufterfrischer", 1000000, 1.0, 1, 1 },
                    { "8c96f81e-65c5-422e-bce4-1b2f15154b52", "GLART", 2, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Mikrofasertücher Autozubehör", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(918), 100000, 14.99, null, "Mikrofasertücher", 1000000, 5.0, 1, 1 },
                    { "8ccf1c73-c102-4f12-981e-231b8a83b293", "Oneil", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Upgrade4cars Nackenkissen", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(719), 100000, 29.949999999999999, null, "Nackenkissen", 1000000, 1.0, 1, 1 },
                    { "a3660e25-0ab3-4018-9d21-025c5b9a36bb", "Oneil", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Upgrade4cars Armlehne Autozubehör Innenraum", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(837), 100000, 20.5, null, "Armlehne", 1000000, 1.0, 1, 1 },
                    { "a7b673e2-1e9b-4f39-8ebf-feb64ed0ef6f", "Oneil", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Upgrade4cars Lenkradbezug Rot Schwarz Leder Optik Lenkradschutz in Universal Größe 37-39 cm Lenkradhülle für Sommer & Winter Autozubehör Innenraum", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(626), 100000, 29.989999999999998, null, "Lenkradschutz", 1000000, 1.0, 1, 1 },
                    { "b2e0486f-abdc-4b07-aa65-33bd5d8b463a", "ADAC", 6, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Arbeitsjacken und -westen", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1537), 100000, 45.990000000000002, null, "Arbeitsjacken und -westen", 1000000, 1.0, 1, 1 },
                    { "b599db4d-5818-47e3-8b12-6698e0da45a5", "Brubaker", 3, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Skitasche Autozubehör", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1163), 100000, 8.3200000000000003, null, "Skitasche", 1000000, 1.0, 1, 1 },
                    { "c066708c-cdf9-4f54-bedd-3b25fc380b5f", "M MOTOS", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Kofferraumwanne Kofferraummatte passt für Audi Q3 II, unterer Kofferraumboden 2018 Verbessern Sie Ihren Reisekomfort mit Antirutschmatte Auto", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(596), 100000, 49.990000000000002, null, "Gepäckraumschalen", 1000000, 1.0, 1, 1 },
                    { "c48b26e0-84d1-4a1c-915c-e38ae4fe6349", "Oneil", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Upgrade4cars Kindersitz Autozubehör Innenraum", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(867), 100000, 142.99000000000001, null, "Kindersitz", 1000000, 1.0, 1, 1 },
                    { "cb087879-587b-4291-a1a7-491fe1543dc6", "Herdio", 7, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Auto Lautsprecher Herdio 6,5 Zoll Deckenlautsprecher, 160 Watt Bluetooth Einbaulautsprecher, Bündige Montage Sound, für Zuhause Badezimmer Küche Büro mit Full Range Sound", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1600), 100000, 82.989999999999995, null, "Auto Lautsprecher", 1000000, 1.0, 1, 1 },
                    { "ce77726c-bf43-4306-a3c9-8ec0cb72b9f0", "NABIYE", 3, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Dachbox Autozubehör", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1148), 100000, 76.489999999999995, null, "Dachbox", 1000000, 1.0, 1, 1 },
                    { "d2569ea8-be38-40ef-a746-e9391e7c90dd", "Oneil", 3, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Eiskratzer Autozubehör", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1115), 100000, 11.98, null, "Eiskratzer", 1000000, 1.0, 1, 1 },
                    { "d2e947e5-fba0-4939-8958-ff12eeb5b9ba", "ANUNU", 6, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Schutz vor Schweißen Vollmaske mit Filter", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1522), 100000, 45.990000000000002, null, "Schutz vor Schweißen", 1000000, 1.0, 1, 1 },
                    { "d64f1711-f987-4c8d-92e7-3810cf66f704", "SolidWork", 6, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Augenschutz", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1448), 100000, 19.989999999999998, null, "Augenschutz", 1000000, 1.0, 1, 1 },
                    { "dc6442f1-9cf8-470f-9dfe-d85b0c93bfd8", "SONAX", 2, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Reinigungsbürste Autozubehör", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(978), 100000, 13.99, null, "Reinigungsbürste", 1000000, 3.0, 1, 1 },
                    { "e3dc278e-3498-4b9d-8eff-10e79adbf5cb", "VANMASS", 5, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Autotelefonhalter", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1433), 100000, 8.3200000000000003, null, "Autotelefonhalter", 1000000, 1.0, 1, 1 },
                    { "e4d891de-dd4b-4081-a206-8fd5a88588a0", "Sony", 7, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Auto Verstärker Sony XMN1004 Kfz-Verstärker (1000 Watt)", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1614), 100000, 99.900000000000006, null, "Auto Verstärker", 1000000, 1.0, 1, 1 },
                    { "ec6bde56-18a7-4143-a680-67decd0f35ba", "SONAX", 2, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Reinigungstücher Autozubehör", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1086), 100000, 8.3200000000000003, null, "Reinigungstücher", 1000000, 3.0, 1, 1 },
                    { "ed6eef3f-f126-4fed-8243-d4dec9ded2c1", "Vanderfields", 6, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Staubmasken und Atemschutzmasken", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1493), 100000, 26.890000000000001, null, "Staubmasken und Atemschutzmasken", 1000000, 1.0, 1, 1 },
                    { "f20a0ca8-628e-4d04-a70a-bc0fc3932319", "Oneil", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Auto-Staubsauger", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(670), 100000, 49.990000000000002, null, "Auto-Staubsauger", 1000000, 1.0, 1, 1 },
                    { "f34fbd37-0f9b-4920-a783-b6842b34ed88", "CHARON", 4, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Benzinkanister", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1385), 100000, 64.75, null, "Benzinkanister", 1000000, 1.0, 1, 1 },
                    { "f4e263d4-480e-44e5-9841-acaaf452060f", "Tadussi", 4, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Abschleppseil", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1355), 100000, 24.949999999999999, null, "Abschleppseil", 1000000, 1.0, 1, 1 },
                    { "f68aefc7-2a2b-4b37-a58c-7d4f4407757e", "Oneil", 1, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Upgrade4cars Autositzschutz", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(704), 100000, 22.969999999999999, null, "Autositzschutz", 1000000, 1.0, 1, 1 },
                    { "f7d4b4f4-801a-467d-8831-1c356b056900", "Uvex", 6, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Arbeitshosen und Overalls", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1462), 100000, 59.990000000000002, null, "Arbeitshosen und Overalls", 1000000, 1.0, 1, 1 },
                    { "f814b368-c01f-40ad-b688-c33824962724", "Brubaker", 4, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Warndreieck", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1206), 100000, 16.989999999999998, null, "Warndreieck", 1000000, 1.0, 1, 1 },
                    { "f904e173-5fb4-4da6-be9d-975462c99b12", "JBL", 7, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Subwoofer JBL Stadium 102SSI 10 Zoll Subwoofer Auto Set von Harman Kardon - Leistungsstarke 1350 Watt Kfz Bassbox Autolautsprecher - 30Hz – 175Hz - 250mm mit SSI Impedanzschalter", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1644), 100000, 75.989999999999995, null, "Subwoofer", 1000000, 1.0, 1, 1 },
                    { "f949c6c9-79cb-46c1-a9e7-c58e310069fb", "Hama", 7, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567), "Autoradio-Kondensator Hama Hochleistungs-Entstörfilter, 10 Amp", new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(1658), 100000, 14.99, null, "Autoradio-Kondensator", 1000000, 1.0, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ChassisNumber", "Color", "CubicCapacity", "EnginePower", "FuelId", "GearboxId", "LastUpdateTime", "Mileage", "Model", "NettoPrice", "NumberOfSeats", "OwnWeight", "PhotoPath", "Sold", "YearOfProduction" },
                values: new object[,]
                {
                    { "0367adf0-0e48-4695-9ae0-4789aa12e979", "dsfg8ds5fssgfsd65", "schwarz", 4000.0, 210, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(74), 160000.0, "Bmw", 4500.0, 3, 1640, null, false, new DateTime(2010, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "04fabd32-4e33-4c8b-a18a-d665a6f7d069", "srg55d12fg21dfs9", "Weiss", 5177.0, 120, 2, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(202), 512833.0, "Audi", 3000.0, 2, 1852, null, false, new DateTime(1999, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "103c9a80-1a14-47d9-b91a-f702a7289952", "vsda6ffertew8sdf3", "weiss", 1408.0, 56, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(40), 214563.0, "Skoda", 1500.0, 5, 950, null, false, new DateTime(2002, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "2397edd8-3527-48f2-957f-9697d6d2ed10", "vs3daffew8sdf3", "Weis", 1420.0, 57, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 563, DateTimeKind.Local).AddTicks(9995), 214563.0, "Skoda", 1500.0, 5, 950, null, false, new DateTime(2003, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "27f361ff-d52b-4efa-9c91-bbef81a2ac24", "dsfgd13s5fssgfsd710", "dunkelpink", 5660.0, 142, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(219), 160260.0, "Bmw", 2800.0, 2, 1950, null, false, new DateTime(2005, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "3510fbb0-f75e-49d4-9f7c-e9568ffe8c82", "we36xsgvf6556dfseh2", "rot", 0.0, 122, 1, 3, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(25), 298603.0, "Tesla", 35000.0, 5, 989, null, false, new DateTime(2019, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "49978b50-50fd-474c-b06d-8129f8a11715", "XFOW5GH125GW65NS9FF", "Blau", 2500.0, 150, 2, 2, new DateTime(2023, 2, 15, 13, 51, 11, 563, DateTimeKind.Local).AddTicks(9882), 156478.0, "BMW", 6200.0, 5, 1450, null, false, new DateTime(2016, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "588d1664-c8dc-4e1a-ac7c-d92695ab530c", "XFOW3AT98573GWNS9FF", "Schwarz", 950.0, 54, 2, 1, new DateTime(2023, 2, 15, 13, 51, 11, 563, DateTimeKind.Local).AddTicks(9928), 84335.0, "Citoen", 5300.0, 5, 988, null, false, new DateTime(2018, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "6268b991-2d2f-46a9-ba34-cf357fbd5394", "dsfgds5fssgf24sd821", "schwarz", 5660.0, 142, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(380), 309455.0, "Mondeo", 3999.0, 2, 1667, null, false, new DateTime(2005, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "6525f990-89e3-44e1-81cf-48923b02d0b0", "vsdaffew8s22df19", "weiss", 4693.0, 44, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(346), 9944.0, "Passat", 5900.0, 3, 1778, null, false, new DateTime(1997, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "6ddc5bc8-843a-49ea-af34-a5d96e728687", "XFOW3AT12563GWNS9FF", "Gelb", 1040.0, 61, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 563, DateTimeKind.Local).AddTicks(9899), 150355.0, "Suzuki", 4500.0, 5, 1120, null, false, new DateTime(2018, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "731a6aca-758c-4f14-948c-ed10a21362cf", "wf0utkw20sd4417", "schwarz", 3726.0, 89, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(331), 204353.0, "Primera", 6600.0, 2, 1550, null, false, new DateTime(2004, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "77248bbc-3f4c-4546-b24d-7d6d545ef309", "wf0utk14wsd4411", "schwarz", 6143.0, 103, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(234), 267003.0, "Ford", 4299.0, 2, 2050, null, false, new DateTime(2007, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "845b9a2a-047a-4e71-977b-ec90efc7cb9c", "XFOW323EG56692GWNS9FF", "Grün", 2002.0, 110, 2, 1, new DateTime(2023, 2, 15, 13, 51, 11, 563, DateTimeKind.Local).AddTicks(9863), 350355.0, "Mercedes", 5100.0, 5, 1670, null, false, new DateTime(2012, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "8721af74-520b-4ba8-82d8-ca19a385bb43", "XFOW3AT612G54WNS9FF", "Weis", 1670.0, 75, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 563, DateTimeKind.Local).AddTicks(9843), 298603.0, "Opel", 3500.0, 5, 1170, null, false, new DateTime(2014, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "92296a71-66d3-4f92-957a-1c567aaf9bc5", "wf0utkwsd154312", "lila", 1795.0, 86, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(253), 356197.0, "Polo", 2899.0, 5, 1350, null, false, new DateTime(2001, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "948ffe3e-e7da-436b-899d-bb0e09ecbd23", "srg55dfg721dfs4", "pink", 2300.0, 190, 2, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(59), 50000.0, "Audi", 5299.0, 3, 1400, null, false, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "99252330-4b2b-45e8-aaab-766c7e85a6f4", "vsda11ffew8sdf8", "hellrot", 4693.0, 44, 3, 2, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(120), 214563.0, "Skoda", 2399.0, 5, 1750, null, false, new DateTime(1997, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "b5dc49fe-caac-4888-8968-1c7b906eabb3", "XFOW3AT345253GWNS9FF", "Pink", 1400.0, 66, 3, 2, new DateTime(2023, 2, 15, 13, 51, 11, 563, DateTimeKind.Local).AddTicks(9947), 25355.0, "Volkswagen", 6250.0, 5, 972, null, false, new DateTime(2020, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "ba4b951f-a80e-4d13-9aa4-bf04c512d209", "wf0utk4wsd4ht21", "grau", 1783.0, 85, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(10), 298003.0, "Ford", 1850.0, 5, 1320, null, false, new DateTime(2004, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "c64a6e17-65d6-42d5-8021-fd236363aaec", "srg55dfg2123dfs20", "pink", 5196.0, 122, 2, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(362), 363636.0, "C Class", 2900.0, 2, 1860, null, false, new DateTime(1999, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "c7f82ef6-2593-4410-9761-57b7e3e586b6", "we36xsgvf61656dfs13", "grau", 1900.0, 132, 1, 3, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(268), 10591.0, "A4", 5599.0, 5, 1315, null, false, new DateTime(2019, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "d141c387-794d-42ca-9f63-b47016ecf75e", "wf0utkwsd9436", "lila", 3726.0, 89, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(88), 86958.0, "Ford", 2300.0, 5, 1550, null, false, new DateTime(2004, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "d921ed83-cb6b-42ff-9c57-1202e9218876", "wf0utk1wsd421", "grau", 1783.0, 85, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 563, DateTimeKind.Local).AddTicks(9964), 289000.0, "Ford", 1800.0, 5, 1300, null, false, new DateTime(2003, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "db02ef68-7ae2-49f5-a69c-c562687f6824", "XFOW3AT152HhWNS9FF", "silber", 1765.0, 85, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 563, DateTimeKind.Local).AddTicks(9782), 287355.0, "Ford", 2700.0, 5, 1270, null, false, new DateTime(2013, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "de959079-fb80-44b1-a1af-a14e0f07eb44", "vsdaf17few8sdf14", "hellrot", 1988.0, 95, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(284), 311713.0, "Vectra", 2800.0, 3, 1950, null, false, new DateTime(2003, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "e2ffbc03-da80-4f2e-9c8d-bb0ca96876ab", "dsfgds195fssgfsd716", "dunkelpink", 4133.0, 210, 3, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(316), 306806.0, "Civic", 3900.0, 5, 1620, null, false, new DateTime(2010, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "f56b5c77-6679-4808-a7e3-61e74a887ac2", "we36x10sgvf656dfs7", "grau", 0.0, 125, 1, 3, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(105), 59676.0, "Tesla", 12999.0, 5, 1010, null, false, new DateTime(2017, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "f79f4961-c661-4918-a2fe-b44d8ce08702", "srg55dfg2118dfs15", "weiss", 2315.0, 190, 2, 1, new DateTime(2023, 2, 15, 13, 51, 11, 564, DateTimeKind.Local).AddTicks(300), 120890.0, "Saxo", 4800.0, 5, 1406, null, false, new DateTime(2015, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) },
                    { "f8f5182e-d973-4a71-9f3f-a7b911d31a0d", "we36xsgvf2656dfs2", "rot", 1900.0, 130, 1, 3, new DateTime(2023, 2, 15, 13, 51, 11, 563, DateTimeKind.Local).AddTicks(9980), 0.0, "Tesla", 28000.0, 5, 1000, null, false, new DateTime(2019, 10, 20, 15, 9, 12, 123, DateTimeKind.Unspecified).AddTicks(4567) }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUser_Spokens",
                columns: new[] { "SpokenLanguesId", "UserId" },
                values: new object[,]
                {
                    { 3, "f8f19bd4-fba0-4a81-8ad1-4ecef9060dc6" },
                    { 4, "f8f19bd4-fba0-4a81-8ad1-4ecef9060dc6" },
                    { 6, "f8f19bd4-fba0-4a81-8ad1-4ecef9060dc6" },
                    { 12, "f8f19bd4-fba0-4a81-8ad1-4ecef9060dc6" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "Create Role", "true", "f8f19bd4-fba0-4a81-8ad1-4ecef9060dc6" },
                    { 2, "Edit Role", "true", "f8f19bd4-fba0-4a81-8ad1-4ecef9060dc6" },
                    { 3, "Delete Role", "true", "f8f19bd4-fba0-4a81-8ad1-4ecef9060dc6" },
                    { 4, "Create Role", "true", "02e69598-6679-4435-8b99-1bea500c87de" },
                    { 5, "Create Role", "true", "96ddf804-0261-4d83-821a-c87c37cc87f7" },
                    { 6, "Edit Role", "true", "96ddf804-0261-4d83-821a-c87c37cc87f7" },
                    { 7, "Delete Role", "true", "96ddf804-0261-4d83-821a-c87c37cc87f7" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "7cc51bee-8c9a-4cdf-ba5b-8bce93d6cede", "02e69598-6679-4435-8b99-1bea500c87de" },
                    { "1f154c36-490e-4f6b-baea-5d573a237311", "96ddf804-0261-4d83-821a-c87c37cc87f7" },
                    { "a38f9a3e-7108-4f0e-98af-70d2636d20c6", "9e8b5c56-46f5-4ffd-828f-aacf965d17d5" },
                    { "79ce4d97-90ed-4e0d-a99a-7d9b491345f5", "f8f19bd4-fba0-4a81-8ad1-4ecef9060dc6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_Spokens_SpokenLanguesId",
                table: "ApplicationUser_Spokens",
                column: "SpokenLanguesId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CountryId",
                table: "AspNetUsers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SexId",
                table: "AspNetUsers",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CarAccessories_CAPGId",
                table: "CarAccessories",
                column: "CAPGId");

            migrationBuilder.CreateIndex(
                name: "IX_CarAccessories_UnitNameId",
                table: "CarAccessories",
                column: "UnitNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_FuelId",
                table: "Cars",
                column: "FuelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_GearboxId",
                table: "Cars",
                column: "GearboxId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CarAccessoriesId",
                table: "OrderItems",
                column: "CarAccessoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CarId",
                table: "OrderItems",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderStatusId",
                table: "OrderItems",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ShoppingCartStatusId",
                table: "OrderItems",
                column: "ShoppingCartStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_CarAccessoriesId",
                table: "ShoppingCartItems",
                column: "CarAccessoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_CarId",
                table: "ShoppingCartItems",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                table: "ShoppingCartItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartOrderStatusId",
                table: "ShoppingCartItems",
                column: "ShoppingCartOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartStatusId",
                table: "ShoppingCartItems",
                column: "ShoppingCartStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReplenishmentList_ProductId",
                table: "StockReplenishmentList",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUser_Spokens");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "StockReplenishmentList");

            migrationBuilder.DropTable(
                name: "SpokenLangues");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "ShoppingCartStatus");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "CarAccessories");

            migrationBuilder.DropTable(
                name: "Fuel");

            migrationBuilder.DropTable(
                name: "Gearbox");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CarAccessoriesProductGroup");

            migrationBuilder.DropTable(
                name: "CarAccessoriesUnit");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Sex");
        }
    }
}
