using RecruitmentProject.Test.Helpers;
using RecruitmentProject.Test.TestApi;
using RecruitmentProject.Test.TestApi.Models;

namespace RecruitmentProject.TestApi;

public class TestClientApi
{
    private readonly HttpClient _httpClient;

    public TestClientApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }  

    public async Task<(Book? book, int StatusCode, string? ResponseMessage, string endpoint)> GetBook(int id)
    {
        var endpoint = $"{UrlSegments.Books}{id}";
        var (book, statusCode, responseMessage) = await _httpClient.Get<Book>(endpoint);
      return (book, statusCode, responseMessage, endpoint);
    }

    public async Task<(List<Activity>? activities, int StatusCode, string? ResponseMessage, string endpoint)> GetActivities()
    {
        var endpoint = UrlSegments.Activities;
        var (activities, statusCode, responseMessage) = await _httpClient.Get<List<Activity>>(endpoint);
        return (activities, statusCode, responseMessage, endpoint);
    }

    public async Task<(Author? author, int StatusCode, string? ResponseMessage, string endpoint)> PostAuthor(Author requestBody)
    {
        var endpoint = UrlSegments.Authors;
     var (author, statusCode, responseMessage) = await _httpClient.Post<Author>(endpoint, requestBody);
        return (author, statusCode, responseMessage, endpoint);
    }

    public async Task<(Book? book, int StatusCode, string? ResponseMessage, string endpoint)> PutBook(int id, Book requestBody)
    {
        var endpoint = $"{UrlSegments.Books}{id}";
        var (book, statusCode, responseMessage) = await _httpClient.Put<Book>(endpoint, requestBody);
        return (book, statusCode, responseMessage, endpoint);
    }

    public async Task<(bool Success, int StatusCode, string? ResponseMessage, string endpoint)> DeleteAuthor(int id)
    {
        var endpoint = $"{UrlSegments.Authors}{id}";
        var (success, statusCode, responseMessage) = await _httpClient.Delete(endpoint);
      return (success, statusCode, responseMessage, endpoint);
    }
}
