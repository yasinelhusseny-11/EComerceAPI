# E-Commerce API

A RESTful E-Commerce Web API built with **ASP.NET Core**, **Entity Framework Core**, and **SQL Server**.

This project was developed to practice building a real-world backend application using clean architecture concepts, repository pattern, DTOs, AutoMapper, authentication, authorization, validation, business logic, and global exception handling.

---

## 🚀 Features

* Product management
* Category management
* Shopping cart management
* Cart items management
* Order creation and management
* User authentication using JWT
* Authorization
* DTO-based API responses
* AutoMapper for object mapping
* Input validation
* Repository Pattern
* Dependency Injection
* Entity Framework Core
* SQL Server database
* Entity relationships
* Global Exception Handling Middleware
* Swagger API documentation
* RESTful API endpoints

---

## 🛠️ Technologies

* **C#**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **LINQ**
* **JWT Authentication**
* **AutoMapper**
* **Swagger / OpenAPI**
* **Repository Pattern**
* **Dependency Injection**
* **Data Annotations / Validation**
* **Git & GitHub**

---

## 🏗️ Architecture & Development Process

The project was developed progressively through the following stages:

```text
Models
   ↓
DbContext
   ↓
Relationships
   ↓
Migrations & Database
   ↓
Repositories
   ↓
Dependency Injection
   ↓
Controllers
   ↓
DTOs
   ↓
AutoMapper
   ↓
Validation
   ↓
JWT Authentication & Authorization
   ↓
Cart & Order Business Logic
   ↓
Global Exception Handling
   ↓
Swagger & API Testing
```

This approach helped keep the application organized and separated responsibilities between different layers.

---

## 📁 Project Structure

```text
EComerceAPI
│
├── Controllers
│   ├── ProductController.cs
│   ├── CategoryController.cs
│   ├── CartController.cs
│   └── OrderController.cs
│
├── Data
│   └── EComerceContext.cs
│
├── DTOs
│   ├── ProductDto.cs
│   ├── CategoryDto.cs
│   ├── CartDto.cs
│   ├── CartItemDto.cs
│   └── OrderDto.cs
│
├── Exceptions
│   └── GlobalExceptionHandlerMiddleware.cs
│
├── Models
│   ├── Product.cs
│   ├── Category.cs
│   ├── User.cs
│   ├── Cart.cs
│   ├── CartItem.cs
│   ├── Order.cs
│   └── OrderItem.cs
│
├── Repositories
│   ├── IProductRepository.cs
│   ├── ProductRepository.cs
│   ├── ICartRepository.cs
│   ├── CartRepository.cs
│   ├── IOrderRepository.cs
│   └── OrderRepository.cs
│
├── Migrations
│
├── MappingProfile.cs
│
├── Program.cs
├── appsettings.json
└── EComerceAPI.csproj
```

> The exact files and structure may evolve as the project is extended.

---

# 🗄️ Database Design

The application uses **SQL Server** as the database and **Entity Framework Core** as the ORM.

The main entities are:

```text
Category
   │
   └── Products
          │
          ├── CartItems
          │
          └── OrderItems

User
 │
 └── Cart
       │
       └── CartItems

User
 │
 └── Orders
       │
       └── OrderItems
```

Entity relationships were configured using Entity Framework Core before creating migrations and updating the database.

---

# 📦 Product Management

The API provides CRUD operations for products.

Main operations include:

```http
GET     /api/Product
GET     /api/Product/{id}
POST    /api/Product
PUT     /api/Product/{id}
DELETE  /api/Product/{id}
```

Product information is exposed through DTOs instead of directly returning the entity.

Example product properties:

```text
Id
Name
Description
Price
StockQuantity
ImageUrl
CategoryId
```

This prevents exposing unnecessary entity information and provides better control over the API response.

---

# 🛒 Shopping Cart

The project includes shopping cart functionality.

The cart system handles:

* Creating/retrieving a user's cart
* Adding products to the cart
* Updating item quantities
* Removing items
* Managing cart items
* Connecting cart items with products

The cart logic is implemented through the repository layer and exposed through API controllers.

---

# 📦 Orders

The API also contains order functionality.

The order system works with:

* Orders
* Order Items
* Products
* Users
* Cart data

The business logic connects the shopping cart with the order creation process.

This provides a foundation that can later be extended with features such as:

* Payment processing
* Order status management
* Shipping
* Order history
* Inventory management

---

# 🔐 Authentication & Authorization

The project uses **JWT (JSON Web Tokens)** for authentication.

The authentication flow is based on:

```text
User Login
    ↓
Validate Credentials
    ↓
Generate JWT
    ↓
Client Receives Token
    ↓
Client Sends Token
    ↓
API Validates Token
    ↓
Authorized Request
```

