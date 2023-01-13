using HomefulBackEnd.Models;
using HomefulBackEnd.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HomefulBackEnd.Services
{
    public class LoginService : ILoginService
    {
        private readonly IMongoCollection<CompleteProfile> _profilesCollection;

        public LoginService(

            IOptions<HomefulDatabaseSettings> homefulDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                homefulDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                homefulDatabaseSettings.Value.DatabaseName);

            _profilesCollection = mongoDatabase.GetCollection<CompleteProfile>(
                homefulDatabaseSettings.Value.ProfilesCollectionName);
        }

        public async Task<CompleteProfile?> GetAsync(string id)
        {
            Console.WriteLine("hit");
            CompleteProfile profile = await _profilesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return profile;
        }


    }
}
