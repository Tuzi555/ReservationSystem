using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Logic;

namespace ReservationSystemAPI.Controllers;
[Route("api/admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IClassData _classData;
    private readonly IClassScheduleData _classScheduleData;
    private readonly IClassScheduleCreationValidator _classScheduleCreationValidator;

    public AdminController(IClassData classData, IClassScheduleData classScheduleData, IClassScheduleCreationValidator classScheduleCreationValidator)
    {
        _classData = classData;
        _classScheduleData = classScheduleData;
        _classScheduleCreationValidator = classScheduleCreationValidator;
    }

    /// <summary>
    /// Creates a new class that can be scheduled. Class name has to be unique. Id can remain 0 (db handles id assignment).
    /// </summary>
    /// <param name="classModel"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Creates a new class schedule. API call returns an error, if admin tries to create schedule for non-existent class. Id can remain 0 (db handles id assignment).
    /// </summary>
    /// <param name="classScheduleModel"></param>
    /// <returns></returns>
    [HttpPost("class-schedule")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<ClassScheduleModel>> CreateClassSchedule(ClassScheduleModel classScheduleModel)
    {
        if (!await _classScheduleCreationValidator.ClassExists(_classData, classScheduleModel.ClassId))
        {
            return Problem("Can not create schedule for non-existent class.");
        }
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
