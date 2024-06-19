using ParserXML;
using System;

/// <summary>
/// Program
/// </summary>
public class Program
{
    /// <summary>
    /// Main
    /// </summary>
    public static void Main()
    {
        string filePath = "contacts.xml";
        Parser.ParseContacts(filePath);

        while (true)
        {
            Console.WriteLine("\n" +
                "1. Display Contacts\n" +
                "2. Search Contacts\n" +
                "3. Exit\n" +
                "Choose option:");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Parser.DisplayContacts();
                    break;
                case "2":
                    Parser.SearchContacts();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

}