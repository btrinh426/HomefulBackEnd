using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomefulBackEnd.Models
{
    public class CompleteProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public Profile Profile { get; set; } = null!;

        public List<string> Position { get; set; } = null!;

        public List<Area> AreaOfChoice { get; set; } = null!;

        public List<string> IdealArea { get; set; } = null!;

    }
}
