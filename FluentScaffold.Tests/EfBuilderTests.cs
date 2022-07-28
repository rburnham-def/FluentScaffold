using System;
using System.Linq;
using FluentScaffold.Tests.TestData;
using NUnit.Framework;
using FluentScaffold.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FluentScaffold.Tests;

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
    public void BuildADbContext()
    {
        var userId = Guid.Parse("65579043-8112-480C-A885-C6157947F0F3");
        new TestScaffold()
            .BuildDbContext(_dbContext)
            .With(new User()
            {
                Id = userId,
                Email = "Bob@test.com",
                Name = "Bob"
            });

        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
        Assert.IsNotNull(user);
        Assert.IsTrue(user?.Id == userId);
    }
}