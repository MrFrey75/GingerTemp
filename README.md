# Ginger Template

A fully functional C# .NET template for creating modern, scalable applications with multiple client implementations (Web API, Desktop, Web App, CLI, and Mobile).

## Description
Ginger Template provides a comprehensive, production-ready foundation for building C# .NET applications. It follows industry best practices including clean architecture, dependency injection, and MVVM patterns, offering a solid starting point for enterprise-level development.

## 📚 Documentation

Quick links to all documentation:
- **[QUICKSTART.md](QUICKSTART.md)** - Get up and running in 5 minutes
- **[FOLDER_STRUCTURE.md](FOLDER_STRUCTURE.md)** - Complete directory and file organization
- **[PROJECT_STRUCTURE.md](PROJECT_STRUCTURE.md)** - Detailed project breakdown and dependencies
- **[CODING_STANDARDS.md](CODING_STANDARDS.md)** - C# code conventions and best practices
- **[IMPLEMENTATION_COMPLETION.md](IMPLEMENTATION_COMPLETION.md)** - Comprehensive implementation details and examples

## 📑 Table of Contents

- [Solution Structure](#solution-structure)
- [Core Services](#core-services)
- [Getting Started](#getting-started)
- [Building & Running](#building--running)
- [Architecture & Design Patterns](#architecture--design-patterns)
- [Project Details](#project-details)
- [Configuration](#configuration-)
- [Database](#database)
- [Security Features](#security-features-)
- [Logging](#logging-)
- [Error Handling](#error-handling)
- [Contributing](#contributing)
- [License](#license)

## Solution Structure
The solution is structured into 7 main projects to separate concerns and improve maintainability:

- **GingerTemplate.Core**: Core application logic, data access layer, and shared utilities. Contains business entities, services, repositories, and the Entity Framework Core database context configured for SQLite.
    - **Models**: Domain models and data transfer objects.
    - **Services**: Service interfaces and implementations for business logic (Logging, Configuration).
    - **Repositories**: Generic repository pattern for data access (IRepository<T>).
    - **Context**: Entity Framework Core database context and configurations.
    - **Migrations**: Database schema migrations.
    - **Utilities**: Helper classes (Guard validation, RetryPolicy with exponential backoff, ExecutionTimer).
    - **Converters**: Type conversion utilities (String, Enum, DateTime, Collection converters).
    - **Configuration**: Typed configuration classes and file management (ApplicationSettings, DatabaseSettings, etc.).
    - **Extensions**: 14 extension method files (Null, Nullable, Enum, Collection, DateTime, Int, Bool, Double, List, Dictionary, TimeSpan, Char, Decimal, Json).
    - **Enums**: Application enumeration types.
    - **Exceptions**: Custom exception hierarchy for error handling.

- **GingerTemplate.WebApi**: ASP.NET Core REST API that exposes backend services and business logic through RESTful endpoints.
    - **Controllers**: API endpoint controllers.
    - **Middleware**: Exception handling and request processing middleware.

- **GingerTemplate.DesktopApp**: Cross-platform desktop application using Avalonia UI with MVVM architecture. Provides a rich client interface for desktop users.

- **GingerTemplate.WebApp**: ASP.NET Core Razor Pages web application providing a user interface for web-based interaction with backend services.

- **GingerTemplate.CLI**: Command-line interface application for system administration and batch operations. Provides administrative tools and automated task execution.
    - **Commands**: Command definitions and handlers for CLI operations.
        - **-help**: Displays help information for available commands.
        - **-list-users**: Lists all users in the system.
        - **-add-user**: Adds a new user with specified details.
        - **-remove-user**: Removes a user by ID.
    - **Options**: Command-line argument parsing and configuration using System.CommandLine.
    - **Output**: Formatted console output and result display. Using Spectre.Console for rich text formatting.

- **GingerTemplate.MobileApp**: Mobile application (ready for .NET MAUI or Xamarin). Provides a foundation for cross-platform mobile development.

- **GingerTemplate.Tests**: Unit tests and integration tests using xUnit and Moq for comprehensive test coverage.

## Core Services
The `GingerTemplate.Core` project includes several core services that provide essential functionality for the application:

**✅ Implemented:**
- **Central Logging Service** ✅: Provides correlation-aware structured logging with context properties. Uses Serilog with console, file, and Seq sinks. Integrated across all projects (WebApi, WebApp, CLI).
- **Configuration Service** ✅: Manages JSON-based configuration with hierarchical key access, typed section loading, and environment-specific overrides (dev/prod). Includes `ConfigurationFileManager` for file operations.

**🔄 In Progress:**
- **Authentication Service**: Handles user authentication and authorization with JWT tokens.
- **User Profile Service**: Manages user-related operations such as registration, profile management, and password recovery with role-based access control.

**📋 Planned:**
- **Email Service**: Facilitates sending emails for notifications, confirmations, and alerts.
- **Data Validation Service**: Validates data inputs using FluentValidation framework.
- **Notification Service**: Sends notifications via multiple channels (email, SMS, push).
- **Caching Service**: Implements memory and Redis caching mechanisms.
- **Background Task Service**: Manages scheduled jobs and background processing.
- **File Storage Service**: Handles file uploads, downloads, and storage.
- **Plugin Service**: Supports extensibility through plugin architecture.
- **Theme Management Service**: Application theme and UI customization.

## Getting Started

### Prerequisites
- .NET 10.0 SDK or higher
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
   dotnet ef database update --project src/GingerTemplate.Core
   ```

## Building & Running

### Build the Solution
```bash
dotnet build
```

### Run Web API
```bash
dotnet run --project src/GingerTemplate.WebApi
```

### Run Web Application
```bash
dotnet run --project src/GingerTemplate.WebApp
```

### Run Desktop Application
```bash
dotnet run --project src/GingerTemplate.DesktopApp
```

### Run CLI Application
```bash
dotnet run --project src/GingerTemplate.CLI
```

### Run Tests
```bash
dotnet test
```

## Architecture & Design Patterns

### Clean Architecture
The solution follows clean architecture principles with clear separation of concerns:
- **Presentation Layer**: Web API, Web App, Desktop App, CLI, and Mobile App projects
- **Business Logic Layer**: Core services and business logic
- **Data Access Layer**: Repository pattern and Entity Framework Core
- **Cross-Cutting Concerns**: Logging, validation, and configuration

### Design Patterns Implemented
- **Repository Pattern**: Data access abstraction in GingerTemplate.Core
- **Dependency Injection**: IoC container configured in startup
- **MVVM**: Model-View-ViewModel pattern in DesktopApp
- **Service Locator**: Service registration and resolution
- **Factory Pattern**: Object creation and instantiation
- **Command Pattern**: CLI command handling with System.CommandLine

## Project Details

### GingerTemplate.Core
Core application logic, interfaces, abstractions, and data access layer.

**Subfolders:**
- **Models**: Data transfer objects and domain models
- **Services**: Service interfaces and base implementations
- **Repositories**: Generic repository pattern for data access
- **Context**: Entity Framework Core database context
- **Migrations**: Database schema migrations
- **Utilities**: Helper functions and extension methods
- **Extensions**: LINQ and type extension methods
- **Enums**: Application enumeration types
- **Exceptions**: Custom exception hierarchy

### GingerTemplate.WebApi
ASP.NET Core REST API implementation exposing backend services.

**Features:**
- RESTful endpoint design
- Swagger/OpenAPI documentation
- JWT authentication
- Input validation
- Exception handling middleware

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

### GingerTemplate.CLI
Command-line interface for administrative operations and batch processing.

**Features:**
- System.CommandLine for command parsing
- Structured command organization
- Serilog integration for logging
- Repository access for data operations
- Formatted console output

### GingerTemplate.MobileApp
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
- CLI command tests

## Configuration ✅

Environment-specific configuration using JSON files:
- **ConfigurationService** loads and parses JSON config files with hierarchical key access (colon-delimited)
- **Files**: `config.json` (development defaults), `config.production.json` (production overrides)
- **Typed sections**: ApplicationSettings, DatabaseSettings, LoggingSettings, ApiSettings, SecuritySettings, EmailSettings, CacheSettings
- **Access patterns**: `GetSection<T>(key)`, `GetValue<T>(key)`, `TryGetSection<T>()`, `TryGetValue<T>()`
- **Hierarchical keys**: Example: `Database:ConnectionString` maps to nested structure in JSON
- **Type safety**: All configuration sections are strongly typed POCO classes
- **DI registration**: Injected in WebApi, WebApp, and CLI for global access

### appsettings.json (Logging)
Serilog configuration for console, file, and centralized logging:
```json
{
  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {CorrelationId} {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  }
}
```

### Environment-Specific Settings
- `config.json` - Development environment (Information log level, detailed errors, Memory cache)
- `config.production.json` - Production environment (Warning log level, limited details, Redis cache)

## Database

The application uses Entity Framework Core with SQLite for data access:

### Migrations
Create and apply database migrations:
```bash
dotnet ef migrations add MigrationName --project src/GingerTemplate.Core
dotnet ef database update --project src/GingerTemplate.Core
```

### Seeding Data
The Core project includes seed data functionality for initial data population.

### Connection String
Configured via `DatabaseSettings:ConnectionString` in config.json or environment-specific overrides.

## Security Features 🔄

Foundation implemented with planned enhancements:
- **Input validation**: Guard clauses in Utilities for NotNull, NotNullOrWhiteSpace, GreaterThanZero checks
- **JWT token-based authentication** (planned)
- **Role-based authorization** (planned)
- **SQL injection prevention** through Entity Framework Core ORM
- **CORS configuration** (ready in WebApi)
- **Password hashing and encryption** (planned)

## Logging ✅

The application uses a comprehensive centralized logging service:
- **Serilog integration** across WebApi, WebApp, and CLI with structured JSON output
- **Correlation tracking** via `ILoggingService.BeginCorrelationScope()` for request tracing
- **Contextual properties** support for custom metadata in logs
- **Environment-specific configuration**: console output for development, file/Seq for production
- **Request logging** middleware in ASP.NET Core apps automatically logs incoming requests
- **Log format template**: `[{Timestamp:HH:mm:ss} {Level:u3}] {CorrelationId} {Message:lj}{NewLine}{Exception}`
- **Log levels**: Development (Information), Production (Warning)
- **Sinks**: Console (all), File (configured), Seq (centralized)

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
