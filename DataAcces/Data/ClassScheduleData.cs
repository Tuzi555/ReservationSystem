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

    public Task<IEnumerable<ClassScheduleModel>> GetClassSchedules()
    {
        return _db.LoadData<ClassScheduleModel, dynamic>("reservation_system.spClassSchedules_GetAll", new { });
    }
}
