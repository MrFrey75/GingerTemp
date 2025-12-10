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

    - **Endpoints**: This folder contains the API controllers and route definitions.

- **GingerTemplate.Data**: This project contains data access implementations, repositories, and database context. It is responsible for interacting with the database and performing CRUD operations. Will be easily switched to any database provider. But designed with Sqlite for development and testing purposes.

- **GingerTemplate.DesktopApp**: AvaloniaUI desktop application project. MVVM structure with views, view models, and models. 

- **GingerTemplate.WebApp**: This project contains the web application implementation using ASP.NET Core. It provides a user interface for interacting with the backend services.

- **GingerTemplate.AndroidApp**: This project contains the Android application implementation using Xamarin or .NET MAUI.

- **GingerTemplate.Tests**: This project contains unit tests and integration tests for the application.

## Core Services
The `GingerTemplate.Core` project includes several core services that provide essential functionality for the application:
- **Authentication Service**: Handles user authentication and authorization.
- **User Profile Service**: Manages user-related operations such as registration, profile management, and password recovery. Including JWT token generation and validation. Includes role-based access control.
- **Central Logging Service**: Provides logging capabilities for tracking application events and errors. Uses serilog and allows logging to various sinks (console, file, etc.).
- **Email Service**: Facilitates sending emails for notifications, confirmations, and alerts.
- **Data Validation Service**: Validates data inputs to ensure they meet specified criteria before processing.
- **Configuration Service**: Manages application configuration settings and environment variables.
- **Notification Service**: Sends notifications to users via different channels (email, SMS, push notifications).
- **Caching Service**: Implements caching mechanisms to improve application performance and reduce database load.
- **Plugin Service**: Supports extensibility by allowing the integration of plugins or modules into the application.

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

## Error Handling

### Enhanced Error Handling Strategy

Ginger Template implements a robust, multi-layered error handling approach to ensure reliability, maintainability, and user-friendly error communication.

### Custom Exception Hierarchy

The framework defines a hierarchy of custom exceptions for different error scenarios:

```csharp
// Base exception for all application exceptions
public class ApplicationException : Exception { }

// Domain/business logic exceptions
public class BusinessLogicException : ApplicationException { }
public class ValidationException : ApplicationException { }
public class AuthenticationException : ApplicationException { }
public class AuthorizationException : ApplicationException { }

// Data access exceptions
public class RepositoryException : ApplicationException { }
public class DatabaseException : ApplicationException { }

// External service exceptions
public class ExternalServiceException : ApplicationException { }
public class EmailServiceException : ExternalServiceException { }

// Configuration exceptions
public class ConfigurationException : ApplicationException { }
```

### Global Exception Handling Middleware

The Web API project includes middleware for centralized exception handling:

- **Exception Logging**: All exceptions are logged with full stack traces and context
- **HTTP Response Mapping**: Exceptions are mapped to appropriate HTTP status codes
- **Structured Error Responses**: Consistent JSON error format returned to clients
- **Sensitive Data Protection**: Stack traces hidden in production environment

**Example Error Response:**
```json
{
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Invalid input provided",
    "details": [
      {
        "field": "email",
        "message": "Email format is invalid"
      }
    ],
    "timestamp": "2025-12-10T10:30:00Z",
    "traceId": "0HN1GKOD5HLSA:00000001"
  }
}
```

### Try-Catch-Log Pattern

All service methods implement standardized error handling:

```csharp
public async Task<User> GetUserAsync(int id)
{
    try
    {
        ValidateUserId(id);
        var user = await _userRepository.GetUserAsync(id);
        
        if (user == null)
        {
            throw new BusinessLogicException($"User with ID {id} not found");
        }
        
        return user;
    }
    catch (DatabaseException ex)
    {
        _logger.LogError($"Database error retrieving user {id}: {ex.Message}");
        throw new RepositoryException("Failed to retrieve user from database", ex);
    }
    catch (ValidationException ex)
    {
        _logger.LogWarning($"Validation failed for user ID {id}: {ex.Message}");
        throw;
    }
    catch (Exception ex)
    {
        _logger.LogError($"Unexpected error retrieving user {id}: {ex}");
        throw new ApplicationException("An unexpected error occurred", ex);
    }
}
```

### Input Validation

Comprehensive input validation prevents invalid data from propagating through the system:

- **Fluent Validation**: Data annotation and fluent validation frameworks
- **Custom Validators**: Business logic-specific validation rules
- **Early Validation**: Input validation at API controller level
- **Detailed Error Messages**: Clear feedback on validation failures

### API Error Responses

The Web API returns standardized HTTP status codes:

- **200 OK**: Successful request
- **400 Bad Request**: Validation errors or malformed input
- **401 Unauthorized**: Authentication required
- **403 Forbidden**: Insufficient permissions
- **404 Not Found**: Resource not found
- **409 Conflict**: Business logic violation
- **500 Internal Server Error**: Unexpected server errors
- **503 Service Unavailable**: External service failures

### Retry Strategies

For transient failures, the framework implements resilience patterns:

```csharp
// Polly retry policy for transient failures
var retryPolicy = Policy
    .Handle<HttpRequestException>()
    .Or<TimeoutException>()
    .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: attempt => 
            TimeSpan.FromSeconds(Math.Pow(2, attempt)),
        onRetry: (outcome, timespan, retryCount, context) =>
        {
            _logger.LogWarning(
                $"Retry attempt {retryCount} after {timespan.TotalSeconds}s");
        });
```

### Logging Best Practices

Error logging follows these principles:

1. **Log Levels**:
   - **Fatal**: Application cannot continue
   - **Error**: Operation failed, should be investigated
   - **Warning**: Unexpected but recoverable condition
   - **Information**: Normal application flow events
   - **Debug**: Detailed diagnostic information

2. **Context Information**:
   - User ID (when applicable)
   - Request ID/Trace ID for correlation
   - Method/operation being performed
   - Input parameters (sanitized)
   - Execution duration

3. **Security**:
   - Never log passwords or sensitive tokens
   - Sanitize user input before logging
   - Use structured logging for easier analysis
   - Redact PII (Personally Identifiable Information)

### Exception Recovery Strategies

Different exception types have specific recovery approaches:

| Exception Type | Recovery Strategy | User Message |
|---|---|---|
| `ValidationException` | Return 400 Bad Request | Show validation errors |
| `AuthenticationException` | Redirect to login | "Please log in again" |
| `AuthorizationException` | Return 403 Forbidden | "You don't have permission" |
| `RepositoryException` | Retry or fallback | "Database temporarily unavailable" |
| `ExternalServiceException` | Retry with backoff | "Service temporarily unavailable" |
| `ApplicationException` | Log & return 500 | "An error occurred, please try again" |

### Health Checks

The application includes health check endpoints to monitor system status:

```bash
GET /health
GET /health/ready
GET /health/live
```

Health checks monitor:
- Database connectivity
- External service availability
- Memory and resource usage
- Cache system status

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