using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ClassScheduleModel
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
    }
}
