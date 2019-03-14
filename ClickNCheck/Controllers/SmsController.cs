using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using ClickNCheck.Data;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : TwilioController
    {
        [HttpPost]
        [Route("ReceiveConsent")]
        public async Task<TwiMLResult> Index([FromHeader]SmsRequest responses)
        {
            int verificationID = 0;
            string answer = null;
            string phone = responses.From;
            string phoneNumber = $"+{ phone.Substring(3)}";
            var messagingResponse = new MessagingResponse();

            try
            {
                string[] arBody = responses.Body.Trim().Split(" ");
                verificationID = Convert.ToInt32(arBody[0]);
                answer = arBody[1];
            }
            catch (Exception)
            {
                messagingResponse.Message($"Response not known!: {responses.Body}");
                return TwiML(messagingResponse);
            }
            messagingResponse.Message($"We are happy to confirm to have recieved you consent: {responses.Body}" );

            CandidatesController candidates = new CandidatesController(new ClickNCheckContext());

            await candidates.PutConsent(verificationID, phoneNumber, answer);

            return TwiML(messagingResponse);
        }
    }
}
