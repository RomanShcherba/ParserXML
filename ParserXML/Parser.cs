using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Xml.Linq;

public class Program
{
    /// <summary>
    /// New list
    /// </summary>
    public static List<Contact> Contacts = new List<Contact>();

    public static void Main()
    {
        string filePath = "contacts.xml";
        ParseContacts(filePath);

        while (true)
        {
            Console.WriteLine("1. Display Contacts");
            Console.WriteLine("2. Search Contacts");
            Console.WriteLine("3. Exit");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    DisplayContacts();
                    break;
                case "2":
                    SearchContacts();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }
    /// <summary>
    /// Class for properties in Contact
    /// </summary>
    public class Contact
    {
        public string Name { get; set; }
        public Dictionary<string, string> Phones { get; set; }
        public Address Address { get; set; }
        public decimal NetWorth { get; set; }

        public Contact()
        {
            Phones = new Dictionary<string, string>();
        }
    }
    /// <summary>
    /// Class for properties in Address
    /// </summary>
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postal { get; set; }
    }
    /// <summary>
    /// Parsing contacts
    /// </summary>
    /// <param name="filePath">The parameter that points to the path to the file</param>
    public static void ParseContacts(string filePath)
    {
        XElement root = XElement.Load(filePath);

        var contacts = from contact in root.Elements("Contact")
                       select new Contact
                       {
                           Name = (string)contact.Element("Name"),
                           Phones = contact.Elements("Phone")
                                           .ToDictionary(p => (string)p.Attribute("Type"), p => (string)p),
                           Address = new Address
                           {
                               Street = (string)contact.Element("Address").Element("Street"),
                               City = (string)contact.Element("Address").Element("City"),
                               State = (string)contact.Element("Address").Element("State"),
                               Postal = (string)contact.Element("Address").Element("Postal"),
                           },
                           NetWorth = ParseNetWorth(contact.Element("NetWorth")?.Value)
                       };

        Contacts.AddRange(contacts);
    }
    /// <summary>
    /// Method for display information
    /// </summary>
    public static void DisplayContacts()
    {
        foreach (var contact in Contacts)
        {
            Console.WriteLine($"Name: {contact.Name}");
            foreach (var phone in contact.Phones)
            {
                Console.WriteLine($"{phone.Key} Phone: {phone.Value}");
            }
            Console.WriteLine($"Address: {contact.Address.Street}, {contact.Address.City}, {contact.Address.State}, {contact.Address.Postal}");
            Console.WriteLine($"Net worth: {contact.NetWorth}");
            Console.WriteLine();

        }
    }
    /// <summary>
    /// Searching in console
    /// </summary>
    public static void SearchContacts()
    {
        Console.WriteLine("Search by:");
        Console.WriteLine("1. Name:");
        Console.WriteLine("2. Phone:");
        Console.WriteLine("3. City:");
        Console.WriteLine("4. Net Worth:");
        Console.Write("Choose an option: ");
        string option = Console.ReadLine();
        Console.Write("Enter search option: ");
        string searchOption = Console.ReadLine();

        var contactFilter = new List<Contact>();

        switch (option)
        {
            case "1":
                contactFilter = FilterContacts("Name", searchOption);
                break;
            case "2":
                contactFilter = FilterContacts("Phone", searchOption);
                break;
            case "3":
                contactFilter = FilterContacts("City", searchOption);
                break;
            case "4":
                contactFilter = FilterContacts("NetWorth", searchOption);
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }

        if (searchOption.Any())
        {
            foreach (var contact in contactFilter)
            {
                Console.WriteLine($"Name: {contact.Name}");
                foreach (var phone in contact.Phones)
                {
                    Console.WriteLine($"{phone.Key} Phone: {phone.Value}");
                }
                Console.WriteLine($"Address: {contact.Address.Street}, {contact.Address.City}, {contact.Address.State}, {contact.Address.Postal}");
                Console.WriteLine($"Net Worth: {contact.NetWorth}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No contacts found.");
        }
    }
    /// <summary>
    /// The method by which the search is performed
    /// </summary>
    /// <param name="criteria">searching by field criteria</param>
    /// <param name="searchOption">parameter by searching</param>
    /// <returns></returns>
    public static List<Contact> FilterContacts(string criteria, string searchOption)
    {
        switch (criteria)
        {
            case "Name":
                return Contacts.Where(c => c.Name.IndexOf(searchOption, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            case "Phone":
                return Contacts.Where(c => c.Phones.Values.Any(p => p.IndexOf(searchOption, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();
            case "City":
                return Contacts.Where(c => c.Address.City.IndexOf(searchOption, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            case "NetWorth":
                if (decimal.TryParse(searchOption, out decimal netWorth))
                {
                    return Contacts.Where(c => c.NetWorth == netWorth).ToList();
                }
                else
                {
                    Console.WriteLine("Invalid Net Worth.");
                    return new List<Contact>();
                }
            default:
                Console.WriteLine("Invalid criteria.");
                return new List<Contact>();
        }
    }

    /// <summary>
    /// Parsing net worth from string to value 
    /// </summary>
    /// <param name="value">parameter for net worth</param>
    /// <returns></returns>
    private static decimal ParseNetWorth(string value)
    {
        if (string.IsNullOrEmpty(value) || value == "None")
        {
            return 0;
        }
        if (decimal.TryParse(value, out decimal result))
        {
            return result;
        }
        return 0;
    }

}