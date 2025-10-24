using Microsoft.Extensions.DependencyInjection;
using RecruitmentProject.Test.Helpers;
using RecruitmentProject.Test.Setup;
using RecruitmentProject.Test.TestApi.Models;
using RecruitmentProject.TestApi;
using System.Text.Json;
using Xunit.Abstractions;

namespace RecruitmentProject.Test.Tests;

public class AuthorsTests : BaseTest
{
    private readonly TestApiTestSteps _testApiTestCase;
    private readonly TestLogger _logger;

    public AuthorsTests(TestFixture fixture, ITestOutputHelper output) : base(fixture)
    {
        _testApiTestCase = Services.GetRequiredService<TestApiTestSteps>();
        _logger = new TestLogger(output);
    }

    [Fact]
    public async Task PostAuthor_ShouldReturnSameBodyAsRequest()
    {
        // Arrange
        var requestBody = new Author
        {
            Id = 0,
            IdBook = 1,
            FirstName = "John",
            LastName = "Doe"
        };

        // Act
        var (data, statusCode, responseMessage, endpoint) = await _testApiTestCase.PostAuthorStep(requestBody);

        // Assert
        _logger.AssertWithLogging(() => Assert.Equal(200, statusCode), endpoint, "Status Code", 200, statusCode, statusCode, JsonSerializer.Serialize(data));
        _logger.AssertWithLogging(() => Assert.NotNull(data), endpoint, "Created Author", "Not Null", statusCode, statusCode, JsonSerializer.Serialize(data));

        _logger.AssertWithLogging(() => Assert.Equal(requestBody.IdBook, data.IdBook), endpoint, nameof(requestBody.IdBook), requestBody.IdBook, data.IdBook, statusCode, JsonSerializer.Serialize(data));
        _logger.AssertWithLogging(() => Assert.Equal(requestBody.FirstName, data.FirstName), endpoint, nameof(requestBody.FirstName), requestBody.FirstName, data.FirstName, statusCode, JsonSerializer.Serialize(data));
        _logger.AssertWithLogging(() => Assert.Equal(requestBody.LastName, data.LastName), endpoint, nameof(requestBody.LastName), requestBody.LastName, data.LastName, statusCode, JsonSerializer.Serialize(data));
    }

    [Fact]
    public async Task DeleteAuthor_ShouldBeSuccessful()
    {
        // Arrange
        var requestBody = new Author
        {
            Id = 0,
            IdBook = 1,
            FirstName = "Jane",
            LastName = "Smith"
        };

        //Act
        var (createdAuthor, createStatusCode, createResponseMessage, createEndpoint) = await _testApiTestCase.PostAuthorStep(requestBody);

        _logger.AssertWithLogging(() => Assert.Equal(200, createStatusCode), createEndpoint, "Status Code", 200, createStatusCode, createStatusCode, JsonSerializer.Serialize(createdAuthor));
        _logger.AssertWithLogging(() => Assert.NotNull(createdAuthor), createEndpoint, "Created Author", "Not Null", createdAuthor, createStatusCode, JsonSerializer.Serialize(createdAuthor));

        // Act - Delete the created author
        var (success, deleteStatusCode, deleteResponseMessage, deleteEndpoint) = await _testApiTestCase.DeleteAuthorStep(createdAuthor!.Id);

        // Assert - Delete successful
        _logger.AssertWithLogging(() => Assert.Equal(200, deleteStatusCode), deleteEndpoint, "Status Code", 200, deleteStatusCode, deleteStatusCode, JsonSerializer.Serialize(success));
        _logger.AssertWithLogging(() => Assert.True(success), deleteEndpoint, "Delete Success", true, success, deleteStatusCode, JsonSerializer.Serialize(success));
    }
}
