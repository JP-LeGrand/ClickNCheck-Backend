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
        readonly string accountSid = "AC7377f313abdd9e003a9bee0b1e523c3f";
        readonly string authToken = "752c3e3782dbbcf36aaa0d2e77f041d9";
        readonly PhoneNumber fromNumber = new PhoneNumber("+12019891465");
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
