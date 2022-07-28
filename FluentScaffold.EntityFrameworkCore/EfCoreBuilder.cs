using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FluentScaffold.Core;

public class EfCoreBuilder<TDbContext>: Builder where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public EfCoreBuilder(TestScaffold testScaffold) : base(testScaffold)
    {
        _dbContext = testScaffold.ServiceProvider.GetRequiredService<TDbContext>();
    }

    public EfCoreBuilder<TDbContext> With<TEntity>(TEntity entity)
    {
        if (entity != null)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        return this;
    }
}