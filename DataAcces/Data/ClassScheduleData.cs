using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;

public class ClassScheduleData : IClassScheduleData
{
    private readonly ISqlDataAccess _db;

    public ClassScheduleData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<ClassScheduleInfoModel>> GetClassSchedules()
    {
        return _db.LoadData<ClassScheduleInfoModel, dynamic>("reservation_system.spClassSchedules_GetAll", new { });
    }

    public Task<IEnumerable<ClassScheduleInfoModel>> GetClassSchedule(int id)
    {
        return _db.LoadData<ClassScheduleInfoModel, dynamic>("reservation_system.spClassSchedules_Get_By_Id", new { Id = id });
    }

    public Task InsertClassSchedule(ClassScheduleModel classScheduleModel)
    {
        return _db.SaveData("reservation_system.spClassSchedules_Insert", new { classScheduleModel.ClassId, classScheduleModel.StartTime, classScheduleModel.EndTime });
    }
}
