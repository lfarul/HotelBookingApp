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
            if (context.Reservations.Where(x => x.CheckInDate == reservation.CheckInDate && x.Room.RoomID == reservation.Room.RoomID).Any())
            {
                return RedirectToAction("ReservationExists");
            }

            else
            {
                Models.Reservation reservationModel = new Models.Reservation();

                reservationModel.Room = context.Rooms.FirstOrDefault(x => x.RoomID == reservation.Room.RoomID);
                reservationModel.UserName = userManager.GetUserName(User);
                reservationModel.CheckInDate = reservation.CheckInDate;

                Reservation newReservation = _reservationRepository.Add(reservationModel);
                context.SaveChanges();
            }
            return RedirectToAction("MyReservation");
        }

    
        public IActionResult ReservationExists()
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

        /*
        // Po dokonaniu rezerwacji, klient przechodzi do Podsumowania  - ReservationDetails
        public ViewResult ReservationDetails(int id)
        {
            ReservationDetailsViewModel reservationDetailsViewModel = new ReservationDetailsViewModel()
            {

                UserID = userManager.GetUserId(User),
                Appointment = _appointmentRepository.GetAppointment(id),
                Doctor = _doctorRepository.GetDoctor(id),
            };

            return View(appointmentDetailsViewModel);
        }

        // Po umówieniu wizyty, podsumowanie można zapisać do PDF
        public async Task<IActionResult> AppointmentPDF(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await context.Appointments
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return new ViewAsPdf(appointment);
        }

        // Tutaj wyświetlają się wizyty tylko dla zalogowanego pacjenta
        public async Task<IActionResult> MyAppointment(int id)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var appointment = _appointmentRepository.GetAllAppointment();
            appointment = appointment.Where(p => p.UserName == user.UserName);
            ViewBag.TerazJest = DateTime.Now;
            return View(appointment);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        // Edytuję wizytę
        [HttpGet]
        public ViewResult EditAppointment(int id)
        {
            Appointment appointment = _appointmentRepository.GetAppointment(id);
            AppointmentEditViewModel appointmentEditViewModel = new AppointmentEditViewModel
            {
                AppointmentID = appointment.AppointmentID,
                DoctorID = appointment.DoctorID,
                UserName = appointment.UserName,
                AppointmentDate = appointment.AppointmentDate

            };
            return View(appointmentEditViewModel);
        }

        // Dokonuję zmian w edytowanej wizycie
        [HttpPost]
        public IActionResult EditAppointment(AppointmentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Appointment appointment = _appointmentRepository.GetAppointment(model.AppointmentID);
                appointment.DoctorID = model.DoctorID;
                appointment.UserName = model.UserName;
                appointment.AppointmentDate = model.AppointmentDate;

                _appointmentRepository.Update(appointment);

                return RedirectToAction("MyAppointment");
            }
            return View();
        }

        // Pokazuje szczegóły dla wizyty
        public async Task<IActionResult> DetailAppointment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await context.Appointments
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // Usuwa wizytę
        public ActionResult DeleteAppointment(int id)
        {
            // before deleting a Pacjent, we need to find them first
            Appointment appointment = context.Appointments.Find(id);
            if (appointment != null)
            {
                context.Appointments.Remove(appointment);
                context.SaveChanges();
            }
            return RedirectToAction("MyAppointment");
        }

        private bool AppointmentExists(int id)
        {
            return context.Appointments.Any(e => e.AppointmentID == id);
        }
    }
}*/
    }
}