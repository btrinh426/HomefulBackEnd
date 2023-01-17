namespace HomefulBackEnd.Models
{
    public class PasswordData
    {
        public string HashedPassword { get; set; }

        public byte[] Salt { get; set; }
    }
}
