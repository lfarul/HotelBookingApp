using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.ViewModels
{
    public class RoomCreateViewModel
    {

        [Required(ErrorMessage = "Please provide room number")]
        [Display(Name = "Room number")]
        public int RoomNumber { get; set; }

        [Required(ErrorMessage = "Please provide price")]
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please provide type")]
        [Display(Name = "Type")]
        public string Type { get; set; }

        public string Description { get; set; }

        public IFormFile Photo { get; set; }
    }
}
