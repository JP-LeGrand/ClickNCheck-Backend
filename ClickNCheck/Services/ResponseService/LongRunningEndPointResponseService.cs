using ClickNCheck.Data;
using ClickNCheck.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Services.ResponseService
{
    public class LongRunningEndPointResponseService : IResponseService
    {
        async void IResponseService.Process(object responseObject)
        {
            ClickNCheckContext clickNCheckContext = new ClickNCheckContext();
            UploadService uploadService = new UploadService();
            JObject o = (JObject)JToken.FromObject(responseObject);

            CheckResultService checkResultService = new CheckResultService(clickNCheckContext, uploadService);
            byte[] file = Convert.FromBase64String(o["file"].ToString());

           
            string resultStatus = (string)o["resultStatus"];
            string resultDescription = (string)o["resultDesciption"];
            //int checkID = (o["CheckId"]).ToObject<int>(); ;
            JToken token = (o["checkId"]);
            int value = token.ToObject<int>();

            //File.WriteAllBytes(@"c:\result.pdf", file);

            //IFormFileCollection formFiles = new FormFileCollection();
            //var fFile = formFiles.GetFile("c:\result.pdf"); 
            //formFiles.Append(fFile);

            await checkResultService.SaveResult(value, resultStatus, resultDescription, null);
        }

    }
}
