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

        [Display(Name = "Total Price")]
        public double TotalPrice { get; set; }

        public int CountDays { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check in Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "Please provide check in date")]
        public DateTime? CheckInDate { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Check out Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "Please provide check out date")]
        public DateTime? CheckOutDate { get; set; }

        // relacja 1:1
        public int RoomID { get; set; }
        public virtual Room Room { get; set; }


    }
}
