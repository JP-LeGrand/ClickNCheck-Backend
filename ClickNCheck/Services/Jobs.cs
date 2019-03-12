using ClickNCheck.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Services
{
    public class Jobs
    {
        private ClickNCheckContext _context;
        EmailService emailService = new EmailService();
        public Jobs()
        {
            _context = new ClickNCheckContext();
        }

        public void CheckUserPasswords()
        {
            var today = DateTime.Now;
            //get all users
            var users = _context.User.ToList();

            //check expiry date for password
            foreach(var user in users)
            {
                //calculate the number of days
                int daysLeft = (user.PasswordExpiryDate - today).Days;
                //if less than 5 notify user
                if (daysLeft <= 5)
                {
                    string body = emailService.PasswordExpiry();
                    body = body.Replace("UserName", user.Name);
                    body = body.Replace("expiryCount", daysLeft.ToString());

                    emailService.SendMail(user.Email, "Password Expiry", body);
                }
            }
        }
    }
}
