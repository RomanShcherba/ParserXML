using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserXML
{
    /// <summary>
    /// Class for properties in Contact
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Property that parsing Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Key-value property for phone display
        /// </summary>
        public Dictionary<string, string> Phones { get; set; }
        /// <summary>
        /// Property that display address information
        /// </summary>
        public Address Address { get; set; }
        /// <summary>
        /// Property for net worth
        /// </summary>
        public decimal NetWorth { get; set; }

    }
}
