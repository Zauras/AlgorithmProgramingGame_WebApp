using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AlgorithmProgramingGame_WebApp.Providers.DataModels
{
    public class UserEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public int SubmissionsCount { get; set; }
        
        public ScoreEntity[] Scores { get; set; }

    }

    public class ScoreEntity
    {
        public int CodeTaskId { get; set; }
        
        public string SolutionCode { get; set; }
    }
}