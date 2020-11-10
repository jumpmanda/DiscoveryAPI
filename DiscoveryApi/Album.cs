using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Runtime.Serialization;

namespace DiscoveryApi
{
    [DataContract]
    public class Album
    {
        [DataMember(Name = "id")]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        [DataMember(Name = "artist")]
        public string Artist { get; set; }

        [DataMember(Name="title")]
        public string Title { get; set; }

        [DataMember(Name = "genre")]
        public string Genre { get; set; }

    }  
}
