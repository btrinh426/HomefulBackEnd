namespace HomefulBackEnd.Models
{
    public class Profile
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? DateOfBirth { get; set; }

        public string? CurrentLocation { get; set; }

        public List<string>? Status { get; set; }

    }
}
