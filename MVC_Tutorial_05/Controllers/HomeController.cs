using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_Tutorial_05.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MVC_Tutorial_05.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult IssueCookies()
        {
            HttpContext.Session.SetInt32("TestNumber", 12345);
            HttpContext.Session.SetString("UserGuid", Guid.NewGuid().ToString());

            var cookieOption = new CookieOptions()
            {
                Expires = new DateTimeOffset(DateTime.Now.AddSeconds(30))
            };

            HttpContext.Response.Cookies.Append(
                "SimpleCookie", 
                "cookie value in plain text",
                cookieOption
            );

            var preference = new UserPreference()
            {
                Theme = "dark",
                FontFamily = "Arial",
                FontSize = "12"
            };

            HttpContext.Response.Cookies.Append(
                "ComplexCookie",
                JsonConvert.SerializeObject(preference),
                cookieOption
            );
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CookiesTest()
        {
            var intKey = HttpContext.Session.GetInt32("TestNumber");
            var userGuid = HttpContext.Session.GetString("UserGuid");
            if (string.IsNullOrEmpty(userGuid))
            {
                // Login
            }
            else
            {
                // proceed
            }

            var cookie = HttpContext.Request.Cookies["SimpleCookie"];
            var cookie2 = JsonConvert.DeserializeObject<UserPreference>(
                HttpContext.Request.Cookies["ComplexCookie"]
            );

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ModelValidationTest()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public DemoModelValidationResult ModelValidationTest(DemoModel model)
        {
            var result = new DemoModelValidationResult();
            if (ModelState.IsValid)
            {
                result.IsValid = true;
            }
            else
            {
                result.Errors = new List<ValidationError>();
                foreach (var key in ModelState.Keys)
                {
                    if (ModelState[key].Errors.Count > 0)
                    {
                        result.Errors.Add(new ValidationError() 
                        { 
                            FieldName = key,
                            Message = ModelState[key].Errors[0].ErrorMessage
                        });
                    }
                }
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DemoModel model)
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult jQuery()
        {
            return View();
        }

        public async Task<IActionResult> GetDummySelectList() // 400 badrequest
        {
            await Task.Delay(3000);
            return Json(new List<DemoListItem>()
            {
                new DemoListItem() { Name = "選項 1", Value = "value 1" },
                new DemoListItem() { Name = "選項 2", Value = "value 2" },
                new DemoListItem() { Name = "選項 3", Value = "value 3" },
            });
        }

        public void HeartBeat()
        {
            Console.WriteLine("beep");
        }

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
