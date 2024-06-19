using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserXML
{
    /// <summary>
    /// Class for properties in Address
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Property for street information
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Property for display city
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Property for state information
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Property for postal information
        /// </summary>
        public string Postal { get; set; }
    }
}
