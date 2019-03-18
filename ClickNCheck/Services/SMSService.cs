using ClickNCheck.Data;
using ClickNCheck.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private ClickNCheckContext _context = new ClickNCheckContext();

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

        //The method below updates the consent to true when candidate approves 
        public async Task<string> PutConsent(int verificationID, string phoneNumber, string answer)
        {
            if (answer.ToLower().Contains("yes"))
            {
                List<Candidate> candidatesList = _context.Candidate.Where(c => c.Phone == phoneNumber).ToList();

                if (candidatesList.Count != 1)
                {
                    return "The unique candidate not found!";
                }

                Candidate candidate = candidatesList.First();

                try
                {
                    List<Candidate_Verification> cvcList = _context.Candidate_Verification.Where(it => it.VerificationCheckID == verificationID).ToList();
                    foreach (Candidate_Verification cvc in cvcList)
                    {
                        if (cvc.CandidateID == candidate.ID)
                        {
                            cvc.HasConsented = true;
                            _context.Candidate_Verification.Update(cvc);
                            await _context.SaveChangesAsync();

                            break;
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateExists(candidate.ID))
                    {
                        return ("Not Found");
                    }
                    else
                    {
                        throw;
                    }
                }

                return ("Consent recieved.");
            }
            return ("Consent not given.");
        }

        private bool CandidateExists(int id)
        {
            return _context.Candidate.Any(e => e.ID == id);
        }
    }
}
