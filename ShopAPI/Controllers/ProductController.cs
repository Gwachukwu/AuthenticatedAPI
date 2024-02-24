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
public class ProductsController : ControllerBase
{

    private readonly ILogger<ProductsController> _logger;
    private readonly AppSecurityContext _dbSec;
    private readonly AppDataContext _dbData;

    public ProductsController(ILogger<ProductsController> logger
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
        var cart = await _dbData.Products
                          .Include(product => product.Category)
                             .ToListAsync();
        return cart;
    }

    // GET: /Products/ByCategory/5
    [HttpGet("ByCategory/{categoryId}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int categoryId)
    {
        var products = await _dbData.Products
            .Where(p => p.Category.Id == categoryId)
            .Include(product => product.Category)
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
        var categoryId = product.Category.Id;

        var category = await _dbData.Categories
                         .FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category != null)
        {
            product.Category = category;
        }

        _dbData.Products.Add(product);
        await _dbData.SaveChangesAsync();
        return "Product added successfully";
    }

}