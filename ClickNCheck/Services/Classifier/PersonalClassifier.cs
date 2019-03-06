using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ClickNCheck.Services.ResponseService;

namespace ClickNCheck.Services.Classifier
{
    public class PersonalClassifier : IClassifier
    {
        public IResponseService ResponseService(MailMessage mailmessage)
        {
            IResponseService response = null;
            if (mailmessage.Subject.Contains("Personal"))
            {
                response = new LongRunningMailPersonal();
            }
            return response;
        }
    }
}
