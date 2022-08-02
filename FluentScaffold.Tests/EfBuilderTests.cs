using System;
using System.Linq;
using NUnit.Framework;
using FluentScaffold.Core;
using FluentScaffold.Tests.ApplicationUnderTest.Data;
using FluentScaffold.Tests.CustomBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FluentScaffold.Tests;

[TestFixture]
public class EfBuilderTests
{
    private TestDbContext _dbContext;

    [SetUp]
    public void Setup()
    {
        var contextOptions = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase("TestDbContext")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        
        _dbContext = new TestDbContext(contextOptions);
        

    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }

    [Test]
    public void EFBuilder_Can_Access_User_From_TestScaffolds_DbContext()
    {
        var userId = Guid.Parse("65579043-8112-480C-A885-C6157947F0F3");
        new TestScaffold()
            .BuildDbContext(_dbContext)
            .With(new User(
                id:userId,
                email : "Bob@test.com",
                password: "",
                name : "Bob", 
                dateOfBirth: DateOnly.FromDateTime(DateTime.Now.AddYears(-15))
            ));

        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
        Assert.IsNotNull(user);
        Assert.IsTrue(user?.Id == userId);
    }

    [Test]
    public void EBBuilder_Can_Extend_EfBuilder()
    {   
        var userId = Guid.Parse("65579043-8112-480C-A885-C6157947F0F3");
        new TestScaffold()
            .BuildDbContext(_dbContext)
            .WithDefaultCatalogue()
            .With(new User(
                id:userId,
                email : "Bob@test.com",
                password: "",
                name : "Bob", 
                dateOfBirth: DateOnly.FromDateTime(DateTime.Now.AddYears(-15))
            ))
            .WithShoppingCart(userId);

        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
        var shopping = _dbContext.ShoppingCart.FirstOrDefault(s => s.User.Id == userId);
        var items = _dbContext.Items.ToList();
        
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