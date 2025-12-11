# GingerTemplate Folder Structure

> 📚 **Part of the [GingerTemplate Documentation](README.md)** - See also: [Quick Start](QUICKSTART.md) | [Project Structure](PROJECT_STRUCTURE.md) | [Coding Standards](CODING_STANDARDS.md)

## Solution Layout

The Ginger Template solution is organized with all source code projects in a `src` folder, following industry best practices for solution organization.

```
GingerTemplate/
├── .git/                          # Git repository
├── .gitignore                     # Git ignore file
├── README.md                      # Main project documentation
├── LICENSE                        # MIT License
├── CODING_STANDARDS.md            # Coding standards guide
├── FOLDER_STRUCTURE.md            # This file
├── QUICKSTART.md                  # Quick start guide
├── PROJECT_STRUCTURE.md           # Project structure details
├── IMPLEMENTATION_COMPLETION.md   # Comprehensive implementation guide
├── GingerTemplate.sln             # Solution file (Visual Studio)
│
└── src/                           # Source code folder
    ├── GingerTemplate.Core/       # Core application logic & data access
    │   ├── Services/              # Service interfaces and implementations
    │   ├── Models/                # Domain models and DTOs
    │   ├── Repositories/          # Repository pattern implementations
    │   ├── Context/               # Entity Framework Core DbContext
    │   ├── Configuration/         # Typed configuration classes and file management
    │   ├── Converters/            # Type converters
    │   ├── Migrations/            # EF Core database migrations
    │   ├── Exceptions/            # Custom exception classes
    │   ├── Extensions/            # Extension methods
    │   ├── Utilities/             # Utility classes and helpers
    │   ├── Enums/                 # Enumeration types
    │   ├── Interfaces/            # Public interfaces
    │   └── GingerTemplate.Core.csproj
    │
    ├── GingerTemplate.WebApi/     # REST API
    │   ├── Controllers/           # API controllers
    │   ├── Endpoints/             # Endpoint definitions
    │   ├── Middleware/            # Custom middleware
    │   ├── Filters/               # Action filters
    │   ├── Mappings/              # AutoMapper profiles
    │   ├── appsettings.json       # Configuration
    │   ├── Program.cs             # Application startup
    │   └── GingerTemplate.WebApi.csproj
    │
    ├── GingerTemplate.WebApp/     # ASP.NET Core Web Application
    │   ├── Pages/                 # Razor Pages
    │   ├── Components/            # Reusable components
    │   ├── Models/                # View models
    │   ├── Services/              # Application services
    │   ├── wwwroot/               # Static files (CSS, JS)
    │   ├── appsettings.json       # Configuration
    │   ├── Program.cs             # Application startup
    │   └── GingerTemplate.WebApp.csproj
    │
    ├── GingerTemplate.DesktopApp/ # Avalonia Desktop Application
    │   ├── Views/                 # XAML views
    │   ├── ViewModels/            # MVVM view models
    │   ├── Models/                # Desktop application models
    │   ├── Services/              # Desktop services
    │   ├── App.axaml              # Application definition
    │   ├── Program.cs             # Application entry point
    │   └── GingerTemplate.DesktopApp.csproj
    │
    ├── GingerTemplate.CLI/        # Command-Line Interface Application
    │   ├── Commands/              # CLI command definitions
    │   ├── Options/               # Command-line options and arguments
    │   ├── Output/                # Output formatting utilities
    │   ├── Services/              # CLI services
    │   ├── Program.cs             # Application entry point
    │   └── GingerTemplate.CLI.csproj
    │
    ├── GingerTemplate.MobileApp/  # Mobile Application (MAUI or Xamarin)
    │   ├── Views/                 # Mobile views
    │   ├── ViewModels/            # MVVM view models
    │   ├── Models/                # Mobile models
    │   ├── Services/              # Mobile services
    │   ├── App.xaml               # Application definition
    │   ├── MauiProgram.cs         # MAUI configuration
    │   └── GingerTemplate.MobileApp.csproj
    │
    └── GingerTemplate.Tests/      # Unit and Integration Tests
        ├── Unit/                  # Unit tests
        ├── Integration/           # Integration tests
        ├── Fixtures/              # Test fixtures and helpers
        ├── Mocks/                 # Mock objects
        ├── appsettings.json       # Test configuration
        └── GingerTemplate.Tests.csproj
```

## Directory Organization Benefits

### src/ Folder

- **Cleaner root**: Keeps the solution root clean with only configuration files
- **Clear separation**: Source code is physically separated from documentation and tools
- **Scalability**: Makes it easier to add other folders like `tests/`, `docs/`, `tools/` at the root level
- **Industry standard**: Follows common practices used by large projects (ASP.NET, .NET Runtime, etc.)

### Project Folders

Each project follows a consistent internal structure:

- **Core**: Models, Services, Repositories, DbContext, Migrations, Exceptions, Extensions, Utilities, Enums, Interfaces
- **WebApi**: Controllers, Endpoints, Middleware, Filters, Mappings
- **WebApp**: Pages, Components, Models, Services
- **DesktopApp**: Views, ViewModels, Models, Services
- **CLI**: Commands, Options, Output, Services
- **MobileApp**: Views, ViewModels, Models, Services
- **Tests**: Unit tests, Integration tests, Fixtures, Mocks

## Building and Running

### Build the entire solution

```bash
cd /home/fray/Projets/GingerTemp
dotnet build
```

### Run specific projects

```bash
# Web API
dotnet run --project src/GingerTemplate.WebApi/GingerTemplate.WebApi.csproj

# Web Application
dotnet run --project src/GingerTemplate.WebApp/GingerTemplate.WebApp.csproj

# Desktop Application
dotnet run --project src/GingerTemplate.DesktopApp/GingerTemplate.DesktopApp.csproj

# CLI Application
dotnet run --project src/GingerTemplate.CLI/GingerTemplate.CLI.csproj

# Tests
dotnet test src/GingerTemplate.Tests/GingerTemplate.Tests.csproj
```

## Project Dependencies

```
GingerTemplate.WebApi
├── GingerTemplate.Core

GingerTemplate.WebApp
├── GingerTemplate.Core

GingerTemplate.DesktopApp
├── GingerTemplate.Core

GingerTemplate.CLI
├── GingerTemplate.Core

GingerTemplate.MobileApp
├── GingerTemplate.Core

GingerTemplate.Tests
├── GingerTemplate.Core
└── GingerTemplate.WebApi
```

## File Organization Best Practices

### One File Per Type (Usually)

- Each service interface and implementation in the same file (e.g., `UserService.cs` contains both `IUserService` and `UserService`)
- Each controller in its own file
- Each model in its own file
- Each CLI command in its own file

### Namespace Conventions

```
GingerTemplate.{LayerOrFeature}.{SubCategory}
```

Examples:

- `GingerTemplate.Core.Services`
- `GingerTemplate.Core.Models`
- `GingerTemplate.Core.Exceptions`
- `GingerTemplate.Core.Repositories`
- `GingerTemplate.WebApi.Controllers`
- `GingerTemplate.CLI.Commands`

### Folder Names

- Use **PascalCase** for folder names matching namespace segments
- Keep folder hierarchy aligned with namespace structure
- Avoid deep folder hierarchies (usually 3-4 levels maximum)

## Next Steps

1. **Explore the Core Project**: Review the base services and models
2. **Review Coding Standards**: Check `CODING_STANDARDS.md` for development guidelines
3. **Read Quick Start**: See `QUICKSTART.md` for immediate next steps
4. **Configure Database**: Update connection strings in `appsettings.json`
5. **Start Development**: Begin implementing your business logic
