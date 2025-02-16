using HotelManagement.Models;
using HotelManagement.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hotelManagement.BLL.Services;
using hotelManagement.Domain.Models;
using HotelManagementFinal.BLL.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using hotelManagamentFinal.Models.DTO;
namespace HotelManagement.Controllers
{
    public class RoomController : Controller 
    {
//
        private readonly HotelManagementDbContext _context;
        private readonly IRoomService roomsService;
        private readonly IMailSenderService _mailSenderService;
        private readonly IBillService _billService;
        public RoomController(HotelManagementDbContext context, IRoomService service, 
            IMailSenderService mailSenderService, IBillService billService)
        {
            _context = context;
            roomsService = service;
            _mailSenderService = mailSenderService;
            _billService = billService;
        }
        public IActionResult RoomView()
        {
            var rooms = _context.Rooms.Include(r => r.RoomType).ToList(); 
            var roomTypes = _context.RoomTypes.ToList();
            

            var model = new RoomViewModel
            {
                Room = rooms,
                NewRoom = new Room(),
                RoomTypes = roomTypes ,
                EditRoom = new Room()
            };

            return View(model);
        }

        //Fshirja e nje dhome
        //26/01/2025

        [HttpPost]
        public IActionResult DeleteRoom(int id)
        {
            try
            {
                // Check if the room exists
                var room = roomsService.DeleteRoom(id);
                if (room == false)
                {
                    return Json(new { success = false, message = "Delete room" });
                }
                return Json(new { success = true, message = "Room deleted successfully." });
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                return Json(new { success = false, message = "An error occurred while deleting the room." });
            }
        }



        //Marsel
        //metoda per shtimin e nje dhome
        //15/01/2024
        [HttpPost]
        public IActionResult CreateRoomSql(NewRoomDTO model)
        {
            try {
                //SendEmail();
                var isSaved = false;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if(model.RoomId != 0)
            {
                isSaved = roomsService.EditRoom(new CreateRoom
                {
                    RoomId = model.RoomId,
                    RoomFloor = (int)model.RoomFloor,
                    RoomNumber = model.RoomNumber,
                    RoomTypeId = (int)model.RoomTypeId,
                });
            }
            else {
                isSaved = roomsService.AddRoom(new CreateRoom
                {
                    RoomFloor = (int)model.RoomFloor,
                    RoomNumber = model.RoomNumber,
                    RoomTypeId = (int)model.RoomTypeId,
                });
            }
                if (isSaved)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, errors = new[] { "Failed to save the room. Please try again." } });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = new[] { "An unexpected error occurred. Please try again later." } });

            }
        }
        [HttpGet]
        //public async Task<IActionResult> SendEmail()
        //{
        //    try { 
        //    await _mailSenderService.SendEmailAsync("selishira2017@gmail.com", "Test Subject", "This is a test email.");
        //    return Ok("Email sent successfully!");
        //    }
        //    catch(Exception ex)
        //    {
        //        return Ok("Email sent successfully!");

        //    }
        //}


        //public IActionResult GenerateBill(int rezervimId)
        //{
        //    // Call the GenerateBillPdf method to get the PDF as a byte array
        //    byte[] billPdf = _billService.GenerateBillPdf(rezervimId);

        //    // Return the PDF file as a download response
        //    return File(billPdf, "application/pdf", "HotelBill.pdf");
        //}

        public async Task<IActionResult> GetRoomList()
        {
            try
            {
                var rooms = await roomsService.GetRoomsAsync();

                var roomDtos = rooms.Select(room => new NewRoomDTO
                {
                    RoomId = room.RoomId,         
                    RoomTypeId = room.RoomTypeId, 
                    RoomFloor = room.RoomFloor,   
                    RoomNumber = room.RoomNumber  ,
                    RoomTypeName = room.RoomTypeName,
                    BasePrice = room.Price
                }).ToList();


                return Json(roomDtos);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
