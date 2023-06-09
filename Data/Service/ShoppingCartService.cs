﻿using CarDealershipASPNETMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipASPNETMVC.Data.Service
{
    public class ShoppingCartService : EntityIntBaseRepository<ShoppingCartModel>, IShoppingCartService
    {
        private readonly AppDbContext context;

        public ShoppingCartService(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        static bool ShoppingInProgress = false;

        public async Task EditQuantityAsync(ShoppingCartItemModel shoppingCartItem)
        {
            var cartItem = context.ShoppingCartItems.FirstOrDefault(x => x.Id == shoppingCartItem.Id);
            cartItem.Quantity = shoppingCartItem.Quantity;
            await context.SaveChangesAsync();
        }

        public async Task SaveForLater(int id)
        {
            var shoppingCartItem = await context.ShoppingCartItems.FirstOrDefaultAsync(n => n.Id == id);
            shoppingCartItem.ShoppingCartStatusId = 3; // Für später gespeichert
            await context.SaveChangesAsync();
        }

        public async Task AddToCart(int id)
        {
            var shoppingCartItem = await context.ShoppingCartItems.FirstOrDefaultAsync(n => n.Id == id);
            shoppingCartItem.ShoppingCartStatusId = 1; // Im Einkaufswagen
            await context.SaveChangesAsync();
        }
 
        public async Task AddCarAccessoriesItemToCart(CarAccessoriesModel carAccessoriesItem, int quantity, string userId, string userRole)
        {
            // User data
            var user = await context.Users.FindAsync(userId);

            // Tax Percentage Value
            var tax = await context.Country.FirstOrDefaultAsync(co => co.Id == user.CountryId);
            double taxPercentageValue = 0; if (tax != null) taxPercentageValue = tax.CountryTaxPercentageValue;

            //The last ShoppingCart Id
            int nextShoppingCartId = 1;
            var ShoppingCart = await context.ShoppingCarts.OrderByDescending(sc => sc.Id).FirstOrDefaultAsync();
            if (ShoppingCart != null) nextShoppingCartId = Convert.ToInt32(ShoppingCart.Id);

            // started shopping?
            if (!ShoppingInProgress)
            {
                nextShoppingCartId++;
                ShoppingInProgress = true;
            }

            // ShoppingCart data 
            var shoppingCart = await context.ShoppingCarts.FirstOrDefaultAsync(n => n.Id == nextShoppingCartId);

            //The last ShoppingCartItem Id
            var shoppingCartItemLast = await context.ShoppingCartItems.OrderByDescending(spi => spi.Id).FirstOrDefaultAsync();
            int nextShoppingCartItemId = 0; if (shoppingCartItemLast != null) nextShoppingCartItemId = shoppingCartItemLast.Id;

            // Whether the item is in the current shopping cart list
            var shoppingCartItem = await context.ShoppingCartItems
                .FirstOrDefaultAsync(spi => spi.CarAccessories.Id == carAccessoriesItem.Id && spi.ShoppingCartId == nextShoppingCartId);

            // Checking if there is enough stock. Deduction from stock if already ordered
            var carAccessories = await context.CarAccessories.FirstOrDefaultAsync(ca => ca.Id == carAccessoriesItem.Id);
            if (carAccessories != null && quantity < carAccessories.QuantityOfStock)
            {
                // Create Stock Replenishment List
                if (carAccessories.QuantityOfStock < carAccessories.MinimumStockQuantity)
                {
                    var newListItem = new StockReplenishmentListModel()
                    {
                        ProductName = carAccessories.ProductName,
                        ProductId = carAccessories.Id,
                        SRLTimeStamp = DateTime.Now,
                        OrderedStatus = true
                    };
                    context.StockReplenishmentList.Add(newListItem);
                    await context.SaveChangesAsync();
                }

                if (shoppingCart == null)
                {
                    shoppingCart = new ShoppingCartModel()
                    {
                        UserId = userId,
                        Email = user.Email
                    };

                    await context.ShoppingCarts.AddAsync(shoppingCart);
                    await context.SaveChangesAsync();

                    shoppingCartItem = new ShoppingCartItemModel()
                    {
                        CarAccessoriesId = carAccessoriesItem.Id,
                        Quantity = quantity,
                        ShippedDate = DateTime.Now.AddDays(4),
                        SaleAmount = carAccessoriesItem.NetSellingPrice,
                        TaxPercentageValue = taxPercentageValue,
                        ShoppingCartOrderStatusId = 1, // Ausstehend
                        ShoppingCartStatusId = 1, // Im Einkaufswagen
                        ShoppingCartId = shoppingCart.Id // next Id 
                    };

                    await context.ShoppingCartItems.AddAsync(shoppingCartItem);
                    await context.SaveChangesAsync();
                }

                else
                {
                    if (shoppingCartItem == null)
                    {
                        shoppingCartItem = new ShoppingCartItemModel()
                        {
                            CarAccessoriesId = carAccessoriesItem.Id,
                            Quantity = quantity,
                            ShippedDate = DateTime.Now.AddDays(4),
                            SaleAmount = carAccessoriesItem.NetSellingPrice,
                            TaxPercentageValue = taxPercentageValue,
                            ShoppingCartOrderStatusId = 1, // Ausstehend
                            ShoppingCartStatusId = 1, // Im Einkaufswagen
                            ShoppingCartId = shoppingCart.Id // next Id 
                        };

                        await context.ShoppingCartItems.AddAsync(shoppingCartItem);
                        await context.SaveChangesAsync();
                    }

                    else
                    {
                        shoppingCartItem.Quantity = quantity;
                        await context.ShoppingCartItems.AddAsync(shoppingCartItem);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task AddCarItemToCart(CarModel carItem, string userId, string userRole)
        {
            // User data
            var user = await context.Users.FindAsync(userId);

            // Tax Percentage Value
            var tax = await context.Country.FirstOrDefaultAsync(co => co.Id == user.CountryId);
            double taxPercentageValue = 0; if (tax != null) taxPercentageValue = tax.CountryTaxPercentageValue;

            //The last ShoppingCart Id
            int nextShoppingCartId = 1;
            var ShoppingCart = await context.ShoppingCarts.OrderByDescending(sc => sc.Id).FirstOrDefaultAsync();
            if (ShoppingCart != null) nextShoppingCartId = Convert.ToInt32(ShoppingCart.Id);

            // started shopping?
            if (!ShoppingInProgress)
            {
                nextShoppingCartId++;
                ShoppingInProgress = true;
            }

            // ShoppingCart data 
            var shoppingCart = await context.ShoppingCarts.FirstOrDefaultAsync(n => n.Id == nextShoppingCartId);

            //The last ShoppingCartItem Id
            var shoppingCartItemLast = await context.ShoppingCartItems.OrderByDescending(sp => sp.Id).FirstOrDefaultAsync();
            int nextShoppingCartItemId = 0; if (shoppingCartItemLast != null) nextShoppingCartItemId = shoppingCartItemLast.Id;

            // Whether the item is in the current shopping cart list
            var shoppingCartItem = await context.ShoppingCartItems
                .FirstOrDefaultAsync(car => car.Cars.Id == carItem.Id && car.ShoppingCartId == nextShoppingCartId);


            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCartModel()
                {
                    UserId = userId,
                    Email = user.Email
                };

                await context.ShoppingCarts.AddAsync(shoppingCart);
                await context.SaveChangesAsync();

                shoppingCartItem = new ShoppingCartItemModel()
                {
                    CarId = carItem.Id,
                    Quantity = 1, 
                    ShippedDate = DateTime.Now.AddDays(4),
                    SaleAmount = carItem.NettoPrice,
                    TaxPercentageValue = taxPercentageValue,
                    ShoppingCartOrderStatusId = 1, // Ausstehend
                    ShoppingCartStatusId = 1, // Im Einkaufswagen
                    ShoppingCartId = shoppingCart.Id // next Id 
                };

                await context.ShoppingCartItems.AddAsync(shoppingCartItem);
                await context.SaveChangesAsync();
            }

            else
            {
                if (shoppingCartItem == null)
                {
                    shoppingCartItem = new ShoppingCartItemModel()
                    {
                        CarId = carItem.Id,
                        Quantity = 1,
                        ShippedDate = DateTime.Now.AddDays(4),
                        SaleAmount = carItem.NettoPrice,
                        TaxPercentageValue = taxPercentageValue,
                        ShoppingCartOrderStatusId = 1, // Ausstehend
                        ShoppingCartStatusId = 1, // Im Einkaufswagen
                        ShoppingCartId = shoppingCart.Id // next Id 
                    };

                    await context.ShoppingCartItems.AddAsync(shoppingCartItem);
                    await context.SaveChangesAsync();
                }

                else
                {
                    shoppingCartItem.Quantity = 1;
                    await context.ShoppingCartItems.AddAsync(shoppingCartItem);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task Remove(int id)
        {
            var shoppingCartItem = await context.ShoppingCartItems.FirstOrDefaultAsync(n => n.Id == id);

            if (shoppingCartItem != null)
            {
                context.ShoppingCartItems.Remove(shoppingCartItem);
            }
            await context.SaveChangesAsync();
        }

        public async Task<List<ShoppingCartModel>> GetShoppingCartByUserIdAndRoleAsync(string userId, string userRole)
        {
            var shoppingCart = await context.ShoppingCarts.Include(n => n.ShoppingCartItem).ThenInclude(n => n.CarAccessories)
                                                    .Include(n => n.ShoppingCartItem).ThenInclude(n => n.Cars)
                                                    .Include(n => n.ShoppingCartItem).ThenInclude(n => n.OrderStatus)
                                                    .Include(n => n.ShoppingCartItem).ThenInclude(n => n.ShoppingCartStatus)
                                                    .Include(n => n.User).ToListAsync();

            if (userRole != "Admin")
            {
                shoppingCart = shoppingCart.Where(n => n.UserId == userId).ToList();
            }

            return shoppingCart;
        }

        public async Task<ShoppingCartItemModel> GetByItemIdAsync(int id)
        {
            var shoppingCartItem = await context.ShoppingCartItems.Include(c => c.CarAccessories).ThenInclude(m => m.carAccessoriesProductGroupModel)
                                                                  .Include(c => c.CarAccessories).ThenInclude(u => u.carAccessoriesUnitModel)
                                                                  .FirstOrDefaultAsync(n => n.Id == id);
            return shoppingCartItem;
        }

        public async Task<List<ShoppingCartItemModel>> GetShoppingCartItems()
        {
            // Im Einkaufswagen
            var shoppingCartItems = await context.ShoppingCartItems.Where(sc => sc.ShoppingCartStatusId == 1).ToListAsync();
            return shoppingCartItems;
        }

    }
}