Protected endpoints can require a valid JWT token through ASP.NET Core authorization.

JWT provides a stateless authentication mechanism suitable for REST APIs.

---

# 🔄 Repository Pattern

The project uses the **Repository Pattern** to separate data-access logic from controllers.

For example:

```csharp
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}
```

The controller depends on the abstraction:

```text
Controller
     ↓
IProductRepository
     ↓
ProductRepository
     ↓
Entity Framework Core
     ↓
SQL Server
```

This makes the application easier to maintain and allows the data-access logic to remain separated from HTTP-related logic.

---

# 💉 Dependency Injection

ASP.NET Core's built-in **Dependency Injection** system is used throughout the application.

Repositories and other required services are registered in `Program.cs`.

Example:

```csharp
builder.Services.AddScoped<IProductRepository, ProductRepository>();
```

The required dependencies are then injected into controllers through constructors.

---

# 🔄 DTOs & AutoMapper

The API uses **Data Transfer Objects (DTOs)** instead of exposing entity models directly.

For example:

```text
Entity
Product
   ↓
AutoMapper
   ↓
ProductDto
   ↓
API Response
```

AutoMapper is used to simplify mapping between entities and DTOs.

This provides:

* Better control over API responses
* Separation between database models and API contracts
* Reduced exposure of internal entity properties
* Cleaner controller code

---

# ✅ Validation

Request validation was added to prevent invalid data from entering the application.

Validation is applied to incoming DTOs before processing requests.

Examples include validating:

* Required fields
* Product information
* Prices
* Quantities
* User input

Invalid requests return appropriate HTTP responses instead of being processed as valid data.

---

# ⚠️ Global Exception Handling

The project includes a custom **Global Exception Handler Middleware**.

Instead of writing repetitive `try/catch` blocks inside every controller, unexpected exceptions are handled centrally.

```text
HTTP Request
     ↓
Middleware
     ↓
Controller
     ↓
Repository
     ↓
Exception
     ↓
Global Exception Handler
     ↓
Consistent HTTP Response
```

This improves:

* Error handling consistency
* Controller readability
* Maintainability
* API reliability

---

# 📖 Swagger / OpenAPI

Swagger is used to document and test the API.

It provides an interactive interface where API endpoints can be:

* Viewed
* Tested
* Inspected
* Used with different request parameters
* Tested with authentication tokens

Swagger is especially useful during backend development because it allows the API to be tested without building a frontend application.

---

# ⚙️ Getting Started

## 1. Clone the repository

```bash
git clone <YOUR-GITHUB-REPOSITORY-URL>
```

## 2. Navigate to the project

```bash
cd EComerceAPI
```

## 3. Configure SQL Server

Update the connection string in your configuration file according to your local SQL Server setup.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  }
}
```

Do not commit passwords, secrets, or other sensitive credentials to GitHub.

---

## 4. Apply Entity Framework migrations

Make sure Entity Framework Core tools are installed, then run:

```bash
dotnet ef database update
```

This will create/update the database based on the existing migrations.

---

## 5. Run the API

```bash
dotnet run
```

Or run the project directly from Visual Studio.

---

## 6. Open Swagger

After running the application, open the Swagger URL shown in the terminal.

Swagger can then be used to explore and test the available endpoints.

---

# 🧪 API Testing

The API can be tested using:

* Swagger UI
* Postman

Testing focuses on:

* CRUD operations
* Validation
* Authentication
* Authorization
* Cart operations
* Order operations
* Error handling
* HTTP status codes

---

# 📌 Future Improvements

Possible future improvements include:

* Payment gateway integration
* Refresh Tokens
* Role-based Admin dashboard
* Product search and advanced filtering
* Pagination
* Sorting
* Advanced logging
* Unit testing
* Integration testing
* Caching
* Email notifications
* Image/file upload
* Docker support
* Deployment to a cloud platform
* Frontend application

---

# 🎯 What I Learned

Through this project, I practiced building a complete backend API from the ground up.

Key concepts covered:

* Designing backend models
* Relational database design
* Entity Framework Core
* LINQ
* Migrations
* Repository Pattern
* Dependency Injection
* RESTful API design
* DTOs
* AutoMapper
* Validation
* JWT Authentication
* Authorization
* Business Logic
* Middleware
* Global Exception Handling
* Swagger
* API testing
* Git & GitHub

The main goal of the project was not only to make the API work, but also to understand **how the different backend components communicate with each other**.

---

# 👨‍💻 Project Status

**Completed — Backend E-Commerce API**

The core backend functionality has been implemented, including authentication, authorization, product management, cart and order logic, validation, DTO mapping, repositories, and global exception handling.

The project can be extended in the future with payment integration, testing, deployment, and a frontend application.

---

## ⭐ If you found this project useful

Feel free to explore the source code and follow the development journey.
