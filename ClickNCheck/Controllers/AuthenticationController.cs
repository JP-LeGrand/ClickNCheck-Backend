using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClickNCheck.Data;
using ClickNCheck.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ClickNCheck.Services;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ClickNCheckContext _context;
        public CodeGenerator _code = new CodeGenerator();

        private IConfiguration _config;

        EmailService mailS = new EmailService();

        public AuthenticationController(ClickNCheckContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Route("login")]
        // ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] string[] credentials) //string email, string password)
        {
            var user = await _context.User
                .SingleOrDefaultAsync(m => m.Email == credentials[0]);

            if (user != null)
            {
                if (credentials[1] == user.Password)
                {

                    return Ok(user.ID);
                }
                else
                {
                    return Unauthorized();
                }
            }

            return Unauthorized();

        }


        [HttpPost]
        [Route("otp")]
        public ActionResult<User> sendOTP([FromBody]int user_id)
        {
            string otp = _code.randomNumberGenerator();
            User user = _context.User.Find(user_id);
            user.Otp = otp;
            _context.Update(user);
            _context.SaveChanges();
            mailS.SendMail(user.Email, "OTP", "<p>" + otp + "</p>");
            return Ok(otp);
        }


        [HttpPost]
        [Route("checkOtp")]
        public ActionResult<User> checkOTP([FromBody] string[] user_otp)
        {

            User user = _context.User.Find(Convert.ToInt32(user_otp[0]));
            if (user.Otp == user_otp[1])
            {
                /* user.Otp = 0;
                 _context.Update(user);
                 _context.SaveChanges();*/
                var recruiters = _context.Roles.Where(x => x.UserTypeId == 3).Select(x => x.UserId).ToList();
                // var Users.Where(x => x.orgid ={ id} && roles.contains(x.roleid))
                var admins = _context.Roles.Where(x => x.UserTypeId == 1).Select(x => x.UserId).ToList();
                // var admins = _context.User.FromSql($"SELECT * FROM User WHERE ID IN( SELECT UserID FROM Roles WHERE UserTypeID = 1)").ToList();
                string user_name = user.Name + " " + user.Surname;

                if (admins.Contains(user.ID))
                {
                    string[] token_role_user = { _code.BuildToken(user, _config), "admin", user_name };
                    return Ok(token_role_user);
                }
                else if (recruiters.Contains(user.ID))
                {
                    string[] token_role_user_img = { _code.BuildToken(user,_config), "recruiter", user_name, user.PictureUrl };
                    return Ok(token_role_user_img);
                }

            }
            return Unauthorized();
        }



        [Authorize]
        [HttpPost]
        [Route("isLoggedIn")]
        public ActionResult isLoggedIn()
        {
            User user = _context.User.Find(Convert.ToInt32(User.Claims.First().Value));
            var recruiters = _context.Roles.Where(x => x.UserTypeId == 3).Select(x => x.UserId).ToList();
            var admins = _context.Roles.Where(x => x.UserTypeId == 1).Select(x => x.UserId).ToList();
            string user_name = user.Name + " " + user.Surname;

            if (admins.Contains(user.ID))
            {
                return Ok("admin");
            }
            else if (admins.Contains(user.ID) && recruiters.Contains(user.ID))
            {
                return Ok("admin_recruiter");
            }
            else if (recruiters.Contains(user.ID))
            {
                return Ok("recruiter");
            }
            return Unauthorized();
        }

        //TODO:
       //// [Authorize]
       // [Route("logout")]
       // public async Task<IActionResult> Logout()
       // {
       //     await HttpContext.SignOutAsync();
       //     return Ok();
       // }
    }
}