using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReservationSystemAPI.Controllers
{
    [Route("api/info")]
    [ApiController]
    public class ClassInfoController : ControllerBase
    {
        private readonly IClassScheduleData _classScheduleData;
        private readonly IClassData _classData;

        public ClassInfoController(IClassScheduleData classScheduleData, IClassData classData)
        {
            _classScheduleData = classScheduleData;
            _classData = classData;
        }

        [AllowAnonymous]
        [HttpGet("class-schedules")]
        public async Task<ActionResult<ClassScheduleModel>> GetClassSchedules()
        {
            try
            {
                return Ok(await _classScheduleData.GetClassSchedules());
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("classes")]
        public async Task<ActionResult<ClassModel>> GetClasses()
        {
            try
            {
                return Ok(await _classData.GetClasses());
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
