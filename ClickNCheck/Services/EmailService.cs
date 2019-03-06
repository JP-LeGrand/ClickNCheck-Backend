using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using Limilabs.Client.IMAP;
using Limilabs.Mail;
using System.Reflection;
using System.Net.Sockets;
using System.Net.Security;
using Limilabs.Client.POP3;
using MailMessage = System.Net.Mail.MailMessage;
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
        readonly int imapPort = 993;
        readonly bool enableSSL = true;
        readonly string emailFromAddress = "clickncheckservice@gmail.com"; //Sender Email Address
        readonly string password = "clickncheck@123"; //Sender Password
        //IClassifier classifier = new IClassifier();
        static readonly string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";
        readonly string userId = "me";


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
            string response;
            try
            {
                using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                {
                    MailAssignment(mail, emailFromAddress, To, Subject, Body);
                    SmtpSend(mail);
                    response = mail.DeliveryNotificationOptions.ToString();
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
            mailMessage.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
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

        public string verCheckAuthMail()
        {
            string emailBody = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\RequestVerCheckAuth.html"));

            return emailBody;
        }

        public string AuthorizeVerification()
        {
            string emailBody = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\RecruiterNotificationGranted.html"));

            return emailBody;
        }

        public string RefuseVerficationEmail()
        {
            string emailBody = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\RecruiterNotificationRefused.html"));

            return emailBody;
        }

        public void CheckMails(MailMessage smtpMail)
        {
            try
            {
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

        public void ReadEmailsByGmailAPI()
        {
            UserCredential credential;
            // System.Net.Mail.MailMessage mailMessage;
            string credentialPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Services\credentials.json");
            using (var stream =
                new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            //UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");


            UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List(userId);
            // Filter by labels
            request.LabelIds = new List<String>() { "INBOX", "UNREAD" };
            // Get message list
            IList<Message> messages = request.Execute().Messages;
            if ((messages != null) && (messages.Count > 0))
            {
                foreach (var msg in messages)
                {

                    Message m = service.Users.Messages.Get("me", msg.Id).Execute();
                    var a = service.Users.Messages.Attachments;
                    string subject = "";
                    string from = "";
                    string to = "";
                    foreach (var header in m.Payload.Headers)
                    {
                        if (header.Name == "Subject")
                        {
                            subject = header.Value;
                        }
                        else if (header.Name == "From")
                        {
                            from = header.Value;
                        }
                        else if (header.Name == "To")
                        {
                            to = header.Value;
                        }
                    }

                    MailMessage email = new MailMessage();
                    email.Subject = subject;
                    email.From = new System.Net.Mail.MailAddress(from);
                    email.To.Add(to);

                    foreach (MessagePart p in m.Payload.Parts)
                    {
                        string decodedString = "";
                        if (p.MimeType == "text/html")
                        {
                            byte[] data = FromBase64ForUrlString(p.Body.Data);

                            decodedString = Encoding.UTF8.GetString(data);
                            email.Body = decodedString;
                        }


                        if (p.MimeType == "application/pdf")
                        {
                            var attID = p.Body.AttachmentId;
                            MessagePartBody att = service.Users.Messages.Attachments.Get("me", msg.Id, attID).Execute();
                            String attData = att.Data.Replace('-', '+');
                            attData = attData.Replace('_', '/');

                            byte[] file = Convert.FromBase64String(attData);
                            File.WriteAllBytes(@"c:\" + p.Filename, file);
                            email.Attachments.Add(new System.Net.Mail.Attachment(@"c:\" + p.Filename));
                        }
                        CheckMails(email);
                    }

                    request.Q = "is:unread";

                }
            }
            else
            {
                //log Email
            }
        }

        public static byte[] FromBase64ForUrlString(string base64ForUrlInput)
        {
            int padChars = (base64ForUrlInput.Length % 4) == 0 ? 0 : (4 - (base64ForUrlInput.Length % 4));
            StringBuilder result = new StringBuilder(base64ForUrlInput, base64ForUrlInput.Length + padChars);
            result.Append(String.Empty.PadRight(padChars, '='));
            result.Replace('-', '+');
            result.Replace('_', '/');
            return Convert.FromBase64String(result.ToString());
        }

    }
}

