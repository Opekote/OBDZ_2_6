using lab_2_6.Entities;
using MongoDB.Driver;
using System;
using MongoDB.Bson;

namespace lab_2_6.Services
{
    public class ShipmentService
    {
        private readonly IMongoCollection<Shipment> _shipments;

        public ShipmentService(IMongoDatabase database)
        {
            _shipments = database.GetCollection<Shipment>("shipments");
        }

        public void ListAll()
        {
            var shipments = _shipments.Find(shipment => true).ToList();
            foreach (var shipment in shipments)
            {
                Console.WriteLine($"{shipment.ShipmentID}: {shipment.TrackingNumber}, {shipment.SenderID}, {shipment.RecipientID}, {shipment.Weight}, {shipment.Dimensions}, {shipment.Status}, {shipment.PackageId}");
            }
        }

        public void Create()
        {
            string trackingNumber, dimensions, status, senderAddress, recipientAddress, input;
            decimal weight;
            long packageId, shipmentid, senderID, recipientID;
            DateTime? deliveryDate;
            
            
            do 
            {
                Console.WriteLine("Enter shipment ID:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out shipmentid) || shipmentid < 0);

            do
            {
                Console.WriteLine("Enter Tracking Number:");
                trackingNumber = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(trackingNumber));
            
            
            do 
            {
                Console.WriteLine("Enter package ID:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out packageId) || packageId < 0);

            do
            {
                Console.WriteLine("Enter Sender ID:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out senderID) || senderID < 0);

            do
            {
                Console.WriteLine("Enter Recipient ID:");
                 input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out recipientID) || recipientID < 0);

            do
            {
                Console.WriteLine("Enter Weight:");
            } while (!decimal.TryParse(Console.ReadLine(), out weight) || weight < 0);

            do
            {
                Console.WriteLine("Enter Dimensions:");
                dimensions = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(dimensions));

            do
            {
                Console.WriteLine("Enter Status:");
                status = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(status));

            do
            {
                Console.WriteLine("Enter Delivery Date (optional, format: yyyy-MM-dd):");
                input = Console.ReadLine();
                deliveryDate = string.IsNullOrEmpty(input) ? (DateTime?)null : DateTime.Parse(input);
            } while (deliveryDate != null && deliveryDate <= DateTime.Now);

            do
            {
                Console.WriteLine("Enter Sender Address:");
                senderAddress = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(senderAddress));

            do
            {
                Console.WriteLine("Enter Recipient Address:");
                recipientAddress = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(recipientAddress));

            var shipment = new Shipment
            {
                ShipmentID = shipmentid,
                TrackingNumber = trackingNumber,
                SenderID = senderID,
                RecipientID = recipientID,
                Weight = weight,
                Dimensions = dimensions,
                ShipmentDate = DateTime.Now,
                DeliveryDate = deliveryDate,
                Status = status,
                SenderAddress = senderAddress,
                RecipientAddress = recipientAddress
            };

            _shipments.InsertOne(shipment);
        }

        public void Update()
        {
            long shipmentid;
            string input;
            
            do 
            {
                Console.WriteLine("Enter shipment ID:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out shipmentid) || shipmentid < 0);
            var shipment = _shipments.Find(s => s.ShipmentID == shipmentid).FirstOrDefault();
            
            if (shipment != null)
            {
                Console.WriteLine("Select the property you want to update:");
                Console.WriteLine("1. Tracking Number");
                Console.WriteLine("2. Weight");
                Console.WriteLine("3. Dimensions");
                Console.WriteLine("4. Status");
                Console.WriteLine("5. Delivery Date");
                Console.WriteLine("6. Sender Address");
                Console.WriteLine("7. Recipient Address");
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
                                Console.WriteLine("Enter new Tracking Number:");
                                shipment.TrackingNumber = Console.ReadLine();
                            } while (string.IsNullOrWhiteSpace(shipment.TrackingNumber));
                            break;
                        case "2":
                            decimal newWeight;
                            do
                            {
                                Console.WriteLine("Enter new Weight:");
                            } while (!decimal.TryParse(Console.ReadLine(), out newWeight) || newWeight < 0);
                            shipment.Weight = newWeight;
                            break;
                        case "3":
                            do
                            {
                                Console.WriteLine("Enter new Dimensions:");
                                shipment.Dimensions = Console.ReadLine();
                            } while (string.IsNullOrWhiteSpace(shipment.Dimensions));
                            break;
                        case "4":
                            do
                            {
                                Console.WriteLine("Enter new Status:");
                                shipment.Status = Console.ReadLine();
                            } while (string.IsNullOrWhiteSpace(shipment.Status));
                            break;
                        case "5":
                            DateTime newDeliveryDate;
                            do
                            {
                                Console.WriteLine("Enter new Delivery Date (format: yyyy-MM-dd):");
                            } while (!DateTime.TryParse(Console.ReadLine(), out newDeliveryDate));
                            shipment.DeliveryDate = newDeliveryDate;
                            break;
                        case "6":
                            do
                            {
                                Console.WriteLine("Enter new Sender Address:");
                                shipment.SenderAddress = Console.ReadLine();
                            } while (string.IsNullOrWhiteSpace(shipment.SenderAddress));
                            break;
                        case "7":
                            do
                            {
                                Console.WriteLine("Enter new Recipient Address:");
                                shipment.RecipientAddress = Console.ReadLine();
                            } while (string.IsNullOrWhiteSpace(shipment.RecipientAddress));
                            break;
                        case "0":
                            return; // Cancel update
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
                } while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5" && choice != "6" && choice != "7" && choice != "0");

                _shipments.ReplaceOne(s => s.ShipmentID == shipmentid, shipment);
            }
            else
            {
                Console.WriteLine("Shipment not found.");
            }
        }

        public void Delete()
        {
            long shipmentid;
            string input;
            
            do 
            {
                Console.WriteLine("Enter package ID:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out shipmentid) || shipmentid < 0);
            Console.WriteLine("Enter Shipment ID to delete:");
            var id = Console.ReadLine();
            _shipments.DeleteOne(shipment => shipment.ShipmentID == shipmentid);
        }
        
    }
}
