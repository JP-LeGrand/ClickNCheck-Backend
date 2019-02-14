using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Linq;
using ClickNCheck.Models;
using System.Net.Mime;

namespace ClickNCheck
{
    public class EmailService
    {
        string smtpAddress = "smtp.gmail.com";
        int portNumber = 587;
        bool enableSSL = true;
<<<<<<< HEAD
        string emailFromAddress = "dlaminixolani440@gmail.com"; //Sender Email Address  
        string password = "159357OLANI"; //Sender Password  
=======
        string emailFromAddress = "dlaminixolani440@gmail.com"; //Sender Email Address
        string password = "159357OLANI"; //Sender Password
>>>>>>> dev

        public bool SendMail(string To, string Subject, string Body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
<<<<<<< HEAD
                    MailAssignment(mail, emailFromAddress, To, Subject, Body);//"<a href = 'https://localhost:44312/api/Organization/signup/"+Body +"'>Xolani</h1>");
=======
                    MailAssignment(mail, emailFromAddress, To, Subject, Body);
>>>>>>> dev
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
            SmtpClient smtp = new SmtpClient(smtpAddress, portNumber);
            smtp.Credentials = new NetworkCredential(emailFromAddress, password);
            smtp.EnableSsl = enableSSL;
            smtp.Send(mail);
        }


    }
<<<<<<< HEAD
}
=======
}
>>>>>>> dev
