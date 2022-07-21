using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Logic;

namespace ReservationSystemAPI.Controllers;
[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IReservationData _reservationData;
    private readonly IConfiguration _configuration;
    private readonly IReservationCreationValidator _reservationCreationValidator;
    private readonly IUserIdentifier _userIdentifier;
    private readonly IClassScheduleData _classScheduleData;

    public UserController(IReservationData reservationData, IConfiguration configuration, IReservationCreationValidator reservationCreationValidator, IUserIdentifier userIdentifier, IClassScheduleData classScheduleData)
    {
        _reservationData = reservationData;
        _configuration = configuration;
        _reservationCreationValidator = reservationCreationValidator;
        _userIdentifier = userIdentifier;
        _classScheduleData = classScheduleData;
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<int>> GetCurrentUserId()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var userId = _userIdentifier.GetUserIdFromToken(accessToken, _configuration);
        return userId;
    }

    [HttpPost("reservation")]
    [Authorize]
    public async Task<ActionResult<ReservationModel>> CreateReservation(ReservationModel reservationModel)
    {
        if (!await _reservationCreationValidator.IsFree(_classScheduleData, reservationModel.ClassScheduleId))
        {
            return Problem("Class is full.");
        }

        if (await _reservationCreationValidator.HasReservation(_reservationData, reservationModel.ClassScheduleId, reservationModel.UserId))
        {
            return Problem("User can have only one reservation.");
        }

        var accessToken = await HttpContext.GetTokenAsync("access_token");
        int currentUserId = _userIdentifier.GetUserIdFromToken(accessToken, _configuration);

        if (!_reservationCreationValidator.SelfReservation(currentUserId, reservationModel.UserId))
        {
            return Problem("User can not create reservation for another user");
        }

        try
        {
            await _reservationData.InsertReservation(reservationModel);
            return Ok(reservationModel);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

    }
}
