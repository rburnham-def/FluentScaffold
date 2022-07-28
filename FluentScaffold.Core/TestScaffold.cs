using Microsoft.Extensions.DependencyInjection;

namespace FluentScaffold.Core;


public class TestScaffold : ServiceCollection
{

    public ServiceProvider ServiceProvider { get; private set; }

    public TestScaffold()
    {
        ServiceProvider = ServiceCollectionContainerBuilderExtensions.BuildServiceProvider(this);
    }
    public void BuildServiceProvider()
    {
        // Build the IoC and get a provider
        ServiceProvider = ServiceCollectionContainerBuilderExtensions.BuildServiceProvider(this);
    }
}