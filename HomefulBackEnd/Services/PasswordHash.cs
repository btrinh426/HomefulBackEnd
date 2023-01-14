using MongoDB.Driver.Core.Events;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;

namespace HomefulBackEnd.Services
{
    public class PasswordHash
    {
        const int SaltSize = 16, HashSize = 20, HashIter = 10000;
        byte[] salt;
        string password;

        public string Hash(string? password, byte[] salt)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            if(salt == null)
            {
                salt = RandomNumberGenerator.GetBytes(SaltSize);
            }
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password!), salt, HashIter, hashAlgorithm, HashSize);
            Console.WriteLine(Convert.ToHexString(hash));
            return Convert.ToHexString(hash);        
        }

        public string Verify(string? password, byte[] salt)
        {
            var hashed = Hash(password, salt);
            Console.WriteLine(hashed);
            return hashed;
        }


        
    }
}
