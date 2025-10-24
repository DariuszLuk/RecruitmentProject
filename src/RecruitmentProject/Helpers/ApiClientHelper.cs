using System.Net.Http.Json;

namespace RecruitmentProject.Test.Helpers;

public static class ApiClientHelper
{
    private static async Task<(T? Data, int StatusCode, string? ResponseMessage)> ProcessResponseAsync<T>(HttpResponseMessage response)
    {
        var statusCode = (int)response.StatusCode;
        var responseMessage = response.ReasonPhrase;

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<T>();
            return (data, statusCode, responseMessage);
        }

        return (default, statusCode, responseMessage);
    }

    public static async Task<(T? Data, int StatusCode, string? ResponseMessage)> Get<T>(this HttpClient httpClient, string endpoint)
    {
        var response = await httpClient.GetAsync(endpoint);
        return await ProcessResponseAsync<T>(response);
    }

    public static async Task<(T? Data, int StatusCode, string? ResponseMessage)> Post<T>(this HttpClient httpClient, string endpoint, object requestBody)
    {
        var response = await httpClient.PostAsJsonAsync(endpoint, requestBody);
        return await ProcessResponseAsync<T>(response);
    }

    public static async Task<(T? Data, int StatusCode, string? ResponseMessage)> Put<T>(this HttpClient httpClient, string endpoint, object requestBody)
    {
        var response = await httpClient.PutAsJsonAsync(endpoint, requestBody);
        return await ProcessResponseAsync<T>(response);
    }

    public static async Task<(bool Success, int StatusCode, string? ResponseMessage)> Delete(this HttpClient httpClient, string endpoint)
    {
        var response = await httpClient.DeleteAsync(endpoint);
        var statusCode = (int)response.StatusCode;
        var responseMessage = response.ReasonPhrase;

        return (response.IsSuccessStatusCode, statusCode, responseMessage);
    }
}
