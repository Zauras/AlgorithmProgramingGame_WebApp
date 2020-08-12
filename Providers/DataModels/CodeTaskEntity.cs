using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
    
namespace AlgorithmProgramingGame_WebApp.Providers.DataModels
{
    public class CodeTaskEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public int CodeTaskId { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }
    }
}