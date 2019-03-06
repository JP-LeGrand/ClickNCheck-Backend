using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ClickNCheck.Services.ResponseService;

namespace ClickNCheck.Services.Classifier
{
    public class ResidencyClassifier : IClassifier
    {
        public IResponseService ResponseService(MailMessage mailmessage)
        {
            IResponseService response = null;
            if (mailmessage.Subject.Contains("Residency"))
            {
                response = new LongRunningMailResidency();
            }
            return response;
        }
    }
}
