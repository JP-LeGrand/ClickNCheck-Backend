﻿using checkStub;
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
        ////private ConnectToAPI obje;

        public AvailableController(ClickNCheckContext context)
        {
            _context = context;
        }

        // GET api/available
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendor()
        {
            return await _context.Vendor.ToListAsync();
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
        // POST: api/available
        [HttpPost]
        [Route("addVendor")]
        public async Task<ActionResult<Vendor>> addVendor([FromBody] JObject input)
        {
            JToken checkCategories = input.SelectToken("categories");
            List<int> theCheckCategories = new List<int>();

            foreach (var item in checkCategories)
            {
                theCheckCategories.Add((int)item);
            }

            Vendor v = new Vendor
            {
                Name = input.SelectToken("vendorName").ToString()
            };

            //find checkCategory
            for (int i = 0; i < theCheckCategories.Count; i++)
            {
                var checkCategory = await _context.CheckCategory.FindAsync(theCheckCategories[i]);

                if (checkCategory == null)
                {
                    return NotFound("The check category " + checkCategory.Category + " does not exist");
                }
                //add recruiter to job profile
                v.Vendor_Category.Add(new Vendor_Category { Vendor = v, CheckCategory = checkCategory });
            }

            _context.Vendor.Add(v);
            await _context.SaveChangesAsync();

            return await _context.Vendor.LastAsync();
        }
        // POST: api/available/runChecks/{jsonObject}
        [HttpPost]
        [Route("runChecks/{jsonObject}")]
        public async Task<Object> runChecks(int candidateId, [FromRoute]JObject jsonObject)
        {
            Candidate candidate = await _context.Candidate.FindAsync(candidateId);
            CheckerRunner checkRunner = new CheckerRunner(_context, candidate, jsonObject);
            
            return await checkRunner.startChecks();
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
                        List<int> creditVendorServiceID = new List<int> { serviceId };
                        Credit creditcheck = new Credit(_context, candidate);
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
                    string storagePath = $"C:/vault/{candidate.Organisation.Name}/{candidate.Name}/{DateTime.Now}";
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
