# GingerTemplate Solution - Project Structure

> 📚 **Part of the [GingerTemplate Documentation](README.md)** - See also: [Quick Start](QUICKSTART.md) | [Folder Structure](FOLDER_STRUCTURE.md) | [Coding Standards](CODING_STANDARDS.md)

## Solution Overview

The **GingerTemplate** solution has been successfully created with the following structure:

### Solution File

- `GingerTemplate.sln` - Main solution file containing all projects

### Projects

#### 1. **GingerTemplate.Core** (Class Library - net10.0)

Core application logic, data access, and abstractions. This is the central project containing all business logic and data infrastructure.

**Folders:**

- `Models/` - Data models and DTOs (User.cs example created)
- `Services/` - Service interfaces and implementations (LoggingService.cs, ConfigurationService.cs)
- `Repositories/` - Repository pattern implementations (Repository.cs)
- `Context/` - Entity Framework Core DbContext (GingerTemplateDbContext.cs)
- `Configuration/` - Typed configuration classes and file management (ApplicationConfiguration.cs, ConfigurationFileManager.cs)
- `Converters/` - Type conversion utilities (StringConverters.cs, EnumConverters.cs, DateTimeConverters.cs, CollectionConverters.cs)
- `Migrations/` - EF Core database migrations
- `Utilities/` - Helper utilities and functions (Guard.cs, RetryPolicy.cs, ExecutionTimer.cs)
- `Extensions/` - Extension methods (14 files: NullExtensions.cs, EnumExtensions.cs, DateTimeExtensions.cs, etc.)
- `Enums/` - Enumeration types (UserEnums.cs)
- `Exceptions/` - Custom exception hierarchy

**Key Files Created:**

- `Exceptions/ApplicationException.cs` - Base exception class
- `Exceptions/DomainExceptions.cs` - BusinessLogicException, ValidationException, AuthenticationException, AuthorizationException
- `Exceptions/DataAccessExceptions.cs` - RepositoryException, DatabaseException
- `Exceptions/ServiceExceptions.cs` - ExternalServiceException, EmailServiceException, ConfigurationException
- `Models/User.cs` - Sample User model
- `Services/LoggingService.cs` - Logging service with correlation scope tracking
- `Services/ConfigurationService.cs` - Configuration loading and management with JSON files
- `Services/ConfigurationFileManager.cs` - File I/O operations for configuration
- `Configuration/ApplicationConfiguration.cs` - 7 typed configuration classes (ApplicationSettings, DatabaseSettings, etc.)
- `Converters/StringConverters.cs` - Safe string parsing utilities
- `Converters/EnumConverters.cs` - Generic enum parsing
- `Converters/DateTimeConverters.cs` - DateTime/Unix epoch conversions
- `Converters/CollectionConverters.cs` - Collection conversion utilities
- `Utilities/Guard.cs` - Validation guard clauses
- `Utilities/RetryPolicy.cs` - Retry logic with exponential backoff
- `Utilities/ExecutionTimer.cs` - Timing utilities for code execution
- `Repositories/Repository.cs` - Generic repository interface with CRUD operations
- `Context/GingerTemplateDbContext.cs` - Entity Framework DbContext with User entity configuration
- `Extensions/` - 14 individual extension files for various types (Null, Enum, Collection, DateTime, etc.)
- `Enums/UserEnums.cs` - UserRole and UserStatus enumerations
- `config.json` - Development configuration (Information logging, detailed errors, Memory cache)
- `config.production.json` - Production configuration (Warning logging, restricted CORS, Redis cache)

**Dependencies:**

- Serilog (4.3.0)
- Serilog.Extensions.Logging (10.0.0)
- Serilog.Sinks.File (6.0.0)
- Serilog.Sinks.Seq (6.0.0)
- Microsoft.Extensions.DependencyInjection.Abstractions (10.0.1)
- Microsoft.Extensions.Logging (10.0.1)
- Microsoft.Extensions.Configuration (10.0.1)
- FluentValidation (12.1.1)
- Microsoft.EntityFrameworkCore (10.0.1)
- Microsoft.EntityFrameworkCore.Sqlite (10.0.1)

---

#### 2. **GingerTemplate.WebApi** (ASP.NET Core Web API - net10.0)

REST API implementation for backend services with Serilog logging and configuration service integration.

**Folders:**

- `Endpoints/` - API controllers and routes
- `Middleware/` - Custom middleware components

**Key Files Created:**

- `Middleware/ExceptionHandlingMiddleware.cs` - Global exception handling with structured error responses
- `Program.cs` - Serilog host configuration, DI setup for ILoggingService and IConfigurationService, /health endpoint
- `appsettings.json` - Serilog configuration with console sink including CorrelationId in template

**Dependencies:**

- GingerTemplate.Core
- Serilog.AspNetCore (10.0.0)
- Serilog.Settings.Configuration (10.0.0)
- Serilog.Sinks.Console (6.1.1)
- Microsoft.Extensions.Hosting (10.0.1)
- Built-in ASP.NET Core packages

---

#### 3. **GingerTemplate.WebApp** (ASP.NET Core Razor Pages - net10.0)

Web application for user interface with Serilog logging and configuration service integration.

