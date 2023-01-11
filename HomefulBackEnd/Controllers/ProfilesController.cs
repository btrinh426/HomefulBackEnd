using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomefulBackEnd.Models;
using HomefulBackEnd.Services;
using HomefulBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace HomefulBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private IProfilesService _service = null;
        //private readonly ProfilesService _profilesService;

        public ProfilesController(IProfilesService service
            , ILogger<ProfilesController> logger)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<CompleteProfile>> GetAllAsync() =>
            await _service.GetAllAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CompleteProfile>> Get(string id)
        {
            var profile = await _service.GetAsync(id);

            if(profile == null)
            {
                return NotFound();
            }
            return profile;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CompleteProfile newProfile)
        {
            try
            {
                await _service.CreateAsync(newProfile);

                return CreatedAtAction(nameof(Get), new { id = newProfile.Id }, newProfile);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
