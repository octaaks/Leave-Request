using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class ProfilController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Id = HttpContext.Session.GetString("Id");
            return View();
        }
    }
}
