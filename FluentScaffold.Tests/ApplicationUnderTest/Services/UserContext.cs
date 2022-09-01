using FluentScaffold.Tests.ApplicationUnderTest.Data;

namespace FluentScaffold.Tests.ApplicationUnderTest.Services;

//Mock Authenticated User Context
public class UserContext: IUserContext
{
    private readonly User? _user;
    
    public UserContext(IAuthService authService, string email, string password)
    {
        _user = authService.AuthenticateUser(email, password);
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