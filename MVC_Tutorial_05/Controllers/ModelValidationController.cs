using Microsoft.AspNetCore.Mvc;
using MVC_Tutorial_05.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Tutorial_05.Controllers
{
    public class ModelValidationController : Controller
    {
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

    }
}
