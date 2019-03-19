using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickNCheck.Data;
using ClickNCheck.Models;

using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using ClickNCheck.Services;

using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections;
using Newtonsoft.Json;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class UsersController : ControllerBase
    {
        private ClickNCheckContext _context;
        EmailService emailService = new EmailService();
        LinkCode _model = new LinkCode();
        User _userModel = new User();
        Roles _role = new Roles();
        UploadService uploadService = new UploadService();
        CodeGenerator codeGenerator = new CodeGenerator();
        Hashing Hashing = new Hashing();

        public UsersController(ClickNCheckContext context)
        {
            _context = context;
        }

        [HttpPost()]
        [AllowAnonymous]
        [Route("signUp")]
        public ActionResult<User> regAdmin(User[] administrators)
        {
            _context.User.AddRange(administrators);
            _context.SaveChanges();

            return Ok("yes");
        }

        [HttpPost()]
        [AllowAnonymous]
        [Route("signUpRec")]
        public ActionResult<User> regRec(User[] users)
        {
            var _entryType = _context.UserType.FirstOrDefault(x => x.ID == 3);


            for (int x = 0; x < users.Length; x++)
            {
                CodeGenerator _codeGenerator = new CodeGenerator();
                EmailService _emailService = new EmailService();
                LinkCode _linkCode = new LinkCode();
                string code = _codeGenerator.generateCode();
                _linkCode.Code = code;
                _linkCode.Used = false;
                users[x].LinkCode = _linkCode;
                
                users[x].Roles.Add(new Roles { User = users[x], UserType = _entryType });
                if (_entryType.Type == "Recruiter")
                {
                    string emailBody = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\SignUpEmail.html"));
                    emailBody = emailBody.Replace("href=\"#\" ", "href=\""+Constants.BASE_URL+"Users/signup/" + code + "\""); 
                    _emailService.SendMail(users[x].Email, "Recruiter Signup", emailBody);
                }
                else if (_entryType.Type == "Manager")

                {
                    string emailBody = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\SignUpEmail.html"));
                    emailBody = emailBody.Replace("href=\"#\" ", "href=\"" + Constants.BASE_URL + "Users/signup/" + code + "\"");

                    _emailService.SendMail(users[x].Email, "Manager Signup", emailBody);

                }



            }
            _context.User.AddRange(users);
            _context.SaveChanges();

            return Ok("success");
        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        [HttpPost]
        [Route("CreateRecruiters/{id}")]
        public ActionResult<User> CreateRecruiters(int id, JObject jObject)
        {
            //find the organisation using the admin id
            var admin = _context.User.Find(id);
            Organisation org = admin.Organisation;
            var role = _context.UserType.Find(3);

            User recruiter = new User();
            recruiter.Name = (string)jObject["Name"];
            recruiter.Email = (string)jObject["Email"];
            recruiter.EmployeeNumber = (int)jObject["EmployeeNumber"];
            recruiter.Organisation = org;
            recruiter.OrganisationID = org.ID;
            recruiter.Phone = (string)jObject["Phone"];
            recruiter.Surname = (string)jObject["Surname"];
            //recruiter.Roles = (string)jObject["Surname"];

            Roles r = new Roles();

            r.UserType = role;
            r.User = recruiter;

            string code = "";
            CodeGenerator _codeGenerator = new CodeGenerator();
            EmailService _emailService = new EmailService();
            LinkCode _linkCode = new LinkCode();
            code = _codeGenerator.generateCode();
            _linkCode.Code = code;
            _linkCode.Used = false;
            recruiter.LinkCode = _linkCode;
            recruiter.Guid = Guid.NewGuid();

            string emailBody = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\RecruiterEmail.html"));
            emailBody = emailBody.Replace("href=\"#\" ", "href=\"" + Constants.BASE_URL + "Users/signup/" + code + "\"");
            emailService.SendMail(recruiter.Email, "Recruiter Signup", emailBody);

            _context.User.Add(recruiter);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("PostUsers")]
        public ActionResult<User> PostUsers(JObject users_usertypes)
        {
            JArray jusers = (JArray)users_usertypes["users"];
            JArray usertypes = (JArray)users_usertypes["usertypes"];

            List<User> users = jusers.ToObject<List<User>>();

            List<string> arr_usertype_id = usertypes.ToObject<List<string>>();

            List<UserType> _entryType = new List<UserType>();



            string code = "";
            for (int x = 0; x < users.Count; x++)
            {
                CodeGenerator _codeGenerator = new CodeGenerator();
                EmailService _emailService = new EmailService();
                LinkCode _linkCode = new LinkCode();
                code = _codeGenerator.generateCode();
                _linkCode.Code = code;
                _linkCode.Used = false;
                users[x].LinkCode = _linkCode;
                users[x].Guid = Guid.NewGuid();
            }

            _context.User.AddRange(users);
            _context.SaveChanges();

            for (int x = 0; x < users.Count; x++)
            {
                var user = _context.User.First(y => y.Email == users[x].Email);


                string[] usertype_id = arr_usertype_id.ElementAt(x).Split(",");

                for (int j = 0; j < usertype_id.Length; j++)
                {
                    var role = new Roles { UserId = user.ID, UserTypeId = Convert.ToInt32(usertype_id[j]) };

                    _context.Roles.Add(role);
                }



                for (int y = 0; y < usertype_id.Length; y++)
                {
                    if (usertype_id[y] == "3")//recruiter
                    {
                        string emailBody = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\RecruiterEmail.html"));
                        emailBody = emailBody.Replace("href=\"#\" ", "href=\"" + Constants.BASE_URL + "Users/signup/" + code + "\"");
                        emailService.SendMail(users[x].Email, "Recruiter Signup", emailBody);
                    }
                    else if (usertype_id[y] == "1")//admin


                    {
                        string emailBody = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\SignUpEmail.html"));
                        emailBody = emailBody.Replace("href=\"#\" ", "href=\"" + Constants.BASE_URL + "Users/signup/" + code + "\"");

                        emailService.SendMail(users[x].Email, "Admin Signup", emailBody);

                    }
                }

            }

            _context.SaveChanges();

            return Ok("success");

        }




        [HttpGet]
        [AllowAnonymous]
        [Route("signup/{code}")]
        public ActionResult<string> checkCode(string code)
        {
            LinkCode _userCode = _context.LinkCodes.FirstOrDefault(x => x.Code == code);

            if (_userCode != null)
            {
                User user = _context.User.FirstOrDefault(u => u.LinkCodeID == _userCode.ID);
                var recruiters = _context.Roles.Where(x => x.UserTypeId == 3).Select(x => x.UserId).ToList();
                var admins = _context.Roles.Where(x => x.UserTypeId == 1).Select(x => x.UserId).ToList();

                if (recruiters.Contains(user.ID))
                {
                    return Redirect("https://s3.amazonaws.com/clickncheck-frontend-tafara/components/recruiter/recruiterRegistration/recruter_registration.html?code=" + user.ID);
                }
                else if (admins.Contains(user.ID))
                {
                    return Redirect("https://s3.amazonaws.com/clickncheck-frontend-tafara/components/admin/adminRegistration/admin_registration.html");
                }

                return Unauthorized(code);

            }
            else
            {
                return Unauthorized(code);
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("registration")]
        public ActionResult<string> registerUser([FromBody] string[] Password)
        {
            //  var code = Response.

            var pass = Password[0];
            var code = Password[1];

            var codeEntry = _context.LinkCodes.FirstOrDefault(x => x.Code == code);
            var codeID = codeEntry.ID;

            var userEntry = _context.User.FirstOrDefault(d => d.LinkCodeID == codeID);

            userEntry.Password = pass;
            _context.User.Update(userEntry);

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> registerRecruiter([FromBody] string[] id_pass_manager)
        {

            int recruiter_id = Convert.ToInt32(id_pass_manager[0]);
            string pass = id_pass_manager[1];
            int manager_id = -1;
            string salt = codeGenerator.generateCode();
            string hashedpassword = Hashing.MD5Hash(pass + salt);

            if (id_pass_manager.Length > 2)
            {
                manager_id = Convert.ToInt32(id_pass_manager[2]);
            }

            

            User user = _context.User.FirstOrDefault(d => d.ID == recruiter_id);



            var recruiters = _context.Roles.Where(x => x.UserTypeId == 3).Select(x => x.UserId).ToList();
            var admins = _context.Roles.Where(x => x.UserTypeId == 1).Select(x => x.UserId).ToList();

            if (recruiters.Contains(user.ID))
            {
                user.Password = hashedpassword;
                user.Salt = salt;
                user.PasswordExpiryDate = DateTime.Now.AddDays(30);
                user.ManagerID = manager_id;
                _context.User.Update(user);
                await _context.SaveChangesAsync();
                return Ok("recruiter");
            }
            else if (admins.Contains(user.ID))
            {
                user.Password = hashedpassword;
                user.Salt = salt;
                user.PasswordExpiryDate = DateTime.Now.AddDays(30);
                _context.User.Update(user);
                await _context.SaveChangesAsync();
                return Ok("admin");
            }

            return BadRequest();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var User = await _context.User.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User User)
        {
            if (id != User.ID)
            {
                return BadRequest();
            }

            _context.Entry(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users/CreateUser
        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<User>> PostUser(User User)
        {
            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = User.ID }, User);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var User = await _context.User.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }

            _context.User.Remove(User);
            await _context.SaveChangesAsync();

            return User;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.ID == id);
        }

        private bool JobProfileExists(int id)
        {
            return _context.JobProfile.Any(e => e.ID == id);
        }
        // POST: api/Users/5/AssignRecruiters
        [HttpPost()]
        [Route("{id}/AssignRecruiters")]
        public async Task<IActionResult> AssignRecruiters(int id, [FromBody]int[] ids)
        {
            int jobId = id;

            //find job profile
            var jobProfile = await _context.JobProfile.FindAsync(jobId);

            if (jobProfile == null)
            {
                return NotFound("This Job Profile does not exist");
            }
            EmailService emailService = new EmailService();
            string emailBody = emailService.RecruiterMail();

            //find recruiters
            for (int i = 0; i < ids.Length; i++)
            {
                var recruiter = await _context.User.FindAsync(ids[i]);

                if (recruiter == null)
                {
                    return NotFound("The recruiter " + recruiter.Name + recruiter.Surname + " does not exist");
                }
                //add recruiter to job profile
                jobProfile.Recruiter_JobProfile.Add(new Recruiter_JobProfile { JobProfile = jobProfile, Recruiter = recruiter });
                emailService.SendMail(recruiter.Email, "New Job Profile", emailBody);
                //emailService.SendMail(recruiter.Email,"New Job Profile Assignment",);
                //ghost
            }
            //save changes to job profile
            _context.Entry(jobProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = _context.User.Any(e => e.ID == jobId);
                if (!JobProfileExists(jobId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(jobProfile);
        }


        // GET: api/Users/GetAllRecruiters/5
        [HttpGet("recruiter/organisation/managers/{recruiter_id}")]
        [AllowAnonymous]
        public IEnumerable GetRecruiterOrganisationManagers(int recruiter_id)
        {
            var all_managers = _context.Roles.Where(x => x.UserTypeId == 4).Select(x => x.UserId);
            var organisation_id = _context.User.Find(recruiter_id).OrganisationID;

            var org_users = _context.User.Where(x => x.OrganisationID == organisation_id).ToList();

            List<User> managers = new List<User>();

            for (int i = 0; i < org_users.Count; i++)
            {
                if (all_managers.Contains(org_users.ElementAt(i).ID))
                {
                    managers.Add(org_users.ElementAt(i));
                }
            }
            // var managers = org_users.Intersect(all_managers).ToList();

            return managers;
        }

        [HttpGet]
        [Route("GetRecruiters")]
        public JObject GetRecruiters()
        {
            int admin_id = Convert.ToInt32(User.Claims.First().Value);
            var admin = _context.User.Find(admin_id);
            List<Roles> rec_roles = _context.Roles.Where(r => r.UserTypeId == 3).ToList();

            dynamic jrecruiters = new JObject();
            List<JObject> lrecruiters = new List<JObject>();

            for (int i = 0; i < rec_roles.Count(); i++)
            {
                var recruiter = _context.User.Find(rec_roles.ElementAt(i).UserId);

                dynamic jrecruiter = new JObject();
                jrecruiter.ID = recruiter.ID;
                jrecruiter.Name = recruiter.Name;
                jrecruiter.Surname = recruiter.Surname;
                lrecruiters.Add(jrecruiter);
            }

            jrecruiters.Recruiters = JArray.FromObject(lrecruiters);

            return jrecruiters;
        }


        // GET: api/Users/GetAllRecruiters/5
        [HttpGet("Organization/{id}/recruiters")]
        public async Task<IEnumerable<object>> GetAllRecruitersAsync(int id)
        {
            var orgs = await _context.Organisation.Where(i => i.ID == id).ToListAsync();
            var roles = _context.Roles;
            var user_types = _context.UserType;

            var recruiters = await _context.User.Join(orgs,
                                                        u => u.OrganisationID,
                                                        o => o.ID,
                                                        (user, org) => new
                                                        {
                                                            user.ID,
                                                            user.Name,
                                                            user.Surname,
                                                            user.Roles
                                                        })
                                                        .Join(roles,
                                                        u => u.ID,
                                                        r => r.UserId,
                                                        (user, role) => new
                                                        {
                                                            user.ID,
                                                            user.Name,
                                                            user.Surname,
                                                            role.UserTypeId
                                                        })
                                                        .Where(u => u.UserTypeId == 3)
                                                        .ToListAsync();

            return recruiters;
        }

        [HttpGet]
        [Route("UploadImage")]
        public async Task<IEnumerable<UserType>> UploadImage()
        {
            var userTypes = await _context.UserType.Where(u => u.Type != "Administrator" && u.Type != "SuperAdmin").ToListAsync();
            return userTypes;
        }

        [HttpPost("{id}/UploadFile")]
        public async Task<IActionResult> UploadFile(int id, [FromForm]IFormCollection contractFiles)
        {
            var user = _context.User.Find(id);
            var org = _context.Organisation.Find(user.OrganisationID);

            string bloburl = "";
            foreach (var formFile in contractFiles.Files.ToList())
            {
                if (formFile.Length <= 0)
                {
                    continue;
                }

                using (var ms = new MemoryStream())
                {
                    formFile.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    //uploadSuccess = await uploadService.UploadImage(formFile.FileName, fileBytes, null);
                    bloburl = await uploadService.UploadImage(user.Guid, org.Guid, formFile.FileName, fileBytes, null);
                    user.PictureUrl = bloburl;
                    await _context.SaveChangesAsync();
                }
            }



            if (bloburl != "")
                return Ok("Upload Success");
            else
                return NotFound("Upload Error");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("UpdatePassword/{id}")]
        public async Task<ActionResult<string>> UpdatePasswordAsync(int id, [FromBody]string password)
        {
            var user = _context.User.Find(id);
            string salt = codeGenerator.generateCode();
            string hashedpassword = Hashing.MD5Hash(password + salt);
            if(user != null)
            {
                user.Password = hashedpassword;
                user.Salt = salt;
                user.PasswordExpiryDate = DateTime.Now.AddDays(30);

                try
                {
                    var mailbody = emailService.PasswordChange();
                    mailbody = mailbody.Replace("UserName", user.Name);
                    emailService.SendMail(user.Email, "Password Changed", mailbody);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("IsPasswordExpired/{id}")]
        public ActionResult<bool> IsPasswordExpired(int id)
        {
            var user = _context.User.Find(id);
            return (user.PasswordExpiryDate - DateTime.Now).Days <= 0;
        }

    }


}
