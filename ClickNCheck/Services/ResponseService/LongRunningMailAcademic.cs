using ClickNCheck.Data;
using ClickNCheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ClickNCheck.Services.ResponseService
{
    public class LongRunningMailAcademic : IResponseService
    {
        public static ClickNCheckContext _context = new ClickNCheckContext();
        public static UploadService _uploadService = new UploadService();
        CheckResultService resultService = new CheckResultService(_context, _uploadService);
        public void Process(object responseObject)
        {
            throw new NotImplementedException();
        }

        public void Process(MailMessage message)
        {
            int checkid = 1;
            //process message
            //SaveResult
        }
    }
}
