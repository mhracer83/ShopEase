using ShopEase.Api.Repositories;
using ShopEase.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// bind connection-strings from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

// register your repo & service
builder.Services.AddSingleton<IProductRepository, MySqlProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddScoped<ICartRepository, MySqlCartRepository>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:5233").AllowAnyHeader().AllowAnyMethod()
    );
});

builder.Services.AddControllers();

var app = builder.Build();

// ... other middleware
app.UseRouting();
app.UseCors();
app.UseAuthorization(); // if using authentication
app.MapControllers();

app.Run();
