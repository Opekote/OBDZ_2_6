using lab_2_6.Entities;
using MongoDB.Driver;
using System;
using MongoDB.Bson;

namespace lab_2_6.Services
{
    public class TransactionService
    {
        private readonly IMongoCollection<Transaction> _transactions;

        public TransactionService(IMongoDatabase database)
        {
            _transactions = database.GetCollection<Transaction>("transactions");
        }

        public void ListAll()
        {
            var transactions = _transactions.Find(transaction => true).ToList();
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"{transaction.TransactionID}: {transaction.ShipmentID}, {transaction.ServiceID}, {transaction.TransactionDate}, {transaction.Amount}");
            }
        }

        public void Create()
        {
            long shipmentID, serviceID, transactionID;

            string input;
            
            do 
            {
                Console.WriteLine("Enter transaction ID:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out transactionID) || transactionID < 0);

            do 
            {
                Console.WriteLine("Enter shipment ID:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out shipmentID) || shipmentID < 0);
            
            
            do 
            {
                Console.WriteLine("Enter Service ID:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out serviceID) || serviceID < 0);
            

            DateTime transactionDate;
            do
            {
                Console.WriteLine("Enter Transaction Date (yyyy-MM-dd):");
            } while (!DateTime.TryParse(Console.ReadLine(), out transactionDate));

            decimal amount;
            do
            {
                Console.WriteLine("Enter Amount:");
            } while (!decimal.TryParse(Console.ReadLine(), out amount));

            var transaction = new Transaction
            {
                TransactionID = transactionID,
                ShipmentID = shipmentID,
                ServiceID = serviceID,
                TransactionDate = transactionDate,
                Amount = amount
            };

            _transactions.InsertOne(transaction);
        }

        public void Update()
        {
            string input;
            long transactionID;
            
            do 
            {
                Console.WriteLine("Enter transaction ID to update:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out transactionID) || transactionID < 0);

            var transaction = _transactions.Find(t => t.TransactionID == transactionID).FirstOrDefault();
            if (transaction != null)
            {
                Console.WriteLine("Select the property you want to update:");
                Console.WriteLine("1. Shipment ID");
                Console.WriteLine("2. Service ID");
                Console.WriteLine("3. Transaction Date");
                Console.WriteLine("4. Amount");
                Console.WriteLine("0. Cancel");

                string choice;
                do
                {
                    choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            long newShipmentID;
                            do
                            {
                                Console.WriteLine("Enter new Shipment ID:");
                                input = Console.ReadLine();
                            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out newShipmentID) || newShipmentID < 0);
                            transaction.ShipmentID = newShipmentID;
                            break;
                        case "2":
                            long newServiceID;
                            do
                            {
                                Console.WriteLine("Enter new Service ID:");
                                input = Console.ReadLine();
                            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out newServiceID) || newServiceID < 0);
                            transaction.ServiceID = newServiceID;
                            break;
                        case "3":
                            DateTime newTransactionDate;
                            do
                            {
                                Console.WriteLine("Enter new Transaction Date (yyyy-MM-dd):");
                            } while (!DateTime.TryParse(Console.ReadLine(), out newTransactionDate));
                            transaction.TransactionDate = newTransactionDate;
                            break;
                        case "4":
                            decimal newAmount;
                            do
                            {
                                Console.WriteLine("Enter new Amount:");
                            } while (!decimal.TryParse(Console.ReadLine(), out newAmount));
                            transaction.Amount = newAmount;
                            break;
                        case "0":
                            return; // Cancel update
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
                } while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "0");

                _transactions.ReplaceOne(t => t.TransactionID == transactionID, transaction);
            }
            else
            {
                Console.WriteLine("Transaction not found.");
            }
        }

        public void Delete()
        {
            string input;
            long transactionID;
            do 
            {
                Console.WriteLine("Enter transaction ID to delete:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out transactionID) || transactionID < 0);

            _transactions.DeleteOne(transaction => transaction.TransactionID == transactionID);
        }
    }
}
