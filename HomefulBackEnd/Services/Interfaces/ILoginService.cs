using HomefulBackEnd.Models;

namespace HomefulBackEnd.Services.Interfaces
{
    public interface ILoginService
    {
        Task<CompleteProfile> GetAsync(string id);
    }
}
