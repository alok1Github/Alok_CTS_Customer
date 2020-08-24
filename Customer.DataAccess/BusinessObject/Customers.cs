using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.DataAccess.BusinessObject
{
    public class Customers : BusinessObjectBase
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        // This is needed for serialization
        public Customers()
        { }
    }
}
