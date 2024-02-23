using ShopAPI.Data;
using ShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IdentityAPI.Controllers;

// [Authorize]
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{

    private readonly ILogger<ProductController> _logger;
    private readonly AppSecurityContext _dbSec;
    private readonly AppDataContext _dbData;

    public ProductController(ILogger<ProductController> logger
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
        var cart = await _dbData.Products.ToListAsync();
        return cart;
    }

    // GET: /Products/ByCategory/5
    [HttpGet("ByCategory/{categoryId}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int categoryId)
    {
        var products = await _dbData.Products
            .Where(p => p.Category.Id == categoryId)
            .ToListAsync();

        if (products == null || !products.Any())
        {
            return NotFound("No products found for the specified category.");
        }

        return products;
    }

    [HttpPost]
    public async Task<string> AddProduct(Product product)
    {
        _dbData.Products.Add(product);
        await _dbData.SaveChangesAsync();
        return "Product added successfully";
    }

}