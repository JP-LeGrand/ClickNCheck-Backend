using Microsoft.AspNetCore.Mvc;
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


        //GET: api/<controller>
        //[HttpGet]
        //public string Get()
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
        [Route("Compuscan")]
        public async void Compuscan()
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/Compuscan", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("Umalusi")]
        public async void Umalusi()
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/Umalusi", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("Afiswitch")]
        public async void Afiswitch()
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/Afiswitch", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("LexisNexis")]
        public async void LexisNexis(string value)
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/LexisNexis", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("MIE")]
        public async void MIE()
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/MIE", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("PNet")]
        public async void PNet()
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/PNet", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("Transunion")]
        public async void Transunion()
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/Transunion", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("XDS")]
        public async void XDS()
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/XDS", soapText);
            _soap.Process(response);
        }

        // POST api/<controller>/therest
        [HttpGet]
        [Route("Experian")]
        public async void Experian()
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/Experian", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("SAPS")]
        public async void SAPS()
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/SAPS", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("FSCA")]
        public async void FSCA()
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/FSCA", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("INSETA")]
        public async void INSETA()
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/INSETA", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("TrafficDepartment")]
        public async void TrafficDepartment()
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/TrafficDepartment", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("HomeAffairs")]
        public async void HomeAffairs()
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/HomeAffairs", soapText);
            _soap.Process(response);
        }
        // POST api/<controller>/therest
        [HttpGet]
        [Route("SAQA")]
        public async void SAQA()
        {
            string response = await _soap.connectXML("https://localhost:44393/api/soapcheck/SAQA", soapText);
            _soap.Process(response);
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
