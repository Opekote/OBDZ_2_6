using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace lab_2_6.Entities
{
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        
        public string Id { get; set; }
        
        
        [BsonElement("transactionid")]
        public long TransactionID { get; set; }

        [BsonElement("shipmentid")]
        public long ShipmentID { get; set; }
        
        
        [BsonElement("serviceid")]
        public long ServiceID { get; set; }
        
        [BsonElement("transactiondate")]

        public DateTime TransactionDate { get; set; }
        
        [BsonElement("amount")]
        public decimal Amount { get; set; }
    }
}