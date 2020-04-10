using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.Models;
using HotelBooking.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class WebApiController : Controller
    {
        private readonly ReservationRepository reservationRepository;
        private readonly RoomRepository roomRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public WebApiController(ReservationRepository reservationRepository,
            RoomRepository roomRepository, UserManager<ApplicationUser> userManager)
        {
            this.reservationRepository = reservationRepository;
            this.roomRepository = roomRepository;
            this.userManager = userManager;
        }

        public JsonResult GetAllReservationsJson()
        {
            var res = reservationRepository.GetAllReservations();
            return Json(res);
        }

        public ObjectResult GetRoom()
        {
            Room model = roomRepository.GetRoom(21);

            return new ObjectResult(model);
        }

        public JsonResult GetUsers()
        {
            var listUsers = userManager.Users;
            return Json(listUsers);
        }

        public ObjectResult GetUsers2()
        {
            var listUsers = userManager.Users;
            return new ObjectResult(listUsers);
        }

        public JsonResult GetPrice()
        {
            var price = roomRepository.GetPrice(21);

            Room model = roomRepository.GetPrice(21);

            return Json(model);

            //return Json(model);
        }
    }
}