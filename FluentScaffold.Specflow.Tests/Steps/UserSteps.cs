using FluentScaffold.Core;
using FluentScaffold.Tests.ApplicationUnderTest;
using FluentScaffold.Tests.ApplicationUnderTest.Data;
using TechTalk.SpecFlow.Assist;

namespace FluentScaffold.Specflow.Tests.Steps;

[Binding]
public class UserSteps
{
    private readonly ScenarioContext _scenarioContext;

    public UserSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }
    
    [Given(@"I am Authenticated as the User")]
    public void GivenIAmAnAuthenticatedAUser(Table table)
    {

        var user = table.CreateInstance<User>();
        user.Id = Defaults.CurrentUserId;
        var testScaffold = _scenarioContext.Get<TestScaffold>(nameof(TestScaffold));


        testScaffold.EfCoreBuilder<TestDbContext>()
            .With(user)
            .WithShoppingCart(user.Id)
            .Build();
        
        _scenarioContext.Add("CurrentUserId", user.Id);
    }
}