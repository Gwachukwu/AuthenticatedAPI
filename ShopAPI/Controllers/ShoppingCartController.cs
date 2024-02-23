using ShopAPI.Data;
using ShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace IdentityAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly AppSecurityContext _dbSec;
    private readonly AppDataContext _dbData;

    public WeatherForecastController(ILogger<WeatherForecastController> logger
        , AppSecurityContext dbSec
        , AppDataContext dbData)
    {
        _logger = logger;
        _dbSec = dbSec;
        _dbData = dbData;
    }

    // [HttpPut]
    // public async Task<Testmodel?> Put()
    // {
    //     var userName = (User.Identity?.Name ?? String.Empty);
    //     var tm = _dbData.Testmodels.Where(tm => tm.Name == userName).FirstOrDefault();
    //     return tm;
    // }

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