using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ClickNCheck.Data;
using ClickNCheck.Models;
using System.Reflection;
using System.IO;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorsController : ControllerBase
    {
        private ClickNCheckContext _context;
        EmailService mailS = new EmailService();
        LinkCode _model = new LinkCode();

        public AdministratorsController(ClickNCheckContext context)
        {
            _context = context;
        }

        [HttpPost()]
        [Route("sendMail")] //check if you need this routes
        public ActionResult sendMail(string email)
        {
            string code = generateCode();

            LinkCode _model = new LinkCode();
            string emailBody = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\SignUpEmail.html"));

            emailBody = emailBody.Replace("href=\"#\" ", "href=\"https://localhost:44347/api/Administrators/signup/" + code + "\"");
            _model.Code = code;
            _model.Used = false;
            _context.LinkCodes.Add(_model);
            _context.SaveChanges();
            EmailService mailS = new EmailService();

            mailS.SendMail(email, "nane", emailBody);
           
            // return Ok(email);
            return Ok();
        }

       [HttpPost()]
       [Route("recruiter")]
        public ActionResult<User> regRecruiter(User [] recruiter)
        {
            _context.User.AddRange(recruiter);
            _context.SaveChanges();
            for (int i = 0; i < recruiter.Length; i++)
            {
                sendMail(recruiter[i].Email);
            }
            return Ok("yes");
        }

        [HttpPost()]
        [Route("admin")]
        public ActionResult<User> regAdmin(User[] admin)
        {
            _context.User.AddRange(admin);
            _context.SaveChanges();

            for(int i = 0; i < admin.Length; i++)
            {
                sendMail(admin[i].Email);
            }
            return Ok("yes");
        }
        
        [HttpGet]
        [Route("administrator/signUp")]
         public async Task<ActionResult<IEnumerable<User>>> getAdministrators()
            {
             var _administrators = await _context.User.ToListAsync();
             foreach (var _administrator in _administrators) {
                var _email = _administrator.Email;
                var code = generateCode();
                string emailBody = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\SignUpEmail.html"));
                 emailBody = emailBody.Replace("href=\"#\" ", "href=\"https://localhost:44347/api/" + _administrator.UserType + "/ signup/" + code + "\"");
                mailS.SendMail(_email, "Recruiter Sign Up Link", emailBody);
                _model.Code = code;
                _model.Used = false;
                _context.LinkCodes.Add(_model);

            }
            _context.SaveChanges();

            return Ok("yeesa");
            }


           
        

        public string generateCode()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random rand = new Random();

            string code = new string(Enumerable.Repeat(characters, 10).Select(s => s[rand.Next(s.Length)]).ToArray());

            while (_context.LinkCodes.FirstOrDefault(c => c.Code == code) != null)
            {
                code = new string(Enumerable.Repeat(characters, 10).Select(s => s[rand.Next(s.Length)]).ToArray());

            }

            return code;
        }



        public string randomNumberGenerator()
        {
            Random generator = new Random();
            String r = generator.Next(0, 999999).ToString("D6");
            return r;
        }

        [Route("signup/{code}")]
        public ActionResult<string> checkCode(string code)
        {
            LinkCode our_code = _context.LinkCodes.FirstOrDefault(x => x.Code == code);

            if (our_code != null && our_code.Used == false)
            {
                //return "Yey, You can register";
                return Redirect("http://clickncheckkb.s3-website.us-east-2.amazonaws.com/");
            }
            else
            {
                return "Link Error: This link has either been used or is invalid";
            }
        }

        /*
         * Generating and sending links
         * 
         */

       
    }
}
