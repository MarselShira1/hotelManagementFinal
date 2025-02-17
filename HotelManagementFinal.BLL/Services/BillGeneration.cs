using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using hotelManagement.Domain.Models;
using hotelManagement.BLL.Services;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using System.ComponentModel.DataAnnotations;
using HotelManagementFinal.Domain.Models;
using hotelManagement.DAL.Persistence.Entities;
using Microsoft.AspNetCore.Http;

namespace HotelManagementFinal.BLL.Services
{
    public interface IBillService
    {
        Task<byte[]> GenerateBillPdf(int? rezervimId, GenerateBillModel? generateBill = null, string userName = "");

    }
    internal class BillService : IBillService
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IRoomRateService _roomRateService;
        private readonly IExtraServiceService _extraServiceService;


        public BillService(IAuthService authService, IBookingService bookingService,
            IUserService userService, IRoomService roomService, IRoomTypeService roomTypeService,
            IRoomRateService roomRateService, IExtraServiceService extraServiceService)
        {
            _authService = authService;
            _bookingService = bookingService;
            _userService = userService;
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            _roomRateService = roomRateService;
            _extraServiceService = extraServiceService;
        }
        public async Task<byte[]> GenerateBillPdf(int? rezervimId, GenerateBillModel? generateBill, string userName = "")
        {
            RezervimModel rezervimDetails = new RezervimModel(); 
            CreateRoom roomDetails;
            TipDhome roomType;
            RoomRate roomRate;
            int numberOfDays;
            decimal roomPrice;
            decimal totalRoomPrice;
            decimal totalBillPrice;
            List<ExtraServiceModel> extraServices = new List<ExtraServiceModel>();
            if (rezervimId.HasValue)
            {
                // Fetch reservation details if rezervimId is provided
                rezervimDetails = await _bookingService.GetRezervimById(rezervimId.Value);
                var user = _userService.GetUserById(rezervimDetails.UserId);
                userName = user.Emer + " " + user.Mbiemer;
                roomDetails = await _roomService.GetRoomById(rezervimDetails.DhomeId);
                roomType = _roomTypeService.GetRoomTypeById((int)roomDetails.RoomTypeId);
                roomRate =   _roomRateService.GetRoomRateById(rezervimDetails.RoomRateId);
                numberOfDays = (rezervimDetails.CheckOut.ToDateTime(TimeOnly.MinValue) - rezervimDetails.CheckIn.ToDateTime(TimeOnly.MinValue)).Days;
                roomPrice = roomType.CmimBaze * roomRate.RateMultiplier;
                extraServices = await _extraServiceService.GetExtraService(rezervimDetails.Id);

            }
            else if (generateBill != null)
            {
                roomDetails = await _roomService.GetRoomById(generateBill.roomId);
                roomType = _roomTypeService.GetRoomTypeById((int)roomDetails.RoomTypeId);
                roomRate = _roomRateService.GetRoomRateById(generateBill.roomRateId);
                numberOfDays = (generateBill.CheckOut.ToDateTime(TimeOnly.MinValue) - generateBill.CheckIn.ToDateTime(TimeOnly.MinValue)).Days;
                roomPrice = roomType.CmimBaze * roomRate.RateMultiplier;
                rezervimDetails.CheckIn = generateBill.CheckIn;
                rezervimDetails.CheckOut = generateBill.CheckOut;
            }
            else
            {
                throw new ArgumentException("Either rezervimId or generateBill must be provided.");
            }

            totalRoomPrice = roomPrice * numberOfDays;
            totalBillPrice = totalRoomPrice;


            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                // Add Hotel Name
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                Paragraph title = new Paragraph("Hotel ISE - Invoice", titleFont) { Alignment = Element.ALIGN_CENTER };
                document.Add(title);
                document.Add(new Paragraph("\n"));

                // Add Customer Details
                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                document.Add(new Paragraph("Customer Details:", headerFont));
                document.Add(new Paragraph("Name: " + userName));
                document.Add(new Paragraph("Room Number: " + roomDetails.RoomNumber));
                document.Add(new Paragraph("Check-in Date: " + rezervimDetails.CheckIn));
                document.Add(new Paragraph("Check-out Date: " + rezervimDetails.CheckOut));
                document.Add(new Paragraph("\n"));

                // Add Bill Details in Table Format
                PdfPTable table = new PdfPTable(3) { WidthPercentage = 100 };
                table.SetWidths(new float[] { 50f, 20f, 30f });

                table.AddCell(new PdfPCell(new Phrase("Description", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("Days", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("Amount ($)", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });

                table.AddCell($"Room Charges (${roomPrice})");
                table.AddCell(numberOfDays.ToString());
                table.AddCell(totalRoomPrice.ToString());

                if (extraServices.Count > 0)
                {
                    foreach (var extraService in extraServices)
                    {
                        table.AddCell(extraService.Name);
                        table.AddCell("-");
                        table.AddCell($"${extraService.Price}");
                        totalBillPrice += extraService.Price;
                    }
                }

                PdfPCell totalCell = new PdfPCell(new Phrase("Total Amount", headerFont)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_RIGHT };
                table.AddCell(totalCell);
                table.AddCell(new PdfPCell(new Phrase(totalBillPrice.ToString(), headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });

                document.Add(table);
                document.Close();

                return stream.ToArray();
            }
        }
    }
}
