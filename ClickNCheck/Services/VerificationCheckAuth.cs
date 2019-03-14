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

        public void changedChecks(ClickNCheckContext _context, int recID, int verCheckID)
        {
            var verChecks = _context.VerificationCheckChecks.Where(c => c.VerificationCheckID == verCheckID).OrderBy(v=>v.Order);
            
            var recruiter = _context.User.Find(recID);
            var manager = _context.User.Find(recruiter.ManagerID); 
            string checks = "";
            foreach (var check in verChecks)
            {
                var services = _context.Services.Find(check.ServicesID);
                checks += "<li>" + services.Name + "</li>";
            }

            EmailService emailService = new EmailService();
            string emailBody = emailService.verCheckAuthMail();
            emailBody = emailBody.Replace("{ManagerName}", manager.Name + " " + manager.Surname);
            emailBody = emailBody.Replace("{RecruiterName}", recruiter.Name + " " + recruiter.Surname);
            emailBody = emailBody.Replace("{Checks}", checks);
            emailBody = emailBody.Replace("{NO}", Constants.BASE_URL + "Candidates/checkAuth/false/" + verCheckID);
            emailBody = emailBody.Replace("{YES}", Constants.BASE_URL + "Candidates/checkAuth/true/" + verCheckID);

            emailService.SendMail(manager.Email, "Check List Change Authorisation", emailBody);              
        }
    }
}
