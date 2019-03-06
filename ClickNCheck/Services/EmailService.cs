﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Linq;
using ClickNCheck.Models;
using System.Net.Mime;
using System.IO;
using System.Reflection;
using Limilabs.Client.IMAP;
using Limilabs.Mail;
using ClickNCheck.Services.ResponseService;

namespace ClickNCheck
{
    public class EmailService
    {
        readonly string smtpAddress = "smtp.gmail.com";
        readonly string popAddress = "pop.gmail.com";
        readonly int portNumber = 587;
        readonly int popPort = 995;
        readonly bool enableSSL = true;
        readonly string emailFromAddress = "clickncheckservice@gmail.com"; //Sender Email Address
        readonly string password = "clickncheck@123"; //Sender Password

        public bool SendMail(string To, string Subject, string Body)
        {
            try
            {
                using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
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

        private void MailAssignment(System.Net.Mail.MailMessage mailMessage, string From, string To, string Subject, string Body)
        {
            mailMessage.From = new System.Net.Mail.MailAddress(From);
            mailMessage.To.Add(To);
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = Body;
            
        }

        private void SmtpSend(System.Net.Mail.MailMessage mail)
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

            string emailBody = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\CandidateEmail.html"));

            return emailBody;
        }

        public string CandidateConsentedMail()
        {
            string htmlstring  = File.ReadAllText(@"C:\repos\Click-N-Check-Backend\ClickNCheck\Files\CandidateConsentedMail.html");
            /*string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Files\CandidateConsentedMail.html");
            string htmlstring = System.IO.File.ReadAllText(path);*/
            return htmlstring;
        }

        public void CheckMails()
        {
            using (Imap client = new Imap())
            {
                client.ConnectSSL("imap.gmail.com", 993);
                client.UseBestLogin(emailFromAddress, password);
                client.SelectInbox();

                MailMessage smtpMail = new MailMessage();

                List<long> uids = client.Search(Flag.Unseen);
                foreach (long uid in uids)
                {
                    var eml = client.GetMessageByUID(uid);
                    IMail mail = new MailBuilder().CreateFromEml(eml);

                    try
                    {
                        //process emails
                    }
                    catch(Exception ex)
                    {
                        //set message to unread.
                    }
                }
            }
        }
    }
}

