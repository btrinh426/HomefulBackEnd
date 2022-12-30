using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomefulBackEnd.Models;
using HomefulBackEnd.Services;

namespace HomefulBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;

        public ProfilesController(ProfilesService profilesService) =>
            _profilesService = profilesService;

        [HttpGet]
        public async Task<List<CompleteProfile>> Get() =>
            await _profilesService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CompleteProfile>> Get(string id)
        {
            var profile = await _profilesService.GetAsync(id);

            if(profile == null)
            {
                return NotFound();
            }
            return profile;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CompleteProfile newProfile)
        {
            await _profilesService.CreateAsync(newProfile);

            return CreatedAtAction(nameof(Get), new { id = newProfile.Id }, newProfile);
        }

    }
}
