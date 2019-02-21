using ClickNCheck.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Services
{
    public class CodeGenerator
    {
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

    }
}
