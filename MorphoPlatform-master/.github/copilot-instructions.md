# Morpho Backend
Morpho Backend is an ASP.NET Core 8.0 web application built using the ASP.NET Boilerplate framework. It consists of multiple projects including a Web API host, MVC web application, Entity Framework Core data layer, and supporting utilities. The application uses PostgreSQL as its primary database.

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Working Effectively

### Bootstrap, Build, and Test the Repository:
- `sudo apt-get update && sudo apt-get install -y postgresql postgresql-contrib` -- Install PostgreSQL (takes 2-3 minutes). NEVER CANCEL.
- `sudo systemctl start postgresql && sudo systemctl enable postgresql` -- Start PostgreSQL service
- `sudo -u postgres psql -c "ALTER USER postgres PASSWORD '6954';"` -- Set postgres password to match configuration
- `sudo -u postgres createdb MorphoDb` -- Create the application database
- Fix NuGet configuration if MyGet source fails (network restricted environments): 
  ```bash
  cp NuGet.Config NuGet.Config.backup
  echo '<?xml version="1.0" encoding="utf-8"?>
  <configuration>
    <packageSources>
      <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
    </packageSources>
  </configuration>' > NuGet.Config
  ```
- `dotnet restore` -- takes 18 seconds. NEVER CANCEL. Set timeout to 60+ minutes.
- `dotnet build --no-restore -c Release` -- takes 26 seconds. NEVER CANCEL. Set timeout to 60+ minutes.
- `dotnet test -c Release --no-build` -- takes 15 seconds. NEVER CANCEL. Set timeout to 30+ minutes.

### Database Migration:
- Run the migrator BEFORE starting applications:
  ```bash
  cd src/Morpho.Migrator
  dotnet run --configuration Release
  # When prompted "Continue to migration for this host database and all tenants..? (Y/N):", answer Y
  ```
- Migration takes ~3 minutes. NEVER CANCEL.

### Run the Applications:
- **ALWAYS run the bootstrapping steps first**
- **ALWAYS run database migration before starting applications**
- **API Host**: `cd src/Morpho.Web.Host && dotnet run --configuration Release` 
  - Runs on https://localhost:44311
  - Swagger UI available at https://localhost:44311/swagger/index.html
- **MVC Web App**: `cd src/Morpho.Web.Mvc && dotnet run --configuration Release`
  - Runs on http://localhost:5000
  - Provides the main web interface with AdminLTE theme

## Validation

### Manual Testing Requirements:
- **ALWAYS manually validate functionality after making changes**
- Test the API by accessing https://localhost:44311/swagger/index.html
- Test basic API functionality: `curl -k https://localhost:44311/api/services/app/Session/GetCurrentLoginInformations`
- Test the MVC app by accessing http://localhost:5000 (shows login page)
- Default login credentials: username `admin`, password `123qwe` (configured by seeding)
- Verify database connectivity by checking application logs during startup

### Application Health Checks:
- Monitor logs during startup - both applications should connect to PostgreSQL successfully
- Watch for Entity Framework query logs indicating successful database connectivity
- API should show "Application started. Press Ctrl+C to shut down." message
- MVC app should show similar startup confirmation

## Common Tasks

### Build Issues:
- If MyGet source fails during `dotnet restore`, update NuGet.Config to use only nuget.org (see bootstrap steps)
- Build warnings about missing XML documentation are normal and expected
- PostgreSQL connection failures mean the database service is not running or password is incorrect

### Database Issues:
- If migration fails, ensure PostgreSQL is running: `sudo systemctl status postgresql`
- If database connection fails, verify password: `sudo -u postgres psql -c "SELECT version();"`
- Connection string format: `Host=localhost;Database=MorphoDb;Username=postgres;Password=6954`

### Project Structure:
The solution contains these key projects:
- **Morpho.Core** - Domain entities and business rules
- **Morpho.Application** - Application services and DTOs  
- **Morpho.EntityFrameworkCore** - Data access layer with EF Core
- **Morpho.Web.Core** - Shared web components
- **Morpho.Web.Host** - Web API host (REST API)
- **Morpho.Web.Mvc** - MVC web application with UI
- **Morpho.Migrator** - Database migration utility
- **Morpho.Tests** - Unit tests
- **Morpho.Web.Tests** - Web integration tests

### Configuration Files:
- **appsettings.json** - Database connection strings and app settings
- **NuGet.Config** - Package sources (use only nuget.org if MyGet fails)
- **Morpho.sln** - Solution file with all projects
- **.github/workflows/Deploy_On_Staging_Server** - CI/CD pipeline

### Common Commands Reference:

#### Repository Root Contents:
```
.dockerignore          Morpho.sln           build/               src/
.gitattributes         NuGet.Config         docker/              test/
.github/               README.md            rename.ps1
LICENSE                _screenshots/
```

#### Build and Test Process:
```bash
# Full build process (run in repository root):
dotnet restore                                    # ~18 seconds
dotnet build --no-restore -c Release            # ~26 seconds  
dotnet test -c Release --no-build               # ~15 seconds

# Database setup:
cd src/Morpho.Migrator && dotnet run -c Release # ~3 minutes with user confirmation

# Run applications:
cd src/Morpho.Web.Host && dotnet run -c Release # API on https://localhost:44311
cd src/Morpho.Web.Mvc && dotnet run -c Release  # MVC on http://localhost:5000
```

#### Docker Support:
- `build/build-mvc.ps1` - PowerShell script for building MVC Docker image
- `build/build-with-ng.sh` - Bash script for building with Angular (if applicable)
- Both scripts build Docker images but require Docker to be installed

### Technology Stack:
- **.NET 8.0** - Target framework
- **ASP.NET Boilerplate** - Application framework
- **Entity Framework Core 8.0** - ORM with PostgreSQL provider
- **PostgreSQL** - Primary database
- **AdminLTE** - UI theme for MVC application
- **Swagger/OpenAPI** - API documentation
- **xUnit** - Testing framework

### Critical Reminders:
- **NEVER CANCEL** long-running commands (builds, tests, migrations)
- **ALWAYS** set timeouts of 60+ minutes for builds, 30+ minutes for tests
- **ALWAYS** run database migration before starting applications
- **ALWAYS** validate functionality manually after changes using API calls and UI testing
- **CRITICAL**: The MyGet NuGet source may fail in restricted environments - update NuGet.Config to use only nuget.org
- PostgreSQL must be running with correct credentials for applications to start
- Both Web.Host and Web.Mvc applications cannot run simultaneously on different ports due to shared database connections
- Build produces 303 warnings about XML documentation - this is normal and expected
- API returns JSON responses and is fully functional for testing application logic