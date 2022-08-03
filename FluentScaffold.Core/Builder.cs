namespace FluentScaffold.Core;

public class Builder: IBuilder
{
    private readonly TestScaffold _testScaffold;
    protected readonly Queue<Action> _buildActions = new Queue<Action>();

    protected Builder(TestScaffold testScaffold)
    {
        _testScaffold = testScaffold;
    }

    protected void Enqueue(Action action)
    {
        _buildActions.Enqueue(action);
    }

    /// <summary>
    /// Build the current builder actions and return the TestScaffold context. 
    /// </summary>
    /// <returns></returns>
    public TestScaffold Build()
    {
        while (_buildActions.Any())
        {
            var action = _buildActions.Dequeue();
            action();
        }

        return _testScaffold;
    }
}

public interface IBuilder
{
}