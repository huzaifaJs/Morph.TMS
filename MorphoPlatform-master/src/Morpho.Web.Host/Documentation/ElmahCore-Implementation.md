# ElmahCore Implementation Guide

## Overview

This document provides comprehensive guidance on the ElmahCore implementation in the Morpho ASP.NET Boilerplate project. ElmahCore is an error logging middleware that captures, stores, and displays unhandled exceptions and custom error logs in a web-based dashboard.

## Table of Contents

1. [Installation](#installation)
2. [Configuration](#configuration)
3. [Usage](#usage)
4. [Security](#security)
5. [Database Schema](#database-schema)
6. [Testing](#testing)
7. [Troubleshooting](#troubleshooting)
8. [Best Practices](#best-practices)

## Installation

### NuGet Packages

The following packages have been installed in the `Morpho.Web.Host` project:

```xml
<PackageReference Include="ElmahCore" Version="2.1.2" />
<PackageReference Include="ElmahCore.Postgresql" Version="2.1.2" />
```

### Dependencies

- **PostgreSQL** - ElmahCore uses your existing PostgreSQL database
- **ASP.NET Core 8.0** - Compatible with the current framework version
- **Entity Framework Core** - For database operations

## Configuration

### appsettings.json

The following configuration has been added to `appsettings.json`:

```json
{
  "ElmahCore": {
    "Path": "/elmah",
    "CheckPermissionAction": null
  }
}
```

**Configuration Options:**
- `Path`: The URL path where the ElmahCore dashboard will be accessible
- `CheckPermissionAction`: Custom permission checking (set to null, using code-based authentication)

### Startup.cs Configuration

#### Services Configuration (ConfigureServices method):

```csharp
// Configure ElmahCore
services.AddElmah<ElmahCore.Postgresql.PgsqlErrorLog>(options =>
{
    options.ConnectionString = _appConfiguration.GetConnectionString("Default");
    options.Path = _appConfiguration["ElmahCore:Path"];
    options.OnPermissionCheck = context => context.User.Identity.IsAuthenticated;
});
```

**Key Features:**
- Uses PostgreSQL as the error log storage provider
- Inherits the existing database connection string
- Restricts access to authenticated users only

#### Middleware Configuration (Configure method):

```csharp
app.UseAuthentication();
app.UseAuthorization();

// Use ElmahCore middleware
app.UseElmah();

app.UseAbpRequestLocalization();
```

**Important:** ElmahCore middleware is placed after authentication and authorization to ensure proper security checks.

## Usage

### Accessing the Dashboard

1. **Start the application**
2. **Navigate to:** `https://localhost:44311/elmah`
3. **Authentication required:** You must be logged in to access the dashboard

### Automatic Error Capture

ElmahCore automatically captures:
- Unhandled exceptions
- HTTP errors (404, 500, etc.)
- Application crashes
- Request processing errors

### Manual Error Logging

#### Basic Error Logging

```csharp
try
{
    // Your code that might throw an exception
    SomeMethodThatMightFail();
}
catch (Exception ex)
{
    // Log the error to ElmahCore
    ElmahExtensions.RiseError(ex);
    
    // Handle the error appropriately
    return BadRequest("An error occurred");
}
```

#### Custom Error Messages

```csharp
// Create a custom exception for logging
var customException = new ApplicationException("Custom error message with context");
ElmahExtensions.RiseError(customException);
```

#### Error Logging with Context

```csharp
try
{
    ProcessUserData(userId);
}
catch (Exception ex)
{
    // Add context to the error
    var contextualException = new InvalidOperationException(
        $"Failed to process user data for User ID: {userId}. Original error: {ex.Message}", 
        ex);
    
    ElmahExtensions.RiseError(contextualException);
    throw; // Re-throw if needed
}
```

### Test Endpoints

The following test endpoints have been added to `HomeController`:

#### 1. Test Exception Logging
- **URL:** `/Home/TestElmahError`
- **Purpose:** Generates and logs a test exception
- **Response:** Confirmation message with timestamp

#### 2. Test Custom Logging
- **URL:** `/Home/TestElmahLog?message=your-custom-message`
- **Purpose:** Logs a custom error message
- **Parameters:** 
  - `message` (optional): Custom message to log
- **Response:** Confirmation message

## Security

### Authentication Requirements

- **Dashboard Access:** Restricted to authenticated users only
- **Permission Check:** `context.User.Identity.IsAuthenticated`
- **No Public Access:** Anonymous users cannot view error logs

### Security Best Practices

1. **Production Considerations:**
   ```csharp
   // For production, consider more restrictive access
   options.OnPermissionCheck = context => 
       context.User.Identity.IsAuthenticated && 
       context.User.IsInRole("Administrator");
   ```

2. **IP Restriction (Optional):**
   ```csharp
   options.OnPermissionCheck = context => 
       context.User.Identity.IsAuthenticated && 
       IsAllowedIP(context.Connection.RemoteIpAddress);
   ```

3. **Environment-Based Access:**
   ```csharp
   options.OnPermissionCheck = context => 
       _hostingEnvironment.IsDevelopment() || 
       context.User.IsInRole("Developer");
   ```

## Database Schema

ElmahCore automatically creates the following table in your PostgreSQL database:

### ELMAH_Error Table Structure

| Column | Type | Description |
|--------|------|-------------|
| ErrorId | UUID | Primary key, unique identifier |
| Application | VARCHAR(60) | Application name |
| Host | VARCHAR(50) | Host server name |
| Type | VARCHAR(100) | Exception type |
| Source | VARCHAR(60) | Exception source |
| Message | VARCHAR(500) | Error message |
| User | VARCHAR(50) | User context |
| StatusCode | INTEGER | HTTP status code |
| TimeUtc | TIMESTAMP | UTC timestamp |
| Sequence | INTEGER | Sequential number |
| AllXml | TEXT | Complete error details in XML |

### Database Considerations

- **Storage:** Errors are persisted in PostgreSQL
- **Performance:** Indexed for efficient querying
- **Retention:** No automatic cleanup (implement custom retention policy if needed)

## Testing

### Manual Testing Steps

1. **Start the application:**
   ```bash
   dotnet run --project src/Morpho.Web.Host
   ```

2. **Generate test errors:**
   - Visit: `https://localhost:44311/Home/TestElmahError`
   - Visit: `https://localhost:44311/Home/TestElmahLog?message=TestMessage`

3. **View errors in dashboard:**
   - Navigate to: `https://localhost:44311/elmah`
   - Login if prompted
   - Verify errors appear in the list

### Integration Testing

```csharp
[Test]
public async Task ElmahCore_Should_Log_Exceptions()
{
    // Arrange
    var exception = new InvalidOperationException("Test exception");
    
    // Act
    ElmahExtensions.RiseError(exception);
    
    // Assert
    // Verify exception is logged to database
    var errorExists = await CheckErrorExistsInDatabase(exception.Message);
    Assert.IsTrue(errorExists);
}
```

## Troubleshooting

### Common Issues

#### 1. Dashboard Not Accessible
**Problem:** 404 error when accessing `/elmah`
**Solution:**
- Verify `app.UseElmah()` is called in `Configure` method
- Check the path configuration in `appsettings.json`
- Ensure middleware order is correct

#### 2. Authentication Required Error
**Problem:** Unauthorized access to dashboard
**Solution:**
- Ensure user is logged in
- Check `OnPermissionCheck` configuration
- Verify authentication middleware is properly configured

#### 3. Database Connection Issues
**Problem:** Errors not being stored
**Solution:**
- Verify PostgreSQL connection string
- Check database connectivity
- Ensure database user has CREATE TABLE permissions

#### 4. Missing Error Details
**Problem:** Errors appear but with limited information
**Solution:**
- Check exception handling in controllers
- Verify `ElmahExtensions.RiseError()` is called correctly
- Review the AllXml column for complete details

### Debugging Steps

1. **Enable detailed logging:**
   ```csharp
   services.AddLogging(builder => 
   {
       builder.AddConsole();
       builder.SetMinimumLevel(LogLevel.Debug);
   });
   ```

2. **Check database logs:**
   ```sql
   SELECT * FROM "ELMAH_Error" ORDER BY "TimeUtc" DESC LIMIT 10;
   ```

3. **Verify middleware registration:**
   ```csharp
   // Ensure this order in Configure method
   app.UseAuthentication();
   app.UseAuthorization();
   app.UseElmah(); // After auth middleware
   ```

## Best Practices

### 1. Error Context
Always provide meaningful context in error messages:

```csharp
catch (Exception ex)
{
    var contextException = new ApplicationException(
        $"Operation failed for User: {userId}, Entity: {entityId}, Action: {action}", 
        ex);
    ElmahExtensions.RiseError(contextException);
}
```

### 2. Sensitive Data Protection
Avoid logging sensitive information:

```csharp
// Bad
ElmahExtensions.RiseError(new Exception($"Login failed for password: {password}"));

// Good
ElmahExtensions.RiseError(new Exception($"Login failed for user: {username}"));
```

### 3. Performance Considerations
- Don't log every exception in high-traffic scenarios
- Consider implementing error throttling for repeated errors
- Use async logging when possible

### 4. Error Classification
Use different exception types for different error categories:

```csharp
// Business logic errors
throw new BusinessException("Invalid business rule violation");

// Data access errors
throw new DataAccessException("Database operation failed");

// External service errors
throw new ExternalServiceException("Third-party API call failed");
```

### 5. Monitoring Integration
Consider integrating with external monitoring tools:

```csharp
catch (Exception ex)
{
    // Log to ElmahCore
    ElmahExtensions.RiseError(ex);
    
    // Also log to external monitoring (Application Insights, etc.)
    _telemetryClient.TrackException(ex);
}
```

## Maintenance

### Regular Tasks

1. **Monitor Error Volume:** Check dashboard regularly for error trends
2. **Database Cleanup:** Implement retention policy for old errors
3. **Performance Review:** Monitor database size and query performance
4. **Security Audit:** Review access logs and permissions

### Retention Policy Example

```sql
-- Delete errors older than 30 days
DELETE FROM "ELMAH_Error" 
WHERE "TimeUtc" < NOW() - INTERVAL '30 days';
```

## Support and Resources

- **ElmahCore GitHub:** https://github.com/ElmahCore/ElmahCore
- **ASP.NET Boilerplate Documentation:** https://aspnetboilerplate.com/
- **PostgreSQL Documentation:** https://www.postgresql.org/docs/

---

**Document Version:** 1.0  
**Last Updated:** $(Get-Date -Format "yyyy-MM-dd")  
**Author:** Morpho Development Team
