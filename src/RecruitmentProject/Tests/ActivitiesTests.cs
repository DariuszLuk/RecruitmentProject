using Microsoft.Extensions.DependencyInjection;
using RecruitmentProject.Test.Helpers;
using RecruitmentProject.Test.Setup;
using RecruitmentProject.TestApi;
using Xunit.Abstractions;

namespace RecruitmentProject.Test.Tests;

public class ActivitiesTests : BaseTest
{
    private readonly TestApiTestSteps _testApiTestCase;
    private readonly TestLogger _logger;
    
    public ActivitiesTests(TestFixture fixture, ITestOutputHelper output) : base(fixture)
    {
        _testApiTestCase = Services.GetRequiredService<TestApiTestSteps>();
        _logger = new TestLogger(output);
    }

    [Fact]
    public async Task GetActivities_ShouldReturn30Activities_AndNoDueDateYesterday()
    {
        // Arrange
        var yesterday = DateTime.UtcNow.Date.AddDays(-1);

        // Act
        var (data, statusCode, responseMessage, endpoint) = await _testApiTestCase.GetActivitiesStep();

        // Assert
        _logger.AssertWithLogging(() => Assert.Equal(200, statusCode), endpoint, "Status Code", 200, statusCode, statusCode, responseMessage);
        _logger.AssertWithLogging(() => Assert.NotNull(data), endpoint, "Data Not Null", "Not Null", data != null ? "Not Null" : "Null", statusCode, System.Text.Json.JsonSerializer.Serialize(data));
        _logger.AssertWithLogging(() => Assert.Equal(30, data.Count), endpoint, "Activity Count", 30, data.Count, statusCode, System.Text.Json.JsonSerializer.Serialize(data));

        // Assert - No activity with dueDate = yesterday
        var activitiesWithYesterdayDueDate = data
            .Where(a => a.DueDate.Date == yesterday)
            .ToList();

        _logger.AssertWithLogging(() => Assert.Empty(activitiesWithYesterdayDueDate), endpoint, "Activities with yesterday's due date", 0, activitiesWithYesterdayDueDate.Count, statusCode, System.Text.Json.JsonSerializer.Serialize(activitiesWithYesterdayDueDate));
    }
}
