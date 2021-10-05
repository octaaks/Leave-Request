using Leave_Request.Models;
using Leave_Request.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NETCore.Context;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Leave_Request.Repositories.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, int>
    {
        private readonly MyContext myContext;
        public IConfiguration configuration;

        public AccountRepository(IConfiguration configuration, MyContext myContext) : base(myContext)
        {
            this.configuration = configuration;
            this.myContext = myContext;
        }

        public string Registration(RegistrationVM registrationVM)
        {
            Employee employee = new Employee();
            employee.Id = registrationVM.Id;
            employee.Name = registrationVM.Name;
            employee.Address = registrationVM.Address;
            employee.gender = (Employee.Gender)registrationVM.gender;
            employee.Phone = registrationVM.Phone;
            employee.BirthDate = registrationVM.BirthDate;
            employee.Salary = registrationVM.Salary;
            employee.JoinDate = registrationVM.JoinDate;
            employee.ReligionId = registrationVM.ReligionId;
            employee.JobId = registrationVM.JobId;
            myContext.Employees.Add(employee);
            myContext.SaveChanges();

            Account account = new Account();
            account.Id = registrationVM.Id;
            account.Email = registrationVM.Email;
            string saltPassword = BCrypt.Net.BCrypt.GenerateSalt(12);
            account.Password = BCrypt.Net.BCrypt.HashPassword(registrationVM.Password, saltPassword);
            myContext.Accounts.Add(account);
            myContext.SaveChanges();

            return "ok";
        }

        public string Checking(int id, string Email, string Phone)
        {
            if (myContext.Employees.Where(e => e.Id == id).Count() != 0)
            {
                return "Register unsuccessfull! Id has been register";
            }
            else if (myContext.Accounts.Where(a => a.Email == Email).Count() != 0)
            {
                return "Register unsuccessfull! Email has been register";
            }
            else if (myContext.Employees.Where(e => e.Phone == Phone).Count() != 0)
            {
                return "Register unsuccessfull! Phone has been register";
            }

            return "successful";
        }

        public int CheckLoginEmail(string email)
        {
            var checkLoginEmail = (from a in myContext.Accounts where a.Email == email select new LoginVM { Id = a.Id }).ToList();
            if (checkLoginEmail.Count == 0)
                return 0;
            return checkLoginEmail[0].Id;
        }

        public bool CheckLoginPassword(int id, string password)
        {
            var checkLoginPass = (from e in myContext.Employees
                                  join a in myContext.Accounts on e.Id equals a.Id
                                  where e.Id == id
                                  select new LoginVM
                                  {
                                      Id = e.Id,
                                      Password = a.Password
                                  }).ToList();

            if (BCrypt.Net.BCrypt.Verify(password, checkLoginPass[0].Password))
                return true;
            return false;
        }

        public string[] GetRoles(int id)
        {
            var getRoles = (from a in myContext.Accounts
                            join ar in myContext.AccountRoles on a.Id equals ar.Id
                            join r in myContext.Roles on ar.RoleId equals r.Id
                            where id == ar.Id
                            select new Role
                            {
                                Name = r.Name
                            }).ToList();
            string[] GetRoles = new string[getRoles.Count];
            for (int i = 0; i < getRoles.Count; i++)
            {
                GetRoles[i] = getRoles[i].Name;
            }
            return GetRoles;
        }

        public void GetJWT(int id, LoginVM loginVM)
        {
            string[] roles = GetRoles(id);
            var Claims = new List<Claim>();
            Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            Claims.Add(new Claim("Id", id.ToString()));
            Claims.Add(new Claim("Email", loginVM.Email));
            foreach (string role in roles)
            {
                Claims.Add(new Claim("roles", role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims: Claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);
        }   

        public int ForgetPassword(string email)
        {
            /*Guid myuuid = Guid.NewGuid().ToString();
            string newPassword = myuuid.ToString();*/
            //100 = email ga ketemu

            var account = myContext.Accounts.Where(e => e.Email == email).FirstOrDefault();
            if (account == null)
            {
                return 100;
            }
            //generate new password
            string passwordReset = Guid.NewGuid().ToString();
            account.Password = BCrypt.Net.BCrypt.HashPassword(passwordReset);

            string bodyEmail = $"Password anda yang baru adalah {account.Password}";
            SendEmail(bodyEmail, account.Email);
            myContext.SaveChanges();
            return 1;
        }

        public int ChangePassword(ChangePasswordVM cpVM)
        {
            //return 100 = NIK tdk terdaftar
            //return 200 = email tdk terdaftar
            //return 300 = password salah
            //return 400 = confirm password tdk match

            var account = myContext.Accounts.Where(e => e.Email == cpVM.Email).FirstOrDefault();
            if (account == null)
            {
                return 200;
            }

            //var pass = myContext.Accounts.Where(a => a.Password == cpVM.OldPassword).FirstOrDefault();
            //if (pass == null)
            //{
            //    return 300;
            //}

            if (!BCrypt.Net.BCrypt.Verify(cpVM.OldPassword, account.Password))
            {
                return 300;
            }

            if (cpVM.NewPassword != cpVM.ConfirmNewPassword)
            {
                return 400;
            }

            //insert new password
            string newPassword = cpVM.ConfirmNewPassword;
            account.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            //save ke DB
            Update(account);

            return 1;
        }
        public static void SendEmail(string htmlString, string toMailAddress)
        {
            string fromMail = "octa.aks@gmail.com";
            string fromPassword = "######";
            MailMessage message = new MailMessage();

            message.From = new MailAddress(fromMail);
            message.To.Add(new MailAddress(toMailAddress));
            message.Subject = "Test";
            message.Body = "<html><body>" + htmlString + "<html><body>";
            message.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,

            };
            smtpClient.Send(message);
        }
    }
}
