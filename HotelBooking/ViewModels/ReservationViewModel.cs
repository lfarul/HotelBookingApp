using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.ViewModels
{
    public class ReservationViewModel
    {
        public ReservationViewModel()
        {
            Users = new List<string>();
        }

        public int ResrvationID { get; set; }
        public double TotalPrice { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }

        public Room Room { get; set; }
        public int RoomID { get; set; }
        public float Price { get; set; }
        public string Type { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Check in Date")]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CheckInDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check out Date")]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CheckOutDate { get; set; }

        public int CountNights { get; set; }

        public List<string> Users { get; set; }
    }
}
