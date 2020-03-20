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

        public int ReservationID { get; set; }
        public double TotalPrice { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }

        public Room Room { get; set; }
        public int RoomID { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }

        public int CountDays { get; set; }


        [Display(Name = "Check in Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? CheckInDate { get; set; }


        [Display(Name = "Check out Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? CheckOutDate { get; set; }

        public List<string> Users { get; set; }
    }
}
