using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.ViewModels
{
    public class RoomEditViewModel : RoomCreateViewModel
    {
        public int RoomID { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
