using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClickNCheck.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickNCheck.Data;
using Microsoft.AspNetCore.Http;
using ClickNCheck.Services;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {// GET api/values

        private ClickNCheckContext _context;
       
        EmailService mailS = new EmailService();
        ContractUpload uploadService = new ContractUpload();

        public OrganizationController(ClickNCheckContext context)
        {

            _context = context;

        }

        // GET: api/Organisations/5
        //The method below will display an organisation
        [HttpGet]
        [Route("getOrganisation/{id}")]
        public async Task<ActionResult<Organisation>> GetOrganisation(int id)
        {
            var organisation = await _context.Organisation.FindAsync(id);

            if (organisation == null)
            {
                return NotFound();
            }

            return organisation;
        }

        //The method below will display respective admins belonging to an organisation, default admins
        [HttpGet]
        [Route("getAdmins/{id}")]
        public async Task<ActionResult<Organisation>> GetOrganisationAdmins(int id)
        {
            var admins = from org in _context.Organisation.Where(o => o.ID == id)
                         join users in _context.ContactPerson on org.ContactPerson.ID equals users.ID
                         select new
                         {
                             id = users.ID,
                             name = users.Name,
                             email = users.Email,
                             phone = users.Phone
                         };
            return Ok(admins);
        }

        [HttpPost]
        [Route("createOrganisation")] // create organization
        public async Task<ActionResult<Organisation>> PostOrganisation([FromBody]Organisation organisation)        {
            _context.Organisation.Add(organisation);
            var result = await _context.SaveChangesAsync();

            var user = await _context.Organisation.LastAsync();
            int id  = user.ID;
           
            return Ok(id);
        }

        [HttpPost]
        [Route("createJP")] // creatin a job profile
        public async Task<ActionResult<JobProfile>> PostJP([FromBody] JobProfile jobProfile)
        {
            _context.JobProfile.Add(jobProfile);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> PostContract([FromForm]IFormCollection contractFiles)
        {
            var uploadSuccess = false;
            foreach (var formFile in contractFiles.Files.ToList())
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



    }
}
 