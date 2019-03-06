using ClickNCheck.Data;
using ClickNCheck.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

            await checkResultService.SaveResult((int)o["CheckId"], (string)o["resultStatus"], (string)o["resultDescription"], (IFormCollection)o["resultFiles"]);
        }
    }
}
