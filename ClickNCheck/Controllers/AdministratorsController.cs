using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ClickNCheck.Data;
using ClickNCheck.Models;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorsController : ControllerBase
    {
        private ClickNCheckContext _context;

        public AdministratorsController(ClickNCheckContext context)
        {
            _context = context;
        }

        [HttpPost()]
        [Route("sendMail")] //check if you need this routes
        public ActionResult sendMail(string person, string email)
        {
            string code = generateCode();
            LinkCode _model = new LinkCode();
            string emailBody = System.IO.File.ReadAllText("C:\\Users\\Xolani Dlamini\\Documents\\Retro\\CnC\\Click-N-Check-Backend\\ClickNCheck\\Email.html");

            emailBody = emailBody.Replace("href=\"#\" ", "href=\"https://localhost:44347/api/" + person + "/signup/" + code + "\"");
            //try removing these 2 variables
            /*  var list = Recruiters.ReadDetails("C:\\testData.csv");
               var email = Recruiters.getEmails(list);
               foreach (var item in email) {
                   string code = mailS.generateCode();
                   _model.Code = code;
                   _model.Used = false;
                   _context.LinkCodes.Add(_model);

                   mailS.SendMail(item, "testing", $"C:\\Users\\Mpinane Mohale\\Desktop\\Standard Bank\\Mine\\RegistrationEmail\\index.html");
               }*/

            _model.Code = code;
            _model.Used = false;
            _context.LinkCodes.Add(_model);

            EmailService mailS = new EmailService();
            mailS.SendMail(email, "nane", emailBody);
            _context.SaveChanges();
            // return Ok(email);
            return Ok();
        }

        [HttpPost()]
       [Route("recruiter")]
        public ActionResult<User> regRecruiter(User [] recruiter)
        {
            _context.User.AddRange(recruiter);
            _context.SaveChanges();

            return Ok("yes");
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

        [Route("signup/{code}")]
        public ActionResult<string> checkCode(string code)
        {
            LinkCode our_code = _context.LinkCodes.FirstOrDefault(x => x.Code == code);

            if (our_code != null && our_code.Used == false)
            {
                return "Yey, You can register";
            }
            else
            {
                return "Link Error: This link has either been used or is invalid";
            }
        }
    }
}
