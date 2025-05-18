using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ShopEase.Api.Models;

namespace ShopEase.Api.Repositories
{
    public class MySqlCartRepository : ICartRepository
    {
        private readonly string _connectionString;

        public MySqlCartRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ShopEaseDatabase");
        }

        public async Task<List<CartItem>> GetCartItemsAsync(int userId)
        {
            var cartItems = new List<CartItem>();
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            var cmd = conn.CreateCommand();
            cmd.CommandText =
                @"
                SELECT ci.CartItemID, ci.UserID, ci.ProductID, ci.Quantity, ci.AddedAt,
                       p.Name, p.Price, p.Category
                FROM CartItems ci
                JOIN Products p ON ci.ProductID = p.ProductID
                WHERE ci.UserID = @UserID";
            cmd.Parameters.AddWithValue("@UserID", userId);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                cartItems.Add(
                    new CartItem
                    {
                        CartItemID = reader.GetInt32(0),
                        UserID = reader.GetInt32(1),
                        ProductID = reader.GetInt32(2),
                        Quantity = reader.GetInt32(3),
                        AddedAt = reader.GetDateTime(4),
                        Product = new Product
                        {
                            ProductID = reader.GetInt32(2),
                            Name = reader.GetString(5),
                            Price = reader.GetDecimal(6),
                            Category = reader.GetString(7),
                        },
                    }
                );
            }
            return cartItems;
        }

        public async Task AddOrUpdateCartItemAsync(CartItem item)
        {
            const string sql =
                @"
        INSERT INTO CartItems (UserID, ProductID, Quantity)
        VALUES (@UserID, @ProductID, @Quantity)
        ON DUPLICATE KEY UPDATE Quantity = Quantity + @Quantity;
    ";
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", item.UserID);
            cmd.Parameters.AddWithValue("@ProductID", item.ProductID);
            cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM CartItems WHERE CartItemID = @CartItemID";
            cmd.Parameters.AddWithValue("@CartItemID", cartItemId);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
