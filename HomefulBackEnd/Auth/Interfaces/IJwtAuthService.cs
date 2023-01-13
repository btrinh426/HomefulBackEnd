using HomefulBackEnd.Services.Interfaces;

namespace HomefulBackEnd.Auth.Interfaces
{
    public interface IJwtAuthService

    {
        public string Authenticate(string username, string password);
    }
}
