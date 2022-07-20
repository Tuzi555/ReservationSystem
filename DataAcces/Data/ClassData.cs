using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;
public class ClassData : IClassData
{
    private readonly ISqlDataAccess _db;

    public ClassData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<ClassModel>> GetClasses()
    {
        return _db.LoadData<ClassModel, dynamic>("reservation_system.spClasses_GetAll", new { });
    }
    public Task InsertClass(ClassModel classModel)
    {
        return _db.SaveData("reservation_system.spClasses_Insert", new { classModel.Name, classModel.Description, classModel.Capacity });
    }
}
