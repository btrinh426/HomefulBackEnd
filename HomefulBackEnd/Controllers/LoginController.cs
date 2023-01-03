using HomefulBackEnd.Models;
using HomefulBackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace HomefulBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService) =>
        
            _loginService = loginService;

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CompleteProfile>> Get(string id)
        {
            Console.WriteLine("hit");
            var profile = await _loginService.GetAsync(id);

            if (profile == null)
            {
                return NotFound();
            }
            return profile;
        }
        
    }
}
