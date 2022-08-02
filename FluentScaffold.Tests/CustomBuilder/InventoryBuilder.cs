using System;
using System.Collections.Generic;
using FluentScaffold.Core;
using FluentScaffold.Tests.ApplicationUnderTest.Data;
using Microsoft.EntityFrameworkCore;

namespace FluentScaffold.Tests.CustomBuilder;

public static class InventoryBuilderExtensions
{
    /// <summary>
    /// Adds a Shopping cart for the User
    /// </summary>
    public static EfCoreBuilder<TDbContext> WithShoppingCart<TDbContext>(
        this EfCoreBuilder<TDbContext> builder,  Guid userId) where TDbContext: DbContext
    {
        builder.With(new ShoppingCart()
        {
            Id = Guid.NewGuid(),
            UserId = userId
        });
        
        return builder;
    }
    
    /// <summary>
    /// Adds a set of sample Items to the DBContext
    /// </summary>
    public static EfCoreBuilder<TDbContext> WithDefaultCatalogue<TDbContext>(
        this EfCoreBuilder<TDbContext> builder) where TDbContext: DbContext
    {
        builder.WithRange(new List<Item>()
        {
            new() { Id = Guid.NewGuid(), Title = "Minions", Price = 21},
            new() { Id = Guid.NewGuid(), Title = "Avengers", Price = 24},
            new() { Id = Guid.NewGuid(), Title = "Minions", Price = 14, AgeRestriction = 15}
        });
        
        return builder;
    }
}