using DataAccess.Data;

namespace Services.Logic;
public interface IClassScheduleCreationValidator
{
    Task<bool> ClassExists(IClassData classData, int classId);
}
