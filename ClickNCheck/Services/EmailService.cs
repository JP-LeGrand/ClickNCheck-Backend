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
                return false;
            }
            return true;
        }

        private void MailAssignment(MailMessage mailMessage, string From, string To, string Subject, string Body)
        {
            mailMessage.From = new MailAddress(From);
            mailMessage.To.Add(To);
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = Body;
            
        }

        private void SmtpSend(MailMessage mail)
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
            string emailBody = File.ReadAllText(@"..\ClickNCheck\Files\RecruiterEmail.html");
            return emailBody;
        }

       public string CandidateMail()
        {
            string emailBody = File.ReadAllText(@"C:\repos\Clink-N-Check-Backend\ClickNCheck\Files\CandidateEmail.html");
            //use relative paths from now on. not the absolute
            return emailBody;
        }

        public string CandidateConsentedMail()
        {
            string htmlstring  = File.ReadAllText(@"C:\repos\Click-N-Check-Backend\ClickNCheck\Files\CandidateConsentedMail.html");
            /*string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Files\CandidateConsentedMail.html");
            string htmlstring = System.IO.File.ReadAllText(path);*/
            return htmlstring;
        }
    }
}

