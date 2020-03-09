using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.DataSeed
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().HasData(

                new Room() { RoomID = 1, Type = "Deluxe 1", Price = 150 },
                new Room() { RoomID = 2, Type = "Deluxe 1", Price = 150 },
                new Room() { RoomID = 3, Type = "Deluxe 1", Price = 150 },
                new Room() { RoomID = 4, Type = "Deluxe 1", Price = 150 },
                new Room() { RoomID = 5, Type = "Deluxe 1", Price = 150 },
                new Room() { RoomID = 6, Type = "Deluxe 2", Price = 250 },
                new Room() { RoomID = 7, Type = "Deluxe 2", Price = 250 },
                new Room() { RoomID = 8, Type = "Deluxe 2", Price = 250 },
                new Room() { RoomID = 9, Type = "Deluxe 2", Price = 250 },
                new Room() { RoomID = 10, Type = "Deluxe 2", Price = 250 },
                new Room() { RoomID = 11, Type = "Superior 2", Price = 450 },
                new Room() { RoomID = 12, Type = "Superior 2", Price = 450 },
                new Room() { RoomID = 13, Type = "Superior 2", Price = 450 },
                new Room() { RoomID = 14, Type = "Superior 2", Price = 450 },
                new Room() { RoomID = 15, Type = "Superior 2", Price = 450 },
                new Room() { RoomID = 16, Type = "Family 3", Price = 450 },
                new Room() { RoomID = 17, Type = "Family 3", Price = 450 },
                new Room() { RoomID = 18, Type = "Family 3", Price = 450 },
                new Room() { RoomID = 19, Type = "Family 3", Price = 450 },
                new Room() { RoomID = 20, Type = "Family 3", Price = 450 }

        );
        
        }
    }
}