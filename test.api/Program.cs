
using edupay.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using test.app.Errors;
using test.app.Midleweres;
using test.app.Service;
using test.infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(ProductService).Assembly);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors();
builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentityOptions();
builder.Services.AddDbContext<DBContextProduct>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("tenant1")));
builder.Services.AddDbContext<DBContextCompany>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("tenant2")));
builder.Services.AddAplicacionServices(builder.Configuration);
builder.Services.AddJwt(builder.Configuration);
// builder.Services.AddJwt(builder.Configuration);
var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TenantMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var contextProducto = services.GetRequiredService<DBContextProduct>();
        await contextProducto.Database.MigrateAsync();

        var contextCompany = services.GetRequiredService<DBContextCompany>();
        await contextProducto.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Ocurri� un error durante la migraci�n");
    }
}

app.UseCors("CorsPolicy");

app.Run();


