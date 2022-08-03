
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace FluentScaffold.Core;

public static class TestScaffoldExtensions  
{
    public static EfCoreBuilder<T> WithEfCoreBuilder<T>(this TestScaffold testScaffold, T dbContext) where T: DbContext
    {
        testScaffold.AddSingleton(_ => dbContext);
        testScaffold.BuildServiceProvider();

        return new EfCoreBuilder<T>(testScaffold);
    }
}