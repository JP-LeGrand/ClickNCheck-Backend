using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ClickNCheck.Services
{
    public class SMSService
    {
        readonly string accountSid = "AC6d8cdd1e99e0b7139a3df9530633b241";
        readonly string authToken = "79c9bc4cfd2a11ce18d91fcc01bef0af";
        readonly PhoneNumber fromNumber = new PhoneNumber("+19704328601");
        public void SendSMS(string messageBody, string userNumber)
        {
            //Initialize Client
            TwilioClient.Init(accountSid,authToken);

            //Convert Number string into phone number
            PhoneNumber toNumber = new PhoneNumber(userNumber);

            //SendSMS
            var message = MessageResource.Create(
              to: toNumber,
              from: fromNumber,
              body: messageBody);
        }
    }
}
