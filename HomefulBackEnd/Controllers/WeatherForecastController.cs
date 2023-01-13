using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HomefulBackEnd.Auth;

namespace HomefulBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
        private readonly JwtAuthService _jwtAuthenticationManager;

        public WeatherForecastController(JwtAuthService jwtAuthenticationManager)
        {
            this._jwtAuthenticationManager = jwtAuthenticationManager;
        }

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };



        [Authorize]
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser(LoginModel user)
        {
            var token = _jwtAuthenticationManager.Authenticate(user.Username, user.Password);
            if(token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}