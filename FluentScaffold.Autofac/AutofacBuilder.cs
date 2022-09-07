using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentScaffold.Core;

namespace FluentScaffold.Autofac;

public class AutofacBuilder: Builder
{
    private readonly ContainerBuilder _containerBuilder;

    public AutofacBuilder(TestScaffold testScaffold) : base(testScaffold)
    {
        // create the Autofac container builder
        _containerBuilder = new ContainerBuilder();
        
        _containerBuilder.Populate(testScaffold.ServiceCollection);
    }
    
    /// <summary>
    /// Provides access to the Container Builder for more complex scenarios.
    /// </summary>
    /// <param name="builderFunc">An action that takes a Autofac ContainerBuilder</param>
    /// <returns></returns>
    public AutofacBuilder WithContainerBuilder(Action<ContainerBuilder> builderFunc)
    {
        Enqueue(() => builderFunc(_containerBuilder));
        return this;
    }
    
    public AutofacBuilder WithType<T>() where T : class
    {
        Enqueue(() => _containerBuilder.RegisterType<T>());
        return this;
    }
    
    public AutofacBuilder WithTypeAs<T, TInterface>() where TInterface : notnull where T : notnull
    {
        Enqueue(() => _containerBuilder.RegisterType<T>().As<TInterface>());
        return this;
    }

    
    public AutofacBuilder WithInstance<T>(T service) where T : class
    {
        Enqueue(() => _containerBuilder.RegisterInstance(service));
        return this;
    }
    
    public AutofacBuilder WithInstanceAs<T, TInterface>(T service) where T : class where TInterface : notnull
    {
        Enqueue(() => _containerBuilder.RegisterInstance(service).As<TInterface>());
        return this;
    }
    
    
    public AutofacBuilder WithSingletonType<T>() where T : class
    {
        Enqueue(() => _containerBuilder.RegisterType<T>().SingleInstance().InstancePerLifetimeScope());
        return this;
    }
    
    public AutofacBuilder WithSingletonTypeAs<T, TInterface>() where T : class where TInterface : notnull
    {
        Enqueue(() => _containerBuilder.RegisterType<T>().SingleInstance().As<TInterface>().InstancePerLifetimeScope());
        return this;
    }

    public AutofacBuilder WithSingletonAs<T, TInterface>(T service) where T : class where TInterface : notnull
    {
        Enqueue(() => _containerBuilder.RegisterInstance(service).As<TInterface>().SingleInstance());
        return this;
    }
    


    public override TestScaffold Build()
    {
        var testScaffold = base.Build();

        var container = _containerBuilder.Build();
        var serviceProvider = new AutofacServiceProvider(container);
        
        testScaffold.SetServiceProvider(serviceProvider);
        
        return testScaffold;
    }
}