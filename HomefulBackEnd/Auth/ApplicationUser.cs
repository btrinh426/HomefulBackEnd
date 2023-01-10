using HomefulBackEnd.Models;

namespace HomefulBackEnd.Auth
{
    public class ApplicationUser : CompleteProfile
    {
        public string? RefreshToken { get; set; }
        public DateTime RefresTokenExpiryTime { get; set; }
    }
}
