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
        public string customerId { get; set; }

        [JsonProperty(PropertyName = "bankDetails")]
        public BankDetailsModel BankDetails { get; set; }

        [JsonProperty(PropertyName = "ttl", NullValueHandling = NullValueHandling.Ignore)]
        public int? TTL { get; set; }

        [JsonProperty(PropertyName = "address")]
        public AddressModel Address { get; set; }

        [JsonProperty(PropertyName = "personalDetail")]
        public PersonalDetailsModel PersonalDetail { get; set; }


    }
}
