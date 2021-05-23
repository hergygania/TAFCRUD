using Inspiro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Inspiro.BL;

namespace Inspiro.Controllers
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
            var dp = new DataProcess();

            string msg = string.Empty;
            var response = dp.GetAll(ref msg);
            return View(response);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Insert(KaryawanModel data)
        {
            var dp = new DataProcess();
            string msg = "";
            var response = dp.Insert(data, ref msg);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Test()
        {
            var a = "";

            return Ok(a);
        }
    }
}
