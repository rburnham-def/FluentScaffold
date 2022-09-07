using System;
using System.Linq;
using Autofac;
using FluentScaffold.Core;
using FluentScaffold.Tests.ApplicationUnderTest.Data;
using FluentScaffold.Tests.ApplicationUnderTest.Services;
using FluentScaffold.Tests.CustomBuilder;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FluentScaffold.Tests;

[TestFixture]
public class IntegrationTest
{

    /// <summary>
    /// Example of a more complicated Component Integration Test.
    /// ShoppingCartService depends on UserContext which depends on AuthService to resolve the user. 
    /// -- Setup DB Structure with Custom EFBuilder
    /// -- Setup Dependant AuthServices
    /// -- Setup Ioc & Dependant Services
    /// -- Attempt Purchase of age restricted content
    /// </summary>
    [Test]
    public void ComponentIntegrationTest_UserCanAddToCart()
    {
        using var dbContext = TestDbContextFactory.Create();

        var email= "Jim@test.com";
        var password = "SupperSecretPa$$word";
        
        // Arrange
        var userId = Guid.Parse("A5A743C3-A02F-4CA3-94F8-B0ECAF4A6345");
        var itemId = Guid.Parse("7ED3A7D5-8A69-485E-87E5-AE0D9E1BB470");
        var testScaffold = new TestScaffold()
            .EfCoreBuilder(dbContext)
            .WithDefaultCatalogue(itemId)
            .With(new User(
                id: userId,
                email: email,
                password: password,
                name: "Jimmy",
                dateOfBirth: DateOnly.FromDateTime(DateTime.Now.AddYears(-8))
            ))
            .WithShoppingCart(userId)
            .Build()
            .AutofacBuilder()
            .WithSingletonTypeAs<AuthService, IAuthService>()
            .WithContainerBuilder(builder =>
                builder.Register(c =>
                    {
                        // Mock an Authenticated User Context
                        return new UserContext(c.Resolve<IAuthService>(), email, password);
                    })
                    .As<IUserContext>()
                    .InstancePerLifetimeScope()
            )
            .WithType<ShoppingCartService>()
            .Build();
        
        

        // Act
        var shoppingCartService = testScaffold.Resolve<ShoppingCartService>();
        shoppingCartService.AddItemToCart(itemId);

        // Assert 
        var cart = dbContext.ShoppingCart.Include(s => s.Inventory).FirstOrDefault(u => u.UserId == userId);
        Assert.IsTrue(cart?.Inventory.Any(i => i.Id == itemId));
    }
}