using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public class Contact
{
    public string Name { get; set; }
    public Dictionary<string, string> Phones { get; set; }
    public Address Address { get; set; }
    public decimal? NetWorth { get; set; }

    public Contact()
    {
        Phones = new Dictionary<string, string>();
    }
}

public class Address
{
    public string Street1 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Postal { get; set; }
}

public class Program
{
    public static List<Contact> Contacts = new List<Contact>();

    public static void Main(string[] args)
    {
        string filePath = "contacts.xml"; 
        ParseContacts(filePath);

        while (true)
        {
            Console.WriteLine("\n1. Display All Contacts");
            string option = Console.ReadLine();

        }
    }

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
                               Street1 = (string)contact.Element("Address").Element("Street1"),
                               City = (string)contact.Element("Address").Element("City"),
                               State = (string)contact.Element("Address").Element("State"),
                               Postal = (string)contact.Element("Address").Element("Postal"),
                           },
                           NetWorth = contact.Element("NetWorth").Value == "None" ? (decimal?)null : decimal.Parse(contact.Element("NetWorth").Value)
                       };

        Contacts.AddRange(contacts);
    }
}