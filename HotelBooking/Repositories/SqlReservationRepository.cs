using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.DataContext;
using HotelBooking.Models;

namespace HotelBooking.Repositories
{
    public class SqlReservationRepository : ReservationRepository
    {
        private readonly ApplicationDbContext context;

        // ApplicationDbContext class injection by constructor method
        public SqlReservationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Reservation Add(Reservation reservation)
        {
            context.Reservations.Add(reservation);
            context.SaveChanges();
            return reservation;
        }

        public Reservation Delete(int ReservationID)
        {
            // before deleting Reservation, we need to find them first
            Reservation reservation = context.Reservations.Find(ReservationID);
            if (reservation != null)
            {
                context.Reservations.Remove(reservation);
                context.SaveChanges();
            }
            return reservation;
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return context.Reservations;
        }

        public Reservation GetReservation(int ReservationID)
        {
            return context.Reservations.Find(ReservationID);
        }

        public Reservation Update(Reservation reservationUpdate)
        {
            var reservation = context.Reservations.Attach(reservationUpdate);
            reservation.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return reservationUpdate;
        }
    }
}
