using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementFinal.BLL.Services
{

    public interface IMailSenderService {
        Task SendEmailAsync(string to, string subject, string body);
    }


    internal class MailSenderService : IMailSenderService
    {
        private readonly IConfiguration _configuration;

        public MailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public   Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var mail = "m4rselsh@gmail.com";
                var pw = "ghbk kwtr hxkj gqyt";
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mail, pw)
                };
                return client.SendMailAsync(
                    new MailMessage(from: mail, to: to, subject, body));
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }

    }
}
