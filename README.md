# Test Automation Project - FakeRestAPI

This is a .NET 9 test automation project using xUnit to test the FakeRestAPI (http://fakerestapi.azurewebsites.net).

## 📁 Project Structure

```
RecruitmentProject/
├── Helpers/                # Testing utilities
│   ├── TestLogger.cs       # Custom assertion failure logger
│   └── ApiClientHelper.cs  # HTTP client extension methods
├── Setup/                  # Test infrastructure
│   ├── Configuration/
│   │   └── ApiSettings.cs
│   ├── TestFixture.cs      # xUnit test fixture
│   └── SetupForAllTests.cs # DI configuration
├── TestApi/                # API client wrapper
│   ├── Models/                 # API data models
│   │   ├── Activity.cs
│   │   ├── Author.cs
│   │   └── Book.cs
│   ├── TestClientApi.cs    # HTTP client wrapper
│   ├── TestApiTestSteps.cs # Test step methods
│   └── UrlSegments.cs      # API endpoint constants
├── Tests/                  # Test classes
│   ├── ActivitiesTests.cs
│   ├── AuthorsTests.cs
│   └── BooksTests.cs
├── BaseTest.cs             # Abstract base class for all tests
├── AssemblyInfo.cs         # Parallel test configuration
└── appsettings.json        # Configuration
```

## 🧪 Features

### ✅ Test Coverage

1. **GET /api/v1/Activities**
   - Verifies successful response (200 OK)
   - Validates exactly 30 activities are returned
   - Ensures no activities have due date = yesterday

2. **POST /api/v1/Authors**
   - Verifies successful response (200 OK)
   - Validates response body matches request body

3. **PUT /api/v1/Books/{id}**
   - Verifies successful response (200 OK)
   - Validates response body matches request body

4. **GET /api/v1/Books/{id}** (IDs 1–10)
   - Verifies successful response (200 OK)
   - Validates ID in response matches request ID
   - Validates PageCount: 100 for ID=1, 200 for ID=2, ..., 1000 for ID=10

5. **DELETE /api/v1/Authors/{id}**
   - Creates an author via POST
   - Deletes the created author
   - Verifies successful deletion (200 OK)

### 🧾 Assertion Failure Logging

All assertion failures are logged with detailed information:
- Endpoint URL  
- Request body  
- HTTP status code  
- Response message  
- Expected response  
- Actual response  

### ⚙️ Parallel Test Execution

Tests are configured to run in parallel for improved performance:
- Max 6 parallel threads (configured in `AssemblyInfo.cs`)
- Each test class can run independently
- Theory tests within a class can run in parallel

### 🧩 Clean Architecture

- **Dependency Injection**: All components use DI for better testability  
- **Separation of Concerns**: Clear separation between API client, models, and tests  
- **Configuration Management**: Centralized configuration via `appsettings.json`  
- **Reusable Components**:  
  - `TestClientApi` — HTTP client wrapper  
  - `TestApiTestSteps` — Test step methods  
  - `ApiClientHelper` — Extension methods for HTTP operations  
  - `BaseTest` — Abstract base class implementing `IClassFixture<TestFixture>`  

## ▶️ Running the Tests

### Command Line

```bash
dotnet test
```

### Visual Studio

1. Open **Test Explorer** (`Test > Test Explorer`)  
2. Click **Run All Tests**

### Run Specific Tests

```bash
# Run only Activities tests
dotnet test --filter "FullyQualifiedName~ActivitiesTests"

# Run only Books tests
dotnet test --filter "FullyQualifiedName~BooksTests"

# Run only Authors tests
dotnet test --filter "FullyQualifiedName~AuthorsTests"
```

## ⚙️ Configuration

Edit `appsettings.json` to change the API base URL:

```json
{
  "ApiSettings": {
    "BaseUrl": "http://fakerestapi.azurewebsites.net"
  }
}
```

## 📦 Dependencies

- .NET 9.0  
- xUnit 2.9.2  
- Microsoft.Extensions.DependencyInjection 9.0.10  
- Microsoft.Extensions.Http 9.0.10  
- Microsoft.Extensions.Configuration 9.0.10  
- Microsoft.Extensions.Configuration.Json 9.0.10  
- Microsoft.Extensions.Options.ConfigurationExtensions 9.0.10  
- Microsoft.NET.Test.Sdk 17.12.0  
- coverlet.collector 6.0.2  

## 🧾 Test Output Example

When a test fails, you'll see detailed logging:

```
=== ASSERTION FAILURE ===
Endpoint: /api/v1/Activities
Field: Activity Count
Expected: 30
Actual: 25
Status Code: 200
Response Body: [{"id":1,"title":"Activity 1",...}]
========================
```

## 🧭 Design Decisions

1. **HttpClient via DI** – Using `IHttpClientFactory` for proper HttpClient lifecycle management  
2. **TestLogger** – Custom logger to capture all assertion failure details  
3. **Theory Tests** – Using xUnit Theory for parameterized tests (Books GET 1–10)  
4. **Async/Await** – All API calls are asynchronous for better performance  
5. **Tuple Returns** – API client methods return tuples with data, status code, and message for comprehensive validation  
6. **BaseTest Class** – Abstract base class implementing `IClassFixture<TestFixture>` for DI support  
7. **ApiClientHelper** – Extension methods for reusable HTTP operations (GET, POST, PUT, DELETE)  
8. **Test Steps Pattern** – `TestApiTestSteps` provides a clean layer between tests and API client  
9. **URL Constants** – Centralized endpoint definitions in `UrlSegments` class
