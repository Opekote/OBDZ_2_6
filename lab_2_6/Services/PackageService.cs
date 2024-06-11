using lab_2_6.Entities;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using MongoDB.Bson;

namespace lab_2_6.Services
{
    public class PackageService
    {
        private readonly IMongoCollection<Package> _packages;

        public PackageService(IMongoDatabase database)
        {
            _packages = database.GetCollection<Package>("packages");
        }

        public void ListAll()
        {
            var packages = _packages.Find(package => true).ToList();
            foreach (var package in packages)
            {
                Console.WriteLine($"{package.PackageID}: {package.PackageType}, {package.ContentDescription}, {package.Value}");
            }
        }

      

        public void Create()
        {
            long packageid, shipmentid;
            string packageType, contentDescription, input;
            decimal value;
            
            do 
            {
                Console.WriteLine("Enter package ID:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out packageid) || packageid < 0);
            
            do 
            {
                Console.WriteLine("Enter shipment ID:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out shipmentid) || shipmentid < 0);

            do
            {
                Console.WriteLine("Enter Package Type:");
                packageType = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(packageType));

            do
            {
                Console.WriteLine("Enter Content Description:");
                contentDescription = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(contentDescription));

            do
            {
                Console.WriteLine("Enter Value:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !decimal.TryParse(input, out value) || value < 0);

            var package = new Package
            {
                PackageID = packageid,
                PackageType = packageType,
                ShipmentID = shipmentid,
                ContentDescription = contentDescription,
                Value = value
            };

            _packages.InsertOne(package);
        }

        public void Update()
        {
            string packageType, contentDescription, input;
            long packageid, shipmentid;
            decimal value;

            do 
            {
                Console.WriteLine("Enter package ID:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out packageid) || packageid < 0);

            var package = _packages.Find(p => p.PackageID == packageid).FirstOrDefault();
            if (package != null)
            {
                Console.WriteLine("Select the property you want to update:");
                Console.WriteLine("1. Package Type");
                Console.WriteLine("2. Content Description");
                Console.WriteLine("3. Value");
                Console.WriteLine("4. ShipmentId");
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
                                Console.WriteLine("Enter Package Type:");
                                packageType = Console.ReadLine();
                            } while (string.IsNullOrWhiteSpace(packageType));
                            package.PackageType = packageType;
                            break;
                        case "2":
                            do
                            {
                                Console.WriteLine("Enter Content Description:");
                                contentDescription = Console.ReadLine();
                            } while (string.IsNullOrWhiteSpace(contentDescription));
                            package.ContentDescription = contentDescription;
                            break;
                        case "3":
                            do
                            {
                                Console.WriteLine("Enter Value:");
                                input = Console.ReadLine();
                            } while (string.IsNullOrWhiteSpace(input) || !decimal.TryParse(input, out value) || value < 0);
                            package.Value = value;
                            break;
                        case "4":
                            do
                            {
                                Console.WriteLine("Enter ShipmentId:");
                                input = Console.ReadLine();
                            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out shipmentid) || shipmentid < 0);
                            package.ShipmentID = shipmentid;
                            break;
                        case "0":
                            return; // Cancel update
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
                } while (choice != "1" && choice != "2" && choice != "3" && choice != "0");

                _packages.ReplaceOne(p => p.PackageID == packageid, package);
            }
            else
            {
                Console.WriteLine("Package not found.");
            }
        }

        public void Delete()
        {

            string input;
            long packageid;
            do 
            {
                Console.WriteLine("Enter Package ID to delete:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out packageid) || packageid < 0);


            _packages.DeleteOne(package => package.PackageID == packageid);
        }
    }
}
