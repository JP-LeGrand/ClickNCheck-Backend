using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Linq;
using ClickNCheck.Models;
using System.Net.Mime;
using System.IO;

namespace ClickNCheck
{
    public class EmailService
    {
<<<<<<< HEAD:ClickNCheck/Services/EmailService.cs
        readonly string smtpAddress = "smtp.gmail.com";
        readonly int portNumber = 587;
        readonly bool enableSSL = true;
        readonly string emailFromAddress = "dlaminixolani440@gmail.com"; //Sender Email Address
        readonly string password = "159357OLANI"; //Sender Password
=======
        string smtpAddress = "smtp.gmail.com";
        int portNumber = 587;
        bool enableSSL = true;

        string emailFromAddress = "dlaminixolani440@gmail.com"; //Sender Email Address  
        string password = "159357OLANI"; //Sender Password  


>>>>>>> Dev:ClickNCheck/EmailService.cs

        public bool SendMail(string To, string Subject, string Body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
<<<<<<< HEAD:ClickNCheck/Services/EmailService.cs
                    MailAssignment(mail, emailFromAddress, To, Subject, Body);
=======

                    MailAssignment(mail, emailFromAddress, To, Subject, Body);//"<a href = 'https://localhost:44312/api/Organization/signup/"+Body +"'>Xolani</h1>");

>>>>>>> Dev:ClickNCheck/EmailService.cs
                    SmtpSend(mail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public void MailAssignment(MailMessage mailMessage, string From, string To, string Subject, string Body)
        {
            mailMessage.From = new MailAddress(From);
            mailMessage.To.Add(To);
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = Body;
        }

        public void SmtpSend(MailMessage mail)
        {
            SmtpClient smtp = new SmtpClient(smtpAddress, portNumber)
            {
                Credentials = new NetworkCredential(emailFromAddress, password),
                EnableSsl = enableSSL
            };
            smtp.Send(mail);
        }

        public string RecruiterMail()
        {
            string emailBody = System.IO.File.ReadAllText(@"..\ClickNCheck\Files\RecruiterEmail.html");
            return emailBody;
        }
    }
<<<<<<< HEAD:ClickNCheck/Services/EmailService.cs
}
=======

}

>>>>>>> Dev:ClickNCheck/EmailService.cs
