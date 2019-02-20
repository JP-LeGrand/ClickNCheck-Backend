using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Linq;
using ClickNCheck.Models;
using System.Net.Mime;
using System.IO;
using ClickNCheck.Data;

namespace ClickNCheck
{
    public class EmailService
    {

        readonly string smtpAddress = "smtp.gmail.com";
        readonly int portNumber = 587;
        readonly bool enableSSL = true;
        readonly string emailFromAddress = "dlaminixolani440@gmail.com"; //Sender Email Address
        readonly string password = "159357OLANI"; //Sender Password
         

        public bool SendMail(string To, string Subject, string Body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {

                    MailAssignment(mail, emailFromAddress, To, Subject, Body);
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

        public string generateCode()
        {
            ClickNCheckContext _context = new ClickNCheckContext();
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random rand = new Random();

            string code = new string(Enumerable.Repeat(characters, 10).Select(s => s[rand.Next(s.Length)]).ToArray());

           /* while (_context.LinkCodes.FirstOrDefault(c => c.Code == code) != null)
            {
                code = new string(Enumerable.Repeat(characters, 10).Select(s => s[rand.Next(s.Length)]).ToArray());

            } */

            return code;
        }



        public string randomNumberGenerator()
        {
            Random generator = new Random();
            String r = generator.Next(0, 999999).ToString("D6");
            return r;
        }
    }
}

