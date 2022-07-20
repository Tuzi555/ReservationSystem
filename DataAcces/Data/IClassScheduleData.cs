using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IClassScheduleData
    {
        Task<IEnumerable<ClassScheduleModel>> GetClassSchedules();
    }
}