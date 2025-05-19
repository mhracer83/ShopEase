using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _config;

    public AuthController(IUserService userService, IConfiguration config)
    {
        _userService = userService;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        // Backend password validation (enforces even if frontend is bypassed)
        if (!ModelState.IsValid)
            return BadRequest(
                ModelState
                    .Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .FirstOrDefault()
            );

        if (await _userService.UserExistsAsync(dto.Username, dto.Email))
            return BadRequest("Username or Email already exists.");

        var userId = await _userService.RegisterAsync(dto.Username, dto.Email, dto.Password);
        if (userId == null)
            return StatusCode(500, "Registration failed.");
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Username and password are required.");

        var user = await _userService.AuthenticateAsync(dto.Username, dto.Password);
        if (user == null)
            return Unauthorized("Invalid username or password.");

        var token = JwtHelper.GenerateJwtToken(user, _config);

        // Return token as plain string, or as JSON { token = token }
        return Ok(token);
    }
}
