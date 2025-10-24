using Xunit.Abstractions;
using Xunit.Sdk;

namespace RecruitmentProject.Test.Helpers;

public class TestLogger(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    public void AssertWithLogging(
        Action assertion,
        string endpoint,
        object? requestBody,
        int statusCode,
        string? responseMessage,
        object? expectedResponse,
        object? actualResponse)
    {
        try
        {
            assertion();
        }
        catch (XunitException)
        {
            LogAssertionFailure(endpoint, requestBody, statusCode, responseMessage, expectedResponse, actualResponse);
            throw;
        }
    }

    public void AssertWithLogging(
        Action assertion,
        string endpoint,
        string field,
        object? expectedValue,
        object? actualValue,
        int statusCode,
        string? responseBody = null)
    {
        try
        {
            assertion();
        }
        catch (XunitException)
        {
            LogAssertionFailure(endpoint, field, expectedValue, actualValue, statusCode, responseBody);
            throw;
        }
    }

    private void LogAssertionFailure(
        string endpoint,
        object? requestBody,
        int statusCode,
        string? responseMessage,
        object? expectedResponse,
        object? actualResponse)
    {
        _output.WriteLine("=== ASSERTION FAILURE ===");
        _output.WriteLine($"Endpoint: {endpoint}");
        _output.WriteLine($"Request Body: {System.Text.Json.JsonSerializer.Serialize(requestBody)}");
        _output.WriteLine($"Status Code: {statusCode}");
        _output.WriteLine($"Response Message: {responseMessage}");
        _output.WriteLine($"Expected Response: {System.Text.Json.JsonSerializer.Serialize(expectedResponse)}");
        _output.WriteLine($"Actual Response: {System.Text.Json.JsonSerializer.Serialize(actualResponse)}");
        _output.WriteLine("========================");
    }

    private void LogAssertionFailure(
        string endpoint,
        string field,
        object? expectedValue,
        object? actualValue,
        int statusCode,
        string? responseBody = null)
    {
        _output.WriteLine("=== ASSERTION FAILURE ===");
        _output.WriteLine($"Endpoint: {endpoint}");
        _output.WriteLine($"Field: {field}");
        _output.WriteLine($"Expected: {expectedValue}");
        _output.WriteLine($"Actual: {actualValue}");
        _output.WriteLine($"Status Code: {statusCode}");
        if (!string.IsNullOrEmpty(responseBody))
        {
            _output.WriteLine($"Response Body: {responseBody}");
        }
        _output.WriteLine("========================");
    }    
}
