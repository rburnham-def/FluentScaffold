using Autofac;
using FluentScaffold.Core;
using FluentScaffold.Tests.ApplicationUnderTest.Autofac;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FluentScaffold.Tests;

[TestFixture]
public class AutofacBuilderTests
{
    [Test]
    public void AutofacBuilder_WithContainerBuilder()
    {
        //Register service as instance per lifetime scope using autofac builder.
        var testScaffold = new TestScaffold()
            .AutofacBuilder()
            .WithContainerBuilder(builder =>
                builder.RegisterType<TimeService>().As<ITimeService>().InstancePerLifetimeScope())
            .Build();


        var timeService1 = testScaffold.ServiceProvider?.GetService<ITimeService>();
        var timeService2 = testScaffold.ServiceProvider?.GetService<ITimeService>();

        Assert.Multiple(() =>
        {
            Assert.NotNull(timeService1);
            Assert.NotNull(timeService2);

            //should return the same object reference 
            Assert.AreSame(timeService1, timeService2, "Object reference should match for InstancePerLifetimeScope.");
        });
    }

    [Test]
    public void AutofacBuilder_WithType()
    {
        //Register service as instance per lifetime scope using autofac builder.
        var testScaffold = new TestScaffold()
            .AutofacBuilder()
            .WithType<TimeService>()
            .Build();


        var timeService1 = testScaffold.ServiceProvider?.GetService<TimeService>();
        var timeService2 = testScaffold.ServiceProvider?.GetService<TimeService>();

        Assert.Multiple(() =>
        {
            Assert.NotNull(timeService1);
            Assert.NotNull(timeService2);

            //should return a new instance 
            Assert.AreNotSame(timeService1, timeService2, "Object reference should not match.");
        });
    }
    
    
    [Test]
    public void AutofacBuilder_WithTypeAs()
    {
        //Register service as instance per lifetime scope using autofac builder.
        var testScaffold = new TestScaffold()
            .AutofacBuilder()
            .WithTypeAs<TimeService, ITimeService>()
            .Build();


        var timeService1 = testScaffold.ServiceProvider?.GetService<ITimeService>();
        var timeService2 = testScaffold.ServiceProvider?.GetService<ITimeService>();

        Assert.Multiple(() =>
        {
            Assert.NotNull(timeService1);
            Assert.NotNull(timeService2);

            //should return a new instance 
            Assert.AreNotSame(timeService1, timeService2, "Object reference should not match.");
        });
    }
    
    [Test]
    public void AutofacBuilder_WithInstance()
    {
        //Register service as instance per lifetime scope using autofac builder.
        var timeService = new TimeService();
        var testScaffold = new TestScaffold()
            .AutofacBuilder()
            .WithInstance(timeService)
            .Build();


        var timeService1 = testScaffold.ServiceProvider?.GetService<TimeService>();

        Assert.Multiple(() =>
        {
            Assert.NotNull(timeService1);

            //should return a new instance 
            Assert.AreSame(timeService1, timeService, "Object reference should match.");
        });
    }
    
    [Test]
    public void AutofacBuilder_WithInstanceAs()
    {
        //Register service as instance per lifetime scope using autofac builder.
        var timeService = new TimeService();
        var testScaffold = new TestScaffold()
            .AutofacBuilder()
            .WithInstanceAs<TimeService, ITimeService>(timeService)
            .Build();


        var timeService1 = testScaffold.ServiceProvider?.GetService<ITimeService>();

        Assert.Multiple(() =>
        {
            Assert.NotNull(timeService1);

            //should return a new instance 
            Assert.AreSame(timeService1, timeService, "Object reference should match.");
        });
    }
    
    
    [Test]
    public void AutofacBuilder_WithSingletonType()
    {
        //Register service as instance per lifetime scope using autofac builder.
        var testScaffold = new TestScaffold()
            .AutofacBuilder()
            .WithSingletonType<TimeService>()
            .Build();


        var timeService1 = testScaffold.ServiceProvider?.GetService<TimeService>();
        var timeService2 = testScaffold.ServiceProvider?.GetService<TimeService>();

        Assert.Multiple(() =>
        {
            Assert.NotNull(timeService1);
            Assert.NotNull(timeService2);

            //should return a new instance 
            Assert.AreSame(timeService1, timeService2, "Object reference should match.");
        });
    }
    
    
    [Test]
    public void AutofacBuilder_WithSingletonTypeAs()
    {
        //Register service as instance per lifetime scope using autofac builder.
        var testScaffold = new TestScaffold()
            .AutofacBuilder()
            .WithSingletonTypeAs<TimeService, ITimeService>()
            .Build();


        var timeService1 = testScaffold.ServiceProvider?.GetService<ITimeService>();
        var timeService2 = testScaffold.ServiceProvider?.GetService<ITimeService>();

        Assert.Multiple(() =>
        {
            Assert.NotNull(timeService1);
            Assert.NotNull(timeService2);

            //should return a new instance 
            Assert.AreSame(timeService1, timeService2, "Object reference should match.");
        });
    }

    [Test]
    public void AutofacBuilder_WithSingletonAs()
    {
        //Register service as instance per lifetime scope using autofac builder.
        var timeService = new TimeService();
        var testScaffold = new TestScaffold()
            .AutofacBuilder()
            .WithSingletonAs<TimeService, ITimeService>(timeService)
            .Build();


        var timeService1 = testScaffold.ServiceProvider?.GetService<ITimeService>();

        Assert.Multiple(() =>
        {
            Assert.NotNull(timeService1);

            //should return a new instance 
            Assert.AreSame(timeService1, timeService, "Object reference should match.");
        });
    }
}