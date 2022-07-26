﻿using DataAccess.Models;

namespace DataAccess.Data;
public interface IReservationData
{
    Task InsertReservation(ReservationModel reservationModel);
    Task<IEnumerable<ReservationModel>> GetReservations();
}
