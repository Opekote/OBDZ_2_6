using lab_2_6.Entities;
using MongoDB.Driver;
using System;
using MongoDB.Bson;

namespace lab_2_6.Services
{
    public class ServiceService
    {
        private readonly IMongoCollection<Service> _services;

        public ServiceService(IMongoDatabase database)
        {
            _services = database.GetCollection<Service>("services");
        }

        public void ListAll()
        {
            var services = _services.Find(service => true).ToList();
            foreach (var service in services)
            {
                Console.WriteLine($"{service.ServiceID}: {service.ServiceName}, {service.Description}, {service.Price}");
            }
        }

        public void Create()
        {
            string serviceName, description, input;
            decimal price;
            long serviceId;
            
            do
            {
                Console.WriteLine("Enter service ID:");
                input = Console.ReadLine();
            } while (!long.TryParse(input, out serviceId) || serviceId < 0);

            do
            {
                Console.WriteLine("Enter Service Name:");
                serviceName = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(serviceName));

            do
            {
                Console.WriteLine("Enter Description:");
                description = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(description));

            do
            {
                Console.WriteLine("Enter Price:");
                input = Console.ReadLine();
            } while (!decimal.TryParse(input, out price) || price < 0);

            var service = new Service
            {
                ServiceID = serviceId,
                ServiceName = serviceName,
                Description = description,
                Price = price
            };

            _services.InsertOne(service);
        }

        public void Update()
        {
            string input, serviceName, description;
            decimal price;

            long serviceId;

            do
            {
                Console.WriteLine("Enter service ID to update:");
                input = Console.ReadLine();
            } while (!long.TryParse(input, out serviceId) || serviceId < 0);

            var service = _services.Find(s => s.ServiceID == serviceId).FirstOrDefault();
            if (service != null)
            {
                Console.WriteLine("Select the property you want to update:");
                Console.WriteLine("1. Service Name");
                Console.WriteLine("2. Description");
                Console.WriteLine("3. Price");
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
                                Console.WriteLine("Enter new Service Name:");
                                serviceName = Console.ReadLine();
                            } while (string.IsNullOrWhiteSpace(serviceName));
                            service.ServiceName = serviceName;
                            break;
                        case "2":
                            do
                            {
                                Console.WriteLine("Enter new Description:");
                                description = Console.ReadLine();
                            } while (string.IsNullOrWhiteSpace(description));
                            service.Description = description;
                            break;
                        case "3":
                            do
                            {
                                Console.WriteLine("Enter new Price:");
                                input = Console.ReadLine();
                            } while (!decimal.TryParse(input, out price) || price < 0);
                            service.Price = price;
                            break;
                        case "0":
                            return; // Cancel update
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
                } while (choice != "1" && choice != "2" && choice != "3" && choice != "0");

                _services.ReplaceOne(s => s.ServiceID == serviceId, service);
            }
            else
            {
                Console.WriteLine("Service not found.");
            }
        }

        public void Delete()
        {
            string input;
            long serviceId;
            do
            {
                Console.WriteLine("Enter service ID to delete:");
                input = Console.ReadLine();
            } while (!long.TryParse(input, out serviceId) || serviceId < 0);

            _services.DeleteOne(service => service.ServiceID == serviceId);
        }
    }
}
