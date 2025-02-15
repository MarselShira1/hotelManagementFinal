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
using iText.IO.Font.Constants; // Required for DeviceRgb

namespace HotelManagementFinal.BLL.Services
{
    public interface IBillService
    {
        byte[] GenerateBillPdf(int rezervimId);
    }
    internal class BillService : IBillService
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;

        public BillService(IAuthService authService, IBookingService bookingService,
            IUserService userService, IRoomService roomService) {
            _authService = authService;
            _bookingService = bookingService;
            _userService = userService;
            _roomService = roomService;
        }
        public byte[] GenerateBillPdf(int rezervimId)
        {
            //merr te dhenat e rezervimit
            var rezervimDetails = _bookingService.GetRezervimById(rezervimId);
            var userDetails = _userService.GetUserById(rezervimDetails.Result.UserId);
            var roomDetails = _roomService.GetRoomById(rezervimDetails.Result.DhomeId);
            int numberOfDays = (rezervimDetails.Result.CheckOut.ToDateTime(TimeOnly.MinValue)
                  - rezervimDetails.Result.CheckIn.ToDateTime(TimeOnly.MinValue)).Days;

            //do merret cimimi qe i perkon roomType * roomRateMultiplier per nje dhome qe te shtohet ne fature 
            var price =  _bookingService.CalculatePriceAsync((int)roomDetails.Result.RoomTypeId, rezervimDetails.Result.CheckIn, rezervimDetails.Result.CheckOut);

            using (MemoryStream stream = new MemoryStream())
            {
                // Create a new PDF document
                Document document = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                // Add hotel logo (Replace "logo.png" with your actual path)
                string logoPath = "wwwroot/images/logo.png";
                if (System.IO.File.Exists(logoPath))
                {
                    Image logo = Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(100, 50);
                    logo.Alignment = Element.ALIGN_CENTER;
                    document.Add(logo);
                }

                // Add Hotel Name
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                Paragraph title = new Paragraph("Hotel ISE - Invoice", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                document.Add(new Paragraph("\n"));

                // Add Customer Details
                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                document.Add(new Paragraph("Customer Details:", headerFont));
                document.Add(new Paragraph("Name: "+ userDetails.Emer +" "+ userDetails.Mbiemer));
                document.Add(new Paragraph("Room Number:"+ roomDetails.Result.RoomNumber));
                document.Add(new Paragraph("Check-in Date: "+rezervimDetails.Result.CheckIn));
                document.Add(new Paragraph("Check-out Date: "+ rezervimDetails.Result.CheckOut));
                document.Add(new Paragraph("\n"));

                // Add Bill Details in Table Format
                PdfPTable table = new PdfPTable(3);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 50f, 20f, 30f });

                // Add Table Headers
                table.AddCell(new PdfPCell(new Phrase("Description", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("Days", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("Amount ($)", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });

                // Add Charges
                table.AddCell("Room Charges ($50 per day)");
                table.AddCell(numberOfDays.ToString());
                table.AddCell("$200");

                table.AddCell("Food & Beverages");
                table.AddCell("-");
                table.AddCell("$50");

                table.AddCell("Service Tax (10%)");
                table.AddCell("-");
                table.AddCell("$25");

                // Add Total Row
                Font totalFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                PdfPCell totalCell = new PdfPCell(new Phrase("Total Amount", totalFont)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_RIGHT };
                table.AddCell(totalCell);
                table.AddCell(new PdfPCell(new Phrase(price.Result.ToString(), totalFont)) { HorizontalAlignment = Element.ALIGN_CENTER });

                document.Add(table);
                document.Add(new Paragraph("\n"));

                // Add Thank You Note

                // For iText 7 or iTextSharp 5 (No color)
                Font footerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                 
                // Close Document
                document.Close();

                // Return PDF as downloadable file
                return stream.ToArray();
            }
        }
    }
}
