    using lab_2_6.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MongoDB.Driver;
    using System;
    
    namespace lab_2_6
    {
        class Program
        {
            static void Main(string[] args)
            {
                var serviceProvider = CreateServiceProvider();
    
                var customerService = serviceProvider.GetRequiredService<CustomerService>();
                var shipmentService = serviceProvider.GetRequiredService<ShipmentService>();
                var packageService = serviceProvider.GetRequiredService<PackageService>();
                var reviewService = serviceProvider.GetRequiredService<ReviewService>();
                var serviceService = serviceProvider.GetRequiredService<ServiceService>();
                var transactionService = serviceProvider.GetRequiredService<TransactionService>();
    
                MainMenu(customerService, shipmentService, packageService, reviewService, serviceService, transactionService);
            }
    
            private static void MainMenu(CustomerService customerService, ShipmentService shipmentService, PackageService packageService, ReviewService reviewService, ServiceService serviceService, TransactionService transactionService)
            {
                string option;
                do
                {
                    Console.WriteLine("Main Menu:");
                    Console.WriteLine("1. Customer Menu");
                    Console.WriteLine("2. Shipment Menu");
                    Console.WriteLine("3. Package Menu");
                    Console.WriteLine("4. Review Menu");
                    Console.WriteLine("5. Service Menu");
                    Console.WriteLine("6. Transaction Menu");
                    Console.WriteLine("0. Exit");
    
                    option = Console.ReadLine();
    
                    switch (option)
                    {
                        case "1":
                            CustomerMenu(customerService);
                            break;
                        case "2":
                            ShipmentMenu(shipmentService);
                            break;
                        case "3":
                            PackageMenu(packageService);
                            break;
                        case "4":
                            ReviewMenu(reviewService);
                            break;
                        case "5":
                            ServiceMenu(serviceService);
                            break;
                        case "6":
                            TransactionMenu(transactionService);
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
    
                } while (option != "0");
            }
    
            private static void CustomerMenu(CustomerService customerService)
            {
                string option;
                do
                {
                    Console.WriteLine("Customer Menu:");
                    Console.WriteLine("1. List all Customers");
                    Console.WriteLine("2. Create a new Customer");
                    Console.WriteLine("3. Update an existing Customer");
                    Console.WriteLine("4. Delete a Customer");
                    Console.WriteLine("0. Back to main menu");
    
                    option = Console.ReadLine();
    
                    switch (option)
                    {
                        case "1":
                            customerService.ListAll();
                            break;
                        case "2":
                            customerService.Create();
                            break;
                        case "3":
                            customerService.Update();
                            break;
                        case "4":
                            customerService.Delete();
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
    
                } while (option != "0");
            }
    
            private static void ShipmentMenu(ShipmentService shipmentService)
            {
                string option;
                do
                {
                    Console.WriteLine("Shipment Menu:");
                    Console.WriteLine("1. List all Shipments");
                    Console.WriteLine("2. Create a new Shipment");
                    Console.WriteLine("3. Update an existing Shipment");
                    Console.WriteLine("4. Delete a Shipment");
                    Console.WriteLine("0. Back to main menu");
    
                    option = Console.ReadLine();
    
                    switch (option)
                    {
                        case "1":
                            shipmentService.ListAll();
                            break;
                        case "2":
                            shipmentService.Create();
                            break;
                        case "3":
                            shipmentService.Update();
                            break;
                        case "4":
                            shipmentService.Delete();
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
    
                } while (option != "0");
            }
    
            private static void PackageMenu(PackageService packageService)
            {
                string option;
                do
                {
                    Console.WriteLine("Package Menu:");
                    Console.WriteLine("1. List all Packages");
                    Console.WriteLine("2. Create a new Package");
                    Console.WriteLine("3. Update an existing Package");
                    Console.WriteLine("4. Delete a Package");
                    Console.WriteLine("0. Back to main menu");
    
                    option = Console.ReadLine();
    
                    switch (option)
                    {
                        case "1":
                            packageService.ListAll();
                            break;
                        case "2":
                            packageService.Create();
                            break;
                        case "3":
                            packageService.Update();
                            break;
                        case "4":
                            packageService.Delete();
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
    
                } while (option != "0");
            }
    
            private static void ReviewMenu(ReviewService reviewService)
            {
                string option;
                do
                {
                    Console.WriteLine("Review Menu:");
                    Console.WriteLine("1. List all Reviews");
                    Console.WriteLine("2. Create a new Review");
                    Console.WriteLine("3. Update an existing Review");
                    Console.WriteLine("4. Delete a Review");
                    Console.WriteLine("0. Back to main menu");
    
                    option = Console.ReadLine();
    
                    switch (option)
                    {
                        case "1":
                            reviewService.ListAll();
                            break;
                        case "2":
                            reviewService.Create();
                            break;
                        case "3":
                            reviewService.Update();
                            break;
                        case "4":
                            reviewService.Delete();
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
    
                } while (option != "0");
            }
    
            private static void ServiceMenu(ServiceService serviceService)
            {
                string option;
                do
                {
                    Console.WriteLine("Service Menu:");
                    Console.WriteLine("1. List all Services");
                    Console.WriteLine("2. Create a new Service");
                    Console.WriteLine("3. Update an existing Service");
                    Console.WriteLine("4. Delete a Service");
                    Console.WriteLine("0. Back to main menu");
    
                    option = Console.ReadLine();
    
                    switch (option)
                    {
                        case "1":
                            serviceService.ListAll();
                            break;
                        case "2":
                            serviceService.Create();
                            break;
                        case "3":
                            serviceService.Update();
                            break;
                        case "4":
                            serviceService.Delete();
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
    
                } while (option != "0");
            }
    
            private static void TransactionMenu(TransactionService transactionService)
            {
                string option;
                do
                {
                    Console.WriteLine("Transaction Menu:");
                    Console.WriteLine("1. List all Transactions");
                    Console.WriteLine("2. Create a new Transaction");
                    Console.WriteLine("3. Update an existing Transaction");
                    Console.WriteLine("4. Delete a Transaction");
                    Console.WriteLine("0. Back to main menu");
    
                    option = Console.ReadLine();
    
                    switch (option)
                    {
                        case "1":
                            transactionService.ListAll();
                            break;
                        case "2":
                            transactionService.Create();
                            break;
                        case "3":
                            transactionService.Update();
                            break;
                        case "4":
                            transactionService.Delete();
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
    
                } while (option != "0");
            }
    
            private static ServiceProvider CreateServiceProvider()
            {
                var services = new ServiceCollection();
    
                // MongoDB configuration
                services.AddSingleton<IMongoClient, MongoClient>(sp =>
                {
                    var connectionString = "mongodb://localhost:27017"; // Change if necessary
                    return new MongoClient(connectionString);
                });
    
                services.AddScoped(sp =>
                {
                    var client = sp.GetRequiredService<IMongoClient>();
                    return client.GetDatabase("post_service"); // Change if necessary
                });
    
                services.AddScoped<CustomerService>();
                services.AddScoped<ShipmentService>();
                services.AddScoped<PackageService>();
                services.AddScoped<ReviewService>();
                services.AddScoped<ServiceService>();
                services.AddScoped<TransactionService>();
    
                return services.BuildServiceProvider();
            }
        }
    }