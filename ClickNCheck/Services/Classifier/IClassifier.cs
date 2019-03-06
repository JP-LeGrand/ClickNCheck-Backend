using ClickNCheck.Services.ResponseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ClickNCheck.Services.Classifier
{
    public interface IClassifier
    {
        IResponseService ResponseService(MailMessage mailmessage);
    }
}
