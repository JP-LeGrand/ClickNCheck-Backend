using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ClickNCheck.Services.ResponseService;

namespace ClickNCheck.Services.Classifier
{
    public class CreditClassifier : IClassifier
    {
        public IResponseService ResponseService(MailMessage mailmessage)
        {
            IResponseService response = null;
            if (mailmessage.Subject.Contains("Credit Check Ref:"))
            {
                response = new LongRunningMailCredit();
            }
            return response;
        }
    }
}
