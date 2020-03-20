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

        public int RoomID { get; set; }

        [Display(Name = "Total Price")]
        public double TotalPrice { get; set; }

        public int CountDays { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }


        [Display(Name = "Check in Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode =true)]
        [DataType(DataType.Date)]
        public DateTime? CheckInDate { get; set; }


        [Display(Name = "Check out Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? CheckOutDate { get; set; }

        public virtual Room Room { get; set; }
    }
}
