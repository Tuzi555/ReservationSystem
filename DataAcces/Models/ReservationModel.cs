﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models;
public class ReservationModel
{
    public int Id { get; set; }
    public int ClassScheduleId { get; set; }
    public int UserId { get; set; }
}
