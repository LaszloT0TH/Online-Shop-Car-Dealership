using CarDealershipASPNETMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipASPNETMVC.Data.Service
{
    public class OrderService : EntityIntBaseRepository<OrderModel>, IOrderService
    {
        private readonly AppDbContext context;

        public OrderService(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<OrderModel>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await context.Orders.Include(n => n.OrderItems).ThenInclude(o => o.OrderStatus)
                                            .Include(n => n.OrderItems).ThenInclude(o => o.ShoppingCartStatus)
                                            .Include(n => n.OrderItems).ThenInclude(o => o.CarAccessories).ThenInclude(o => o.carAccessoriesProductGroupModel)
                                            .Include(n => n.OrderItems).ThenInclude(o => o.CarAccessories).ThenInclude(o => o.carAccessoriesUnitModel)
                                            .Include(n => n.OrderItems).ThenInclude(o => o.Cars).ThenInclude(o => o.Fuels)
                                            .Include(n => n.OrderItems).ThenInclude(o => o.Cars).ThenInclude(o => o.Gearbox)
                                            .Include(n => n.User).ToListAsync();
            
            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItemModel> items, string userId, string userEmailAddress)
        {
            var order = new OrderModel()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            foreach (var item in items)
            {
                // deduct it from stock if there is enough or set the car's sold variable to true
                if (item.CarAccessoriesId != null)
                {
                    var carAccessories = await context.CarAccessories.FirstOrDefaultAsync(c => c.Id == item.CarAccessoriesId);
                    if (item.Quantity < carAccessories.QuantityOfStock)
                    {
                        carAccessories.QuantityOfStock = carAccessories.QuantityOfStock - item.Quantity;

                        context.CarAccessories.Update(carAccessories);

                        // if the stock is less than the minimum stock, then a Stock Replenishment list is created
                        if (carAccessories.QuantityOfStock < carAccessories.MinimumStockQuantity)
                        {
                            var listItem = new StockReplenishmentListModel()
                            {
                                ProductId = item.CarAccessoriesId,
                                ProductName = item.CarAccessories.ProductName,
                                OrderedStatus = true,
                                SRLTimeStamp = DateTime.Now
                            };
                            await context.StockReplenishmentList.AddAsync(listItem);
                        }

                        var orderItem = new OrderItemModel()
                        {
                            CarAccessoriesId = item.CarAccessoriesId,
                            CarId = item.CarId,
                            OrderId = order.Id,
                            Quantity = item.Quantity,
                            OrderDate = DateTime.Now,
                            OrderStatusId = item.ShoppingCartOrderStatusId,
                            Discount = 0,
                            ShippedDate = item.ShippedDate,
                            SaleAmount = item.SaleAmount,
                            CountryTaxPercentageValue = item.TaxPercentageValue,
                            ShoppingCartId = item.Id,
                            ShoppingCartStatusId = 2
                        };
                        await context.OrderItems.AddAsync(orderItem);
                    }
                }
                else
                {
                    var car = await context.Cars.FirstOrDefaultAsync(c => c.Id == item.CarId);
                    car.Sold = true;
                    context.Cars.Update(car);

                    var orderItem = new OrderItemModel()
                    {
                        CarAccessoriesId = item.CarAccessoriesId,
                        CarId = item.CarId,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        OrderDate = DateTime.Now,
                        OrderStatusId = item.ShoppingCartOrderStatusId,
                        Discount = 0,
                        ShippedDate = item.ShippedDate,
                        SaleAmount = item.SaleAmount,
                        CountryTaxPercentageValue = item.TaxPercentageValue,
                        ShoppingCartId = item.Id,
                        ShoppingCartStatusId = 2
                    };
                    await context.OrderItems.AddAsync(orderItem);
                }

                // Shopping Cart Status = Bestellt
                var updateItem = await context.ShoppingCartItems.FirstOrDefaultAsync(c => c.Id == item.Id);
                updateItem.ShoppingCartStatusId = 2;
                context.ShoppingCartItems.Update(updateItem);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateOrderStatusAddSalesPersonAsync(int id, string userId)
        {
            // EN
            // update orderStatusId = 2 //Process
            // add salesPerson with Id = User.Id
            // GE
            // update orderStatusId = 2 //Verarbeitung
            // salesPerson mit Id = User.Id hinzufügen
            // HU
            // update orderStatusId = 2 //Feldolgozás alatt
            // értékesítési személy hozzáadása azonosítóval = User.Id
            var order = await context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(i => i.Id == id);

            foreach (var item in order.OrderItems)
            {
                var orderItems = await context.OrderItems.FirstOrDefaultAsync(oi => oi.Id == item.Id);
                orderItems.OrderStatusId = 2;
                orderItems.SalesPersonId = userId;
                context.OrderItems.Update(orderItems);
            }
            await context.SaveChangesAsync();
        }

        public async Task UpdateOrderStatusAndShoppingCartStatusAsync(int id)
        {
            // EN
            // update orderStatusId = 4 //Concluded
            // update shoppingCartStatusId = 4 //Underwegs
            // GE
            // update orderStatusId = 4 //Abgeschlossen
            // update shoppingCartStatusId = 4 //Unterwegs
            // HU
            // update orderStatusId = 4 frissítése //Befejezve
            // update shoppingCartStatusId = 4 //Útközben
            var order = await context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(i => i.Id == id);

            foreach (var item in order.OrderItems)
            {
                var orderItems = await context.OrderItems.FirstOrDefaultAsync(oi => oi.Id == item.Id);
                orderItems.OrderStatusId = 4;
                orderItems.ShoppingCartStatusId = 4;
                context.OrderItems.Update(orderItems);

                var shoppingCartItem = await context.ShoppingCartItems.FirstOrDefaultAsync(oi => oi.Id == item.Id);
                shoppingCartItem.ShoppingCartStatusId = 4;
                context.ShoppingCartItems.Update(shoppingCartItem);
            }
            await context.SaveChangesAsync();
        }

        public async Task UpdateOrderStatusBackPostingToStockAsync(int id)
        {
            // EN
            // update orderStatusId = 3 //Rejected
            // back posting to stock
            // check and if necessary, remove it from the Stock Replenishment list
            // DE
            // update orderStatusId = 3 //Abgelehnt
            // Rückbuchung ins Lager
            // überprüfen und ggf. aus der Bestandsauffüllungsliste entfernen
            // HU
            // OrderStatusId = 3 frissítése //Elutasítva
            // visszaküldés készletre
            // ellenőrizze, és ha szükséges, távolítsa el a Készletfeltöltés listáról
            var order = await context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(i => i.Id == id);

            foreach (var item in order.OrderItems)
            {
                var orderItems = await context.OrderItems.Include(c => c.Cars).Include(ca => ca.CarAccessories).FirstOrDefaultAsync(oi => oi.Id == item.Id);
                orderItems.OrderStatusId = 3;
                context.OrderItems.Update(orderItems);

                if (orderItems.CarAccessories != null)
                {
                    var carAccessories = await context.CarAccessories.FirstOrDefaultAsync(ca => ca.Id == orderItems.CarAccessoriesId);
                    carAccessories.QuantityOfStock = carAccessories.QuantityOfStock + orderItems.CarAccessories.QuantityOfStock;
                    context.CarAccessories.Update(carAccessories);

                    var stockList = await context.StockReplenishmentList.ToListAsync();
                    foreach (var stockListItem in stockList)
                    {
                        if (stockListItem.ProductId == carAccessories.Id && carAccessories.QuantityOfStock > carAccessories.MinimumStockQuantity)
                        {
                            context.StockReplenishmentList.Remove(stockListItem);
                        }
                    }
                }

                if (orderItems.Cars != null)
                {
                    var car = await context.Cars.FirstOrDefaultAsync(c => c.Id == orderItems.CarId);
                    car.Sold = false;
                    context.Cars.Update(car);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
