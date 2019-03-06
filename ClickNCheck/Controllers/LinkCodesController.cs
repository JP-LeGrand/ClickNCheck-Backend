using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickNCheck.Models;
using ClickNCheck.Data;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkCodesController : ControllerBase
    {
        private readonly ClickNCheckContext _context;

        public LinkCodesController(ClickNCheckContext context)
        {
            _context = context;
        }

        [HttpGet]
        public bool IsCodeValid(string code)
        {
            return _context.LinkCodes.Find(code) != null;
        }

        [HttpPost]
        public void UpdateCodeStatus(LinkCode code)
        {
            code.Used = false;
            _context.Update(code);
            _context.SaveChanges();
        }


    }
}
