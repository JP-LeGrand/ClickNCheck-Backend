using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ClickNCheck.Data;
using ClickNCheck.Models;
using ClickNCheck.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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

        // POST: api/available/{id}/sendrequest
        [HttpPost]
        [Route("{id}/sendRequest")]
        public async Task<Object> sendRequest(int id, [FromBody]Object obj)
        {
            ConnectToAPI connect = new ConnectToAPI(0, "https://webservices-uat.compuscan.co.za/NormalSearchService?wsdl");
            string res = await connect.run((JObject)obj);

            res = ConnectToAPI.extractCompuscanRetValue(res);
            if (res != null)
            {
                string pwd = $"{Directory.GetCurrentDirectory()}",
                    zipPath = $"{pwd}/../Services";
                //find out how to access and use Blob storage not local storage
                if (ConnectToAPI.makeZipFile(@"C:/vault/chub_777.zip", res))
                {
                    if (ConnectToAPI.extractTheZip(@"C:/vault/chub_777.zip", @"C:/chub_777"))
                    {

                    }
                }
            }

            return res;
        }

        // DELETE api/available/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