**Folders:**

- `Pages/` - Razor Pages
- `Components/` - Reusable components

**Key Files Created:**

- `Program.cs` - Serilog host configuration, DI setup for ILoggingService and IConfigurationService, /health endpoint
- `appsettings.json` - Serilog configuration with console sink including CorrelationId in template

**Dependencies:**

- GingerTemplate.Core
- Serilog.AspNetCore (10.0.0)
- Serilog.Settings.Configuration (10.0.0)
- Serilog.Sinks.Console (6.1.1)
- Microsoft.Extensions.Hosting (10.0.1)
- ASP.NET Core framework

---

#### 4. **GingerTemplate.CLI** (Console Application - net10.0)

Command-line interface application for system administration and batch operations with Serilog logging and configuration service integration.

**Folders:**

- `Commands/` - CLI command definitions and handlers
- `Options/` - Command-line argument parsing and configuration

**Key Files Created:**

- `Program.cs` - Host.CreateDefaultBuilder with Serilog, DI setup for ILoggingService and IConfigurationService, config loading example
- `appsettings.json` - Serilog configuration with console sink
- `.csproj` - appsettings.json included with CopyToOutputDirectory=PreserveNewest

**Dependencies:**

- GingerTemplate.Core
- System.CommandLine (2.0.0-beta5)
- Microsoft.Extensions.Hosting (10.0.1)
- Serilog.Extensions.Hosting (10.0.0)
- Serilog.Settings.Configuration (10.0.0)
- Serilog.Sinks.Console (6.1.1)
- Microsoft.Extensions.DependencyInjection (10.0.1)

---

#### 5. **GingerTemplate.DesktopApp** (Avalonia Desktop App - net10.0)

Cross-platform desktop application using Avalonia UI.

**Dependencies:**

- GingerTemplate.Core
- Avalonia (11.3.9)
- Avalonia.Desktop
- Avalonia.Themes.Fluent
- Avalonia.Fonts.Inter

---

#### 6. **GingerTemplate.MobileApp** (Class Library - net10.0)

Mobile application project (ready for .NET MAUI integration).

**Dependencies:**

- GingerTemplate.Core

---

#### 7. **GingerTemplate.Tests** (xUnit Test Project - net10.0)

Unit and integration tests.

**Dependencies:**

- GingerTemplate.Core
- GingerTemplate.WebApi
- xUnit
- Moq (4.20.72)
- Microsoft.NET.Test.Sdk (18.0.1)

---

## Project References Architecture

```
GingerTemplate.Tests ──→ GingerTemplate.Core
                     ──→ GingerTemplate.WebApi

GingerTemplate.WebApi ──→ GingerTemplate.Core

GingerTemplate.WebApp ──→ GingerTemplate.Core

GingerTemplate.CLI ──→ GingerTemplate.Core

GingerTemplate.DesktopApp ──→ GingerTemplate.Core

GingerTemplate.MobileApp ──→ GingerTemplate.Core
```

---

## Build Status

✅ **All projects build successfully** (.NET 10.0)

```
GingerTemplate.Core net10.0 succeeded
GingerTemplate.WebApi net10.0 succeeded
GingerTemplate.WebApp net10.0 succeeded
GingerTemplate.CLI net10.0 succeeded
GingerTemplate.DesktopApp net10.0 succeeded
GingerTemplate.MobileApp net10.0 succeeded
GingerTemplate.Tests net10.0 succeeded
```

---

## Getting Started

### Build the solution

```bash
cd /home/fray/Projets/GingerTemp
dotnet build
```

### Run tests

```bash
dotnet test
```

### Run Web API

```bash
dotnet run --project GingerTemplate.WebApi
```

### Run Web App

```bash
dotnet run --project GingerTemplate.WebApp
```

### Run Desktop App

```bash
dotnet run --project GingerTemplate.DesktopApp
```

---

## Documentation Files

The following documentation files are available:

- `README.md` - Comprehensive project documentation
- `CODING_STANDARDS.md` - C# coding standards and conventions
- `LICENSE` - MIT License

---

## Next Steps

1. **Configure Database Connection**: Update `appsettings.json` in WebApi and WebApp projects
2. **Create Entity Framework Migrations**: Run `dotnet ef migrations add InitialCreate --project GingerTemplate.Data`
3. **Implement Services**: Add business logic to the Services folder in Core project
4. **Create API Endpoints**: Add controllers to the Endpoints folder in WebApi project
5. **Add Unit Tests**: Create test classes in the Tests project following AAA pattern
6. **Configure Dependency Injection**: Register services in Program.cs files
7. **Add Authentication/Authorization**: Implement JWT or other security mechanisms
8. **Build UI**: Create Views in WebApp and Desktop/Mobile apps

---

## Technology Stack Summary

- **.NET Version**: 10.0
- **Web Framework**: ASP.NET Core
- **Desktop UI**: Avalonia
- **Database**: SQLite (Entity Framework Core)
- **Logging**: Serilog
- **Validation**: FluentValidation
- **Testing**: xUnit, Moq
- **Architecture**: Clean Architecture with MVVM patterns
- **Dependency Injection**: Microsoft.Extensions.DependencyInjection
