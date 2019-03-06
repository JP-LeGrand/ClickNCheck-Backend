using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ClickNCheck.Services.ResponseService;
using System.IO;
using System.IO.Compression;
using System.Text;
using ClickNCheck.Services;
using System.Xml.Linq;
using Telerik.JustMock.Helpers;
using ClickNCheck.Data;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace ClickNCheck
{
    public class SOAPResponse : IResponseService
    {
        ClickNCheckContext clickNCheckContext = new ClickNCheckContext();
        UploadService UploadService = new UploadService();



        public async void Process(Object check)
        {
            CheckResultService checkResultService = new CheckResultService(clickNCheckContext, UploadService);
            
            XDocument doc = XDocument.Parse(check.ToString());
            int checkId = int.Parse(doc.Descendants("checkid").Single().Value);
            string checkStatus = doc.Descendants("checkstatus").Single().Value;
            string resultStatus = doc.Descendants("resultstatus").Single().Value;
            string resultDescription = doc.Descendants("resultDescription").Single().Value;
            string retData = doc.Descendants("resultData").Single().Value;

            await checkResultService.SaveResult(checkId, resultStatus, resultDescription, null);

            
        }

      
        public static bool makeZipFile(string path, string yourBase64String)
        {
            File.WriteAllBytes(@path, Convert.FromBase64String(yourBase64String));
            return true;
        }

        public static bool extractTheZip(string zipFilepath, string dest)
        {
            ZipFile.ExtractToDirectory(zipFilepath, dest);
            return true;
        }

        public   async Task<string> connectXML(string url, string soapText)
        {
            if (true)
            {
                var webWebServiceUrl = @url;
                var req = (HttpWebRequest)WebRequest.Create(webWebServiceUrl);
                req.ContentType = "text/xml"; //"application/soap+xml;";
                req.Method = "POST";

                using (Stream stm = await req.GetRequestStreamAsync())
                {
                    using (var stmw = new StreamWriter(stm))
                    {
                        stmw.Write(soapText);
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
