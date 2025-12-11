# Ginger Template - Coding Standards

> 📚 **Part of the [GingerTemplate Documentation](README.md)** - See also: [Quick Start](QUICKSTART.md) | [Project Structure](PROJECT_STRUCTURE.md) | [Folder Structure](FOLDER_STRUCTURE.md)

This document outlines the coding standards and conventions to be followed across all GingerTemplate projects.

## C# Naming Conventions

### General Rules

- Use **PascalCase** for type names, method names, and public properties
- Use **camelCase** for local variables, parameters, and private fields
- Use **UPPER_SNAKE_CASE** for constants
- Prefix private fields with underscore: `_privateField`
- Use meaningful, descriptive names that clearly indicate purpose

### Examples

```csharp
// Class names - PascalCase
public class UserService { }

// Method names - PascalCase
public void ProcessUserData() { }

// Properties - PascalCase
public string UserName { get; set; }

// Local variables - camelCase
var userName = "John";
int userCount = 0;

// Private fields - camelCase with underscore
private readonly IUserRepository _userRepository;
private string _internalState;

// Constants - UPPER_SNAKE_CASE
private const string DEFAULT_USER_ROLE = "User";
```

## Code Organization

### File Structure

- One public class per file (with exceptions for related small classes)
- **File name matches the implementation class name, not the interface**
- **Keep interfaces and their associated implementation classes in the same file**
- The interface should be declared before the implementation class in the same file

### Interface and Implementation Co-location

Interfaces must be kept with their primary implementation in the same file. This improves maintainability by keeping related code together.

**File naming convention:**

- File is named after the implementation class: `UserService.cs`
- Both `IUserService` interface and `UserService` class are in the same file
- The interface is declared first, followed by the implementation

**Example structure:**

```
GingerTemplate.Core.Services/
├── UserService.cs (contains IUserService and UserService)
├── EmailService.cs (contains IEmailService and EmailService)
├── AuthenticationService.cs (contains IAuthenticationService and AuthenticationService)
└── DataValidationService.cs (contains IDataValidationService and DataValidationService)
```

**Code example:**

```csharp
// UserService.cs
namespace GingerTemplate.Core.Services
{
    /// <summary>
    /// Interface for user service operations
    /// </summary>
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task SaveUserAsync(User user);
        Task DeleteUserAsync(int id);
    }

    /// <summary>
    /// Implementation of user service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            // Implementation
        }

        public async Task SaveUserAsync(User user)
        {
            // Implementation
        }

        public async Task DeleteUserAsync(int id)
        {
            // Implementation
        }
    }
}
```

### Namespace Conventions

```
GingerTemplate.{LayerOrFeature}.{SubCategory}
```

Examples:

- `GingerTemplate.Core.Services`
- `GingerTemplate.Core.Models`
- `GingerTemplate.Data.Repositories`
- `GingerTemplate.WebApi.Controllers`

### Class Organization

1. Constant and readonly fields
2. Static fields
3. Static properties
4. Instance fields
5. Instance properties
6. Constructors
7. Methods (public first, then private)
8. Event handlers

```csharp
public class UserService
{
    // Constants
    private const string DEFAULT_ROLE = "User";
    
    // Fields
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;
    
    // Properties
    public int ActiveUserCount { get; private set; }
    
    // Constructors
    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    
    // Public methods
    public async Task<User> GetUserByIdAsync(int id) { }
    
    // Private methods
    private void ValidateUser(User user) { }
}
```

## Formatting & Style

### Braces

- Use Allman style (opening braces on new line)
- Always include braces, even for single-line statements

```csharp
if (user != null)
{
    ProcessUser(user);
}

foreach (var item in items)
{
    HandleItem(item);
}
```

### Line Length

- Maximum 120 characters per line
- Break long lines for readability

### Indentation

- Use 4 spaces per indentation level
- No tabs

### Spacing

- One blank line between methods and properties
- No blank lines at the beginning or end of code blocks
- Space after keywords: `if (`, `for (`, `while (`
- Space around operators: `x + y`, `a == b`

