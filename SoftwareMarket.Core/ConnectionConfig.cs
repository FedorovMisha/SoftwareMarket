using Microsoft.AspNetCore.Http;

namespace SoftwareMarket.Core;

public class ConnectionConfig
{
    private readonly IHttpContextAccessor _context;
    public string ConnectionString => $"User ID={_context.HttpContext.Request.Cookies["username"]};Password={_context.HttpContext.Request.Cookies["userpassword"]};Host=localhost;Port=5432;Database=softwaremarket_db;";

    public ConnectionConfig(IHttpContextAccessor context)
    {
        _context = context;
    }
}