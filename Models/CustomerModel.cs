using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.DataAccess.BusinessObject;
using Newtonsoft.Json;

namespace Customer.API.Models
{
    public class CustomerModel
    {
        [JsonProperty(PropertyName = "customerId")]
        public string CustomerId { get; set; }

        // public BankDetails BankDetails { get; set; }

        [JsonProperty(PropertyName = "address")]
        public AddressModel Address { get; set; }

        //  public PersonalDetails PersonalDetail { get; set; }

        public CustomerModel()
        {
        }
    }
}
