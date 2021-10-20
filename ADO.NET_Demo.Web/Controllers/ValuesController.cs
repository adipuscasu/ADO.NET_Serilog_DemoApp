using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ADO.NET_Demo.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ADO.NET_Demo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILoginService loginService,
            ILogger<ValuesController> logger

            )
        {
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

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
            await _loginService.IsLoginValidAsync(loginCredentials);
            var tokenString = _loginService.BuildToken(loginCredentials);

            return Ok(new { Token = tokenString });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> SignIn(SignInDto signInDto)
        {
            return Ok();
        }
    }
}
