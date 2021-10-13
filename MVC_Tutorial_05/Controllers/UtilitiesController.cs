using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MVC_Tutorial_05.Controllers
{
    public class UtilitiesController : Controller
    {
        public string SendEmail()
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("mvc.20210820@gmail.com");
            mail.To.Add("mvc.20210820@gmail.com");
            mail.Subject = "MVC 寄信測試";
            mail.Body = $"<h1>Hello World2!</h1>";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(
                "mvc.20210820@gmail.com", "mvcpassword0822");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
                smtp.Dispose();
                mail.Dispose();
            }
            catch
            {
                throw;
            }

            return "郵件已發送";
        }
    }
}
