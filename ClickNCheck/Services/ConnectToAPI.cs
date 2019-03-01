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
using ClickNCheck.Models;
using System.Text;
using System.IO.Compression;

namespace ClickNCheck.Services
{
    public class ConnectToAPI
    {
        private int apiType;
        private string apiURL;

        public ConnectToAPI()
        {}

        public async Task<string> runCheck(int apitype, string url, Candidate candidateDatabaseModel)
        {
            apiType = apitype;
            apiURL = url;
            try
            {
                switch (apiType)
                {
                    case 0:
                        string soapText = fillSOAPCredentials(candidateDatabaseModel);
                        return await connectXML(apiURL, soapText);
                    case 1:
                        JObject jsonMessage = fillRESTCredentials(candidateDatabaseModel);
                        return await connectJSON(apiURL, form: jsonMessage);
                    default:
                        throw new Exception("Unknown API type");
                }
            }
            catch (Exception e)
            {
                return null;
            };
        }

        private JObject fillRESTCredentials(Candidate candidateDatabaseModel)
        {
            throw new NotImplementedException();
        }

        private string fillSOAPCredentials(Candidate candidateDatabaseModel)
        {
            int id = candidateDatabaseModel.ID;
            string id_pass = candidateDatabaseModel.ID_Passport;
            string name = candidateDatabaseModel.Name,
                surname = candidateDatabaseModel.Surname;
            //RETURN FILLED SOAP TEXT
            return soapText;
        }

        //TODO REST API
        private async Task<string> connectJSON(string url, JObject form)
        {
            throw new Exception();
        }

        private async Task<string> connectXML(string url, string soapText)
        {
            if(true)
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

                using (var response = (HttpWebResponse)req.GetResponseAsync().Result)
                {
                    responseHttpStatusCode = response.StatusCode;

                    if (responseHttpStatusCode == HttpStatusCode.OK)
                    {
                        int contentLength = (int)response.ContentLength;

                        if (contentLength > 0)
                        {
                            int readBytes = 0;
                            int bytesToRead = contentLength;
                            byte[] resultBytes = new byte[contentLength];

                            using (var responseStream = response.GetResponseStream())
                            {
                                while (bytesToRead > 0)
                                {
                                    // Read may return anything from 0 to 10. 
                                    int actualBytesRead = responseStream.Read(resultBytes, readBytes, bytesToRead);

                                    // The end of the file is reached. 
                                    if (actualBytesRead == 0)
                                        break;

                                    readBytes += actualBytesRead;
                                    bytesToRead -= actualBytesRead;
                                }

                                responseText = System.Text.Encoding.UTF8.GetString(resultBytes);
                            }
                        }
                    }
                }


                return responseText;
            }
            throw new Exception();
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
        public static string extractCompuscanRetValue(string res)
        {
            if (res.Contains("<transactionCompleted>true</transactionCompleted>"))
            {
                int start = (res.IndexOf("<retData>") + "<retData>".Length);
                int end = res.IndexOf("</retData>");
                return res.Substring(start, end - start);
            }
            return null;
        }

        public static string decodeBase64(string base64EncodedData)
        {   
            return Encoding.UTF8.GetString(System.Convert.FromBase64String(base64EncodedData));
        }

        const string soapText =
        @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://webServices/"">
        <soapenv:Header/>
        <soapenv:Body>
        <web:DoNormalEnquiry>
        <!--Optional:-->    
        <request>
        <pUsrnme>77985-1</pUsrnme>
        <pPasswrd>devtest</pPasswrd>
        <pVersion>1.0</pVersion>
        <pOrigin>CC-SOAPUI</pOrigin>
        <pOrigin_Version>5.2.1</pOrigin_Version>
        <pInput_Format>XML</pInput_Format>
        <pTransaction><![CDATA[<Transactions>
							<Search_Criteria>
								<CS_Data>Y</CS_Data>
								<CPA_Plus_NLR_Data>Y</CPA_Plus_NLR_Data>
								<Deeds_Data>N</Deeds_Data>
								<Directors_Data>N</Directors_Data>
								<Identity_number>8209147250087</Identity_number>
								<Surname>Doe</Surname>
								<Forename>John</Forename>
								<Forename2></Forename2>
								<Forename3></Forename3>
								<Gender>M</Gender>
								<Passport_flag>N</Passport_flag>
								<DateOfBirth>19820914</DateOfBirth>
								<Address1>10 Mars Street</Address1>
								<Address2>Mars</Address2>
								<Address3></Address3>
								<Address4></Address4>
								<PostalCode>1234</PostalCode>
								<HomeTelCode></HomeTelCode>
								<HomeTelNo></HomeTelNo>
								<WorkTelCode></WorkTelCode>
								<WorkTelNo></WorkTelNo>
								<CellTelNo></CellTelNo>
								<ResultType>XPDF2</ResultType>
								<RunCodix>N</RunCodix>
								<CodixParams></CodixParams>
								<Adrs_Mandatory>Y</Adrs_Mandatory>
								<Enq_Purpose>12</Enq_Purpose>
								<Run_CompuScore>Y</Run_CompuScore>
								<ClientConsent>Y</ClientConsent>            
							</Search_Criteria>
						</Transactions>]]>
                    </pTransaction>
        </request>
        </web:DoNormalEnquiry>
        </soapenv:Body>
        </soapenv:Envelope>";
    }
}
