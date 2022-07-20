using DataAccess.Models;

namespace DataAccess.Data;
public interface IClassData
{
    Task<IEnumerable<ClassModel>> GetClasses();
    Task InsertClass(ClassModel classModel);
}
