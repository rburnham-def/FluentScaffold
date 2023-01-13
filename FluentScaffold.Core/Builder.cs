namespace FluentScaffold.Core;

public class Builder
{
    private readonly TestScaffold _testScaffold;
    private readonly Queue<Action> _buildActions = new();

    protected Builder(TestScaffold testScaffold)
    {
        _testScaffold = testScaffold;
    }

    /// <summary>
    /// Enqueue an action to be applied when Build is called. 
    /// </summary>
    /// <param name="action"></param>
    protected void Enqueue(Action action)
    {
        _buildActions.Enqueue(action);
    }

    /// <summary>
    /// Build the current builder actions and return the TestScaffold context. 
    /// </summary>
    /// <returns></returns>
    public virtual TestScaffold Build()
    {
        while (_buildActions.Any())
        {
            var action = _buildActions.Dequeue();
            action();
        }

        return _testScaffold;
    }
}