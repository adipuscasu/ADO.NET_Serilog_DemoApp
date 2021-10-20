using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ADO.NET_Demo.Web.Areas.Identity.Data;
using ADO.NET_Demo.Web.Controllers;
using ADO.NET_Demo.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ADO.NET_Demo.Web.Services
{
    public class LoginService : ILoginService
    {
        private readonly IdentityContext _identityContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginService(
            IdentityContext identityContext,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager
            )
        {
            _identityContext = identityContext ?? throw new ArgumentNullException(nameof(identityContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task IsLoginValidAsync(LoginCredentials loginCredentials)
        {
            var user = _identityContext.Users.FirstOrDefault(x => x.Email == loginCredentials.Email);
            if (user is not null)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginCredentials.Password, false);
                if (signInResult.Succeeded)
                {
                    return;
                }
            }

            throw new UnauthorizedAccessException("Email and / or password are incorrect");
        }

        public string BuildToken(LoginCredentials loginCredentials)
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
            return tokenString;
        }
    }
}
