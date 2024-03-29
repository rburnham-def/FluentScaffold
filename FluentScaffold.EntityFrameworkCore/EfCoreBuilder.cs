using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace FluentScaffold.Core;

public class EfCoreBuilder<TDbContext>: Builder where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public EfCoreBuilder(TestScaffold testScaffold) : base(testScaffold)
    {
        _dbContext = testScaffold.Resolve<TDbContext>();
    }

    public TDbContext GetDbContext()
    {
        return _dbContext;
    }

    public EfCoreBuilder<TDbContext> With<TEntity>(TEntity entity)
    {
        Enqueue(() =>
        {
            if (entity == null) return;
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        });

        return this;
    }
    
    public EfCoreBuilder<TDbContext> WithRange<T>(IEnumerable<T> entities)
    {
        Enqueue(() =>
        {
            _dbContext.AddRange((IEnumerable<object>)entities);
            _dbContext.SaveChanges();
        });
        return this;
    }
}