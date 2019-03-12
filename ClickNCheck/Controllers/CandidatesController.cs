using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickNCheck.Data;
using ClickNCheck.Models;
using ClickNCheck.Services;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ClickNCheckContext _context;
        CodeGenerator codeGenerator = new CodeGenerator();
        EmailService service = new EmailService();
        UploadService uploadService = new UploadService();
        VerificationCheckAuth checkAuth = new VerificationCheckAuth();

        public CandidatesController(ClickNCheckContext context)
        {
            _context = context;
        }

        // GET: api/Candidates/GetAllCandidates
        [HttpGet]
        [Route("GetAllCandidates")]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetAllCandidates()
        {
            return await _context.Candidate.ToListAsync();
        }


        // GET: api/Candidates/5
        [HttpGet]
        [Route("GetCandidate/{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(int id)
        {
            var candidate = await _context.Candidate.FindAsync(id);

            if (candidate == null)
            {
                return NotFound();
            }

            return candidate;
        }

        // PUT: api/Candidates/5
        [HttpPut]
        [Route("UpdateCandidate/{id}")]
        public async Task<IActionResult> PutCandidate(int id, Candidate candidate)
        {
            if (id != candidate.ID)
            {
                return BadRequest();
            }

            _context.Entry(candidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet]
        [Route("checkAuth/{val}/{verCheckID}")]
        public void receiveVerCheckAuth(string val, int verCheckID)
        {
            var verCheck = _context.VerificationCheck.Find(verCheckID);
            if (val == "true")
            {
                verCheck.IsAuthorize = true;
                 GetVerificationAuthorization(verCheckID);
            }
            else if(val == "false")
            {
                verCheck.IsAuthorize = false;
                 GetVerificationAuthorization(verCheckID);
            }

            _context.Update(verCheck);
            _context.SaveChanges();
        }


        // DELETE: api/Candidates/5
        [HttpDelete]
        [Route("DeleteCandidate/{id}")]
        public async Task<ActionResult<Candidate>> DeleteCandidate(int id)
        {
            var candidate = await _context.Candidate.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            _context.Candidate.Remove(candidate);
            await _context.SaveChangesAsync();

            return candidate;
        }

        [HttpPost]
        [Route("CreateCandidateJObject/{id}")]
        public async Task<ActionResult<Candidate>> CreateCandidate(JObject jObject, int id)
        {
            int verCount = 0;
            var vc = await _context.VerificationCheck.FindAsync(id);

            var jpChecks = (from s in _context.JobProfile_Check
                            where s.JobProfileID == vc.JobProfileID
                            select s).ToList();

            var vcChecks = new List<Candidate_Verification_Check>();

            JArray array = (JArray)jObject["services"];
            int[] services = array.Select(jv => (int)jv).ToArray();

            JArray jcandidates = (JArray)jObject["candidates"];
            List<Candidate> candidates = ((JArray)jcandidates).Select(x => new Candidate
            {
                Email = (string)x["Email"],
                HasConsented = (bool)x["HasConsented"],
                ID_Passport = (string)x["ID_Passport"],
                ID_Type = (string)x["ID_Type"],
                Maiden_Surname = (string)x["Maiden_Surname"],
                Name = (string)x["Name"],
                Surname = (string)x["Surname"],
                Phone = (string)x["Phone"],
                OrganisationID = (int)x["OrganisationID"]
            }).ToList();
            List<int> candIds = new List<int>();
            int additionID = 0;
            for (int x = 0; x < candidates.Count; x++)
            {
                var entry = await _context.Candidate.FirstOrDefaultAsync(d => d.Email == candidates[x].Email);
                
                if (entry != null)
                {
                    //eturn Ok("user exists");
                    continue;
                }
                else
                {
                    var verChecks = _context.Candidate_Verification_Check.Where(verc => verc.Candidate_VerificationID == id).ToList();
                    string checks = "";
                    foreach (var check in verChecks)
                    {
                        checks += "<li>" + check.Services.CheckCategory.Category + "</li>";
                    }
                    var org = _context.Organisation.FirstOrDefault(o => o.ID == candidates[x].OrganisationID);
                    var mailBody = service.CandidateConsentedMail();
                    mailBody = mailBody.Replace("{CandidateName}", candidates[x].Name);
                    mailBody = mailBody.Replace("{OrganisationName}", org.Name);
                    mailBody = mailBody.Replace("{Checks}", checks);
                    var candidateID = candidates[x].ID;
                    mailBody = mailBody.Replace("{link}", "https://s3.amazonaws.com/clickncheck-frontend-tafara/components/candidate/consent/consent.html" + "?id=" + candidateID+"&vc="+id);
                    try
                    {
                        service.SendMail(candidates[x].Email, "Consent for New Verificaiton Request", mailBody);
                        _context.Candidate.Add(candidates[x]);
                        await _context.SaveChangesAsync();
                        candIds.Add(candidates[x].ID);
                    }
                    catch (Exception e)
                    {
                        return BadRequest("some emails have not sent");
                    }
                    await _context.SaveChangesAsync();
                }

                var candidate_Verification = await _context.Candidate_Verification.ToListAsync();
                //Assign candidates to verification
                for (int i = 0; i < candIds.Count; i++)
                {
                    var candid = await _context.Candidate.FindAsync(candIds[i]);

                    if (candid == null)
                    {
                        return NotFound("The candidate " + candid.Name + candid.Surname + " does not exist");
                    }
                    //add candidates to jverification
                    Candidate_Verification addition = new Candidate_Verification { Candidate = candid, VerificationCheck = vc };
                    if (candidate_Verification.Contains(addition))
                    {
                        continue;
                    }
                    else
                    {
                        vc.Candidate_Verification.Add(addition);
                        await _context.SaveChangesAsync();
                        //run authorization check
                        if (vc.IsAuthorize == false && verCount == 0)
                        {
                            if (!(jpChecks.Count == array.Count))
                            {

                                additionID = addition.ID;
                                verCount++;
                            }
                            else
                            {
                                for (int m = 0; m < jpChecks.Count; m++)
                                {
                                    if (jpChecks[m].ServicesID == (int)array[m])
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        checkAuth.changedChecks(_context, vc.RecruiterID, vc.ID, candidate_Verification[0].ID);
                                        verCount++;
                                    }
                                }
                            }
                        }
                       
                    }
                }

                //update verification object
                _context.Entry(vc).State = EntityState.Modified;
            }

            //make the Candidate_Verification_checks
            var cdList = (from s in _context.Candidate_Verification
                          where s.VerificationCheckID == vc.ID
                          select s).ToList();

            var NotStartedStatus = await  _context.CheckStatusType.FindAsync(5);
            var VC = await  _context.CheckStatusType.FindAsync(5);

            for (int i = 0; i < cdList.Count; i++)
            {
                for (int j = 0; j < services.Length; j++)
                {
                    var s = await _context.Services.FindAsync(services[j]);
                    Candidate_Verification_Check candidate_Verification_Check = new Candidate_Verification_Check();
                    candidate_Verification_Check.Candidate_Verification = cdList[i];
                    candidate_Verification_Check.Services = s;
                    candidate_Verification_Check.Order = j + 1;
                    candidate_Verification_Check.CheckStatusType = NotStartedStatus;
                    _context.Candidate_Verification_Check.Add(candidate_Verification_Check);
                    
                }
            }
            await _context.SaveChangesAsync();
            checkAuth.changedChecks(_context, vc.RecruiterID, vc.ID, additionID);

            return Ok();
        }

        private bool CandidateExists(int id)
        {
            return _context.Candidate.Any(e => e.ID == id);
        }

        ////TODO:
        //// POST: api/Candidates/5/AssignCandidates
        //[HttpPost]
        //[Route("{id}/AssignCandidates")]
        //public async Task<IActionResult> AssignCandidates(int id, [FromBody]int[] ids)
        //{
        //    int jobId = id;

        //    VerificationRequest v = new VerificationRequest();
        //    v.DateStarted = DateTime.Now;

        //    //find job profile
        //    var jobProfile = await _context.JobProfile.FindAsync(jobId);

        //    if (jobProfile == null)
        //    {
        //        return NotFound("This Job Profile does not exist");
        //    }

        //    //find Candidates
        //    for (int i = 0; i < ids.Length; i++)
        //    {
        //        var candidate = await _context.Candidate.FindAsync(ids[i]);

        //        if (candidate == null)
        //        {
        //            return NotFound($"The recruiter {candidate.Name} {candidate.Surname} does not exist");
        //        }
        //        //add Candidates to verification request
        //        v.Candidate_VerificationRequest.Add(new Candidate_VerificationRequest { VerificationRequest = v, Candidate = candidate });

        //    }

        //    //add verification request to job profile
        //    jobProfile.VerificationRequest.Add(v);
        //    //save changes to job profile
        //    _context.Entry(jobProfile).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        var exists = _context.User.Any(e => e.ID == jobId);
        //        if (!JobProfileExists(jobId))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Ok(jobProfile);
        //}

        [HttpPost]
        [Route("changePassword/{id}")]
        public async Task<ActionResult<Candidate>> changePassword(int id, [FromBody]string password)
        {
            var candidate = await _context.Candidate.SingleOrDefaultAsync(c => c.ID == id);

            if (candidate != null)
            {
                return Ok(candidate);
            }
            else
            {
                return BadRequest();
            }
        }

        //The method below updates the consent to true when candidate approves 
        [HttpGet]
        [Route("PutConsent/{id}")]
        public async Task<IActionResult> PutConsent(int id)
        {
            var candidate = _context.Candidate.Where(c => c.ID == id).FirstOrDefault();
            if (id != candidate.ID)
            {
                return BadRequest();
            }

            _context.Entry(candidate).State = EntityState.Modified;

            try
            {
                candidate.HasConsented = true;
                _context.Candidate.Update(candidate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            CandidateConsentedEmail(new int[]{id});

            return Ok("Consent recieved");
        }


        [HttpGet]
        [Route("ConsentCandidate/{id}")]
        public async Task<ActionResult<Candidate>> ConsentCandidate(int id, [FromForm]IFormCollection indemnityFile)
        {
            var candidate = await _context.Candidate.FindAsync(id);
            candidate.HasConsented = true;
            _context.SaveChanges();

            var uploadSuccess = false;
            foreach (var formFile in indemnityFile.Files.ToList())
            {
                if (formFile.Length <= 0)
                {
                    continue;
                }

                using (var ms = new MemoryStream())
                {
                    formFile.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    uploadSuccess = await uploadService.UploadToBlob(formFile.FileName, fileBytes, null);

                }
            }

            if (uploadSuccess)
                return Ok("Upload Success");
            else
                return NotFound("Upload Error");
        }

        private bool JobProfileExists(int id)
        {
            return _context.JobProfile.Any(e => e.ID == id);
        }

        // POST: api/Candidates/CandidateConsentedEmail
        [HttpPost]
        [Route("CandidateConsentedEmail")]
        public ActionResult<JObject> CandidateConsentedEmail([FromBody]int[] candidateIDs)
        {
            JObject results = new JObject();

            foreach (int id in candidateIDs)
            {
                var cnd = _context.Candidate.Find(id);

                if (cnd == null)
                    throw new Exception("failed to find candidate it in database");

                int orgID = cnd.OrganisationID;
                var org =  _context.Organisation.Find(orgID);
                string mailBody = service.CandidateConsentedMail();
                //reformat email content
                mailBody = mailBody.Replace("{CandidateName}", cnd.Name).Replace("{OrganisationName}", org.Name); 
                try
                {
                    bool serv = service.SendMail(cnd.Email, "We have just recieved consent to verification", mailBody);
                    results.Add(id.ToString(), serv);
                }
                catch (Exception)
                {

                }
            }

            return Ok(results);
        }

        //The method will send a recruiter an email based on whether they are authorised to add checks or not to a job profile
        [HttpGet]
        [Route("SendAuthorizationEmail/{id}")]
        public  ActionResult<VerificationCheck> GetVerificationAuthorization(int id)
        {
            //Find the verfification with this id
            var verCheck =  _context.VerificationCheck.Find(id);
            if (verCheck == null)
            {
                return NotFound();
            }

            //Finds a Recruiter with this ID
            var recruiter =  _context.User.Find(verCheck.RecruiterID);
            var manager =  _context.User.Find(recruiter.ManagerID);
            var checks = _context.JobProfile_Check.Where(c => c.JobProfileID == verCheck.JobProfileID).ToList();

            string checklist= "";
            foreach (var item in checks)
            {
                var services = _context.Services.Find(item.ServicesID);
                checklist += "<li>" + services.Name + "</li>";
            }
            //So if the authorization has been set to true send an email to the respective recruiter with an approval email
            if (verCheck.IsAuthorize==true)
            {
                var mailBody = service.AuthorizeVerification();
                mailBody = mailBody.Replace("RecruiterName", recruiter.Name);
                mailBody = mailBody.Replace("{Checks}", checklist);
                mailBody = mailBody.Replace("ManagerName", manager.Name);
                service.SendMail(recruiter.Email, "Authorization Granted", mailBody);
            }
            else if (verCheck.IsAuthorize == false)
            {
                var mailBody = service.RefuseVerficationEmail();
                mailBody = mailBody.Replace("RecruiterName", recruiter.Name);
                mailBody = mailBody.Replace("ManagerName", manager.Name);
                mailBody = mailBody.Replace("{Checks}", checklist);
                service.SendMail(recruiter.Email, "Authorization Refused", mailBody);
            }

            return Ok(verCheck);
        }
        [HttpPost]
        [Route("sendCandidateConsent")]
        public ActionResult<JObject> sendCandidateConsent([FromBody]JArray information)
        {
            List<JToken> listOfCandidates = information.ToList();

            JArray succeededToSend = new JArray();
            JArray failedToSend = new JArray();

            JObject combinedList = new JObject ();
            foreach(JObject candidate in listOfCandidates)
            {
                int candidateId = (int)candidate.SelectToken("candidateId");
                int consentType = (int)candidate.SelectToken("consentType");

                Candidate cnd = _context.Candidate.Find(candidateId);
                string orgName = _context.Organisation.Find(cnd.OrganisationID).Name;
                if (cnd == null)
                    return NotFound("Could not find candidate");

                bool success = false;
                switch (consentType)
                {
                    case 0:
                        //Email
                        EmailService consentEmail = new EmailService();
                        string emailBody = consentEmail.CandidateMail().Replace("{CandidateName}", cnd.Name).Replace("{OrganisationName}", orgName).Replace("#TTT", $"{cnd.ID}");
                        success = consentEmail.SendMail(cnd.Email, "Candidate Consent", emailBody);
                        break;
                    case 1:
                        //SMS
                        string messageBody = $@"Good day {cnd.Name}, {orgName} would like to perform a background check on you. If you know what this is about then please reply with 'YES' to consent.";
                        SMSService consentSMS = new SMSService();
                        consentSMS.SendSMS(messageBody, cnd.Phone);
                        //status
                        success = true;
                        break;
                    default:break;
                }

                if (success)
                    succeededToSend.Add(candidate);
                else failedToSend.Add(candidate);
            }
            combinedList.Add("failedToSend", failedToSend);
            combinedList.Add("succeededToSend", succeededToSend);
            return Ok(combinedList);
        }
    }
}
