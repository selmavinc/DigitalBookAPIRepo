using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Common;
using UserRegistartion.DatabaseEntity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthServer.DatabaseEntity;

namespace UserRegistartion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DigitalBooksContext _context;
        private readonly IConfiguration Config;
        public static List<User> dept = new List<User>();
        internal record UserValidationRequestModel([Required] string UserName, [Required] string Password);

        public UsersController(DigitalBooksContext context, IConfiguration configuration)
        {
            _context = context;
            Config = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<List<DbCredentials>> LoginUser(string username, string password)
        {
            UserValidationRequestModel request;
            HttpContext http;
            ITokenService tokenService = null;
            var Creden = await _context.Users
                    .Select(x => new
                    {
                        x.UserName,
                        x.Password,
                    }).ToListAsync();
            var dbSeries = new List<DbCredentials>();

            try
            {
                if (Creden.Contains(Creden.Find(de => de.UserName == username)))
                {
                    var token = tokenService.BuildToken(Config["Jwt:Key"],
                                                        Config["Jwt:Issuer"],
                                                        new[]
                                                        {
                                                            Config["Jwt:Aud1"],
                                                            Config["Jwt:Aud2"],
                                                            Config["Jwt:Aud3"]
                                                                },
                                                        username);

                    var seriesToAdd = new DbCredentials
                    {
                        Token = token,
                        IsAuthenticated = true,
                    };
                    dbSeries.Add(seriesToAdd);
                }
                var seriesToAdd1 = new DbCredentials
                {
                    Token = string.Empty,
                    IsAuthenticated = false,
                };
                dbSeries.Add(seriesToAdd1);

                return dbSeries;
            }
            catch (DbUpdateException e)
            {
                throw new TimeoutException(e.Message, e);
                //if (UserExists(user.UserId))
                //{
                //    return Conflict();
                //}
                //else
                //{
                //    throw;
                //}
            }

            //return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.Where(x => x.RoleId == 1).ToListAsync();
        }

        //// GET: api/Users/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetUser(int id)
        //{
        //  if (_context.Users == null)
        //  {
        //      return NotFound();
        //  }
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return user;
        //}

        //// PUT: api/Users/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, User user)
        //{
        //    if (id != user.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            int userId = 0;
            if (_context.Users == null)
            {
                return Problem("Entity set 'DigitalBooksContext.Users'  is null.");
            }
            var Isregistered = await _context.Users
               .FirstOrDefaultAsync(m => m.EmailId == user.EmailId);
            if (Isregistered == null)
            {
                if (_context.Users.Count() == 0)
                {
                    userId = 0;
                }
                else
                {
                    userId = _context.Users.OrderByDescending(x => x.UserId).Select(x => x.UserId).FirstOrDefault();

                }

                //int userId = 0;
                //if (userlist.Count() > 0)
                //    userId = userlist.Select(x => x.UserId).FirstOrDefault();
                user.UserId = Convert.ToInt32(userId) + 1;
                user.Password = EncryptionDecryprion.EncodePasswordToBase64(user.Password);
                _context.Users.Add(user);
                //}
                //_context.Users.Add(user);
            }
            else
            {
                //return Ok("Already Registered");
                return Ok(new { message = "Already Registered"});
            }
            //var data =  _context.Users.FirstOrDefault();
            //if (data != null)
            //{
            //    //data.UserId = Guid.NewGuid();
            //    data.UserName = user.UserName;
            //    data.EmailId = user.EmailId;
            //    data.Password = EncryptionDecryprion.EncodePasswordToBase64(user.Password);
            //    data.RoleId = user.RoleId;
            //    data.Active = user.Active;
            //    data.FirstName = user.FirstName;
            //    data.LastName = user.LastName;
            //}
            //else
            //{

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetUser", new { id = user.UserId }, user);
            return Ok(new { message = "Registered Successfully" });
            //return Ok("Added");
        }

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    if (_context.Users == null)
        //    {
        //        return NotFound();
        //    }
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
