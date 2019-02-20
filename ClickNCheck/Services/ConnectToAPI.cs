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
using System.Xml.Linq;

namespace ClickNCheck.Services
{
    public class ConnectToAPI
    {
        private readonly int apiType;
        private readonly string apiURL;
        private HttpWebResponse response;
        private Object inputForm;

        public ConnectToAPI(int apitype, string url, Object inputform)
        {
            apiType = apitype;
            apiURL = url;
            inputForm = inputform;
        }
        public HttpWebResponse run()
        {
            try
            {
                switch (apiType)
                {
                    case 0:
                        response = connectXML(apiURL, (XmlDocument)inputForm);
                        return response;
                    case 1:
                        response = connectJSON(apiURL, (JObject)inputForm);
                        return response;
                    default:
                        throw new Exception("Unknown API type");
                }
            }
            catch (Exception e)

            {
                Console.WriteLine("Could not send internet message. "+e.Source);
                return null;
            };
        }

        //TODO
        private HttpWebResponse connectJSON(string url, JObject form)
        {
            throw new Exception();
        }

        private HttpWebResponse connectXML(string url, XmlDocument inputForm)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("./NormalEnquirySOAPRequest.xml");
                string xmlText = fillValuesIntoTemplate(inputForm, xmlDoc).ToString();
                //xml document content in a single string.
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

        private XmlDocument fillValuesIntoTemplate(XmlDocument formFromFrontend, XmlDocument xmlDocument)
        {
            /* For each entry in formFromFrontend find them in the xml
             * and put them in the xml template.
             */
            
            return xmlDocument;
        }
    }
}