```csharp
public class Example
{
    private string _field;

    public string Property { get; set; }

    public void Method1()
    {
        var x = 5 + 3;
        if (x > 0)
        {
            return;
        }
    }

    public void Method2()
    {
        // Code here
    }
}
```

## Comments & Documentation

### XML Documentation

- Use XML documentation comments for all public types, methods, and properties
- Include `<summary>`, `<param>`, and `<returns>` tags

```csharp
/// <summary>
/// Retrieves a user by their unique identifier.
/// </summary>
/// <param name="userId">The unique user identifier</param>
/// <returns>The user if found; otherwise null</returns>
/// <exception cref="ArgumentException">Thrown when userId is invalid</exception>
public async Task<User> GetUserByIdAsync(int userId)
{
    // Implementation
}
```

### Inline Comments

- Use sparingly; code should be self-explanatory
- Explain "why", not "what"
- Use `//` for single-line comments
- Use `/* */` for multi-line comments

```csharp
// Good: Explains the reasoning
if (user.LoginAttempts > MAX_ATTEMPTS)
{
    // Lock account to prevent brute force attacks
    user.IsLocked = true;
}

// Bad: States the obvious
user.IsLocked = true; // Set is locked to true
```

## Async/Await Guidelines

- Use `async`/`await` for all I/O operations
- Use `Task` return type for async methods without return value
- Use `Task<T>` for async methods with return value
- Append `Async` suffix to async method names
- Avoid `async void` except for event handlers

```csharp
// Correct
public async Task<User> GetUserAsync(int id)
{
    return await _repository.GetUserAsync(id);
}

public async Task SaveUserAsync(User user)
{
    await _repository.SaveAsync(user);
}
```

## Dependency Injection

**Always use dependency injection where possible.** This is a core principle of the Ginger Template architecture.

### Core Principles

- **Constructor injection is mandatory** for all dependencies
- **Use interfaces, never concrete types** in dependencies
- **All external dependencies must be injected** via constructor
- **Mark all injected fields as `readonly`** to prevent reassignment
- **No service locator pattern** - avoid static dependencies or direct instantiation
- **Avoid `new` keyword for services** - dependencies should be resolved by the IoC container

### Anti-Patterns to Avoid

```csharp
// ❌ BAD: Direct instantiation
public class UserService
{
    private IUserRepository _repository = new UserRepository(); // Wrong!
    
    public void ProcessUser(User user)
    {
        _repository.Save(user);
    }
}

// ❌ BAD: Static service access
public class UserService
{
    public void ProcessUser(User user)
    {
        ServiceLocator.GetService<IUserRepository>().Save(user); // Wrong!
    }
}

// ❌ BAD: Optional dependencies not injected
public class UserService
{
    private IEmailService _emailService;
    
    public void SendEmail(string email)
    {
        _emailService = new EmailService(); // Wrong!
    }
}
```

### Correct Implementation

```csharp
// ✅ GOOD: Constructor injection with interfaces
public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<UserService> _logger;
    
    // All dependencies injected via constructor
    public UserService(
        IUserRepository userRepository,
        IEmailService emailService,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _logger = logger;
    }
    
    public async Task ProcessUserAsync(User user)
    {
        _userRepository.Save(user);
        await _emailService.SendWelcomeEmailAsync(user.Email);
    }
}
```

### Registering Services in IoC Container

All services must be registered in the dependency injection container at application startup:

```csharp
// In Program.cs or Startup.cs
public void ConfigureServices(IServiceCollection services)
{
    // Register all services with appropriate lifetimes
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IEmailService, EmailService>();
    services.AddSingleton<IConfigurationService, ConfigurationService>();
    services.AddTransient<IDataValidationService, DataValidationService>();
}
```

### Service Lifetimes

- **Transient**: New instance each time - use for stateless utilities
- **Scoped**: One instance per request/scope - use for repository and service classes
- **Singleton**: One instance for application lifetime - use for configuration, logging, caching

### Rules for Using Dependency Injection

