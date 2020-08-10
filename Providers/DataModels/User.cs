﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AlgorithmProgramingGame_WebApp.Providers.DataModels
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string BookName { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }
    }
}