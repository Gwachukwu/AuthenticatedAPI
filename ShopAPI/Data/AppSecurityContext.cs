using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShopAPI.Data;

public class AppSecurityContext : IdentityDbContext
{
    public AppSecurityContext(DbContextOptions<AppSecurityContext> options)
        : base(options)
        {}
}