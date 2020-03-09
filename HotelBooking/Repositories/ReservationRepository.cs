using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.Repositories
{
    public interface ReservationRepository
    {
        Reservation GetReservation(int ReservationID);
        IEnumerable<Reservation> GetAllReservations();
        Reservation Add(Reservation reservation);
        Reservation Update(Reservation reservationUpdate);
        Reservation Delete(int ReservationID);

    }
}
