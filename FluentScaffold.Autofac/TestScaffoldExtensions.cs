using FluentScaffold.Autofac;

// ReSharper disable once CheckNamespace
namespace FluentScaffold.Core;

public static class TestScaffoldExtensions  
{
    public static AutofacBuilder WithAutofacBuilder(this TestScaffold testScaffold)
    {
        testScaffold.BuildServiceProvider();
        return new AutofacBuilder(testScaffold);
    }
}