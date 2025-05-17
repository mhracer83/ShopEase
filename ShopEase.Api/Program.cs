using ShopEase.Api.Repositories;
using ShopEase.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// bind connection-strings from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

// register your repo & service
builder.Services.AddSingleton<ICartRepository, MySqlCartRepository>();
builder.Services.AddTransient<ICartService, CartService>();

builder.Services.AddControllers();
builder.Services.AddCors(o =>
    o.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
);

var app = builder.Build();
app.UseCors();
app.MapControllers();
app.Run();
