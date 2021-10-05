using Leave_Request.Models;
using Leave_Request.Repositories.Data;
using Leave_Request.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Leave_Request.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, int>
    {
        private readonly AccountRepository repository;
        public AccountsController(AccountRepository repository) : base(repository)
        {

            this.repository = repository;
        }
        public ActionResult ChangePassword(ChangePasswordVM cpVM)
        {
            if (ModelState.IsValid)
            {
                //
            }
            int output = repository.ChangePassword(cpVM);
            if (output == 100)
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    message = "NIK tdk terdaftar!"
                });
            }
            else if (output == 200)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Email tdk terdaftar!"
                });
            }
            else if (output == 300)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Password lama salah!"
                });
            }
            else if (output == 400)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Konfirmasi password tidak sama!"
                });
            }
            return Ok(new
            {
                status = HttpStatusCode.OK,
                message = "Success ganti password!"
            });
        }
    }
}
