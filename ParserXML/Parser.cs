using ParserXML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Services;
using System.Xml.Linq;

public class Program
{
    /// <summary>
    /// List for display contact info
    /// </summary>
    public static List<Contact> Contacts = new List<Contact>();
    /// <summary>
    /// Method for parsing and searching
    /// </summary>
    public static void Main()
    {
        string filePath = "contacts.xml";
        ParseContacts(filePath);

        while (true)
        {
            Console.WriteLine("\n1. Display Contacts\n2. Search Contacts\n3. Exit");
            Console.Write("Choose option:");
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
    /// Method for parsing contacts
    /// </summary>
    /// <param name="filePath">Parameter for path to the file</param>
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
                           NetWorth = (string)contact.Element("NetWorth")
                       };

        Contacts.AddRange(contacts);
    }
    /// <summary>
    /// Method for display contact information
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
            string address = $"{contact.Address.Street}, {contact.Address.City}";
            if (!string.IsNullOrEmpty(contact.Address.State))
            {
                address += $",{contact.Address.State}";
            }
            //address += $", {contact.Address.Postal}, {contact.NetWorth}";
            Console.WriteLine($"Address:{address}");
            Console.WriteLine();

        }
    }
    /// <summary>
    /// Searching in console
    /// </summary>
    public static void SearchContacts()
    {
        Console.WriteLine("\nSearch by:\n1. Name\n2. Phone\n3. City\n4. Net Worth\n5. State");
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
            case "5":
                contactFilter = FilterContacts("State", searchOption);
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }

        if (contactFilter.Any())
        {
            foreach (var contact in contactFilter)
            {
                Console.WriteLine($"Name: {contact.Name}");
                foreach (var phone in contact.Phones)
                {
                    Console.WriteLine($"{phone.Key} Phone: {phone.Value}");
                }
                string address = $"{contact.Address.Street}, {contact.Address.City}";
                if (!string.IsNullOrEmpty(contact.Address.State))
                {
                    address += $", {contact.Address.State}";
                }
                address += $", {contact.Address.Postal}, {contact.NetWorth}";
                Console.WriteLine($"Address: {address}");
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
                return Contacts.Where(c => c.Name.StartsWith(searchOption, StringComparison.OrdinalIgnoreCase)).ToList();
            case "Phone":
                return Contacts.Where(c => c.Phones.Values.Any(p => p.StartsWith(searchOption, StringComparison.OrdinalIgnoreCase))).ToList();
            case "City":
                return Contacts.Where(c => c.Address.City.StartsWith(searchOption, StringComparison.OrdinalIgnoreCase)).ToList();
            case "NetWorth":
                return Contacts.Where(c => c.NetWorth.StartsWith(searchOption, StringComparison.OrdinalIgnoreCase)).ToList();
            case "State":
                return Contacts.Where(c => c.Address.State != null && c.Address.State.StartsWith(searchOption, StringComparison.OrdinalIgnoreCase)).ToList();
            default:
                Console.WriteLine("Invalid criteria.");
                return new List<Contact>();
        }
    }
}