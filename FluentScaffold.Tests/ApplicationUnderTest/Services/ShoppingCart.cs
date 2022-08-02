namespace FluentScaffold.Tests.ApplicationUnderTest.Services;


/// <summary>
/// Example of a service that benefits from a Component Integration Test
/// </summary>
public class ShoppingCart
{
    private readonly IAuthService _authService;

    public ShoppingCart(IAuthService authService)
    {
        _authService = authService;
    }

    public void AddToCart()
    {
        
    }
    
}