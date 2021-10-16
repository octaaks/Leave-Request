using Leave_Request.Models;
using Leave_Request.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NETCore.Context;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
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
            if (registrationVM.JoinDate.AddYears(1) <= DateTime.Now)
            {
                employee.TotalLeave = 12;
            }
            myContext.Employees.Add(employee);
            myContext.SaveChanges();

            Account account = new Account();
            account.Id = registrationVM.Id;
            account.Email = registrationVM.Email;
            string saltPassword = BCrypt.Net.BCrypt.GenerateSalt(12);
            account.Password = BCrypt.Net.BCrypt.HashPassword(registrationVM.Password, saltPassword);
            myContext.Accounts.Add(account);
            myContext.SaveChanges();

            AccountRole accountRole = new AccountRole();
            accountRole.AccountId = registrationVM.Id;
            accountRole.RoleId = 1;
            myContext.AccountRoles.Add(accountRole);
            myContext.SaveChanges();

            if (registrationVM.JobId == 6 || registrationVM.JobId == 7)
            {
                AccountRole accountRole_ = new AccountRole();
                accountRole_.AccountId = registrationVM.Id;
                accountRole_.RoleId = 2;
                myContext.AccountRoles.Add(accountRole_);
                myContext.SaveChanges();
            }
            else if (registrationVM.JobId == 2 || registrationVM.JobId == 5)
            {
                AccountRole accountRole_ = new AccountRole();
                accountRole_.AccountId = registrationVM.Id;
                accountRole_.RoleId = 3;
                myContext.AccountRoles.Add(accountRole_);
                myContext.SaveChanges();
            }

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

        public int GetId(string email)
        {
            var checkEmail = myContext.Accounts.Where(e => e.Email == email).FirstOrDefault();
            return checkEmail.Id;
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
                            join ar in myContext.AccountRoles on a.Id equals ar.AccountId
                            join r in myContext.Roles on ar.RoleId equals r.Id
                            where id == ar.AccountId
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

        public JwtSecurityToken GetJWT(int id, LoginVM loginVM)
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

            return token;
        }

        public int ForgotPassword(string email)
        {
            /*Guid myuuid = Guid.NewGuid().ToString();
            string newPassword = myuuid.ToString();*/
            //100 = email ga ketemu

            var account = myContext.Accounts.Where(e => e.Email == email).FirstOrDefault();
            if (account == null)
            {
                return 100;
            }

            //generate token
            account.Token = Guid.NewGuid().ToString();

            // string bodyEmail = $"Here is link for reset your password: https://localhost:44371/api/accounts/reset-password/email={account.Email}&token={account.Token}";
            // SendEmail(bodyEmail, account.Email);
            ResetPassword(email, account.Token);
            myContext.SaveChanges();
            return 1;
        }

        public int ResetPassword(string email, string token)
        {
            //return 100 = token salah
            //return 200 = email salah
            var account = myContext.Accounts.Where(e => e.Email == email).FirstOrDefault();
            if (account == null)
            {
                return 200;
            }
            if (account.Token != token)
            {
                return 100;
            }

            //generate new password
            string passwordReset = Guid.NewGuid().ToString();
            string saltPassword = BCrypt.Net.BCrypt.GenerateSalt(12);
            account.Password = BCrypt.Net.BCrypt.HashPassword(passwordReset, saltPassword);

            //kirim email
            string bodyEmail = $"Your new password : <b>{passwordReset}</b>";
            SendEmail(bodyEmail, account.Email);

            //save ke DB
            Update(account);
            account.Token = null;
            myContext.SaveChanges();

            return 1;
        }

        public int ChangePassword(ChangePasswordVM cpVM)
        {
            //return 200 = email tdk terdaftar
            //return 300 = password salah
            //return 400 = confirm password tdk match

            var account = myContext.Accounts.Where(e => e.Email == cpVM.Email).FirstOrDefault();
            if (account == null)
            {
                return 200;
            }

            if (!BCrypt.Net.BCrypt.Verify(cpVM.OldPassword, account.Password))
            {
                return 300;
            }

            if (cpVM.NewPassword == cpVM.OldPassword)
            {
                return 500;
            }

            if (cpVM.NewPassword != cpVM.ConfirmNewPassword)
            {
                return 400;
            }

            

            //insert new password
            string newPassword = cpVM.ConfirmNewPassword;

            string saltPassword = BCrypt.Net.BCrypt.GenerateSalt(12);
            account.Password = BCrypt.Net.BCrypt.HashPassword(newPassword, saltPassword);

            //save ke DB
            Update(account);

            return 1;
        }

        public int SendEmailToRequester(RequestApprovalVM raVM)
        {
            var account = myContext.Accounts.Where(e => e.Id == raVM.EmployeeId).FirstOrDefault();

            string bodyEmail = $"" +
               $"<p>Berikut adalah status pengajuan cuti anda :</p>" +
               $"<p style='padding-left: 40px;'>Status : <b>{raVM.Status}</b></p>" +
               $"<p style='padding-left: 40px;'>Jenis Cuti : {raVM.LeaveType}</p>" +
               $"<p style='padding-left: 40px;'>Tgl mulai : {raVM.StartDate.ToString("d",CultureInfo.CreateSpecificCulture("en-NZ"))}</p>" +
               $"<p style='padding-left: 40px;'>Tgl selesai : {raVM.EndDate.ToString("d", CultureInfo.CreateSpecificCulture("en-NZ"))}</p>" +
               $"<p style='padding-left: 40px;'>Approver : {raVM.Approver}</p>" +
               $"<p style='padding-left: 40px;'>Catatan : {raVM.Notes}</p>" +
               $"<br>" +
               $"<p>Terima Kasih atas perhatiannya,</p>" +
               $"<p>Salam</p>";

            SendEmail(bodyEmail, account.Email);

            return 1;
        }

        public static void SendEmail(string htmlString, string toMailAddress)
        {
            string fromMail = "#";
            string fromPassword = "#";
            string timeStamp = DateTime.Now.ToString();
            MailMessage message = new MailMessage();

            message.From = new MailAddress(fromMail);
            message.To.Add(new MailAddress(toMailAddress));
            message.Subject = $"Leave Request ({timeStamp})";
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
