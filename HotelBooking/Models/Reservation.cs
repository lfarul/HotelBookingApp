using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }

        public int EmployeeID { get; set; }

        public int RoomID { get; set; }

        [Display(Name = "Check in Date")]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CheckInDate { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Check out Date")]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CheckOutDate { get; set; }

        public virtual Room Room { get; set; }
    }
}
