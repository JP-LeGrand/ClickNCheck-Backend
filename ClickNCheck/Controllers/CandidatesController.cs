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

        // POST: api/Candidates
        [HttpPost]
        [Route("CreateCandidate/{id}")]
        public async Task<ActionResult<Candidate>> CreateCandidate(Candidate[] candidate, int id)
        {
            Candidate_JobProfile _candidate_jp = new Candidate_JobProfile();
            for (int x = 0; x < candidate.Length; x++)

            {
                var entry = await _context.Candidate.FirstOrDefaultAsync(d => d.Email == candidate[x].Email);
                if (entry != null)
                {
                    return Ok("user exists");
                }
                else
                {

                    var org = _context.Organisation.FirstOrDefault(o => o.ID == candidate[x].Organisation.ID);
                    var mailBody = service.CandidateMail();
                    mailBody = mailBody.Replace("{CandidateName}", candidate[x].Name);
                    mailBody = mailBody.Replace("{OrganisationName}", org.Name);
                    var candidateID = candidate[x].ID;
                    mailBody = mailBody.Replace("{link}", "https://s3.amazonaws.com/clickncheck-frontend-tafara/components/candidate/consent/consent.html" + "?id=" + candidateID);
                    try
                    {
                        service.SendMail(candidate[x].Email, "New Verificaiton Request", mailBody);
                        _context.Candidate.Add(candidate[x]);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        return BadRequest("some emails have not sent");
                    }
                    _candidate_jp.CandidateID = candidate[x].ID;
                    _candidate_jp.JobProfileID = id;

                    _context.Candidate_JobProfile.Add(_candidate_jp);
                    await _context.SaveChangesAsync();
                }

               /* TODO: Use this to check if the checks have been changed
                VerificationCheckAuth verificationCheckAuth = new VerificationCheckAuth();
                verificationCheckAuth.changedChecks(_context, User.Claims.First, candidate[x].ID ....)
                */

            }
            return Ok();
        }

        [HttpPost]
        [Route("checkAuth/{val}/{verCheckID}")]
        public void receiveVerCheckAuth(string val, int verCheckID)
        {
            var verCheck = _context.VerificationCheck.Find(verCheckID);
            if (val == "true")
            {
                verCheck.IsAuthorize = true;
            }
            else if(val == "false")
            {
                verCheck.IsAuthorize = false;
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
        [HttpPut]
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

            return Ok(candidate);
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
        public async Task<ActionResult<JObject>> CandidateConsentedEmail([FromBody]int[] candidateIDs)
        {
            JObject results = new JObject();

            foreach (int id in candidateIDs)
            {
                var cnd = _context.Candidate.Find(id);

                if (cnd == null)
                    throw new Exception("failed to find candidate it in database");

                int orgID = 1;
                //orgID = cnd.Organisation.ID;

                var org = _context.Organisation.Find(orgID);
                string mailBody = service.CandidateConsentedMail();
                //reformat email content
                mailBody = mailBody.Replace("CandidateName", cnd.Name).Replace("OrganisationName", org.Name); ;
                try
                {
                    bool serv = service.SendMail(cnd.Email, "We have just recieved consent to verification", mailBody);
                    results.Add(id.ToString(), serv);
                }
                catch (Exception e)
                {

                }
            }

            return results;
        }

        //The method will send a recruiter an email based on whether they are authorised to add checks or not to a job profile
        [HttpGet]
        [Route("SendAuthorizationEmail/{id}")]
        public async Task<ActionResult<VerificationCheck>> GetVerificationAuthorization(int id)
        {
            //Find the verfification with this id
            var verCheck = await _context.VerificationCheck.FindAsync(id);
            if (verCheck == null)
            {
                return NotFound();
            }

            //Finds a Recruiter with this ID
            var recruiter = await _context.User.FindAsync(verCheck.RecruiterID);
            var manager = await _context.User.FindAsync(recruiter.ManagerID);

            //So if the authorization has been set to true send an email to the respective recruiter with an approval email
            if (verCheck.IsAuthorize==true)
            {
                var mailBody = service.AuthorizeVerification();
                mailBody = mailBody.Replace("RecruiterName", recruiter.Name);   
                mailBody = mailBody.Replace("{ManagerName}", manager.Name);
                service.SendMail(recruiter.Email, "Authorization Verification", mailBody);
            }
            else if (verCheck.IsAuthorize == false)
            {
                var mailBody = service.RefuseVerficationEmail();
                mailBody = mailBody.Replace("RecruiterName", recruiter.Name);
                mailBody = mailBody.Replace("{ManagerName}", manager.Name);
                service.SendMail(recruiter.Email, "Authorization Refused", mailBody);
            }

            return Ok(verCheck);
        }
    }
}
