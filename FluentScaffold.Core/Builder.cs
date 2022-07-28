namespace FluentScaffold.Core;

public class Builder: IBuilder
{
    private readonly TestScaffold _testScaffold;

    protected Builder(TestScaffold testScaffold)
    {
        _testScaffold = testScaffold;
    }
}

public interface IBuilder
{
}