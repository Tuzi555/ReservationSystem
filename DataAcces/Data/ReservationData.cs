using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;
public class ReservationData : IReservationData
{
    private readonly ISqlDataAccess _db;

    public ReservationData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task InsertReservation(ReservationModel reservationModel)
    {
        return _db.SaveData("reservation_system.spReservations_Insert", new { reservationModel.ClassScheduleId, reservationModel.UserId });
    }
}
