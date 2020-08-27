using Newtonsoft.Json;

namespace Customer.DataAccess.BusinessObject
{
    public class Customers
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "customerId")]
        public int CustomerId { get; set; }
        public BankDetails BankDetails { get; set; }
        public Address Address { get; set; }

        public PersonalDetails PersonalDetail { get; set; }
        public Customers()
        { }
    }
}
