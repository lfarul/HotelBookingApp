using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required(ErrorMessage = "Please provide first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please provide last name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please provide credit card number")]
        [Display(Name ="Credit Card No")]
        [CreditCard]
        public string CreditCardNo { get; set; }
    }
}
