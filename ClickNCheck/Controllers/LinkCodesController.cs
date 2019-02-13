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


        public bool IsCodeValid(string code)
        {
            return _context.LinkCodes.Find(code) != null;
        }

        public void UpdateCodeStatus(LinkCode code)
        {
            code.Valid = true;
            _context.Update(code);
            _context.SaveChanges();
        }


    }
}
