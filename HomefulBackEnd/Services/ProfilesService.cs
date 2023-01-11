using HomefulBackEnd.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;


namespace HomefulBackEnd.Services
{
    public class ProfilesService
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
            string hashed = gen.Hash(newProfile.Profile.Password);
            newProfile.Profile.Password = hashed;
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
