using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HotelBooking.Repositories
{
    public class ReservationRepositoryImplementation : ReservationRepository
    {
        private List<Reservation> _reservationList;
        public ReservationRepositoryImplementation()
        {
            _reservationList = new List<Reservation>()
            {
                new Reservation() {ReservationID = 1, RoomID = 1, CheckInDate = new DateTime (2020,4,20), CheckOutDate = new DateTime (2020,4,27), UserName = "lfarulewski@yahoo.com" },
                new Reservation() {ReservationID = 2, RoomID = 6, CheckInDate = new DateTime (2020,5,10), CheckOutDate = new DateTime (2020,5,17), UserName = "jbravo@gmail.com" },
                new Reservation() {ReservationID = 3, RoomID = 11, CheckInDate = new DateTime (2020,5,12), CheckOutDate = new DateTime (2020,5,15), UserName = "glineker@hotmail.com" },
                new Reservation() {ReservationID = 4, RoomID = 14, CheckInDate = new DateTime (2020,6,12), CheckOutDate = new DateTime (2020,6,14), UserName = "rlewandowski@gmail.com" },
                new Reservation() {ReservationID = 5, RoomID = 2, CheckInDate = new DateTime (2020,7,10), CheckOutDate = new DateTime (2020,7,10), UserName = "pdatsyuk@gmail.com" },
                new Reservation() {ReservationID = 6, RoomID = 7, CheckInDate = new DateTime (2020,7,12), CheckOutDate = new DateTime (2020,7,15), UserName = "scrosby@hotmail.com" },
                new Reservation() {ReservationID = 7, RoomID = 9, CheckInDate = new DateTime (2020,8,12), CheckOutDate = new DateTime (2020,8,15), UserName = "mjordan@yahoo.com" },
                new Reservation() {ReservationID = 8, RoomID = 15, CheckInDate = new DateTime (2020,8,14), CheckOutDate = new DateTime (2020,8,17), UserName = "rkubica@gmail.com" },
            };

        }
        public Reservation Add(Reservation reservation)
        {
            reservation.ReservationID = _reservationList.Max(e => e.ReservationID)+1;
            _reservationList.Add(reservation);
            return reservation;
        }

        public Reservation Delete(int ReservationID)
        {
            Reservation reservation = _reservationList.FirstOrDefault(e => e.ReservationID == ReservationID);
            if (reservation != null)
            {
                _reservationList.Remove(reservation);
            }
            return reservation;
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservationList;
        }

        public Reservation GetReservation(int ReservationID)
        {
            return _reservationList.FirstOrDefault(e => e.ReservationID == ReservationID);
        }

        public Reservation Update(Reservation reservationUpdate)
        {
            Reservation reservation = _reservationList.FirstOrDefault(e => e.ReservationID == reservationUpdate.ReservationID);
            if (reservation != null)
            {
                reservation.RoomID = reservationUpdate.RoomID;
                reservation.UserName = reservationUpdate.UserName;
                reservation.CheckInDate = reservationUpdate.CheckInDate;
                reservation.CheckOutDate = reservationUpdate.CheckOutDate;
            }
            return reservation;
        }
    }
}
