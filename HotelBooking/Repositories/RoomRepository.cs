using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.Repositories
{
    public interface RoomRepository
    {

        Room GetRoom(int RoomID);
        IEnumerable<Room> GetAllRooms();
        Room Add(Room room);
        Room Update(Room roomUpdate);
        Room Delete(int RoomID);

    }
}
