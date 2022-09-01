using System;
using System.Linq;
using FluentScaffold.Tests.ApplicationUnderTest.Data;

namespace FluentScaffold.Tests.ApplicationUnderTest.Services;


/// <summary>
/// Example of a service that benefits from a Component Integration Test
/// </summary>
public class ShoppingCartService
{
    private readonly TestDbContext _dbContext;
    private readonly IUserContext _userContext;

    public ShoppingCartService(TestDbContext dbContext, IUserContext userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }

    public void AddItemToCart(Guid itemId)
    {
        var user = _userContext.GetAuthenticatedUser();
        if (user == null) return;
        
        var item = _dbContext.Items.FirstOrDefault(i => i.Id == itemId);
        var shoppingCart = _dbContext.ShoppingCart.FirstOrDefault(c => c.UserId == user.Id);
        if(shoppingCart is null)
        {
            shoppingCart = new ShoppingCart()
            {
                UserId = user.Id
            };

            _dbContext.ShoppingCart.Add(shoppingCart);
        }

        if (item != null)
            shoppingCart.Inventory.Add(item);
        // else handle error
            
        _dbContext.SaveChanges();

    }
    
}