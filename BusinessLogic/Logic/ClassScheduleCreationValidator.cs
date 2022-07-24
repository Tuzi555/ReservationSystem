using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;

namespace Services.Logic;
public class ClassScheduleCreationValidator : IClassScheduleCreationValidator
{
    public async Task<bool> ClassExists(IClassData classData, int classId)
    {
        var classes = await classData.GetClasses();
        var relevantClasses = classes.Where(schedule => schedule.Id == classId);
        return relevantClasses.FirstOrDefault() != null;
    }
}