1. **Every public class that depends on other services must accept them in the constructor**
2. **Never use `new` to instantiate services** - let the container handle object creation
3. **Depend on abstractions (interfaces), not concrete implementations**
4. **All injected dependencies should be stored as private readonly fields**
5. **Do not pass around instances of static service locators or factories**
6. **Register dependencies in a central configuration location** (Startup.cs or Program.cs)

### When NOT to Use Constructor Injection

Constructor injection should not be used for:

- **Configuration values** (use `IConfiguration`)
- **Non-service dependencies** (use properties or parameters)
- **Optional behavior** (use optional interfaces with null checks)

```csharp
// Acceptable: Configuration values
public class AppSettings
{
    public string ConnectionString { get; set; }
    public int MaxRetries { get; set; }
}

public class DataService
{
    private readonly AppSettings _settings;
    
    public DataService(IConfiguration config)
    {
        _settings = config.GetSection("AppSettings")
            .Get<AppSettings>();
    }
}
```

## Error Handling

- Use specific exception types
- Avoid catching generic `Exception`

## Error Handling

- Use specific exception types
- Avoid catching generic `Exception`
- Always log exceptions
- Provide meaningful error messages

```csharp
try
{
    var user = await _repository.GetUserAsync(id);
}
catch (UserNotFoundException ex)
{
    _logger.LogWarning($"User not found: {id}");
    throw;
}
catch (DbException ex)
{
    _logger.LogError($"Database error: {ex.Message}");
    throw new ServiceException("Failed to retrieve user", ex);
}
```

## LINQ Usage

- Use method syntax for clarity
- Avoid overly complex LINQ chains
- Break complex queries into multiple statements

```csharp
// Preferred
var activeUsers = _users
    .Where(u => u.IsActive)
    .OrderBy(u => u.Name)
    .ToList();

// Avoid
var activeUsers = (from u in _users where u.IsActive select u).OrderBy(u => u.Name).ToList();
```

## Testing Standards

- Arrange-Act-Assert (AAA) pattern
- Descriptive test method names: `MethodName_Scenario_ExpectedResult`
- One assertion concept per test
- Use meaningful assert messages

```csharp
[Fact]
public async Task GetUserById_WithValidId_ReturnsUser()
{
    // Arrange
    var userId = 1;
    var expectedUser = new User { Id = userId, Name = "John" };
    _mockRepository.Setup(r => r.GetUserAsync(userId))
        .ReturnsAsync(expectedUser);

    // Act
    var result = await _service.GetUserAsync(userId);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(expectedUser.Id, result.Id);
    Assert.Equal(expectedUser.Name, result.Name);
}
```

## Performance Considerations

- Avoid LINQ-to-Objects on large collections
- Use `IEnumerable<T>` for lazy evaluation when appropriate
- Avoid string concatenation in loops; use `StringBuilder`
- Use `using` statements for resource management
- Cache frequently accessed data appropriately

```csharp
// Good: Using StringBuilder
var sb = new StringBuilder();
foreach (var item in largeCollection)
{
    sb.Append(item);
}

// Avoid: String concatenation
string result = "";
foreach (var item in largeCollection)
{
    result += item; // Creates new string each iteration
}
```

## Security Best Practices

- Validate all user input
- Use parameterized queries to prevent SQL injection
- Never hardcode sensitive data (credentials, API keys)
- Use configuration management for secrets
- Implement proper authentication and authorization
- Log security events

## Code Review Checklist

- [ ] Follows naming conventions
- [ ] Proper use of async/await
- [ ] Adequate error handling
- [ ] XML documentation on public members
- [ ] No hardcoded values (use constants/config)
- [ ] Unit tests included
- [ ] No code duplication
- [ ] Performance acceptable
- [ ] Security considerations addressed

## Tools & Configuration

- **EditorConfig**: `.editorconfig` file defines formatting rules
- **StyleCop**: Enforces C# style rules
- **ReSharper/Rider**: Code inspection and refactoring
- **Code Analysis**: FxCop rules enabled in project files

Ensure your IDE is configured to respect these standards and enable code analysis warnings.
