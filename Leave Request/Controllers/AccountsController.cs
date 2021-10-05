using Leave_Request.Models;
using Leave_Request.Repositories.Data;
using Leave_Request.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NETCore.Base;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
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

        [HttpPost("Register")]
        public ActionResult Register(RegistrationVM reg)
        {
            try
            {
                if (repository.Checking(reg.Id, reg.Email, reg.Phone) == "successful")
                {
                    repository.Registration(reg);
                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        message = "Register Successfull"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = repository.Checking(reg.Id, reg.Email, reg.Phone)
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = e.Message
                });
            }
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            try
            {
                int id = repository.CheckLoginEmail(loginVM.Email);
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new
                    {
                        status = (int)HttpStatusCode.NotFound,
                        message = "Login Unsuccessfull. Email not found"
                    });
                }
                else if ((repository.CheckLoginPassword(id, loginVM.Password)))
                {
                   // repository.GetJWT(id, loginVM);
                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        message = "Login successfull !!!",
                        //token = new JwtSecurityTokenHandler().WriteToken(token),
                        //tokenexpired = token.ValidTo
                    });

                    /*return Ok(new JWTokenVM
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Message = "Login Success!"
                    });*/
                }
                else
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Login unsuccessfull. Wrong password!"
                    });
                    /*return Ok(new JWTokenVM
                    {
                        Token = null,
                        Message = "Login Unseccessful!"
                    });*/
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = e.Message
                });
            }
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
