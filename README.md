# InvoiceHub

A Clean Architecture ASP.NET Core 8 Web API implementing:

- Domain-Driven Design principles
- CQRS pattern
- Entity Framework Core
- SQLite persistence
- JWT Authentication
- Role-based authorization ready

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- EF Core
- SQLite
- JWT Authentication

## Features

- Create Invoice
- Get Invoice by Id
- Secure endpoints
- Register/Login
- Password hashing
- Clean layered architecture

## Run

```bash
dotnet ef database update --project InvoiceHub.Infrastructure --startup-project InvoiceHub.API
dotnet run --project InvoiceHub.API