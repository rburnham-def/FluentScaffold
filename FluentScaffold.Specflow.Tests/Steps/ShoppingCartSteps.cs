using System.Linq;
using Autofac;
using FluentScaffold.Core;
using FluentScaffold.Tests;
using FluentScaffold.Tests.ApplicationUnderTest;
using FluentScaffold.Tests.ApplicationUnderTest.Data;
using FluentScaffold.Tests.ApplicationUnderTest.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FluentScaffold.Specflow.Tests.Steps;

[Binding]
public class ShoppingCartSteps
{
    private ScenarioContext _scenarioContext;

    public ShoppingCartSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public void SetupTestUsers()
    {
        var dbContext = TestDbContextFactory.Create();
        var testScaffold = new TestScaffold()
            .EfCoreBuilder(dbContext)
            .WithDefaultCatalogue()
            .Build()
            .AutofacBuilder()
            .WithSingletonTypeAs<AuthService, IAuthService>()
            .WithContainerBuilder(builder =>
                builder.Register(c =>
                    {
                        //Mock Authenticated User Context 
                        var testScaffold = _scenarioContext.Get<TestScaffold>(nameof(TestScaffold));
                        var dbContext = testScaffold.Resolve<TestDbContext>();
                        var user = dbContext.Users.FirstOrDefault(u => u.Id == Defaults.CurrentUserId);
                        return new UserContext(c.Resolve<IAuthService>(), user?.Email, user?.Password);
                    })
                    .As<IUserContext>()
                    .InstancePerLifetimeScope()
            )
            .WithType<ShoppingCartService>()
            .Build();

        _scenarioContext.Add(nameof(TestScaffold), testScaffold);
    }

    [When(@"I Add an Item to my Shopping Cart")]
    public void WhenIAddTheItems()
    {
        var testScaffold = _scenarioContext.Get<TestScaffold>(nameof(TestScaffold));
        var dbContext = testScaffold.Resolve<TestDbContext>();
        
        var item = dbContext.Items.FirstOrDefault(i => i.Title == Defaults.CatalogueItems.DeadPool);
        
        var shoppingCartService = testScaffold.Resolve<ShoppingCartService>();
        shoppingCartService.AddItemToCart(item!.Id);
    }

    [Then(@"I should see these Items in my Shopping Cart")]
    public void ThenIShouldSeeTheseItemsInMyShoppingCart()
    {
        var testScaffold = _scenarioContext.Get<TestScaffold>(nameof(TestScaffold));
        var dbContext = testScaffold.Resolve<TestDbContext>();

        var userId = _scenarioContext.Get<Guid?>("CurrentUserId");
        var item = dbContext.Items.FirstOrDefault(i => i.Title == Defaults.CatalogueItems.DeadPool);

        var cart = dbContext.ShoppingCart.Include(s => s.Inventory).FirstOrDefault(u => u.UserId == userId);
        Assert.IsTrue(cart?.Inventory.Any(i => i.Id == item!.Id));
    }
}