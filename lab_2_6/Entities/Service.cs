using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace lab_2_6.Entities
{
    public class Service
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("serviceid")]
        public long ServiceID { get; set; }

        [BsonElement("servicename")]
        public string ServiceName { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }
    }
}