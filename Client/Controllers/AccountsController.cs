using Client.Base.Controllers;
using Client.Repositories.Data;
using Leave_Request.Models;
using Leave_Request.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, int>
    {
        private readonly AccountRepository repository;
        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Register")]
        public JsonResult Register([FromBody] RegistrationVM entity)
        {
            var result = repository.Register(entity);
            return Json(result);
        }

        [HttpPost("Auth/")]
        public async Task<IActionResult> Auth(string email, string password)
        {
            LoginVM loginVM = new LoginVM();
            loginVM.Email = email;
            loginVM.Password = password;

            var login = new LoginVM { Email = email, Password = password };

            var jwtToken = await repository.Auth(loginVM);
            var token = jwtToken.Token;

            if (token == null)
            {
                return RedirectToAction("index","home");
            }

            HttpContext.Session.SetString("JWToken", token);
            //HttpContext.Session.SetString("Email", login.Email);

            return RedirectToAction("dashboard", "home");
        } 

        [HttpGet("Logout/")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "home");
        }
    }
}
