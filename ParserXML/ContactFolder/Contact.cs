using System.Collections.Generic;

namespace ParserXML
{

    /// <summary>
    /// Contact section
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        public Dictionary<string, string> Phones { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Net Worth
        /// </summary>
        public string NetWorth { get; set; }
    }
}
