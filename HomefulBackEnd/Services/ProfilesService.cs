using HomefulBackEnd.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using HomefulBackEnd.Services.Interfaces;

namespace HomefulBackEnd.Services
{
    public class ProfilesService : IProfilesService
    {
        private readonly IMongoCollection<CompleteProfile> _profilesCollection;

        public ProfilesService(
            IOptions<HomefulDatabaseSettings> homefulDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                homefulDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                homefulDatabaseSettings.Value.DatabaseName);

            _profilesCollection = mongoDatabase.GetCollection<CompleteProfile>(
                homefulDatabaseSettings.Value.ProfilesCollectionName);
        }

        public async Task CreateAsync(CompleteProfile newProfile)
        {
            PasswordHash gen = new PasswordHash();
            PasswordData hashed = gen.Hash(newProfile.Profile.Password, null);
            newProfile.Profile.Password = hashed.HashedPassword;
            newProfile.Profile._Salt= hashed.Salt;
            await _profilesCollection.InsertOneAsync(newProfile);

        }


        public async Task<List<CompleteProfile>> GetAllAsync() =>
            await _profilesCollection.Find(_ => true).ToListAsync();

        public async Task<CompleteProfile?> GetAsync(string id)
        {
            CompleteProfile profile = await _profilesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            
            return profile;
        }

        public async Task<CompleteProfile?> GetProfile(string username)
        {
            CompleteProfile profile = await _profilesCollection.Find(x => x.Profile.Email == username).FirstOrDefaultAsync();

            return profile;
        }
    }
}
