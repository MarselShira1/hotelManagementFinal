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
        byte[] GenerateBillPdf();
    }
    internal class BillService : IBillService
    {
        public BillService() { }
        public byte[] GenerateBillPdf()
        {
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
                Paragraph title = new Paragraph("Hotel XYZ - Invoice", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                document.Add(new Paragraph("\n"));

                // Add Customer Details
                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                document.Add(new Paragraph("Customer Details:", headerFont));
                document.Add(new Paragraph("Name: John Doe"));
                document.Add(new Paragraph("Room Number: 101"));
                document.Add(new Paragraph("Check-in Date: 2025-02-01"));
                document.Add(new Paragraph("Check-out Date: 2025-02-05"));
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
                table.AddCell("4");
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
                table.AddCell(new PdfPCell(new Phrase("$275", totalFont)) { HorizontalAlignment = Element.ALIGN_CENTER });

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
