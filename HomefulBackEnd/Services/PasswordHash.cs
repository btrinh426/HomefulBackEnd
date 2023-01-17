using HomefulBackEnd.Models;
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

        public PasswordData Hash(string? password, byte[] salt)
        {
            PasswordData pwdData = new PasswordData();

            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            if(salt == null)
            {
                salt = RandomNumberGenerator.GetBytes(SaltSize);
                pwdData.Salt = salt;
            }

            pwdData.HashedPassword = Convert.ToHexString(Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password!), salt, HashIter, hashAlgorithm, HashSize));
            return pwdData;      
        }

        public PasswordData Verify(string? password, byte[] salt)
        {
            var hashed = Hash(password, salt);
            Console.WriteLine(hashed);
            return hashed;
        }


        
    }
}
