using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ShopEase.Client;
using ShopEase.Client.Data;

namespace ShopEase.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5144"),
            });

            builder.Services.AddScoped<IProductApiService, ProductApiService>();
            builder.Services.AddScoped<ICartApiService, CartApiService>();

            await builder.Build().RunAsync();
        }
    }
}
