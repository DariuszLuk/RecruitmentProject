using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecruitmentProject.Test.Setup.Configuration;
using RecruitmentProject.TestApi;

namespace RecruitmentProject.Test.Setup;

public static class SetupForAllTests
{
    public static IServiceCollection AddTestApi(this IServiceCollection services)
    {
        services.AddHttpClient<TestClientApi>((serviceProvider, client) =>
        {
            var apiSettings = serviceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;
            client.BaseAddress = new Uri(apiSettings.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        return services;
    }

    public static IServiceCollection AddTestSteps(this IServiceCollection services) =>
      services
        .AddScoped<TestApiTestSteps>();
}
