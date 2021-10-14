using Client.Models;
using Client.Repositories.Data;
using Leave_Request.Models;
using Leave_Request.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Home");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Id = HttpContext.Session.GetString("Id");
            return View();
        }

        public IActionResult LeaveRequest()
        {
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Id = HttpContext.Session.GetString("Id");

            return View();
        }
        public IActionResult ManageLeaveRequest()
        {
            ViewBag.Id = HttpContext.Session.GetString("Id");
            return View();
        }

        public IActionResult ChangePassword()
        {
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Id = HttpContext.Session.GetString("Id");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
