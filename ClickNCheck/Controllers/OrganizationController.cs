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
        UploadService uploadService = new UploadService();

        public OrganizationController(ClickNCheckContext context)
        {

            _context = context;

        }

        // GET: api/Organisations
        //The method below will display a list of organisations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organisation>>> GetOrganisations()
        {
            return await _context.Organisation.ToListAsync();
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
            await _context.Organisation.AddAsync(organisation);
            var result = await _context.SaveChangesAsync();

            var user = await _context.Organisation.LastAsync();
            int id  = user.ID;
           
            return Ok(user);
        }

        //The method below will add a contact person into an organisation
        // POST: api/ContactPersons
        [HttpPut]
        [Route("AddContactPerson/{OrgID}")]
        public async Task<ActionResult<ContactPerson>> PostContactPerson(ContactPerson contactPerson, int OrgID)
        {

            var admins = from org in _context.Organisation.Where(o => o.ID == OrgID)
                         select new
                         {
                             Name = contactPerson.Name,
                             Phone = contactPerson.Phone,
                             Email = contactPerson.Email
                         };

            return CreatedAtAction("GetAdmins/{id}", new { id = contactPerson.ID }, contactPerson);
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
 