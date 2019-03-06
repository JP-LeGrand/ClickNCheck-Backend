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

        public bool changedChecks(ClickNCheckContext _context, int loggedInRecID, int candidateID, int verCheckID, Candidate_Verification candidateVerification, Candidate_JobProfile candidate_JobProfile)
        {
            var verChecks = _context.Candidate_Verification_Check.Where(x => x.Candidate_VerificationID == candidateVerification.ID).ToList();

            var jobChecks = _context.JobProfile_Check.Where(x => x.JobProfileID == candidate_JobProfile.JobProfileID).ToList();
            if (verChecks.Count == jobChecks.Count)
            {
                for (int i = 0; i < verChecks.Count; i++)
                {
                    if (verChecks.ElementAt(i).Order != jobChecks.ElementAt(i).Order)
                    {
                        var recruiter = _context.User.Find(loggedInRecID);
                        var manager = _context.User.Find(recruiter.ManagerID);
                        string checks = "";
                        foreach (var check in verChecks)
                        {
                            checks = "<li>" + check.Services.CheckCategory.Category + "</li>";
                        }

                        EmailService emailService = new EmailService();
                        string emailBody = emailService.verCheckAuthMail();
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
