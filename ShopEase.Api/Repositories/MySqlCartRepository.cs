using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ShopEase.Api.Models;

namespace ShopEase.Api.Repositories
{
    public class MySqlCartRepository : ICartRepository
    {
        private readonly string? _connectionString;

        public MySqlCartRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ShopEaseDatabase");
        }

        public async Task AddProductAsync(Product product)
        {
            const string sql =
                @"INSERT INTO Products (Name, Price, Category) 
                                 VALUES (@Name, @Price, @Category);";

            await using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Category", product.Category);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task RemoveProductAsync(int productId)
        {
            const string sql = "DELETE FROM Products WHERE ProductID = @ProductID;";

            await using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ProductID", productId);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Product>> GetCartItemsAsync()
        {
            var products = new List<Product>();
            const string sql = "SELECT ProductID, Name, Price, Category FROM Products;";

            await using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new MySqlCommand(sql, conn);
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                products.Add(
                    new Product
                    {
                        ProductID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        Category = reader.GetString(3),
                    }
                );
            }
            return products;
        }
    }
}
