using Microsoft.AspNetCore.Mvc;
using MVC_Tutorial_05.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Tutorial_05.Controllers
{
    public class JQueryTestController : Controller
    {
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


    }
}
