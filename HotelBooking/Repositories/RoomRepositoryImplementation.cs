using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.Models;

namespace HotelBooking.Repositories
{
    public class RoomRepositoryImplementation : RoomRepository
    {

        private List<Room> _roomList;
        public RoomRepositoryImplementation()
        {
            _roomList = new List<Room>()
            {
                new Room() {RoomID = 1, Type = "Standard", Price = 150 },
                new Room() {RoomID = 2, Type = "Standard", Price = 150 },
                new Room() {RoomID = 3, Type = "Standard", Price = 150 },
                new Room() {RoomID = 4, Type = "Standard", Price = 150 },
                new Room() {RoomID = 5, Type = "Standard", Price = 150 },
                new Room() {RoomID = 6, Type = "Premium", Price = 250 },
                new Room() {RoomID = 7, Type = "Premium", Price = 250 },
                new Room() {RoomID = 8, Type = "Premium", Price = 250 },
                new Room() {RoomID = 9, Type = "Premium", Price = 250 },
                new Room() {RoomID = 10, Type = "Premium", Price = 250 },
                new Room() {RoomID = 11, Type = "Apartment", Price = 450 },
                new Room() {RoomID = 12, Type = "Apartment", Price = 450 },
                new Room() {RoomID = 13, Type = "Apartment", Price = 450 },
                new Room() {RoomID = 14, Type = "Apartment", Price = 450 },
                new Room() {RoomID = 15, Type = "Apartment", Price = 450 },
            };

        }
        public Room Add(Room room)
        {
            room.RoomID = _roomList.Max(e => e.RoomID) + 1;
            _roomList.Add(room);
            return room;
        }

        public Room Delete(int RoomID)
        {
            Room room = _roomList.FirstOrDefault(e => e.RoomID == RoomID);
            if (room != null)
            {
                _roomList.Remove(room);
            }
            return room;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _roomList;
        }

        public Room GetPrice(int RoomID)
        {
            return _roomList.FirstOrDefault(p => p.Price == RoomID);
        }

        public Room GetRoom(int RoomID)
        {
            return _roomList.FirstOrDefault(e => e.RoomID == RoomID);
        }

        public Room Update(Room roomUpdate)
        {
            Room room = _roomList.FirstOrDefault(e => e.RoomID == roomUpdate.RoomID);
            if (room != null)
            {
                room.RoomID = roomUpdate.RoomID;
                room.Type = roomUpdate.Type;
                room.Price = roomUpdate.Price;
            }
            return room;
        }
    }
}
