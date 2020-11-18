using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CreateWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        [HttpGet("gettoken")]
        public Object GetToken()
        {
            string key = "my_secret_key_12345";
            var issuer = "http://mysite.com";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "Waqar_Ahmed"));

            var token = new JwtSecurityToken(issuer,
                            issuer,
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };
        }
        [HttpPost("getname1")]

        public ActionResult<string> GetName1()
        {
            if (User.Identity.IsAuthenticated)
            {
                var claims = User.Claims;
                return "Valid";
            }
            else
            {
                return "Invalid";
            }
        }
        [HttpPost("getname2")]
        public Object GetName2()
        {
            var claims = User.Claims;
            var name = claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
            return new
            {
                data = name
            };
        }

        [HttpGet("getname")]
        public string GetName()
        {
            return "Waqar";
        }

        [HttpGet("getmarks")]
        public int GetMarks(int rollnumber)
        {
            return 100;

        }
        [HttpPost("saveuser")]
        public string Save(StudentDto dto)
        {
            return "Done";

        }
    }
    public class StudentDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

    }

}
