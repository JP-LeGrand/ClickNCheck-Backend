using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Services
{
    public class ConnectToAPI
    {
        private readonly string apiType;
        private readonly string apiURL;

        public ConnectToAPI(string apitype, string url)
        {
            apiType = apitype.Trim();
            apiURL = url;
        }
        public bool postToAPI()
        {
            switch (apiType.ToLower())
            {
                case "soap":
                    connectXML();
                    break;
                case "rest":
                    connectJSON();
                    break;
                default:break;
            }
            return true;
        }

        private void connectJSON()
        {
            throw new Exception();
        }

        private void connectXML()
        {
            throw new Exception();
        }
    }
}
