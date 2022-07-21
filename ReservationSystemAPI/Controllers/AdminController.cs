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

    [HttpPost("class")]
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

    [HttpPost("class-schedule")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<ClassScheduleModel>> CreateClassSchedule(ClassScheduleModel classScheduleModel)
    {
        try
        {
            await _classScheduleData.InsertClassSchedule(classScheduleModel);
            return Ok(classScheduleModel);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}
