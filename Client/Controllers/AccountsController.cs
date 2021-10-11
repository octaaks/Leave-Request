using Client.Base.Controllers;
using Client.Repositories.Data;
using Leave_Request.Models;
using Leave_Request.ViewModel;
using Leave_Request.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Client.Controllers
{
    [Route("[controller]")]
    public class AccountsController : BaseController<Account, AccountRepository, int>
    {
        readonly HttpClient http = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44371/api/")
        };

        private readonly AccountRepository repository;
        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Register/")]
        public JsonResult Register([FromBody] RegistrationVM entity)
        {
            var result = repository.Register(entity);
            return Json(result);
        }

        [HttpPost("Registering")]
        public IActionResult Registering([FromBody] RegistrationVM entity)
        {
            try
            {
                var conv = JsonConvert.SerializeObject(entity);
                var buffer = System.Text.Encoding.UTF8.GetBytes(conv);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = http.PostAsync("accounts/register", byteContent).Result;
                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("ForgotPass")]
        public IActionResult ForgotPass(EmailVM emailVM)
        {
            try
            {
                var conv = JsonConvert.SerializeObject(emailVM);
                var buffer = System.Text.Encoding.UTF8.GetBytes(conv);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = http.PostAsync("accounts/forgot-password", byteContent).Result;
                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("ChangePass")]
        public IActionResult ChangePass(ChangePasswordVM changePasswordVM)
        {
            try
            {
                var conv = JsonConvert.SerializeObject(changePasswordVM);
                var buffer = System.Text.Encoding.UTF8.GetBytes(conv);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = http.PostAsync("accounts/change-password", byteContent).Result;
                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
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
            var employeeid = jwtToken.Id;

            if (token == null)
            {
                return RedirectToAction("index","home");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Session.SetString("Email", login.Email);
            HttpContext.Session.SetString("Id", employeeid);

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
