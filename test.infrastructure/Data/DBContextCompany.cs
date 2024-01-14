using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using test.domain.Entities;

namespace test.infrastructure.Data;

public class DBContextCompany : IdentityDbContext<User>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    public DBContextCompany(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, DbContextOptions<DBContextCompany> options) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }
    public DbSet<Company> Companies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var tenant = _httpContextAccessor.HttpContext.Items["Tenant"] as string;
        if (tenant != null)
        {
            var connectionString = GetConnectionStringForTenant(tenant);
            optionsBuilder.UseSqlServer(connectionString);
        }
        else
        {
            optionsBuilder.UseSqlServer("defaultConnectionString");
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityUser>().ToTable("User");
    }

    private string GetConnectionStringForTenant(string tenant)
    {
        return _configuration.GetConnectionString(tenant);
    }

}

