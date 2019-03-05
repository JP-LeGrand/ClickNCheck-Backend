using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Services.ResponseService
{
    public class LongRunningEndPointResponseService : IResponseService
    {
        void IResponseService.Process(object responseObject)
        {
            JObject o = (JObject)JToken.FromObject(responseObject);

            //TODO:
            //saveResult(o["CheckId"], o["CheckStatus"], o["ResultStatus"], o["ResultDescription"], o["ResultFiles"]);
        }
    }
}
