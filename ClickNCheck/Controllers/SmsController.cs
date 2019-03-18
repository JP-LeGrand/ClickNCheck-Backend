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
using ClickNCheck.Services;

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

            string body = responses.Body.Trim();
            try
            {
                if(body.Contains(" "))
                {
                    string[] arBody = body.Split(" ");
                    if (arBody.Count() != 2)
                        throw new Exception();
                    try
                    {
                        verificationID = Convert.ToInt32(arBody[0]);
                        answer = arBody[1];
                    }
                    catch (Exception)
                    {
                        verificationID = Convert.ToInt32(arBody[1]);
                        answer = arBody[0];
                    }
                    
                }
                else
                {
                    //TODO
                    bool prevCharWasAlph = true;
                    foreach (char c in body.ToLower())
                    {
                        if(prevCharWasAlph && c <= 'z' && c >= 'a' )
                        {
                            //you have an alpha again
                        }
                        else if (!prevCharWasAlph && c <= 'z' && c >= 'a')
                        {
                            //you have an alpha again
                        }
                    }
                }
            }
            catch (Exception)
            {
                messagingResponse.Message($"Response not known!: {responses.Body}");
                return TwiML(messagingResponse);
            }
            messagingResponse.Message($"We are happy to confirm to have recieved you consent: {responses.Body}" );

            SMSService smsServ = new SMSService();

            await smsServ.PutConsent(verificationID, phoneNumber, answer);

            return TwiML(messagingResponse);
        }
    }
}
