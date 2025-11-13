# Morpho Web Host Documentation

This folder contains documentation for the Morpho Web Host project components and integrations.

## Available Documentation

### [ElmahCore Implementation Guide](./ElmahCore-Implementation.md)
Comprehensive guide for the ElmahCore error logging implementation, including:
- Installation and configuration steps
- Usage examples and best practices
- Security considerations
- Troubleshooting guide
- Database schema information

## Quick Start

### ElmahCore Error Logging

1. **Access Dashboard:** Navigate to `/elmah` (requires authentication)
2. **Test Error Logging:** Visit `/Home/TestElmahError` to generate test errors
3. **Custom Logging:** Use `ElmahExtensions.RiseError(exception)` in your code

### Project Structure

```
Morpho.Web.Host/
├── Controllers/          # API Controllers
├── Documentation/        # Project documentation
├── Startup/             # Application startup configuration
├── wwwroot/             # Static files
└── appsettings.json     # Application configuration
```

## Configuration Files

### Key Configuration Sections

- **ConnectionStrings:** Database connection settings
- **Authentication:** JWT Bearer token configuration
- **ElmahCore:** Error logging configuration
- **Swagger:** API documentation settings

## Development Guidelines

1. **Error Handling:** Always use ElmahCore for error logging
2. **Authentication:** Ensure proper authentication for sensitive endpoints
3. **Documentation:** Keep documentation updated with changes
4. **Testing:** Use provided test endpoints for validation

## Support

For questions or issues related to the Web Host configuration, refer to:
- Project-specific documentation in this folder
- ASP.NET Boilerplate official documentation
- Team development guidelines

---
*Last Updated: $(Get-Date -Format "yyyy-MM-dd")*
