using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReservationSystemAPI.Controllers;
[Route("api/admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IClassData _classData;
    private readonly IClassScheduleData _classScheduleData;

    public AdminController(IClassData classData, IClassScheduleData classScheduleData)
    {
        _classData = classData;
        _classScheduleData = classScheduleData;
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<ClassModel>> CreateClass(ClassModel classModel)
    {
        try
        {
            await _classData.InsertClass(classModel);
            return Ok(classModel);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}
