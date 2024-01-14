
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
using test.app.Errors;
using test.app.Service;
using test.domain.Entities;
using test.domain.Interfaces;
using test.infrastructure.Data;
using test.infrastructure.Repository;

// using Microsoft.IdentityModel.Tokens;

namespace edupay.Api.Extensions;

public static class AplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()   //WithOrigins("https://dominio.com")
                    .AllowAnyMethod()          //WithMethods("GET","POST")
                    .AllowAnyHeader());        //WithHeaders("accept","content-type")
            });

    public static void AddAplicacionServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();


    }

    public static void AddIdentityOptions(this IServiceCollection service)
    {
        service.AddDefaultIdentity<User>(
            options =>
            {
                options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.User.RequireUniqueEmail = true;
            }
        ).AddEntityFrameworkStores<DBContextCompany>().AddDefaultTokenProviders();
    }

    public static void AddValidationErrors(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {

                var errors = actionContext.ModelState.Where(u => u.Value.Errors.Count > 0)
                                                .SelectMany(u => u.Value.Errors)
                                                .Select(u => u.ErrorMessage).ToArray();

                var errorResponse = new ApiValidation()
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });
    }

    public static void AddJwt(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
        .AddCookie()
        .AddJwtBearer(options =>
        {
            options.SaveToken = false;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["Tokens:Issuer"],
                ValidAudience = configuration["Tokens:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]))
            };
        });
    }

}
