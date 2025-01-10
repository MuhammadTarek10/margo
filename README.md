# Margo - E-Commerce Application

<!--toc:start-->

- [Margo - E-Commerce Application](#margo-e-commerce-application)
  - [Features](#features)
  - [Project Structure](#project-structure)
  - [Technologies Used](#technologies-used)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
  - [Installation](#installation) - [**API Endpoints**](#api-endpoints) - [**Future Work**](#future-work)
  <!--toc:end-->

Margo is an e-commerce application built using **Clean Architecture** and **.NET 9 Web API**. It follows the **CQRS pattern** and uses **SQLite** as the database. The application includes features like product management, user authentication, orders, carts, notifications, and more.

## Features

- **Product Management**:
  - Create, update, delete, and retrieve products.
  - Filter products by category.
- **User Management**:
  - Register, login, and manage user profiles.
- **Orders and Carts**:
  - Add products to a cart.
  - Place orders and manage order status.
- **Notifications**:
  - Send notifications for order updates.
- **Chat**:
  - Chat with customer service.
- **Background Jobs**:
  - Send emails asynchronously.
- **Payment Integration**:
  - Process payments using **Stripe**.
- **Admin Dashboard**:
  - View sales analytics and manage products/orders.

## Project Structure

The project follows **Clean Architecture** with the following layers:

- **Domain**:
  - Contains entities, enums, and domain logic.
- **Application**:
  - Contains use cases, commands, queries, and application logic.
- **Infrastructure**:
  - Contains database context, repositories, and external services.
- **API**:
  - Contains controllers and serves as the entry point for the application.

## Technologies Used

- **Backend**:
  - .NET 9
  - Entity Framework Core
  - SQLite
  - MediatR (CQRS)
  - FluentValidation
  - Stripe (for payments)
- **Testing**:
  - xUnit
  - Moq
- **Tools**:
  - Visual Studio / Visual Studio Code
  - dotnet CLI

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQLite](https://sqlite.org/index.html)
- [Stripe Account](https://stripe.com) (for payment integration)

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/MuhammadTarek10/margo.git
   cd margo
   ```

2. Install dependencies:

   ```bash
   dotnet restore
   ```

3. Build the project:

   ```bash
   dotnet build
   ```

4. Run the migrations:

   ```bash
   dotnet ef database update
   ```

5. Run the application:

   ```bash
   dotnet run --project src/Api
   ```

### **API Endpoints**

### Products

| Method | Endpoint                            | Description               |
| ------ | ----------------------------------- | ------------------------- |
| GET    | `/api/products`                     | Get all products.         |
| GET    | `/api/products/{id}`                | Get a product by ID.      |
| GET    | `/api/products/category/{category}` | Get products by category. |
| POST   | `/api/products`                     | Create a new product.     |
| PUT    | `/api/products/{id}`                | Update a product.         |
| DELETE | `/api/products/{id}`                | Delete a product.         |

### **Future Work**

- **Background Jobs**:
  - Implement background jobs for sending emails and processing payments.
- **Authentication and Authorization**:
  - Add JWT or OAuth2 for user authentication and role-based authorization.
- **Admin Dashboard**:
  - Build a dashboard for sales analytics and product/order management.
- **Payment Integration**:
  - Integrate **Stripe** for payment processing.
- **Notifications and Chat**:
  - Implement notifications for order updates and chat with customer service.
