using ClickNCheck.Services.ResponseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace ClickNCheck.Services.Classifier
{
    public class CriminalClassifier : IClassifier
    {
        public IResponseService ResponseService(MailMessage mailmessage)
        {
            IResponseService response = null;
            if(mailmessage.Subject.Contains("Results for"))
            {
                response = new LongRunningMailCredit();
            }
            return response;
        }
        internal static object ResponseService(object smtpMail)
        {
            throw new NotImplementedException();
        }
    }
}
