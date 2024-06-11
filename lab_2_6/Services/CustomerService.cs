using lab_2_6.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MongoDB.Bson;

namespace lab_2_6.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerService(IMongoDatabase database)
        {
            _customers = database.GetCollection<Customer>("customers");
        }

        public void ListAll()
        {
            var customers = _customers.Find(customer => true).ToList();
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.CustomerID}: {customer.FirstName} {customer.LastName}, {customer.Email}, {customer.Phone}");
            }
        }

        public void Create()
        {
            long customerId;
            string firstName, lastName, email, phone;

            do
            {
                Console.WriteLine("Enter Customer Id:");
                customerId = long.Parse(Console.ReadLine());
            } while (customerId <= 0);

            do
            {
                Console.WriteLine("Enter Customer First Name:");
                firstName = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(firstName));

            do
            {
                Console.WriteLine("Enter Customer Last Name:");
                lastName = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(lastName));

            do
            {
                Console.WriteLine("Enter Customer Email:");
                email = Console.ReadLine();
            } while (!IsValidEmail(email));

            do
            {
                Console.WriteLine("Enter Customer Phone:");
                phone = Console.ReadLine();
            } while (!IsValidPhoneNumber(phone));

            var customer = new Customer
            {
                CustomerID = customerId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone
            };

            _customers.InsertOne(customer);
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }

        private bool IsValidPhoneNumber(string phone)
        {
            return phone.All(char.IsDigit);
        }

        public void Update()
{
    long customerId;
    string input, firstName, lastName, email, phone;

    do
    {
        Console.WriteLine("Enter Customer ID to update:");
        input = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out customerId) || customerId < 0);

    var customer = _customers.Find(c => c.CustomerID == customerId).FirstOrDefault();
    if (customer != null)
    {
        bool continueUpdating = true;

        while (continueUpdating)
        {
            Console.WriteLine("Select the property you want to update:");
            Console.WriteLine("1. First Name");
            Console.WriteLine("2. Last Name");
            Console.WriteLine("3. Email");
            Console.WriteLine("4. Phone");
            Console.WriteLine("0. Cancel");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    do
                    {
                        Console.WriteLine("Enter First Name:");
                        firstName = Console.ReadLine();
                    } while (string.IsNullOrWhiteSpace(firstName));
                    customer.FirstName = firstName;
                    continueUpdating = false;
                    break;
                case "2":
                    do
                    {
                        Console.WriteLine("Enter Last Name:");
                        lastName = Console.ReadLine();
                    } while (string.IsNullOrWhiteSpace(lastName));
                    customer.LastName = lastName;
                    continueUpdating = false;
                    break;
                case "3":
                    do
                    {
                        Console.WriteLine("Enter Email:");
                        email = Console.ReadLine();
                    } while (!IsValidEmail(email));
                    customer.Email = email;
                    continueUpdating = false;
                    break;
                case "4":
                    do
                    {
                        Console.WriteLine("Enter Phone Number:");
                        phone = Console.ReadLine();
                    } while (!IsValidPhoneNumber(phone));
                    customer.Phone = phone;
                    continueUpdating = false;
                    break;
                case "0":
                    continueUpdating = false;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        _customers.ReplaceOne(c => c.CustomerID == customerId, customer);
    }
    else
    {
        Console.WriteLine("Customer not found.");
    }
}


        public void Delete()
        {
            long customerId;
            string input;

            do
            {
                Console.WriteLine("Enter Customer ID to delete:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out customerId) || customerId < 0);

            _customers.DeleteOne(customer => customer.CustomerID == customerId);
        }
    }
}
