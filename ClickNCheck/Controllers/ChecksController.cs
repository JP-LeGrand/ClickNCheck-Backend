using ClickNCheck.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Controllers
{

    [Route("api/[controller]")]
    public class ChecksController : Controller
    {
        SOAPResponse _soap = new SOAPResponse();
        RestResponseService _rest = new RestResponseService();

        //TODO:
        //GET: api/<controller>
        //[HttpGet]
        //public string Get(int resType)
        //{
        //    return "ping - responded";
        //}
        //POST api/<controller>
        //[HttpGet]
        //public string Post(string value)
        //{
        //    return "Specify the vendor in the route!";
        //}

        // POST api/<controller>/compuscan
        [HttpGet]
        [Route("{resType}/Compuscan")]
        public async void Compuscan(int resType)
        {
            if (resType == 0)
            {
                string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/Compuscan", soapText);
                _soap.Process(response);
            }
            else
            {
                string response = await _rest.connectJson("checkserviceapi.azurewebsites.net/api/RestCheck/Compuscan", jsonObject);
                _rest.Process(JObject.Parse(response));
            }
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("{resType}/Umalusi")]
        public async void Umalusi(int resType)
        {
            string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/Umalusi", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("{resType}/Afiswitch")]
        public async void Afiswitch(int resType)
        {
            string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/Afiswitch", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("{resType}/LexisNexis")]
        public async void LexisNexis(string value)
        {
            string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/LexisNexis", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("{resType}/MIE")]
        public async void MIE(int resType)
        {
            string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/MIE", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("{resType}/PNet")]
        public async void PNet(int resType)
        {
            string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/PNet", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("{resType}/Transunion")]
        public async void Transunion(int resType)
        {
            string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/Transunion", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("{resType}/XDS")]
        public async void XDS(int resType)
        {
            string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/XDS", soapText);
            _soap.Process(response);
        }

        // POST api/<controller>/therest
        [HttpGet]
        [Route("{resType}/Experian")]
        public async void Experian(int resType)
        {
            string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/Experian", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("{resType}/SAPS")]
        public async void SAPS(int resType)
        {
            string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/SAPS", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("{resType}/FSCA")]
        public async void FSCA(int resType)
        {
            string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/FSCA", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("{resType}/INSETA")]
        public async void INSETA(int resType)
        {
            string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/INSETA", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("{resType}/TrafficDepartment")]
        public async void TrafficDepartment(int resType)
        {
            string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/TrafficDepartment", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("{resType}/HomeAffairs")]
        public async void HomeAffairs(int resType)
        {
            string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/HomeAffairs", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("{resType}/SAQA")]
        public async void SAQA(int resType)
        {
            string response = await _soap.connectXML("checkserviceapi.azurewebsites.net/api/soapcheck/SAQA", soapText);
            _soap.Process(response);
        }


        JObject jsonObject = new JObject(new JProperty("name", "John"),
             new JProperty("surname", "Doe"),
             new JProperty("id", "998087543211"));

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
