using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace lab_2_6.Entities
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("reviewid")]
        public long ReviewID { get; set; }

        [BsonElement("customerid")]
        public long CustomerID { get; set; }

        [BsonElement("shipmentid")]
        public long ShipmentID { get; set; }

        [BsonElement("rating")]
        public int Rating { get; set; }

        [BsonElement("comment")]
        public string Comment { get; set; }

        [BsonElement("reviewdate")]
        public DateTime ReviewDate { get; set; }
    }
}