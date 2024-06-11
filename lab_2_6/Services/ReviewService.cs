using lab_2_6.Entities;
using MongoDB.Driver;
using System;
using MongoDB.Bson;

namespace lab_2_6.Services
{
    public class ReviewService
    {
        private readonly IMongoCollection<Review> _reviews;

        public ReviewService(IMongoDatabase database)
        {
            _reviews = database.GetCollection<Review>("reviews");
        }

        public void ListAll()
        {
            var reviews = _reviews.Find(review => true).ToList();
            foreach (var review in reviews)
            {
                Console.WriteLine($"{review.ReviewID}: {review.Rating}, {review.Comment}, {review.ReviewDate}");
            }
        }

        public void Create()
        {
            string comment, input;
            long customerId, shipmentId, reviewId;
            int rating;

            do
            {
                Console.WriteLine("Enter review ID:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out reviewId) || reviewId < 0);
            
            do
            {
                Console.WriteLine("Enter Customer ID:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out customerId) || customerId < 0);
            

            do
            {
                Console.WriteLine("Enter Shipment ID:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out shipmentId) || shipmentId < 0);

            do
            {
                Console.WriteLine("Enter Rating (1-5):");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out rating) || rating < 1 || rating > 5);

            do
            {
                Console.WriteLine("Enter Comment:");
                comment = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(comment));

            var review = new Review
            {
                ReviewID = reviewId,
                CustomerID = customerId,
                ShipmentID = shipmentId,
                Rating = rating,
                Comment = comment,
                ReviewDate = DateTime.Now
            };

            _reviews.InsertOne(review);
        }

        public void Update()
        {
            string input, comment;
            int rating;

            long reviewId;

            do
            {
                Console.WriteLine("Enter Review ID to update:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out reviewId) || reviewId < 0);

            var review = _reviews.Find(r => r.ReviewID == reviewId).FirstOrDefault();
            if (review != null)
            {
                Console.WriteLine("Select the property you want to update:");
                Console.WriteLine("1. Rating");
                Console.WriteLine("2. Comment");
                Console.WriteLine("0. Cancel");

                string choice;
                do
                {
                    choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            do
                            {
                                Console.WriteLine("Enter new Rating (1-5):");
                                input = Console.ReadLine();
                            } while (!int.TryParse(input, out rating) || rating < 1 || rating > 5);
                            review.Rating = rating;
                            break;
                        case "2":
                            do
                            {
                                Console.WriteLine("Enter new Comment:");
                                comment = Console.ReadLine();
                            } while (string.IsNullOrWhiteSpace(comment));
                            review.Comment = comment;
                            break;
                        case "0":
                            return; // Cancel update
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
                } while (choice != "1" && choice != "2" && choice != "0");

                _reviews.ReplaceOne(r => r.ReviewID == reviewId, review);
            }
            else
            {
                Console.WriteLine("Review not found.");
            }
        }

        public void Delete()
        {
            long reviewId;
            string input;

            do
            {
                Console.WriteLine("Enter Review ID to delete:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out reviewId) || reviewId < 0);

            _reviews.DeleteOne(review => review.ReviewID == reviewId);
        }
    }
}
