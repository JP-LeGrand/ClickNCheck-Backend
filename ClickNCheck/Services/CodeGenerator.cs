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


namespace ClickNCheck.Services
{
    public class CodeGenerator
    {
        //private IConfiguration _config;

        public string generateCode()
        {

            
            ClickNCheckContext _context = new ClickNCheckContext();
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random rand = new Random();

            string code = new string(Enumerable.Repeat(characters, 10).Select(s => s[rand.Next(s.Length)]).ToArray());

            //TODO:
            /* while (_context.LinkCodes.FirstOrDefault(c => c.Code == code) != null)
             {
                 code = new string(Enumerable.Repeat(characters, 10).Select(s => s[rand.Next(s.Length)]).ToArray());

             } */

            return code;
        }

        public string ReferenceNumber()
        {
            string code;
            Random random = new Random();
            code = random.Next(100000, 999999).ToString();
            return code;
        }

        public string randomNumberGenerator()
        {
            const string characters = "0123456789";
            Random rand = new Random();

            string code = new string(Enumerable.Repeat(characters, 5).Select(s => s[rand.Next(s.Length)]).ToArray());

            return code;
        }


        public string BuildToken(User user, IConfiguration _config)
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

    }
}
