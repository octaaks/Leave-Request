using Leave_Request.Models;
using Leave_Request.ViewModels;
using NETCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Leave_Request.Repositories.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, int>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
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
