using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ReservationSystemAPI.Auth;
using Services.Passwords;

namespace ReservationSystemAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserData _userData;
    private readonly IAuthTokenCreator _tokenCreator;

    public AuthController(IUserData userData, IAuthTokenCreator tokenCreator)
    {
        _userData = userData;
        _tokenCreator = tokenCreator;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<UserModel>> Register(UserDto userDto)
    {
        PasswordCreator.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        UserModel user = new UserModel();
        user.Email = userDto.Email;
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.Phone = userDto.Phone;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        try
        {
            await _userData.InsertUser(user);
            return Ok(user);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserModel>> Login(LoginDto login)
    {
        var results = await _userData.GetUser(login.Email);
        if (results == null)
        {
            return BadRequest("User not found.");
        }

        if (!PasswordChecker.VerifyPasswordHash(login.Password, results.PasswordHash, results.PasswordSalt))
        {
            return BadRequest("Wrong password.");
        }

        string token = _tokenCreator.CreateToken(results);
        return Ok(token);
    }
}
