# Online-Shop-Car-Dealership
ASP.Net Core MVC with Entity Framework Web Server with Razor Pages. Data storage in a database on MSSQL Server.

Microsoft identity user authentication. Security roles and privileges.
Google and Facebook authentication.
Email confirmation. Set custom email confirmation time.
Change login password. Banning a user after too many failed login attempts.
Url Id encryption.

Online Shop provides customers, unregistered users and data processing for car dealership employees. Four types of isolation levels: guest, registered customer, employee, manager.
Guest access: list of cars and car accessories, search in the list.
Registered customer: add guest + car accessory product to shopping cart, change quantity, order + own shopping cart (= draft order list), edit, delete shopping cart, send its contents to actual orders. The contents of the basket are stored in a data bank for a long time.
Seller: registered customer + add, edit, delete cars and car accessories + add, search, delete, edit customer data + search, delete, edit customer data.
Manager: Seller + add, search, delete sellers, edit their data + inventory upload list. Change of minimum stock. Car accessory product group, sales units, countries, fuels, transmission types, order statuses, genders, foreign languages insert, update, delete functions.
Stock upload list (automatically generated and maintained list): car accessories have a minimum stock quantity property, this value is set for every change in the stock quantity (=insert, update, delete functions). If the inventory falls below the minimum level, the product is added to the inventory replenishment list. If the stock is full, delete the product from the list. The associated button can only exist if the list contains an element.
Admin: full access.

Shopping cart has five states: In cart and Ordered, Saved for later, On the way, Shipped.
In the shopping cart and Saved for later status, the Buyer and the seller and Manager can edit the quantity of the car accessory product, the seller can also add customers and cars.
Ordered, On the Way, Delivered There is no possibility to edit the list, only the seller and Manager can access it when processing orders.

Orders: There is no seller ID when receiving online customer car accessory orders. During seller processing, the order receives the seller's ID. Then the status of the shopping cart changes to: Ordered.
Your order status:
Pending. The seller modifies the order, can change the product, the quantity (in this case he also overwrites the contents of the shopping cart), can also add a discount, then he receives the seller's ID, the seller can be changed with manager access.
You will receive a status under processing after pressing the Submit button, if you have manager access, your order status can be selected.
Rejected, possible connection point by bank transfer.
Completed status: if the money transfer has taken place and the order has been compiled, the package has been handed over to the delivery service. The shopping cart gets status on the way.

The order status is set to completed and the sale time is timestamped
The shopping cart status = on the way, if the money has been received and the package is ready and handed over to the courier service
if I update the product or the quantity of the product in the orders table, I also update it in the shopping cart table.

Cars and Car Accessories have an image property that can be changed.

