## Talabat API
  This API is built with ASP.NET Core following Clean Architecture principles. It serves as the backend for an e-commerce platform, handling product management, order processing, shopping carts.

## Features
- Product Management: List products, with details like name, description, price, discount, and image URL.
- Types: Organize products into various Types.
- Brands: Organize products into various Brands.
- Shopping Cart: Allows users to manage items in their cart.
- Orders: Supports order placement and tracking, including shipping and total calculations.
- Address: Order shipping place.
- User Authentication: Uses ASP.NET Identity for secure user management.

## System Overview
The System Services Overview:
![Alt text](https://github.com/AhmedMustafaElshennawy/Talabat/blob/master/Diagrams/cloudArchitecture.png)

## Database Schema

The database consists of the following tables:
![Alt text](https://github.com/AhmedMustafaElshennawy/Talabat/blob/master/Diagrams/DataBaseSchema.png)

## Tables
### 1. **Products**

Fields : Id, Name, Description, PictureUrl, Price, Discount, CreatedOn, CategoryId
Stores product details and links to categories.
### 2. **Types**

Fields: Id, Name, Description,
Represents product Type with names and descriptions.
### 3. **Brands**

Fields: Id, Name, Description,
Represents product Brand with names and descriptions.
### 4. **Orders**

Fields: Id, OrderTotalAmount, OrderTotalDiscount, OrderDate, Street, City, PostalCode, Country, ApplicationUserId
Manages user orders with address and total amount details.
### 5. **OrderItems**

Fields: OrderItemId, OrderId, ProductId, Quantity, Price
Represents individual items in an order.
### 6. **Address**

Fields: ReviewId, ProductId, ApplicationUserId, Rating, Comment, ReviewDate
Allows users to submit ratings and comments for products.

### 6. **DeliveryMethod**
Stores shipping options and costs.

## Authentication & Authorization

- The API uses JWT (JSON Web Tokens) for authentication.
- Role-based authorization is implemented to restrict access to certain endpoints.
