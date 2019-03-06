using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ClickNCheck.Data;
using ClickNCheck.Services;
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

        public RestResponseService()
        {
        }

        public async void Process(Object responseObject)
        {
            int checkID;
            string resultStatus;
            string resultDescription;
            IFormCollection resultFiles = null;

            JObject rObject = JObject.FromObject(responseObject);
            checkID = 1;

            if((bool)rObject["checkResult"])
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

        public async Task<string> connectJson(string url, JObject jsonText)
        {
            if (true)
            {
                var webWebServiceUrl = @url;
                var req = (HttpWebRequest)WebRequest.Create(webWebServiceUrl);
                req.ContentType = "text/json"; //"application/soap+xml;";
                req.Method = "POST";

                using (Stream stm = await req.GetRequestStreamAsync())
                {
                    using (var stmw = new StreamWriter(stm))
                    {
                        stmw.Write(jsonText);
                    }
                }

                var responseHttpStatusCode = HttpStatusCode.Unused;
                string responseText = null;

                WebResponse webResponse = req.GetResponse();
                var reader = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();

                return reader;
            }
            throw new Exception();
        }
    }
}
