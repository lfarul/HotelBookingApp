using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.DataContext;
using HotelBooking.Models;

namespace HotelBooking.Repositories
{
    public class SqlRoomRepository : RoomRepository
    {
        private readonly ApplicationDbContext context;

        // ApplicationDbContext class injection by constructor method
        public SqlRoomRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Room Add(Room room)
        {
            context.Rooms.Add(room);
            context.SaveChanges();
            return room;
        }

        public Room Delete(int RoomID)
        {
            // before deleting Room, we need to find them first
            Room room = context.Rooms.Find(RoomID);
            if (room != null)
            {
                context.Rooms.Remove(room);
                context.SaveChanges();
            }
            return room;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return context.Rooms;
        }

        public Room GetRoom(int RoomID)
        {
            return context.Rooms.Find(RoomID);

        }

        public Room Update(Room roomUpdate)
        {
            var room = context.Rooms.Attach(roomUpdate);
            room.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return roomUpdate;
        }
    }
}
