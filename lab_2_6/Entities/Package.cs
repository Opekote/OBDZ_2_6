using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace lab_2_6.Entities
{
    public class Package
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        
        public string Id { get; set; }

        
        [BsonElement("packageid")]
        public long PackageID { get; set; }
        
        [BsonElement("shipmentid")]

        public long ShipmentID { get; set; }
        
        [BsonElement("packagetype")]
        public string PackageType { get; set; }
        
        [BsonElement("contendtdescription")]
        public string ContentDescription { get; set; }
        
        [BsonElement("value")]
        public decimal Value { get; set; }
    }
}