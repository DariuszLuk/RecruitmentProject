using Microsoft.Extensions.DependencyInjection;
using RecruitmentProject.Test.Helpers;
using RecruitmentProject.Test.Setup;
using RecruitmentProject.Test.TestApi.Models;
using RecruitmentProject.TestApi;
using System.Text.Json;
using Xunit.Abstractions;

namespace RecruitmentProject.Test.Tests;

public class BooksTests : BaseTest
{
    private readonly TestApiTestSteps _testApiTestSteps;
    private readonly TestLogger _logger;

    public BooksTests(TestFixture fixture, ITestOutputHelper output) : base(fixture)
    {
        _testApiTestSteps = Services.GetRequiredService<TestApiTestSteps>();
        _logger = new TestLogger(output);
    }

    [Fact]
    public async Task PutBook_ShouldReturnSameBodyAsRequest()
    {
        // Arrange       
        var bookId = 1;
        var requestBody = new Book
        {
            Id = bookId,
            Title = "Updated Book Title",
            Description = "Updated Description",
            PageCount = 500,
            Excerpt = "Updated Excerpt",
            PublishDate = DateTime.UtcNow
        };

        // Act
        var (data, statusCode, responseMessage, endpoint) = await _testApiTestSteps.PutBookStep(bookId, requestBody);

        // Assert
        _logger.AssertWithLogging(() => Assert.Equal(200, statusCode), endpoint, requestBody, statusCode, responseMessage, requestBody, data);
        _logger.AssertWithLogging(() => Assert.NotNull(data), endpoint, "Updated Book", "Not Null", statusCode, statusCode, JsonSerializer.Serialize(data));
        _logger.AssertWithLogging(() => Assert.Equal(requestBody.Id, data.Id), endpoint, "Id", requestBody.Id, data.Id, statusCode, JsonSerializer.Serialize(data));
        _logger.AssertWithLogging(() => Assert.Equal(requestBody.Title, data.Title), endpoint, "Title", requestBody.Title, data.Title, statusCode, JsonSerializer.Serialize(data));
        _logger.AssertWithLogging(() => Assert.Equal(requestBody.Description, data.Description), endpoint, "Description", requestBody.Description, data.Description, statusCode, JsonSerializer.Serialize(data));
        _logger.AssertWithLogging(() => Assert.Equal(requestBody.PageCount, data.PageCount), endpoint, "PageCount", requestBody.PageCount, data.PageCount, statusCode, JsonSerializer.Serialize(data));
        _logger.AssertWithLogging(() => Assert.Equal(requestBody.Excerpt, data.Excerpt), endpoint, "Excerpt", requestBody.Excerpt, data.Excerpt, statusCode, JsonSerializer.Serialize(data));
    }

    [Theory]
    [InlineData(1, 100)]
    [InlineData(2, 200)]
    [InlineData(3, 300)]
    [InlineData(4, 400)]
    [InlineData(5, 500)]
    [InlineData(6, 600)]
    [InlineData(7, 700)]
    [InlineData(8, 800)]
    [InlineData(9, 900)]
    [InlineData(10, 1000)]
    public async Task GetBook_ShouldReturnCorrectIdAndPageCount(int id, int expectedPageCount)
    {
        // Act
        var (data, statusCode, responseMessage, endpoint) = await _testApiTestSteps.GetBookStep(id);

        // Assert - Id in response matches request
        _logger.AssertWithLogging(() => Assert.Equal(id, data!.Id), endpoint, "Id", id, data!.Id, statusCode, JsonSerializer.Serialize(data));

        // Assert - PageCount matches expected value
        _logger.AssertWithLogging(() => Assert.Equal(expectedPageCount, data.PageCount), endpoint, "PageCount", expectedPageCount, data.PageCount, statusCode, JsonSerializer.Serialize(data));
    }
}
