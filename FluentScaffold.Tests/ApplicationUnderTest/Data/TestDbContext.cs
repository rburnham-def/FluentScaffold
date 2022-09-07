using Microsoft.EntityFrameworkCore;

namespace FluentScaffold.Tests.ApplicationUnderTest.Data;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<ShoppingCart> ShoppingCart { get; set; } = null!;
}