using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FluentScaffold.Tests.TestData;

public class User
{
    [Key]
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public string? Email { get; set; }
    
}

public class Item
{
    [Key]
    public Guid Id { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}

public class ShoppingCart
{
    [Key]
    public Guid Id { get; set; }

    public User User { get; set; }
    public List<Item> Inventory { get; set; }
}


public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ShoppingCart> ShoppingCart { get; set; }
} 