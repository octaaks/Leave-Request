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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        public IActionResult Dashboard()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Email = HttpContext.Session.GetString("Email");
                ViewBag.Id = HttpContext.Session.GetString("Id");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult LeaveRequest()
        { 
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Email = HttpContext.Session.GetString("Email");
                ViewBag.Id = HttpContext.Session.GetString("Id");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
        public IActionResult ManageLeaveRequest()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Approver"))
            {
                ViewBag.Id = HttpContext.Session.GetString("Id");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult LeaveTypes()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Id = HttpContext.Session.GetString("Id");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
       
        public IActionResult Employees()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Administrator"))
            {
                ViewBag.Id = HttpContext.Session.GetString("Id");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ChangePassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Email = HttpContext.Session.GetString("Email");
                ViewBag.Id = HttpContext.Session.GetString("Id");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
