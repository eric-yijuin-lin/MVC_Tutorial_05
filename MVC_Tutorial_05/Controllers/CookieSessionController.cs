using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Tutorial_05.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Tutorial_05.Controllers
{
    public class CookieSessionController : Controller
    {
        public IActionResult SetSessionData()
        {
            HttpContext.Session.SetInt32("TestNumber", 12345);
            HttpContext.Session.SetString("UserGuid", Guid.NewGuid().ToString());
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SetCookieData()
        {
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
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SessionTest()
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
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CookiesTest()
        {
            var cookie = HttpContext.Request.Cookies["SimpleCookie"];
            var cookie2 = JsonConvert.DeserializeObject<UserPreference>(
                HttpContext.Request.Cookies["ComplexCookie"]
            );
            return RedirectToAction("Index", "Home");
        }
        public void HeartBeat()
        {
            Console.WriteLine("beep");
        }
    }
}
