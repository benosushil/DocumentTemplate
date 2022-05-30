using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentTemplate
{
    public class Template
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("json")]
        public string Json { get; set; }
    }

    public class Document<T>
    {
        public string Id { get; set; }

        public T document { get; set; }
    }
}