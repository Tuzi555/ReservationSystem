using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;

namespace Services.Logic;
public class ReservationCreationValidator : IReservationCreationValidator
{
    public bool SelfReservation(int currentUserId, int reservationUserId)
    {
        return currentUserId == reservationUserId;
    }

    public async Task<bool> IsFree(IClassScheduleData scheduleData, int scheduleId)
    {
        var classSchedules = await scheduleData.GetClassSchedules();
        var relevantSchedule = classSchedules.Where(schedule => schedule.Id == scheduleId);
        return relevantSchedule.Select(relevantSchedule => relevantSchedule.FreeCapacity).First() > 0;
    }

    public async Task<bool> HasReservation(IReservationData reservationData, int scheduleId, int currentUserId)
    {
        var reservations = await reservationData.GetReservations();
        var relevantReservation = reservations.Where(reservation => reservation.ClassScheduleId == scheduleId && reservation.UserId == currentUserId);
        return relevantReservation != null;
    }
}
