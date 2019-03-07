using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClickNCheck.Data;
using ClickNCheck.Models;

namespace ClickNCheck.Services
{
    public class VerificationCheckAuth
    {

        public VerificationCheckAuth()
        {
        }

        public bool changedChecks(ClickNCheckContext _context, int loggedInRecID, int candidateID, int verCheckID, List<Candidate_Verification> verChecks, List<Candidate_Verification> jobChecks)
        {
            if (verChecks.Count == jobChecks.Count)
            {
                for (int i = 0; i < verChecks.Count; i++)
                {
                    if (verChecks.ElementAt(i).VerificationCheck != jobChecks.ElementAt(i).VerificationCheck)
                    {
                        var recruiter = _context.User.Find(loggedInRecID);
                        var manager = _context.User.Find(recruiter.ManagerID);
                        string checks = "";
                        foreach (var check in verChecks)
                        {
                            checks = "<li>" + check.VerificationCheck.Title + "</li>";
                        }

                        EmailService emailService = new EmailService();
                        string emailBody = emailService.CandidateMail(); //[FIX THIS]
                        emailBody = emailBody.Replace("{ManagerName}", manager.Name + " " + manager.Surname);
                        emailBody = emailBody.Replace("{RecruiterName}", recruiter.Name + " " + recruiter.Surname);
                        emailBody = emailBody.Replace("{Checks}", checks);
                        emailBody = emailBody.Replace("{NO}", Constants.BASE_URL + "Candidates/checkAuth/false/" + verCheckID);
                        emailBody = emailBody.Replace("{YES}", Constants.BASE_URL + "Candidates/checkAuth/true/" + verCheckID);

                        emailService.SendMail(manager.Email, "Check List Change Authorisation", emailBody);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
