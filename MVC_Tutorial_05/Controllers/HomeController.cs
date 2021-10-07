using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_Tutorial_05.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            HttpContext.Session.SetString("TextKey", "Key123");
            HttpContext.Session.SetInt32("IntKey", 12345);

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
            var textKey = HttpContext.Session.GetString("TextKey");
            var intKey = HttpContext.Session.GetInt32("IntKey");
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

        public void HeartBeat()
        {
            Console.WriteLine("beep");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
