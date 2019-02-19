using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace ClickNCheck.Services
{
    public class ConnectToAPI
    {
        private readonly int apiType;
        private readonly string apiURL;
        private HttpWebResponse soapResponse;
        private HttpWebResponse restResponse;
        private Object inputForm;

        public ConnectToAPI(int apitype, string url, Object inputform)
        {
            apiType = apitype;
            apiURL = url;
            inputForm = inputform;
        }
        public HttpWebResponse run()
        {
            switch (apiType)
            {
                case 0:
                    soapResponse = connectXML(apiURL, inputForm);
                    return soapResponse;
                case 1:
                    restResponse = connectJSON(apiURL, inputForm); 
                    return restResponse;
                default: break;
            }
            return null;
        }

        private HttpWebResponse connectJSON(string url, Object form)
        {
            throw new Exception();
        }

        private HttpWebResponse connectXML(string url, Object inputForm)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("./NormalEnquirySOAPRequest.xml");
                string xmlText = fillValuesIntoTemplate(inputForm, xmlDoc).ToString();
                return send(url, xmlText);
            }

            catch (Exception) { return null; }
        }

        private HttpWebResponse send(string destinationUrl, string xmlText)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(xmlText);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            return (HttpWebResponse)request.GetResponse();
        }

        private XmlDocument fillValuesIntoTemplate(object formObj, XmlDocument xmlDocument)
        {
            /* For each entry in formObj find them in the xml
             * and put them in the xml template.
             */
            return xmlDocument;
        }
    }
}
