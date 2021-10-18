using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Demo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        [HttpGet("getFruits")]
        [AllowAnonymous]
        public ActionResult GetFruits()
        {
            List<string> myList = new List<string>
            {
                "Apples", "Bananas"
            };

            return Ok(myList);
        }

        [HttpGet("getFruits/{id}")]
        [AllowAnonymous]
        public ActionResult GetFruits(int id)
        {
            List<string> myList = new List<string>
            {
                "Apples", "Bananas"
            };

            return Ok(myList);
        }

        [HttpGet("getVegetables")]
        public ActionResult GetVegetables()
        {
            List<string> myList = new List<string>
            {
                "Cabbage", "Carrots"
            };

            return Ok(myList);
        }

        
        [AllowAnonymous]
        [HttpPost("getToken")]
        public async Task<ActionResult> GetToken([FromBody] LoginCredentials loginCredentials)
        {
            if (loginCredentials.Email == "some@email.com" && loginCredentials.Password == "pass1234")
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("yfH5LarK5qM1dITJV72XuwcMF4GHB7v0USAZR2FAQpGT5fjLq54otREpxGzE");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, loginCredentials.Email)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
