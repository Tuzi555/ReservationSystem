using DataAccess.Data;

namespace Services.Logic;

public interface IReservationCreationValidator
{
    bool SelfReservation(int currentUserId, int reservationUserId);
    public Task<bool> IsFree(IClassScheduleData scheduleData, int scheduleId);
    public Task<bool> HasReservation(IReservationData reservationData, int scheduleId, int currentUserId);
}
