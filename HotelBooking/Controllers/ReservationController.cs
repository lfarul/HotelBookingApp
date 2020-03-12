using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.DataContext;
using HotelBooking.Models;
using HotelBooking.Repositories;
using HotelBooking.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace HotelBooking.Controllers
{
    public class ReservationController : Controller
    {
        //atrybut readonly uniemożliwia przypadkowe nadanie nowej wartości polu _reservationRepository
        private readonly ReservationRepository _reservationRepository;
        private readonly RoomRepository _roomRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;
        public ReservationController(ReservationRepository reservationRepository, RoomRepository roomRepository,
            UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            this.userManager = userManager;
            this.context = context;
        }

        // Tutaj wyświetlam wszystkie rezerwacje
        public ViewResult GetAllReservations()
        {
            var model = _reservationRepository.GetAllReservations();
            ViewBag.TerazJest = DateTime.Now;
            return View(model);
        }

        // Rezerwuje klient - get
        [HttpGet]
        public ViewResult CreateReservation(int id)
        {
            ReservationViewModel reservation = new ReservationViewModel();
            reservation.RoomID = _roomRepository.GetRoom(id).RoomID;
            //parsowanie do int
            reservation.UserID = userManager.GetUserId(User);
            return View(reservation);
        }

        // Rezerwuje klient  - post
        [HttpPost]
        public RedirectToActionResult CreateReservation(ReservationViewModel reservation)
        {
            if (reservation.CheckInDate < DateTime.Today)
            {
                return RedirectToAction("ReservationLate");
            }

            else if (reservation.CheckInDate == reservation.CheckOutDate)
            {
                return RedirectToAction("ReservationToShort");
            }

            else if (context.Reservations.Where(x => x.CheckInDate == reservation.CheckInDate && x.Room.RoomID == reservation.Room.RoomID).Any())
            {
                return RedirectToAction("ReservationExists");
            }

            else
            {
                Models.Reservation reservationModel = new Models.Reservation();

                reservationModel.Room = context.Rooms.FirstOrDefault(x => x.RoomID == reservation.Room.RoomID);
                reservationModel.UserName = userManager.GetUserName(User);
                reservationModel.CheckInDate = reservation.CheckInDate;
                reservationModel.CheckOutDate = reservation.CheckOutDate;
                //reservationModel.CountNights = reservation.CheckOutDate - reservation.CheckInDate; 
                reservationModel.TotalPrice = reservation.TotalPrice;

                Reservation newReservation = _reservationRepository.Add(reservationModel);
                context.SaveChanges();

            }
            return RedirectToAction("MyReservation");
        }

    
        public IActionResult ReservationExists()
        {
            return View();
        }

        public IActionResult ReservationLate()
        {
            return View();
        }

        public IActionResult ReservationToShort()
        {
            return View();
        }

        // Tutaj Klient dokonuje rezerwacji
        public IActionResult Reservation(int id)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel()
            {
                UserID = userManager.GetUserId(User),
                Room = _roomRepository.GetRoom(id),
                RoomID = _roomRepository.GetRoom(id).RoomID,
            };

            return View(reservationViewModel);
        }

        // Po dokonaniu rezerwacji, klient przechodzi do Podsumowania  - ReservationDetails
        public ViewResult ReservationDetails(int id)
        {
            ReservationDetailsViewModel reservationDetailsViewModel = new ReservationDetailsViewModel()
            {

                UserID = userManager.GetUserId(User),
                Reservation = _reservationRepository.GetReservation(id),
                Room = _roomRepository.GetRoom(id),
            };

            return View(reservationDetailsViewModel);
        }

        // Po rezerwacji pokoju, podsumowanie można zapisać do PDF
        public async Task<IActionResult> ReservationPDF(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await context.Reservations
                .FirstOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return new ViewAsPdf(reservation);
        }

        // Tutaj wyświetlają się rezerwacje zalogowanego klienta
        public async Task<IActionResult> MyReservation(int id)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var reservation = _reservationRepository.GetAllReservations();
            reservation = reservation.Where(p => p.UserName == user.UserName);
            ViewBag.TerazJest = DateTime.Now;
            return View(reservation);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        // Edytuję rezerwację
        [HttpGet]
        public ViewResult EditReservation(int id)
        {
            Reservation reservation = _reservationRepository.GetReservation(id);
            ReservationEditViewModel reservationEditViewModel = new ReservationEditViewModel
            {
                ReservationID = reservation.ReservationID,
                RoomID = reservation.RoomID,
                TotalPrice = reservation.TotalPrice,
                UserName = reservation.UserName,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate

            };
            return View(reservationEditViewModel);
        }

        // Dokonuję zmian w edytowanej rezerwacji
        [HttpPost]
        public IActionResult EditReservation(ReservationEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Reservation reservation = _reservationRepository.GetReservation(model.ReservationID);
                reservation.RoomID = model.RoomID;
                reservation.UserName = model.UserName;
                reservation.CheckInDate = model.CheckInDate;
                reservation.CheckOutDate = model.CheckOutDate;

                _reservationRepository.Update(reservation);

                return RedirectToAction("MyReservation");
            }
            return View();
        }

        // Pokazuje szczegóły dla rezerwacji
        public async Task<IActionResult> DetailReservation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await context.Reservations
                .FirstOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // Usuwam rezerwację
        public ActionResult DeleteReservation(int id)
        {
            // przed usunięciem rezerwacji muszę ją znaleźć
            Reservation reservation = context.Reservations.Find(id);
            if (reservation != null)
            {
                context.Reservations.Remove(reservation);
                context.SaveChanges();
            }
            return RedirectToAction("MyReservation");
        }

        private bool ReservationExists(int id)
        {
            return context.Reservations.Any(e => e.ReservationID == id);
        }
    }
}
 