using System;
using System.Linq;
using FluentScaffold.Core;
using FluentScaffold.Tests.ApplicationUnderTest.Data;
using FluentScaffold.Tests.CustomBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NUnit.Framework;

namespace FluentScaffold.Tests;



[TestFixture]
public class IntegrationTest
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

    /// <summary>
    /// Example of a more complicate Component Integration Test
    /// -- Setup DB Structure with Custom EFBuilder
    /// -- Setup Dependant AuthServices
    /// -- Setup Ioc & Dependant Services
    /// -- Attempt Purchase of age restricted content
    /// </summary>
    [Test]
    [Ignore("Incomplete Implementation")]
    public void ComponentIntegrationTest_UserCanAddToCart()
    {
        // Arrange
        var userId = Guid.Parse("65579043-8112-480C-A885-C6157947F0F3");
        new TestScaffold()
            .BuildDbContext(_dbContext)
            .WithDefaultCatalogue()
            .With(new User(
                id:userId,
                email : "Jim@test.com",
                password: "SupperSecretPa$$word",
                name : "Jimmy", 
                dateOfBirth: DateOnly.FromDateTime(DateTime.Now.AddYears(-8))
            ))
            .WithShoppingCart(userId);

        // Act
        // TODO add IocBuilder
        // Assert 
    }
}