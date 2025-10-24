using RecruitmentProject.Test.Setup;

namespace RecruitmentProject.Test;

public abstract class BaseTest : IClassFixture<TestFixture>
{    
    protected readonly IServiceProvider Services;

    protected BaseTest(TestFixture fixture)
    {       
        Services = fixture.ServiceProvider;
    }
}