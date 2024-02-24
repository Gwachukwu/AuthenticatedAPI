using ShopAPI.Data;
using ShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ShoppingCartController : ControllerBase
{

    private readonly ILogger<ShoppingCartController> _logger;
    private readonly AppSecurityContext _dbSec;
    private readonly AppDataContext _dbData;

    public ShoppingCartController(ILogger<ShoppingCartController> logger
        , AppSecurityContext dbSec
        , AppDataContext dbData)
    {
        _logger = logger;
        _dbSec = dbSec;
        _dbData = dbData;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        var userEmail = User.Identity?.Name ?? String.Empty;

        var cart = await _dbData.ShoppingCarts
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.User == userEmail);

        if (cart == null)
        {
            return NotFound();
        }

        return cart.Products;
    }

    [HttpPost("remove/{id}")]
    public async Task<string> RemoveFromShoppingCart(int id)
    {
        var userEmail = User.Identity?.Name ?? String.Empty;

        var shoppingCart = await _dbData.ShoppingCarts
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.User == userEmail);

        if (shoppingCart == null)
        {
            return "Shopping cart not found";
        }

        var product = shoppingCart.Products.FirstOrDefault(c => c.Id == id);
        if (product == null)
        {
            return "Product not found in cart";
        }

        shoppingCart.Products.Remove(product);
        await _dbData.SaveChangesAsync();
        return "Product removed";
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> AddToCart(int productId)
    {
        var userEmail = User.Identity?.Name ?? String.Empty;
        var product = await _dbData.Products.FindAsync(productId);
        if (product == null)
        {
            return NotFound("Product not found.");
        }

        var cart = await _dbData.ShoppingCarts
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.User == userEmail);

        if (cart == null)
        {
            cart = new ShoppingCart { User = userEmail };
            _dbData.ShoppingCarts.Add(cart);
        }

        cart.Products.Add(product);
        await _dbData.SaveChangesAsync();

        return Ok("Product added to cart.");
    }
}