using System.Globalization;
using System.Linq;
using FluentScaffold.Tests.ApplicationUnderTest.Data;

namespace FluentScaffold.Tests.ApplicationUnderTest.Services;

public class AuthService: IAuthService
{
    private readonly TestDbContext _dbContext;

    public AuthService(TestDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public User? AuthenticateUser(string email, string password)
    {
        // Mock auth 
        var user = _dbContext.Users
            .Where(u => u.Email.ToLower(CultureInfo.InvariantCulture) == email.ToLower(CultureInfo.InvariantCulture))
            .FirstOrDefault(u => u.Password == password);
        return user;
    }
}

public interface IAuthService
{
    User? AuthenticateUser(string email, string password);
}