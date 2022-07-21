using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IClassScheduleData
    {
        Task<IEnumerable<ClassScheduleInfoModel>> GetClassSchedules();
        Task InsertClassSchedule(ClassScheduleModel classScheduleModel);
    }
}
