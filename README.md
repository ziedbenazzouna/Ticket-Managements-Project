# 🎫 Ticket Management System

A simple **Ticket Management System** built with **ASP.NET Core (.NET 9)** and **Blazor WebAssembly** using **MongoDB** as the database.

This application allows authenticated users to manage support tickets while applying **role-based authorization**.  
Admins can manage tickets, while normal users can access the home page but cannot access the ticket management area.


# 🚀 Technologies Used

## Backend
- ASP.NET Core Web API (.NET 9)
- MongoDB
- JWT Authentication
- Role-based Authorization

## Frontend
- Blazor WebAssembly (.NET 9)
- MudBlazor UI Components
- Blazored LocalStorage

## Architecture
- REST API
- Service Layer
- Client-Server Architecture
- JWT Authentication
- Role-based Access Control (Admin / User)

# ⚙️ Prerequisites

Before running the project, make sure you have installed:

- **.NET 9 SDK**
- **MongoDB**
- **Visual Studio 2022 / VS Code**
- **Git**

You can download MongoDB here:

https://www.mongodb.com/try/download/community

# 🗄️ Database Configuration (MongoDB)

Make sure MongoDB is running on your machine.

You can configure MongoDB connection in: TicketManagementProject.API/appsettings.json
