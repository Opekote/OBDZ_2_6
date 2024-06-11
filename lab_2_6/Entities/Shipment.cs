using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace lab_2_6.Entities
{
    public class Shipment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("shipmentid")]
        public long ShipmentID { get; set; }

        [BsonElement("trackingnumber")]
        public string TrackingNumber { get; set; }

        [BsonElement("senderid")]
        public long SenderID { get; set; }

        [BsonElement("recipientid")]
        public long RecipientID { get; set; }

        [BsonElement("weight")]
        public decimal Weight { get; set; }

        [BsonElement("dimensions")]
        public string Dimensions { get; set; }

        [BsonElement("shipmentdate")]
        public DateTime? ShipmentDate { get; set; }

        [BsonElement("deliverydate")]
        public DateTime? DeliveryDate { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("senderaddress")]
        public string SenderAddress { get; set; }

        [BsonElement("recipientaddress")]
        public string RecipientAddress { get; set; }

        [BsonIgnore]
        public IList<Review> Reviews { get; set; }
        
        [BsonElement("packageid")]

        public long PackageId { get; set; }

        public Shipment()
        {
            Reviews = new List<Review>();
        }
    }
}