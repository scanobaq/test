using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace test.app.Midleweres;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path;

        var tenant = GetTenantFromPath(path);

        if (tenant.IsNullOrEmpty())
        {
            context.Items["Tenant"] = "tenant2";
        }
        else
        {
            context.Items["Tenant"] = tenant;
        }

        await _next(context);
    }
    private string GetTenantFromPath(PathString path)
    {
        return path.Value.Split('/')[1];
    }

}
