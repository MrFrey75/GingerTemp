# Ginger Template

A fully functional C# .NET template for creating modern, scalable applications with multiple client implementations (Web API, Desktop, Web App, and Mobile).

## Description
Ginger Template provides a comprehensive, production-ready foundation for building C# .NET applications. It follows industry best practices including clean architecture, dependency injection, and MVVM patterns, offering a solid starting point for enterprise-level development.

## Solution Structure
The solution is structured into multiple projects to separate concerns and improve maintainability:

- **GingerTemplate.Core**: This project contains the core application logic, including interfaces, abstractions, and shared utilities.
    - **Models**: This folder contains the data models used throughout the application.
    - **Services**: This folder contains service interfaces and implementations for business logic.
    - **Utilities**: This folder contains utility classes and helper functions.
    - **Extensions**: This folder contains extension methods to enhance existing classes and types.
    - **Enums**: This folder contains enumeration types used in the application.

- **GingerTemplate.WebApi**: This project contains the Web API implementation using ASP.NET Core. It exposes endpoints for client applications to interact with the backend services.

- **GingerTemplate.Data**: This project contains data access implementations, repositories, and database context. It is responsible for interacting with the database and performing CRUD operations. Will be easily switched to any database provider. B ut designed with Sqlite for development and testing purposes.

- **GingerTemplate.DesktopApp**: AvaloniaUI desktop application project. MVVM structure with views, view models, and models. 

- **GingerTemplate.WebApp**: This project contains the web application implementation using ASP.NET Core. It provides a user interface for interacting with the backend services.

- **GingerTemplate.AndroidApp**: This project contains the Android application implementation using Xamarin or .NET MAUI.

- **GingerTemplate.Tests**: This project contains unit tests and integration tests for the application.

## Core Services
The `GingerTemplate.Core` project includes several core services that provide essential functionality for the application:
- **AuthenticationService**: Handles user authentication and authorization.
- **UserService**: Manages user-related operations such as registration, profile management, and password recovery.
- **Central LoggingService**: Provides logging capabilities for tracking application events and errors. Uses serilog and allows logging to various sinks (console, file, etc.).
- **EmailService**: Facilitates sending emails for notifications, confirmations, and alerts.
- **DataValidationService**: Validates data inputs to ensure they meet specified criteria before processing.
- **ConfigurationService**: Manages application configuration settings and environment variables.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or higher
- Visual Studio 2022 or Visual Studio Code
- SQLite (for development and testing)
- Avalonia Studio (optional, for desktop app development)

### Installation & Setup
1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd GingerTemplate
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Configure the database connection string in `appsettings.json`

4. Run database migrations:
   ```bash
   dotnet ef database update --project GingerTemplate.Data
   ```

## Building & Running

### Build the Solution
```bash
dotnet build
```

### Run Web API
```bash
dotnet run --project GingerTemplate.WebApi
```

### Run Web Application
```bash
dotnet run --project GingerTemplate.WebApp
```

### Run Desktop Application
```bash
dotnet run --project GingerTemplate.DesktopApp
```

### Run Tests
```bash
dotnet test
```

## Architecture & Design Patterns

### Clean Architecture
The solution follows clean architecture principles with clear separation of concerns:
- **Presentation Layer**: Web API, Web App, Desktop App, and Mobile App projects
- **Business Logic Layer**: Core services and business logic
- **Data Access Layer**: Repository pattern and Entity Framework Core
- **Cross-Cutting Concerns**: Logging, validation, and configuration

### Design Patterns Implemented
- **Repository Pattern**: Data access abstraction in GingerTemplate.Data
- **Dependency Injection**: IoC container configured in startup
- **MVVM**: Model-View-ViewModel pattern in DesktopApp
- **Service Locator**: Service registration and resolution
- **Factory Pattern**: Object creation and instantiation

## Project Details

### GingerTemplate.Core
Core application logic, interfaces, and abstractions.

**Subfolders:**
- **Models**: Data transfer objects and domain models
- **Services**: Service interfaces and base implementations
- **Utilities**: Helper functions and extension methods
- **Extensions**: LINQ and type extension methods
- **Enums**: Application enumeration types

### GingerTemplate.WebApi
ASP.NET Core REST API implementation exposing backend services.

**Features:**
- RESTful endpoint design
- Swagger/OpenAPI documentation
- JWT authentication
- Input validation
- Exception handling middleware

### GingerTemplate.Data
Data access layer with Entity Framework Core.

**Features:**
- Generic repository pattern
- Unit of Work implementation
- Database context configuration
- SQLite support (swappable to other providers)
- Migration support

### GingerTemplate.DesktopApp
Cross-platform Avalonia desktop application with MVVM architecture.

**Technology Stack:**
- Avalonia UI framework
- MVVM Toolkit
- Reactive Extensions

### GingerTemplate.WebApp
ASP.NET Core Razor Pages or MVC web application.

**Features:**
- Server-side rendering
- Authentication integration
- Responsive UI
- Form validation

### GingerTemplate.AndroidApp
Mobile application using .NET MAUI or Xamarin Forms.

**Features:**
- Cross-platform mobile support
- Native API integration
- MVVM pattern implementation

### GingerTemplate.Tests
Comprehensive test coverage including unit and integration tests.

**Test Types:**
- Unit tests for services and utilities
- Integration tests for API endpoints
- Data access tests

## Configuration

### appsettings.json
Configuration file for database connection, logging, and application settings:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=gingerdb.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### Environment-Specific Settings
- `appsettings.Development.json` - Development environment
- `appsettings.Production.json` - Production environment
- `appsettings.Testing.json` - Testing environment

## Database

### Migrations
Create and apply database migrations:
```bash
dotnet ef migrations add MigrationName --project GingerTemplate.Data
dotnet ef database update --project GingerTemplate.Data
```

### Seeding Data
The Data project includes seed data functionality for initial data population.

## Security Features

- JWT token-based authentication
- Role-based authorization
- Input validation and sanitization
- SQL injection prevention through ORM
- CORS configuration
- Password hashing and encryption

## Logging

The application uses a centralized logging service:
- Configured for console, file, and structured logging
- Different log levels for development and production
- Integration with Serilog or similar frameworks

## Contributing

1. Create a feature branch (`git checkout -b feature/YourFeature`)
2. Commit your changes (`git commit -m 'Add YourFeature'`)
3. Push to the branch (`git push origin feature/YourFeature`)
4. Open a Pull Request

## Best Practices

- Keep services focused and following Single Responsibility Principle
- Use dependency injection for loose coupling
- Write unit tests for business logic
- Follow C# naming conventions and coding standards
- Document complex business logic
- Use async/await for I/O operations

## Troubleshooting

### Database Connection Issues
- Verify SQLite file path and permissions
- Check connection string in appsettings.json
- Run migrations to ensure schema is created

### Dependency Issues
- Clear NuGet cache: `dotnet nuget locals all --clear`
- Restore packages: `dotnet restore`

### Build Failures
- Ensure .NET SDK version matches project target framework
- Check all project references are correct
- Clean solution: `dotnet clean`

## License
This template is provided as-is for use in your projects.

## Support & Documentation
For detailed documentation and API references, see the individual project README files or visit the project wiki.   