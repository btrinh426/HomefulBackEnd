using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using HomefulBackEnd.Services.Interfaces;
using HomefulBackEnd.Models;
using HomefulBackEnd.Auth.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using HomefulBackEnd.Services;

namespace HomefulBackEnd.Auth
{
    public class JwtAuthService : IJwtAuthService
    {
        private readonly string key;
        private readonly IMongoCollection<CompleteProfile> _profilesCollection;
        private readonly PasswordHash pwdHash;


        private readonly IDictionary<string, string> users = new Dictionary<string, string>()
        {
            {"test", "password"}, {"test1", "pwd"}, {"test2", "pwd2"}
        };

        public JwtAuthService(
            IOptions<HomefulDatabaseSettings> homefulDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                homefulDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                homefulDatabaseSettings.Value.DatabaseName);

            _profilesCollection = mongoDatabase.GetCollection<CompleteProfile>(
                homefulDatabaseSettings.Value.ProfilesCollectionName);
        }

        public JwtAuthService(string? key)
        {
            this.key = key;
        }

        public string Authenticate(string username, string password)
        {
            //if (!users.Any(u => u.Key == username && u.Value == password))
            //{
            //    return null;
            //}
            string key = "SecretKey1234$$JWT";


            CompleteProfile profile = _profilesCollection.Find(x => x.Profile.Email == username).FirstOrDefault();
            if (profile == null)
            {
                return null;
            }

            var hash = new PasswordHash(password, profile.Profile._Salt);
            hash.Verify(password, profile.Profile._Salt);
            string verifiedHash = hash.ToString();

            if (verifiedHash == profile.Profile.Password)
            {
                Console.WriteLine("verified");
            } else
            {
                Console.Write(profile.Profile.Password);
                Console.WriteLine("not verified");
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
