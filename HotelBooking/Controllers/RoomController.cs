using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.DataContext;
using HotelBooking.Models;
using HotelBooking.Repositories;
using HotelBooking.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class RoomController : Controller
    { // dependency injection przez konstruktor
        private readonly RoomRepository _roomRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext _context;

        public RoomController(RoomRepository roomRepository, IHostingEnvironment hostingEnvironment,
            UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _roomRepository = roomRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            _context = context;
        }

        // List of rooms for a customer
        public ViewResult NewRooms()
        {
            var model = _roomRepository.GetAllRooms();
            return View(model);
        }

        // List of rooms for an administrator
        public ViewResult Index()
        {
            var model = _roomRepository.GetAllRooms();
            return View(model);
        }


        public ViewResult Details(int id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Room = _roomRepository.GetRoom(id),
                PageTitle = "Room Details"
            };

            return View(homeDetailsViewModel);
        }



        [HttpGet]
        public ViewResult Edit(int id)
        {
            Room room = _roomRepository.GetRoom(id);
            RoomEditViewModel roomEditViewModel = new RoomEditViewModel
            {
                RoomID = room.RoomID,
                RoomNumber = room.RoomNumber,
                Type = room.Type,
                Price = room.Price,
                Description = room.Description,
                ExistingPhotoPath = room.PhotoPath
            };
            return View(roomEditViewModel);
        }


        [HttpPost]
        public IActionResult Edit(RoomEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Room room = _roomRepository.GetRoom(model.RoomID);
                room.RoomNumber = model.RoomNumber;
                room.Type = model.Type;
                room.Price = model.Price;
                room.Description = model.Description;


                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "Images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    room.PhotoPath = ProcessUploadedFile(model);
                }

                _roomRepository.Update(room);

                return RedirectToAction("NewRooms");
            }
            return View();
        }

        private string ProcessUploadedFile(RoomCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoomCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Room newRoom = new Room
                {
                    Type = model.Type,
                    RoomNumber = model.RoomNumber,
                    Price = model.Price,
                    Description = model.Description,
                    PhotoPath = uniqueFileName
                };

                _roomRepository.Add(newRoom);

                return RedirectToAction("details", new { id = newRoom.RoomID });
            }
            return View();
        }

        public Room Delete(int id)
        {
            // before deleting a Pacjent, we need to find them first
            Room room = _context.Rooms.Find(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
            return room;
        }

    }
}