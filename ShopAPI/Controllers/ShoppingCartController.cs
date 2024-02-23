using ShopAPI.Data;
using ShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
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
    public async Task<IEnumerable<Product>> Get()
    {
        var userEmail = User.Identity?.Name ?? String.Empty;

        var cart = await _dbData.ShoppingCarts
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.User == userEmail);

        return cart.Products;
    }

    [HttpPost("remove/{id}")]
    public async Task<string> RemoveShoppingCart(int id)
    {
        var shoppingCart = await _dbData.ShoppingCarts.FindAsync(id);
        if (shoppingCart == null)
        {
            return "Not found";
        }

        _dbData.ShoppingCarts.Remove(shoppingCart);
        await _dbData.SaveChangesAsync();
        return "Shopping cart removed";
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

    // [HttpGet(Name = "GetTestString")]
    // public async Task<string> Get(string id)
    // {

    //     // var user = await _uMan.FindByNameAsync(User.Identity?.Name ?? String.Empty);
    //     // return user?.Email ?? String.Empty;
    //     // return $"{User.Identity?.Name ?? String.Empty} = {user?.Email ?? String.Empty}";
    //     var rNum = Random.Shared.Next();
    //     _dbData.Add<Testmodel>(new Testmodel() {  Name = User.Identity?.Name ?? String.Empty, TestmodelId = rNum});
    //     await _dbData.SaveChangesAsync();
    //     return rNum.ToString();
    //     // task.Wait();
    //     // var user = task.Result;

    // }

    // [AllowAnonymous]
    // [HttpPost(Name = "GetWeatherForecast")]
    // public IEnumerable<string> Post()
    // {
    //     // var ty = this.GetType();
    //     // var ca = ty.GetCustomAttributesData();
    //     // var ret = ca.Select(c => c.AttributeType.Name);

    //     var ty = this.GetType();
    //     var met = ty.GetMethods();
    //     var mList = met.SelectMany(m => m.GetCustomAttributesData());
    //     var ret = mList.Select(m => m.AttributeType.Name).Where(m => m.StartsWith("Http"));

    //     return ret;
    // }

    // [AllowAnonymous]
    // [HttpOptions(Name = "GetWeatherForecast")]
    // public string Options()
    // {
    //     // var ty = this.GetType();
    //     // var ca = ty.GetCustomAttributesData();
    //     // var ret = ca.Select(c => c.AttributeType.Name);

    //     var ty = this.GetType();
    //     var met = ty.GetMethods();
    //     var mList = met.SelectMany(m => m.GetCustomAttributesData());
    //     var hdr = mList.Select(m => m.AttributeType.Name).Where(m => m.StartsWith("Http")).Select(m => m.Replace("Http", String.Empty).Replace("Attribute", String.Empty).ToUpper());

    //     Response.Headers.Allow = String.Join(',', hdr.ToArray());

    //     return String.Empty;
    // }
}