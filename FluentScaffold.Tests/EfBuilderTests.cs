using System;
using System.Linq;
using NUnit.Framework;
using FluentScaffold.Core;
using FluentScaffold.Tests.ApplicationUnderTest.Data;
using FluentScaffold.Tests.CustomBuilder;

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
        var userId = Guid.Parse("65579043-8112-480C-A885-C6157947F0F3");
        new TestScaffold()
            .EfCoreBuilder(dbContext)
            .WithDefaultCatalogue(Guid.NewGuid())
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
            Assert.IsTrue(user?.Id == userId);

            //Shopping Cart Added to DBContext
            Assert.IsNotNull(shopping);
            
            //Default items added to DBContext
            Assert.IsTrue(items.Count == 3);
        });
    }
}