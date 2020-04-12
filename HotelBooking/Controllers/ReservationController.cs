using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.DataContext;
using HotelBooking.Models;
using HotelBooking.Repositories;
using HotelBooking.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;
        public ReservationController(ReservationRepository reservationRepository, RoomRepository roomRepository,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        // Tutaj wyświetlam wszystkie rezerwacje
        [Authorize(Roles = "Admin")]
        public ViewResult GetAllReservations()
        {
            var reservations = _reservationRepository.GetAllReservations()
                .OrderBy(c=>c.CheckInDate);        
            //var model = _reservationRepository.GetAllReservations();
            //model.OrderBy(c => c.TotalPrice);
            ViewBag.TerazJest = DateTime.Now;
            //return View(model);
            return View(reservations);
        }

        // Rezerwuje klient - get
        [HttpGet]
        [Authorize]
        public ViewResult CreateReservation(int id)
        {
            ReservationViewModel reservation = new ReservationViewModel();
            reservation.RoomID = _roomRepository.GetRoom(id).RoomID;
            reservation.UserID = userManager.GetUserId(User);
            return View(reservation);
        }

        // Rezerwuje klient  - post
        [HttpPost]
        [Authorize]
        public RedirectToActionResult CreateReservation(ReservationViewModel reservation)
        {
            if (reservation.CheckInDate < DateTime.Today)
            {
                return RedirectToAction("ReservationLate");
            }

            else if (reservation.CheckInDate == reservation.CheckOutDate)
            {
                return RedirectToAction("ReservationTooShort");
            }

            else if (reservation.CheckInDate > reservation.CheckOutDate)
            {
                return RedirectToAction("ReservationWronglyScheduled");
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
                reservationModel.TotalPrice = reservation.TotalPrice;
                reservationModel.CountDays = reservation.CountDays;

                if (reservationModel.CheckInDate.HasValue && reservationModel.CheckOutDate.HasValue)
                {
                    DateTime dt1 = reservationModel.CheckInDate.Value;
                    DateTime dt2 = reservationModel.CheckOutDate.Value;

                    reservationModel.CountDays = (dt2 - dt1).Days;

                    reservationModel.TotalPrice = reservationModel.CountDays * reservationModel.Room.Price;

                    Reservation newReservation = _reservationRepository.Add(reservationModel);
                    context.SaveChanges();
                }  
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

        public IActionResult ReservationTooShort()
        {
            return View();
        }

        public IActionResult ReservationWronglyScheduled()
        {
            return View();
        }


        // Tutaj Klient dokonuje rezerwacji
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> MyReservation(int id)
        {  
            
            ApplicationUser user = await GetCurrentUserAsync();
            var reservation = _reservationRepository.GetAllReservations();
            //.Where(p => p.UserName == user.UserName)
            //.OrderBy(c => c.CheckInDate);
            reservation = reservation.Where(p => p.UserName == user.UserName);
            reservation = reservation.OrderBy(c => c.CheckInDate);

            if (reservation.Any())
            {
                ViewBag.TerazJest = DateTime.Now;
                return View(reservation);
            }

            else
            {
                return View ("NoReservation");
            }
        }

        //public IActionResult NoReservation()
        //{
        //    return View();
        //}

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        // Edytuję rezerwację
        [HttpGet]
        [Authorize]
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
                CheckOutDate = reservation.CheckOutDate,
                CountDays = reservation.CountDays,
            };
            return View(reservationEditViewModel);
        }

        // Dokonuję zmian w edytowanej rezerwacji
        [HttpPost]
        [Authorize]
        public IActionResult EditReservation(ReservationEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Reservation reservation = _reservationRepository.GetReservation(model.ReservationID);
                reservation.RoomID = model.RoomID;
                reservation.UserName = model.UserName;
                reservation.CheckInDate = model.CheckInDate;
                reservation.CheckOutDate = model.CheckOutDate;
                reservation.TotalPrice = model.TotalPrice;
                reservation.CountDays = model.CountDays;
                

                if (reservation.CheckInDate < DateTime.Today)
                {
                    return RedirectToAction("ReservationLateEdit");
                }

                if (reservation.CheckInDate == reservation.CheckOutDate)
                {
                    return RedirectToAction("ReservationTooShortEdit");
                }

                if (reservation.CheckInDate > reservation.CheckOutDate)
                {
                    return RedirectToAction("ReservationWronglyScheduledEdit");
                }

                _reservationRepository.Update(reservation);

                //Aktualizacja ilości dni i nowej ceny cłkowitej
                if (reservation.CheckInDate.HasValue && reservation.CheckOutDate.HasValue)
                {
                    DateTime dt1 = reservation.CheckInDate.Value;
                    DateTime dt2 = reservation.CheckOutDate.Value;

                    reservation.CountDays = (dt2 - dt1).Days;

                    var roomId = reservation.RoomID;


                    // tutaj wyciągam z bazy danych cenę pokoju na 2 sposoby
                    var price1 = _roomRepository.GetPrice(reservation.RoomID);

                    //var price2 = _roomRepository.GetPrice(roomId);

                    var price = reservation.Room.Price;


                    //reservation.TotalPrice = reservation.CountDays * reservation.Room.Price;
                    reservation.TotalPrice = reservation.CountDays * price;

                    _reservationRepository.Update(reservation);

                }

                if (signInManager.IsSignedIn(User) && userManager.GetUserAsync(User).Result.UserName == "lfarulewski@yahoo.com")
                {
                    return RedirectToAction("GetAllReservations");
                }
                else
                {
                    return RedirectToAction("MyReservation");
                }
                
            }
            return View();
        }

        public IActionResult ReservationLateEdit()
        {
            return View();
        }

        public IActionResult ReservationTooShortEdit()
        {
            return View();
        }

        public IActionResult ReservationWronglyScheduledEdit()
        {
            return View();
        }

        public IActionResult ReservationExistsEdit()
        {
            return View();
        }


        // Pokazuje szczegóły dla rezerwacji
        [Authorize]
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
        [Authorize]
        public ActionResult DeleteReservation(int id)
        {
            // przed usunięciem rezerwacji muszę ją znaleźć
            Reservation reservation = context.Reservations.Find(id);
            if (reservation != null)
            {
                context.Reservations.Remove(reservation);
                context.SaveChanges();
            }

            if (signInManager.IsSignedIn(User) && userManager.GetUserAsync(User).Result.UserName == "lfarulewski@yahoo.com")
            {
                return RedirectToAction("GetAllReservations");
            }
            else
            {
                return RedirectToAction("MyReservation");
            }      
        }

        private bool ReservationExists(int id)
        {
            return context.Reservations.Any(e => e.ReservationID == id);
        }
    }
}
 