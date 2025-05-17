using System;
using Microsoft.Extensions.Configuration;
using ShopEase.Api.Models;
using ShopEase.Api.Repositories;

class Program
{
    static async System.Threading.Tasks.Task Main()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var repo = new MySqlProductRepository(config);

        // Add two products
        await repo.AddProductAsync(new Product("Laptop", 999.99M, "Electronics"));
        await repo.AddProductAsync(new Product("Coffee Maker", 49.99M, "Appliances"));

        // Remove one
        await repo.RemoveProductAsync(2);

        // Display cart
        var items = await repo.GetProductCatalogAsync();
        foreach (var p in items)
        {
            Console.WriteLine(p.GetProductDetails());
        }
        Console.WriteLine($"Total: ${items.Sum(i => i.Price)}");
    }
}
