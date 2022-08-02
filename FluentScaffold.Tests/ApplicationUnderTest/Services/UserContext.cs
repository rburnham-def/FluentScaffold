using FluentScaffold.Tests.ApplicationUnderTest.Data;

namespace FluentScaffold.Tests.ApplicationUnderTest.Services;

//Mock Authenticated User Context
public class UserContext: IUserContext
{
    private readonly User _user;

    public UserContext(User user)
    {
        _user = user;
    }
    public User GetAuthenticatedUser()
    {
        return _user;
    }
}

public interface IUserContext
{
    User? GetAuthenticatedUser();
}