using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecruitmentProject.Test.Setup.Configuration;

namespace RecruitmentProject.Test.Setup;

public class TestFixture : IDisposable
{
    public IServiceProvider ServiceProvider { get; }
    public IConfiguration Configuration { get; }

    public TestFixture()
    {      
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        var services = new ServiceCollection();
        
        services.Configure<ApiSettings>(Configuration.GetSection(nameof(ApiSettings)));
      
        services
            .AddTestApi()
            .AddTestSteps();

        ServiceProvider = services.BuildServiceProvider();
    }
   
    public void Dispose()
    {
        if (ServiceProvider is IDisposable disposable)
        {
            disposable.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
