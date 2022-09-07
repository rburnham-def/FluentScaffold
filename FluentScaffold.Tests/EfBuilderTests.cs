using System;
using System.Linq;
using NUnit.Framework;
using FluentScaffold.Core;
using FluentScaffold.Tests.ApplicationUnderTest;
using FluentScaffold.Tests.ApplicationUnderTest.Data;

namespace FluentScaffold.Tests;

[TestFixture]
public class EfBuilderTests
{
    [Test]
    public void EFBuilder_Can_Access_User_From_TestScaffolds_DbContext()
    {
        using var dbContext = TestDbContextFactory.Create();
        var userId = Guid.Parse("65579043-8112-480C-A885-C6157947F0F3");
        new TestScaffold()
            .EfCoreBuilder(dbContext)
            .With(new User(
                id:userId,
                email : "Bob@test.com",
                password: "",
                name : "Bob", 
                dateOfBirth: DateOnly.FromDateTime(DateTime.Now.AddYears(-15))
            ))
            .Build();

        var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
        Assert.IsNotNull(user);
        Assert.IsTrue(user?.Id == userId);
    }

    [Test]
    public void EBBuilder_Can_Extend_EfBuilder()
    {   
        using var dbContext = TestDbContextFactory.Create();
        var userId = Guid.Parse("36A6736A-F8AC-4FA2-B33E-0ACB14776C0F");
        new TestScaffold()
            .EfCoreBuilder(dbContext)
            .WithDefaultCatalogue()
            .With(new User(
                id:userId,
                email : "Bob@test.com",
                password: "",
                name : "Bob", 
                dateOfBirth: DateOnly.FromDateTime(DateTime.Now.AddYears(-15))
            ))
            .WithShoppingCart(userId)
            .Build();

        var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
        var shopping = dbContext.ShoppingCart.FirstOrDefault(s => s.User.Id == userId);
        var items = dbContext.Items.ToList();
        
        Assert.Multiple(() =>
        {
            //User Added to dbContext
            Assert.IsNotNull(user);
            Console.WriteLine($"Matched User:{user?.Id}, Expected User: {userId}");
            Assert.IsTrue(user?.Id == userId, "User not found");

            //Shopping Cart Added to DBContext
            Assert.IsNotNull(shopping);
            
            //Default items added to DBContext
            Assert.IsTrue(items.Count == 3, "Item count did not match");
        });
    }

    [Test]
    public void EBBuilder_Throw_Exception_When_Not_Initialized()
    {
        Assert.Catch<InvalidOperationException>(() =>
        {
            new TestScaffold()
                .EfCoreBuilder<TestDbContext>()
                .Build();
        });
    }
    
    
    [Test]
    public void EBBuilder_Can_Defer_Adding_To_DbContext()
    {
        using var dbContext = TestDbContextFactory.Create();
        var testScaffold = new TestScaffold()
                .EfCoreBuilder(dbContext)
                .Build();

        testScaffold.EfCoreBuilder<TestDbContext>()
            .WithDefaultCatalogue()
            .Build();

        var userId = Guid.NewGuid();
        testScaffold.EfCoreBuilder<TestDbContext>()
            .With(new User(userId, "Bob", "bob@test.com", "SuperSecret123", DateOnly.FromDateTime(DateTime.Now.AddYears(-12))))
            .Build();

        var hasItem = dbContext.Items.Any(i => i.Title == Defaults.CatalogueItems.Avengers);
        var hasUser = dbContext.Users.Any(i => i.Id == userId);
        Assert.Multiple(() =>
        {
            Assert.IsTrue(hasItem, "DbContext was not seeded with deferred builder");
            Assert.IsTrue(hasUser, "DbContext was not seeded with second call to deferred builder");
        });

    }
}