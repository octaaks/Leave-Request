using Leave_Request.Models;
using Leave_Request.Repositories.Data;
using Leave_Request.ViewModel;
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
        public IConfiguration configuration;

        public AccountsController(IConfiguration configuration, AccountRepository repository) : base(repository)
        {
            this.configuration = configuration;
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
                if (id == 0)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new
                    {
                        status = (int)HttpStatusCode.NotFound,
                        message = "Login Unsuccessfull. Email not found"
                    });
                }
                else if ((repository.CheckLoginPassword(id, loginVM.Password)))
                {
                    return Ok(new JWTokenVM
                    {
                        Messages = "Login successfull !!!",
                        Token = new JwtSecurityTokenHandler().WriteToken(repository.GetJWT(id, loginVM)),
                        Id = repository.GetId(loginVM.Email).ToString()
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

        [HttpPost("forgot-password")]
        public ActionResult ForgotPassword(EmailVM email)
        {
            int output = repository.ForgotPassword(email.Email);
            if (output == 100)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Email Not Found",
                });
            }
            return Ok(new
            {
                status = HttpStatusCode.OK,
                message = "Your new password has been sent to your email!"
            });
        }

        [HttpGet("reset-password/email={Email}&token={Token}")]
        public ActionResult ResetPassword(string Email, string Token)
        {
            int output = repository.ResetPassword(Email, Token);
            if (output == 100)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Wrong Token !",
                });
            }
            else if (output == 200)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Wrong Email !",
                });
            }
            return Ok(new
            {
                statusCode = StatusCode(200),
                status = HttpStatusCode.OK,
                message = "New Password has been sent to your email!"
            });
        }

        [HttpPost("SendEmailToRequester")]
        public ActionResult SendEmailToRequester(RequestApprovalVM raVM)
        {
            int output = repository.SendEmailToRequester(raVM);

            return Ok(new
            {
                statusCode = StatusCode(200),
                status = HttpStatusCode.OK,
                message = "Email has been sent!"
            });
        }

        [HttpPost("change-password")]
        public ActionResult ChangePassword(ChangePasswordVM cpVM)
        {
            if (ModelState.IsValid)
            {
                //
            }
            int output = repository.ChangePassword(cpVM);
            
            if (output == 200)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Email not registered!"
                });
            }
            else if (output == 300)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Wrong old password!"
                });
            }
            else if (output == 400)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Wrong on confirmation new password!"
                });
            }
            else if (output == 500)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "New password must be different from old password!"
                });
            }
            return Ok(new
            {
                status = HttpStatusCode.OK,
                message = "Password change successfully!"
            });
        }
    }
}
