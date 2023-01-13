using HomefulBackEnd.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace HomefulBackEnd.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthController : ControllerBase
    {
        private IJwtAuthService jwtAuthService = null;

        public JwtAuthController(IJwtAuthService jwtAuthService)
        {
            this.jwtAuthService= jwtAuthService;
        }

        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser(LoginModel user)
        {
            var token = jwtAuthService.Authenticate(user.Username, user.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
