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

        /// <summary>
        /// Returns list of all scheduled classes.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("class-schedules")]
        public async Task<ActionResult<ClassScheduleInfoModel>> GetClassSchedules()
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

        /// <summary>
        /// Returns list of all classes, that can be scheduled.
        /// </summary>
        /// <returns></returns>
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
