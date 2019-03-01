using checkStub;
using ClickNCheck.Data;
using ClickNCheck.Models;
using ClickNCheck.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableController : ControllerBase
    {
        private ClickNCheckContext _context;

        public AvailableController(ClickNCheckContext context)
        {
            _context = context;
        }

        // POST: api/available/addService
        [HttpPost]
        [Route("addService")]
        public async Task<JObject> addService([FromBody] Models.Services serv)
        {
            try
            {
                _context.Services.Add(serv);
                await _context.SaveChangesAsync();

                string name = _context.Services.Find(serv.ID).Name;
                return new JObject { { "success", name } };
            }
            catch (Exception)
            {
                return new JObject { { "response", "failed" } };
            }; 
            
        }

        // GET api/available
        [HttpGet]
        public async Task<List<Vendor>> GetVendor()
        {
            
            try
            {
                Task<List<Vendor>> vendors = _context.Vendor.ToListAsync();
                return await vendors;
            }
            catch (Exception) { return null; }
      
        }
        // GET api/available/services
        [HttpGet]
        [Route("services")]
        public async Task<ActionResult<IEnumerable<Models.Services>>> GetChecks()
        {
            return await _context.Services.ToListAsync();
        }

        // GET: api/available/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            var Vendor = await _context.Vendor.FindAsync(id);

            if (Vendor == null)
            {
                return NotFound();
            }

            return Vendor;
        }

        // POST: api/available/runChecks/
        [HttpPost]
        [Route("runChecks")]
        public async Task<JObject> runChecks([FromBody]JObject requiredChecks)
        {
            CheckerRunner checkRunner = new CheckerRunner(_context, requiredChecks);
            if (await checkRunner.StartChecks())
            {
                return checkRunner.getResults();
            }
            return new JObject { { "message", "faield" } };
            
        }

        // POST: api/available/runCheck/{id}
        [HttpPost]
        [Route("runCheck/{serviceId}")]
        public async Task<Object> runCheck(int serviceId, [FromBody]int candidateId)
        {
            var service = await _context.Services.FindAsync(serviceId);
            var candidate = await _context.Candidate.FindAsync(candidateId);
            Task<JObject> res = null;

            if (service == null)
            {
                return NotFound("Service ID not found!");
            }
            //var Service = vendor.Services.
            if (service.isAvailable)
            {
                switch (service.CheckCategoryID)
                {
                    case 0:
                        //Credit check category
                        JArray creditVendorServiceID = new JArray { { serviceId } };
                        Credit creditcheck = new Credit(_context, candidateId);
                        res = creditcheck.runAllSelectedCreditChecks(creditVendorServiceID);
                        break;
                    case 1:
                        //Criminal check category

                        break;
                    case 2:
                        //Identity check category
                        break;
                    case 3:
                        //Drivers check category
                        break;
                    case 4:
                        //Employment check category
                        break;
                    case 5:
                        //Associations check category
                        break;
                    case 6:
                        //Academic check category
                        break;
                    case 7:
                        //Residency check category
                        break;
                    case 8:
                        //Personal check category
                        break;
                    default:break;
                }

                string results = ConnectToAPI.extractCompuscanRetValue(res.ToString());
                if (results != null)
                {
                    //candidate name might not be unique so use the id/passport number
                    string storagePath = $"C:/vault/{candidate.Organisation.Name}/Candidates/{candidate.ID_Passport}/{service.Name}{DateTime.Now}";
                    //find out how to access and use Blob storage not local storage
                    if (ConnectToAPI.makeZipFile($@"{storagePath}.zip", results))
                    {
                        if (ConnectToAPI.extractTheZip($@"{storagePath}.zip", $@"{storagePath}"))
                        {
                            return Ok($"Results extracted to {storagePath}");
                        }
                    }
                }

                return results;
            }
            else
            {
                //run check stub
            }

            return NotFound("Service currently unavailable");
        }

        // DELETE api/available/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
