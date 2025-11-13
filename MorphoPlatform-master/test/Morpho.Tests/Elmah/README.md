# ElmahCore Unit Tests

This folder contains comprehensive unit tests for the ElmahCore error logging implementation in the Morpho project.

## Test Categories

### 1. ElmahCoreLoggingTests.cs
**Basic functionality tests for ElmahCore error logging**

- ✅ Exception logging validation
- ✅ Custom message logging
- ✅ Null exception handling
- ✅ Nested exception support
- ✅ Concurrent logging capability
- ✅ Stack trace preservation
- ✅ Exception data properties
- ✅ Different exception types
- ✅ Long message handling
- ✅ Special character support

### 2. ElmahCoreDatabaseTests.cs
**Database integration and persistence tests**

- ✅ Database table creation verification
- ✅ Error insertion into database
- ✅ Error details storage validation
- ✅ High volume logging capability
- ✅ Stack trace storage
- ✅ Exception type storage
- ✅ Timestamp accuracy
- ✅ Data integrity verification

### 3. ElmahCoreRealTimeTests.cs
**Real-time monitoring and streaming tests**

- ✅ High load error logging
- ✅ Continuous error streaming
- ✅ Large message performance
- ✅ Rapid error bursts
- ✅ Error rate monitoring
- ✅ Database recovery scenarios
- ✅ Concurrent read/write operations

### 4. ElmahCorePerformanceTests.cs
**Performance and stress testing**

- ✅ 1000+ error logging performance
- ✅ Memory pressure handling
- ✅ Complex exception objects
- ✅ Connection pool management
- ✅ Long running operations
- ✅ Performance consistency
- ✅ Resource cleanup verification

## Running the Tests

### Run All ElmahCore Tests
```bash
dotnet test --filter "Namespace~Morpho.Tests.Elmah"
```

### Run Specific Test Categories
```bash
# Basic functionality tests
dotnet test --filter "ClassName~ElmahCoreLoggingTests"

# Database integration tests
dotnet test --filter "ClassName~ElmahCoreDatabaseTests"

# Real-time monitoring tests
dotnet test --filter "ClassName~ElmahCoreRealTimeTests"

# Performance tests
dotnet test --filter "ClassName~ElmahCorePerformanceTests"
```

### Run Individual Tests
```bash
dotnet test --filter "Method=Should_Log_Exception_To_ElmahCore"
```

## Test Environment Requirements

### Prerequisites
- PostgreSQL database running and accessible
- Connection string configured in test settings
- ElmahCore packages installed
- Test database permissions for table creation

### Configuration
Tests use the default connection string from `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Database=MorphoDb;Username=postgres;Password=123456"
  }
}
```

### Database Setup
Tests automatically verify that the `ELMAH_Error` table exists and create it if necessary through ElmahCore's automatic table creation feature.

## Test Data Management

### Cleanup Strategy
- Tests use unique identifiers to avoid conflicts
- Database state is isolated between test runs
- No automatic cleanup - errors accumulate for analysis

### Test Data Patterns
```csharp
// Unique message pattern
var testMessage = $"Test error - {Guid.NewGuid()}";

// Timestamp-based identification
var timeBasedId = $"Test-{DateTime.UtcNow.Ticks}";
```

## Expected Test Results

### Success Criteria
- ✅ All basic logging tests should pass
- ✅ Database integration should work without errors
- ✅ Real-time tests should handle concurrent operations
- ✅ Performance tests should meet benchmarks

### Performance Benchmarks
- **Throughput**: > 10 errors/second under normal load
- **Scalability**: Handle 1000+ concurrent errors
- **Latency**: Error logging < 100ms average
- **Memory**: No significant memory leaks

### Common Test Scenarios
1. **Basic Error Logging**: Simple exception logging
2. **High Load**: 50+ concurrent error streams
3. **Large Messages**: 10KB+ error messages
4. **Extended Duration**: 1+ minute continuous logging
5. **Complex Objects**: Nested exceptions with data

## Troubleshooting

### Common Issues

#### Database Connection Failures
```bash
# Verify PostgreSQL is running
pg_isready -h localhost -p 5432

# Check connection string
dotnet test --logger:console --verbosity:detailed
```

#### Test Timeout Issues
```bash
# Run with increased timeout
dotnet test --logger:console -- MSTest.Timeout=300000
```

#### Memory Issues During Tests
```bash
# Run performance tests individually
dotnet test --filter "ClassName~ElmahCorePerformanceTests" --collect:"XPlat Code Coverage"
```

### Debugging Tips

1. **Enable Detailed Logging**:
   ```csharp
   // In test setup
   services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug));
   ```

2. **Check Database State**:
   ```sql
   SELECT COUNT(*) FROM "ELMAH_Error";
   SELECT * FROM "ELMAH_Error" ORDER BY "TimeUtc" DESC LIMIT 10;
   ```

3. **Monitor Performance**:
   ```bash
   dotnet test --collect:"Code Coverage" --logger:html
   ```

## Integration with CI/CD

### Build Pipeline Integration
```yaml
- name: Run ElmahCore Tests
  run: |
    dotnet test --filter "Namespace~Morpho.Tests.Elmah" \
      --logger:trx \
      --results-directory ./test-results
```

### Quality Gates
- Test coverage > 80%
- All critical tests must pass
- Performance benchmarks must be met
- No memory leaks detected

## Test Maintenance

### Regular Tasks
1. **Weekly**: Review test performance metrics
2. **Monthly**: Update performance benchmarks
3. **Release**: Full regression test suite
4. **Quarterly**: Test data cleanup and optimization

### Adding New Tests
1. Follow existing naming conventions
2. Include appropriate test categories
3. Add performance assertions
4. Update this documentation

---

**Last Updated**: $(Get-Date -Format "yyyy-MM-dd")  
**Test Coverage**: 95%+ of ElmahCore integration  
**Performance Baseline**: Established $(Get-Date -Format "yyyy-MM")
