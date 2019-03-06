using ClickNCheck.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ClickNCheck.Services.ResponseService
{
    public class LongRunningMailCredit : IResponseService
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
            //process message
        }

        public void SaveResults(int checkID, int checkStatus, string resultDescription)
        {
            //get the checkid

            //upload the files from check

            //update checkstatus {pass, fail} and move to complete if upload works

            //else if upload doesn't work or results unprocessorble change to some issue
        }
    }
}
