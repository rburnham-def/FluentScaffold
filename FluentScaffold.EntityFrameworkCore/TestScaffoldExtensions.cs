
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace FluentScaffold.Core;

public static class TestScaffoldExtensions  
{
    public static EfCoreBuilder<T> EfCoreBuilder<T>(this TestScaffold testScaffold, T dbContext) where T: DbContext
    {
        testScaffold.ServiceCollection.AddSingleton(_ => dbContext);
        testScaffold.BuildServiceProvider();

        return new EfCoreBuilder<T>(testScaffold);
    }
}