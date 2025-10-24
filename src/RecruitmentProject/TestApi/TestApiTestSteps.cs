using Microsoft.Extensions.Options;
using RecruitmentProject.Test.Setup.Configuration;
using RecruitmentProject.Test.TestApi.Models;

namespace RecruitmentProject.TestApi;

public class TestApiTestSteps
{
    private readonly ApiSettings _apiSettings;
    private readonly TestClientApi _testClientApi;

    public TestApiTestSteps(IOptions<ApiSettings> apiSettings, TestClientApi testClientApi)
    {
        _apiSettings = apiSettings.Value;
        _testClientApi = testClientApi;
    }
  
    public async Task<(Book? book, int StatusCode, string? ResponseMessage, string endpoint)> GetBookStep(int id) => await _testClientApi.GetBook(id);
    public async Task<(Book? book, int StatusCode, string? ResponseMessage, string endpoint)> PutBookStep(int id, Book requestBody) => await _testClientApi.PutBook(id, requestBody);

    public async Task<(List<Activity>? activities, int StatusCode, string? ResponseMessage, string endpoint)> GetActivitiesStep() => await _testClientApi.GetActivities();

    public async Task<(Author? author, int StatusCode, string? ResponseMessage, string endpoint)> PostAuthorStep(Author requestBody) => await _testClientApi.PostAuthor(requestBody);

    public async Task<(bool success, int StatusCode, string? ResponseMessage, string endpoint)> DeleteAuthorStep(int id) => await _testClientApi.DeleteAuthor(id);    
}
