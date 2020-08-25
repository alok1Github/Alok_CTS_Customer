using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.DataAccess.BusinessObject
{
    public class Customers : BusinessObjectBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "customerId")]
        public string CustomerId { get; set; }
        public BankDetails BankDetails { get; set; }
        public Address Address { get; set; }

        public PersonalDetails PersonalDetail { get; set; }

        // This is needed for serialization
        public Customers()
        { }
    }
}
