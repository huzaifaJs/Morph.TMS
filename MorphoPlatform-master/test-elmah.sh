#!/bin/bash

# ElmahCore Test Runner Script
# This script runs comprehensive tests for ElmahCore implementation

echo "🔧 ElmahCore Test Runner"
echo "========================"

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Test results tracking
TOTAL_TESTS=0
PASSED_TESTS=0
FAILED_TESTS=0

# Function to run test category
run_test_category() {
    local category_name="$1"
    local filter="$2"
    local description="$3"
    
    echo -e "\n${YELLOW}📋 Running: $description${NC}"
    echo "Filter: $filter"
    echo "----------------------------------------"
    
    # Run the tests and capture output
    if dotnet test --filter "$filter" --logger:console --verbosity:normal; then
        echo -e "${GREEN}✅ $category_name tests PASSED${NC}"
        PASSED_TESTS=$((PASSED_TESTS + 1))
    else
        echo -e "${RED}❌ $category_name tests FAILED${NC}"
        FAILED_TESTS=$((FAILED_TESTS + 1))
    fi
    
    TOTAL_TESTS=$((TOTAL_TESTS + 1))
}

# Check prerequisites
echo "🔍 Checking prerequisites..."

# Check if dotnet is installed
if ! command -v dotnet &> /dev/null; then
    echo -e "${RED}❌ .NET CLI not found. Please install .NET 8.0 SDK${NC}"
    exit 1
fi

# Check if PostgreSQL is accessible (basic check)
echo "🗄️  Checking database connectivity..."
if command -v pg_isready &> /dev/null; then
    if pg_isready -h localhost -p 5432 &> /dev/null; then
        echo -e "${GREEN}✅ PostgreSQL is accessible${NC}"
    else
        echo -e "${YELLOW}⚠️  PostgreSQL connection check failed. Tests may fail if database is not running.${NC}"
    fi
else
    echo -e "${YELLOW}⚠️  pg_isready not found. Skipping database connectivity check.${NC}"
fi

# Build the test projects
echo -e "\n🔨 Building test projects..."
if dotnet build test/Morpho.Tests/Morpho.Tests.csproj && dotnet build test/Morpho.Web.Tests/Morpho.Web.Tests.csproj; then
    echo -e "${GREEN}✅ Test projects built successfully${NC}"
else
    echo -e "${RED}❌ Failed to build test projects${NC}"
    exit 1
fi

echo -e "\n🚀 Starting ElmahCore Tests..."
echo "=============================="

# Run test categories
run_test_category "Basic Logging" "ClassName~ElmahCoreLoggingTests" "Basic ElmahCore Logging Functionality"
run_test_category "Database Integration" "ClassName~ElmahCoreDatabaseTests" "Database Integration and Persistence"
run_test_category "Web Integration" "ClassName~ElmahCoreIntegrationTests" "Web API Integration Tests"
run_test_category "Real-time Monitoring" "ClassName~ElmahCoreRealTimeTests" "Real-time Monitoring and Streaming"
run_test_category "Performance" "ClassName~ElmahCorePerformanceTests" "Performance and Stress Testing"

# Run all ElmahCore tests together for summary
echo -e "\n${YELLOW}📊 Running Complete ElmahCore Test Suite${NC}"
echo "=========================================="
dotnet test --filter "Namespace~Morpho.Tests.Elmah" --logger:console --verbosity:minimal

# Summary
echo -e "\n📈 Test Results Summary"
echo "======================="
echo -e "Total Test Categories: ${TOTAL_TESTS}"
echo -e "Passed: ${GREEN}${PASSED_TESTS}${NC}"
echo -e "Failed: ${RED}${FAILED_TESTS}${NC}"

if [ $FAILED_TESTS -eq 0 ]; then
    echo -e "\n${GREEN}🎉 All ElmahCore test categories PASSED!${NC}"
    echo -e "${GREEN}ElmahCore is working correctly in real-time scenarios.${NC}"
    exit 0
else
    echo -e "\n${RED}💥 Some test categories FAILED!${NC}"
    echo -e "${RED}Please check the test output above for details.${NC}"
    exit 1
fi
