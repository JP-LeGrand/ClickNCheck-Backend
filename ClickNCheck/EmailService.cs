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
        string emailFromAddress = "dlaminixolani440@gmail.com"; //Sender Email Address  
        string password = "159357OLANI"; //Sender Password  
       
        public bool SendMail(string To, string Subject, string Body, string code)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    MailAssignment(mail, emailFromAddress, To, Subject, Body);//"<a href = 'https://localhost:44312/api/Organization/signup/"+Body +"'>Xolani</h1>");
                   SmtpSend(mail);
                }
            }
            catch(Exception e)
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
          //  mailMessage.Attachments.Add(new Attachment("C:\\Users\\Mpinane Mohale\\Desktop\\Standard Bank\\Mine\\RegistrationEmail\\main.png", MediaTypeNames.Image.Jpeg));
        }

        public void SmtpSend(MailMessage mail)
        {
            SmtpClient smtp = new SmtpClient(smtpAddress, portNumber);
            smtp.Credentials = new NetworkCredential(emailFromAddress, password);
            smtp.EnableSsl = enableSSL;
            smtp.Send(mail);
        }

        public string generateCode()
        {
            
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random rand = new Random();

            string code = new string(Enumerable.Repeat(characters, 10).Select(s => s[rand.Next(s.Length)]).ToArray());

         /*  while (_context.LinkCodes.Find(code) != null)
            {
                code = new string(Enumerable.Repeat(characters, 10).Select(s => s[rand.Next(s.Length)]).ToArray());
            }*/

            return code;
        }
    }
}
