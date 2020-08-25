using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Customer.DataAccess.BusinessObject;
using Newtonsoft.Json;

namespace Customer.API.Models
{
    public class CustomerModel
    {
        public CustomerModel()
        {
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "customerId")]
        public int CustomerId { get; set; }

        [JsonProperty(PropertyName = "bankDetails")]
        public BankDetailsModel BankDetails { get; set; }

        [JsonProperty(PropertyName = "address")]
        public AddressModel Address { get; set; }

        [JsonProperty(PropertyName = "personalDetail")]
        public PersonalDetailsModel PersonalDetail { get; set; }


    }
}
