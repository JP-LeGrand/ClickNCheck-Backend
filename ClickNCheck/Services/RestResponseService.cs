using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClickNCheck.Data;
using ClickNCheck.Services.ResponseService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json.Linq;

namespace ClickNCheck.Services
{
    public class RestResponseService : IResponseService
    {
        private readonly ClickNCheckContext _context;
        private readonly UploadService _uploadService;

        public RestResponseService(ClickNCheckContext context, UploadService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }


        public async void ProcessAsync(Object responseObject)
        {
            int checkID;
            string resultStatus;
            string resultDescription;
            IFormCollection resultFiles = null;

            JObject rObject = (JObject)responseObject;
            checkID = 1;

            if((bool)rObject["check"])
            {
                resultStatus = "Cleared";
                resultDescription = "The check has been cleared";
            }
            else
            {
                resultStatus = "Failed";
                resultDescription = "the check has been failed";
            }  
            
            CheckResultService resultService = new CheckResultService(_context, _uploadService);
            await resultService.SaveResult(checkID, resultStatus, resultDescription, resultFiles);
        }
    }
}
