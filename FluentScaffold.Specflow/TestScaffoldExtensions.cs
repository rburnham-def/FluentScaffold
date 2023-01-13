using FluentScaffold.Specflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace FluentScaffold.Core;

public static class TestScaffoldExtensions  
{
    public static SpecflowBuilder WithSpecflow(this TestScaffold testScaffold, Func<SpecflowBuilderConfig, SpecflowBuilderConfig> configure)
    {
        var config = new SpecflowBuilderConfig();
        testScaffold.ServiceCollection.AddSingleton(_ => configure(config));
        return new SpecflowBuilder(testScaffold);
    }
}