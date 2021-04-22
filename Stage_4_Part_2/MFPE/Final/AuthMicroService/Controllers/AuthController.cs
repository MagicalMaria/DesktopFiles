using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthMicroService.Models;
using AuthMicroService.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace AuthMicroService.Controllers
{
    [AllowAnonymous]    //Allow anyone to access this
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _service;
        private readonly ILog _log4net = LogManager.GetLogger(typeof(AuthController));

        public AuthController(IConfiguration configuration, IAuthService service)
        {
            _configuration = configuration;
            _service = service;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Credentials credentials)
        {
            //Adding logging
            _log4net.Info("HttpPost request : " + nameof(Login));

            if (credentials == null) return BadRequest("Empty Credentials");

            try
            {
                var userCredentials = _service.AuthenticateUser(credentials); //Check for the credentials

                if (userCredentials != null)
                {
                    var jwtToken = _service.GenerateJWT(credentials, _configuration); //Get JWT Token for the user

                    if (!string.IsNullOrEmpty(jwtToken))
                    {
                        return Ok(jwtToken);
                    }

                    return BadRequest("Token generation unsuccessful");
                }

                return BadRequest("Invalid Credentials! Try Again...");
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured : " + e.Message + " from " + nameof(Login));
                return BadRequest("Exception Occured");
            }
        }
    }
}
