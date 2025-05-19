using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ShopEase.Api.HelperClasses;

public class UserService : IUserService
{
    private readonly string _connectionString;

    public UserService(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("ShopEaseDatabase");
    }

    // Registration: Check if user exists
    public async Task<bool> UserExistsAsync(string username, string email)
    {
        username = InputSanitizer.SanitizeInput(username);
        email = InputSanitizer.SanitizeInput(email);
        using var conn = new MySqlConnection(_connectionString);
        await conn.OpenAsync();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";
        cmd.Parameters.AddWithValue("@Username", username);
        cmd.Parameters.AddWithValue("@Email", email);

        var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
        return count > 0;
    }

    // Registration: Add user with hashed password
    public async Task<int?> RegisterAsync(string username, string email, string password)
    {
        username = InputSanitizer.SanitizeInput(username);
        email = InputSanitizer.SanitizeInput(email);

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        using var conn = new MySqlConnection(_connectionString);
        await conn.OpenAsync();
        var cmd = conn.CreateCommand();
        cmd.CommandText =
            @"INSERT INTO Users (Username, Email, PasswordHash) 
              VALUES (@Username, @Email, @PasswordHash)";
        cmd.Parameters.AddWithValue("@Username", username);
        cmd.Parameters.AddWithValue("@Email", email);
        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

        try
        {
            await cmd.ExecuteNonQueryAsync();
            return (int)cmd.LastInsertedId;
        }
        catch
        {
            return null;
        }
    }

    // Login: Get user by username
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        using var conn = new MySqlConnection(_connectionString);
        await conn.OpenAsync();

        var cmd = conn.CreateCommand();
        cmd.CommandText =
            "SELECT UserID, Username, Email, PasswordHash FROM Users WHERE Username = @Username";
        cmd.Parameters.AddWithValue("@Username", username);

        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new User
            {
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
            };
        }
        return null;
    }

    // Login: Authenticate user by verifying password
    public async Task<User> AuthenticateAsync(string username, string password)
    {
        var user = await GetUserByUsernameAsync(username);
        if (user == null)
            return null;

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!isPasswordValid)
            return null;

        return user;
    }
}
