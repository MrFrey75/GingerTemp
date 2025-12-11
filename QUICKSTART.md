# GingerTemplate - Quick Start Guide

> 📚 **Part of the [GingerTemplate Documentation](README.md)** - See also: [Project Structure](PROJECT_STRUCTURE.md) | [Folder Structure](FOLDER_STRUCTURE.md) | [Implementation Details](IMPLEMENTATION_COMPLETION.md)

## ✅ Project Created Successfully

Your GingerTemplate solution is now ready for development. Here's what has been set up:

### 📁 Solution Structure

- **7 Projects** created and linked together
- **Clean Architecture** with separation of concerns
- **Error Handling** infrastructure in place
- **Dependency Injection** ready to configure

### 🚀 Quick Commands

#### Build the solution

```bash
cd /home/fray/Projets/GingerTemp
dotnet build
```

#### Run the Web API

```bash
dotnet run --project GingerTemplate.WebApi
```

- Default URL: `https://localhost:5001` (or `http://localhost:5000`)
- Swagger UI: `https://localhost:5001/swagger`

#### Run the Web App

```bash
dotnet run --project GingerTemplate.WebApp
```

- Default URL: `https://localhost:5001` (or `http://localhost:5000`)

#### Run the CLI

```bash
dotnet run --project GingerTemplate.CLI
```

- Loads configuration from config.json
- Demonstrates logging with correlation scopes
- Uses dependency injection for services

#### Run the Desktop App

```bash
dotnet run --project GingerTemplate.DesktopApp
```

#### Run Tests

```bash
dotnet test
```

#### Clean build artifacts

```bash
dotnet clean
```

---

### 📦 What's Included

#### Core Components

✅ Custom exception hierarchy for error handling
✅ User model with basic properties
✅ Logging service with correlation scope tracking and Serilog integration
✅ Configuration service with JSON-based hierarchical key access
✅ Type converters (String, Enum, DateTime, Collection)
✅ Utility helpers (Guard clauses, RetryPolicy, ExecutionTimer)
✅ 14 extension method files for common types
✅ String utility extensions
✅ User role and status enumerations

#### Data Layer

✅ Entity Framework Core configured with SQLite
✅ DbContext with User entity configuration
✅ Generic Repository pattern (CRUD operations)

#### Web API

✅ Serilog logging with correlation tracking
✅ Global exception handling middleware
✅ Structured error response format
✅ Configuration service integration
✅ /health endpoint
✅ Project references configured

#### Web App

✅ Serilog logging with correlation tracking
✅ Configuration service integration
✅ /health endpoint

#### CLI Application

✅ System.CommandLine integration
✅ Generic host with Serilog
✅ Dependency injection setup
✅ Configuration service integration
✅ Correlation scope support

#### Testing

✅ xUnit test framework
✅ Moq for mocking
✅ All projects referenced

---

### 🔧 Configuration Steps

#### 1. Update Database Connection String

Edit `GingerTemplate.WebApi/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=gingerdb.db"
  }
}
```

#### 2. Create Initial Database Migration

```bash
cd GingerTemplate.Data
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### 3. Configure Dependency Injection

Edit `GingerTemplate.WebApi/Program.cs`:

```csharp
// Add services
services.AddScoped<IRepository<User>, Repository<User>>();
services.AddScoped<ILoggingService, LoggingService>();

// Configure DbContext
services.AddDbContext<GingerTemplateDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

#### 4. Create Your First Service

Example structure in `GingerTemplate.Core/Services/`:

```csharp
public interface IUserService
{
    Task<User> GetUserAsync(int id);
    Task<User> CreateUserAsync(User user);
}

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;
    private readonly ILoggingService _logger;

    public UserService(IRepository<User> repository, ILoggingService logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<User> GetUserAsync(int id)
    {
        _logger.LogInformation($"Fetching user with id: {id}");
        return await _repository.GetByIdAsync(id);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _logger.LogInformation($"Creating new user: {user.Email}");
        return await _repository.AddAsync(user);
    }
}
```

#### 5. Create Your First API Endpoint

Create `GingerTemplate.WebApi/Endpoints/UsersController.cs`:

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userService.GetUserAsync(id);
        if (user == null)
            return NotFound();
        
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] User user)
    {
        var created = await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUser), new { id = created.Id }, created);
    }
}
```

---

### 📚 Project Documentation

- **README.md** - Complete project overview
- **CODING_STANDARDS.md** - C# coding standards and best practices
- **PROJECT_STRUCTURE.md** - Detailed project structure documentation
- **LICENSE** - MIT License

---

### 🎯 Recommended Next Steps

1. **Set up the database**
   - Configure connection string
   - Run migrations

2. **Implement core services**
   - Create IUserService and UserService
   - Add more service interfaces as needed

3. **Create API endpoints**
   - Add controllers for each resource
   - Implement CRUD operations

4. **Add authentication**
   - Implement JWT or OAuth
   - Add authorization policies

5. **Build the UI**
   - Create Razor Pages or API clients
   - Implement desktop UI with Avalonia
   - Develop mobile app with MAUI

6. **Write tests**
   - Unit tests for services
   - Integration tests for API endpoints
   - Follow AAA pattern (Arrange-Act-Assert)

7. **Deploy**
   - Configure production appsettings
   - Set up CI/CD pipeline
   - Deploy to hosting platform

---

### 🔗 Useful Links

- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [Avalonia Documentation](https://docs.avaloniaui.net/)

---

### ❓ Troubleshooting

**Build fails with package errors:**

```bash
dotnet nuget locals all --clear
dotnet restore
```

**Database connection issues:**

- Check connection string in appsettings.json
- Verify SQLite file path exists
- Run migrations again

**Port already in use:**

```bash
# Change port in appsettings.json or launchSettings.json
```

**Missing dependencies:**

```bash
cd [ProjectName]
dotnet restore
```

---

### 📝 Notes

- All projects target .NET 10.0
- SQLite is configured for development/testing
- Clean Architecture patterns are implemented
- Dependency Injection is the core pattern
- Custom exception handling is in place

---

**Ready to start coding? Happy development! 🎉**

For detailed information, refer to the **CODING_STANDARDS.md** and **PROJECT_STRUCTURE.md** files.
