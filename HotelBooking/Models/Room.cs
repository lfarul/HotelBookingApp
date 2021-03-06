﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.Models
{
    public class Room
    {
        public int RoomID { get; set; }

        [Required(ErrorMessage = "Please provide room number")]
        [Display(Name = "Room Number")]
        public int RoomNumber { get; set; }

        [Required(ErrorMessage = "Please provide price")]
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please provide type")]
        [Display(Name = "Type")]
        public string Type { get; set; }

        public string Description { get; set; }

        [Display(Name = "Photo")]
        public string PhotoPath { get; set; }

        // relacja 1:wielu
        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}
