# Margo

## Overview

Margo is a feature-rich E-Commerce API designed to facilitate online shopping experiences. Built using .NET Core with a clean architecture approach, it includes features like real-time notifications, chat support, and seller analytics, providing a robust foundation for scalable and efficient e-commerce solutions.

---

## Features

### General Features:

- Authentication and Authorization using JWT.
- Clean Architecture pattern for maintainability.
- SQLite as the database.
- CQRS (Command Query Responsibility Segregation) for query and command separation.

### Cart Management:

- Add products to a cart.
- Remove products from a cart.
- View the current cart.

### Order Management:

- View all orders (admin only).
- View individual orders.
- Create an order from the cart.
- View personal orders.
- Delete an order.

### Product Management (Admin Only):

- Create, update, and delete products.
- View all products.
- View products by category.
- View individual product details.

### Authentication:

- Register new users.
- Login with JWT token generation.

### Notifications:

- Real-time notifications for sellers when purchases are made.

### Chat:

- Chat feature for customers to communicate with customer service.

### Analytics:

- Seller dashboard includes analytics for sales and customer insights.

---

## Project Structure

```
Margo
├── Api
│   ├── Controllers
│   │   ├── AuthController.cs
│   │   ├── CartController.cs
│   │   ├── OrderController.cs
│   │   ├── ProductsController.cs
│   │   ├── ChatController.cs
│   │   ├── NotificationsController.cs
│   │   └── DashboardController.cs
│   ├── Middlewares
│   ├── Program.cs
│   ├── appsettings.json
├── Application
│   ├── Features
│   │   ├── Auth
│   │   │   ├── Commands
│   │   │   └── DTOs
│   │   ├── Cart
│   │   │   ├── Commands
│   │   │   ├── Queries
│   │   │   └── DTOs
│   │   ├── Orders
│   │   │   ├── Commands
│   │   │   ├── Queries
│   │   │   └── DTOs
│   │   ├── Products
│   │   │   ├── Commands
│   │   │   ├── Queries
│   │   │   └── DTOs
│   │   ├── Chat
│   │   │   ├── Commands
│   │   │   ├── Queries
│   │   │   └── DTOs
│   │   ├── Notifications
│   │   │   ├── Commands
│   │   │   ├── Queries
│   │   │   └── DTOs
│   │   └── Dashboard
│   │       ├── Queries
│   │       └── DTOs
│   └── Common
├── Domain
│   ├── Entities
│   ├── Interfaces
│   └── Exceptions
├── Infrastructure
│   ├── BackgroundWorkers
│   │   ├── NotificationWorker.cs
│   │   ├── OrderProcessingWorker.cs
│   ├── Persistence
│   │   ├── AppDbContext.cs
│   │   ├── Migrations
│   │   └── Seeders
│   ├── Repositories
│   └── Services
└── Tests
    ├── UnitTests
    └── IntegrationTests
```

---

## Endpoints

### Authentication

- `POST /api/auth/register` - Register a new user.
- `POST /api/auth/login` - Login and receive a JWT token.

### Cart

- `GET /api/cart` - View current cart.
- `POST /api/cart/add-product` - Add a product to the cart.
- `DELETE /api/cart/remove-product` - Remove a product from the cart.

### Orders

- `GET /api/orders` - Get all orders (admin only).
- `GET /api/orders/{id}` - Get a specific order.
- `POST /api/orders` - Create a new order.
- `GET /api/orders/mine` - Get personal orders.
- `DELETE /api/orders/{id}` - Delete an order.

### Products

- `GET /api/products` - Get all products.
- `GET /api/products/{id}` - Get product details by ID.
- `GET /api/products/category/{category}` - Get products by category.
- `POST /api/products` - Create a new product (admin only).
- `PUT /api/products/{id}` - Update a product (admin only).
- `DELETE /api/products/{id}` - Delete a product (admin only).

### Chat

- `GET /api/chat` - Get all chats for the logged-in user.
- `POST /api/chat` - Create a new chat session.
- `POST /api/chat/{chatId}/message` - Send a message in a specific chat.

---

## Prerequisites

- .NET 9.0 SDK
- SQLite
- Node.js (for real-time features and frontend integration)
- Docker (for containerization, optional)

---

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/margo-api.git
   ```

2. Navigate to the project directory:

   ```bash
   cd margo-api
   ```

3. Build the project:

   ```bash
   dotnet build
   ```

4. Apply migrations to initialize the database:

   ```bash
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run --project Api
   ```

---

## Testing

Run unit and integration tests using the following command:

```bash
dotnet test
```

## Future Work

### **Admin Dashboard**

- **Sales Analytics**:
  - Implement real-time and historical sales analytics for admins.
  - Display key metrics such as total revenue, sales trends, and top-selling products.
- **Order Management**:
  - Allow admins to view, update, and process orders directly from the dashboard.
- **Inventory Management**:
  - Provide tools for managing product stock and categorization.

### **Notifications**

- **Order Status Updates**:
  - Notify users in real-time about the status of their orders (e.g., confirmed, shipped, delivered).
- **Admin Alerts**:
  - Send notifications to admins about low stock, new orders, and system updates.
