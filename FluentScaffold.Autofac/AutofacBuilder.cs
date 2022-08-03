using System.Collections.Immutable;
using FluentScaffold.Core;

namespace FluentScaffold.Autofac;

public class AutofacBuilder: Builder
{
    public AutofacBuilder(TestScaffold testScaffold) : base(testScaffold)
    {
    }

    public AutofacBuilder WithService<T>()
    {
        return this;
    }
}