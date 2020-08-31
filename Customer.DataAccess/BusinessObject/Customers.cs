using Newtonsoft.Json;

namespace Customer.DataAccess.BusinessObject
{
    public class Customers
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "customerId")]
        public string customerId { get; set; }

        [JsonProperty(PropertyName = "ttl", NullValueHandling = NullValueHandling.Ignore)]
        public int? TTL { get; set; }

        [JsonProperty(PropertyName = "bankDetails")]
        public BankDetails BankDetails { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }

        [JsonProperty(PropertyName = "personalDetail")]
        public PersonalDetails PersonalDetail { get; set; }
        public Customers()
        { }
    }
}
