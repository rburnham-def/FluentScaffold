using Microsoft.Extensions.DependencyInjection;

namespace FluentScaffold.Core;


public class TestScaffold
{

    public IServiceCollection ServiceCollection { get; }
    public IServiceProvider? ServiceProvider { get; private set; }

    public TestScaffold()
    {
        ServiceCollection = new ServiceCollection();
        BuildServiceProvider();
    }
    
    /// <summary>
    /// Builds the default .net Service Provider. 
    /// </summary>
    public void BuildServiceProvider()
    {
        // Build the IoC and get a provider
        ServiceProvider = ServiceCollection.BuildServiceProvider();
        
    }

    /// <summary>
    /// Allows providing a custom Service Provider to be used . 
    /// </summary>
    /// <param name="serviceProvider">Custom implementation of IServiceProvider</param>
    public void SetServiceProvider(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }


    /// <summary>
    /// Helper method to resolve a type from ServiceProvider.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public T Resolve<T>()
    {
        if (ServiceProvider == null) throw new NullReferenceException("Service Provider must be built");
        return ServiceProvider.GetRequiredService<T>();
    }
}