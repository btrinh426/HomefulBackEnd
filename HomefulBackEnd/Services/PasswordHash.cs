using MongoDB.Driver.Core.Events;
using System.Security.Cryptography;
using System.Text;

namespace HomefulBackEnd.Services
{
    public class PasswordHashTest
    {
        const int SaltSize = 16, HashSize = 20, HashIter = 10000;
        public string PasswordHash(string? password)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password!), salt, HashIter, hashAlgorithm, HashSize);
            return Convert.ToHexString(hash);        
        }
        
    }
}
