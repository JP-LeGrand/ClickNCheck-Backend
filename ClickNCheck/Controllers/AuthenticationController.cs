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

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ClickNCheckContext _context;

        private IConfiguration _config;

        EmailService mailS = new EmailService();

        public AuthenticationController(ClickNCheckContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Route("login")]
        //, ValidateAntiForgeryToken]
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
            string otp = randomNumberGenerator();
            User user = _context.User.Find(user_id);
            user.Otp = Convert.ToInt32(otp);
            _context.Update(user);
            _context.SaveChanges();
            mailS.SendMail(user.Email, "OTP", "<p>" + otp + "</p>");
            return Ok(otp);
        }

        public string randomNumberGenerator()
        {
            const string characters = "0123456789";
            Random rand = new Random();

            string code = new string(Enumerable.Repeat(characters, 5).Select(s => s[rand.Next(s.Length)]).ToArray());

            return code;
        }

        [HttpPost]
        [Route("checkOtp")]
        public ActionResult<User> checkOTP([FromBody] string[] user_otp)
        {

            User user = _context.User.Find(Convert.ToInt32(user_otp[0]));
            if (user.Otp.ToString() == user_otp[1])
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
                    string []token_role_user = { BuildToken(user), "admin", user_name };
                    return Ok(token_role_user);
                }
                else if (recruiters.Contains(user.ID))
                {
                    string[] token_role_user = { BuildToken(user), "recruiter", user_name };
                    return Ok(token_role_user);
                }

            }
            return Unauthorized();
        }

        private string BuildToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                                    new Claim(JwtRegisteredClaimNames.NameId, user.ID.ToString()),
                                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"], claims,
            expires: DateTime.Now.AddDays(360),
            signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}