using HomefulBackEnd.Models;

namespace HomefulBackEnd.Services.Interfaces
{
    public interface IProfilesService
    {
        Task CreateAsync(CompleteProfile newProfile);
        Task<List<CompleteProfile>> GetAllAsync();

        Task<CompleteProfile> GetAsync(string id);

        Task<CompleteProfile> GetProfile(string username);
    }
}
