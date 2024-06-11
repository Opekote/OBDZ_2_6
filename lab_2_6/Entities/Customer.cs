using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace lab_2_6.Entities
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("customerid")]
        public long CustomerID { get; set; }

        [BsonElement("firstname")]
        public string FirstName { get; set; }

        [BsonElement("lastname")]
        public string LastName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonIgnore]
        public IList<Review> Reviews { get; set; }

        public Customer()
        {
            Reviews = new List<Review>();
        }
    }
}