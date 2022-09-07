using System;
using System.Collections.Generic;
using FluentScaffold.Tests.ApplicationUnderTest;
using FluentScaffold.Tests.ApplicationUnderTest.Data;
using Microsoft.EntityFrameworkCore;


// ReSharper disable once CheckNamespace
namespace FluentScaffold.Core;

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
            new() { Id = Guid.NewGuid(), Title = Defaults.CatalogueItems.Minions, Price = 21},
            new() { Id = Guid.NewGuid(), Title = Defaults.CatalogueItems.Avengers, Price = 24},
            new() { Id = Guid.NewGuid(), Title = Defaults.CatalogueItems.DeadPool, Price = 14, AgeRestriction = 15}
        });
        
        return builder;
    }
}