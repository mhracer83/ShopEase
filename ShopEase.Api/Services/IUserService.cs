using System.Threading.Tasks;

public interface IUserService
{
    // Registration
    Task<bool> UserExistsAsync(string username, string email);
    Task<int?> RegisterAsync(string username, string email, string password);

    // Authentication/Login
    Task<User> AuthenticateAsync(string username, string password);
    Task<User> GetUserByUsernameAsync(string username);
}
