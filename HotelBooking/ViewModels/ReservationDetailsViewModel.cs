using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.ViewModels
{
    public class ReservationDetailsViewModel
    {
        public Reservation Reservation { get; set; }
        public int ReservationID { get; set; }
        public double TotalPrice { get; set; }
        public Room Room { get; set; }
        public int RoomID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }
}
