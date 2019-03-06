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
using ClickNCheck.Services.Classifier;

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

        AcademicClassifier academicClassifier = new AcademicClassifier();
        AssociationsClassifier associationsClassifier = new AssociationsClassifier();
        CreditClassifier creditClassifier = new CreditClassifier();
        CriminalClassifier criminalClassifier = new CriminalClassifier();
        DriversClassifier driversClassifier = new DriversClassifier();
        EmploymentClassifier employmentClassifier = new EmploymentClassifier();
        IdentityClassifier identityClassifier = new IdentityClassifier();
        PersonalClassifier personalClassifier = new PersonalClassifier();
        ProgrammClassifier programmClassifier = new ProgrammClassifier();
        ResidencyClassifier residencyClassifier = new ResidencyClassifier();


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
            try
            {
                MailMessage smtpMail = new MailMessage();
                smtpMail.Body = "Hello World";
                smtpMail.Subject = "Academic";
                IResponseService response;
                //process emails
                if (smtpMail.Body != null)
                {
                    if((response = academicClassifier.ResponseService(smtpMail))!= null)
                    {
                        //response.Process();
                    }
                    else if((response = associationsClassifier.ResponseService(smtpMail)) != null)
                    {
                        //response.Process();
                    }
                    else if ((response = creditClassifier.ResponseService(smtpMail)) != null)
                    {
                        //response.Process();
                    }
                    else if ((response = criminalClassifier.ResponseService(smtpMail)) != null)
                    {
                        //response.Process();
                    }
                    else if ((response = driversClassifier.ResponseService(smtpMail)) != null)
                    {
                        //response.Process();
                    }
                    else if ((response = employmentClassifier.ResponseService(smtpMail)) != null)
                    {
                        //response.Process();
                    }
                    else if ((response = identityClassifier.ResponseService(smtpMail)) != null)
                    {
                        //response.Process();
                    }
                    else if ((response = personalClassifier.ResponseService(smtpMail)) != null)
                    {
                        //response.Process();
                    }
                    else if ((response = programmClassifier.ResponseService(smtpMail)) != null)
                    {
                        //response.Process();
                    }
                    else if ((response = residencyClassifier.ResponseService(smtpMail)) != null)
                    {
                        //response.Process();
                    }
                    else if ((response = residencyClassifier.ResponseService(smtpMail)) == null)
                    {
                        //response.Process();
                    }
                }
            }
            catch(Exception ex)
            {
                //set message to unread.
            }
        }
    }
}

