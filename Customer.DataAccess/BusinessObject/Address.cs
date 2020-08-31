using Newtonsoft.Json;

namespace Customer.DataAccess.BusinessObject
{
    public class Address
    {
        [JsonProperty(PropertyName = "line")]
        public string Line { get; set; }

        [JsonProperty(PropertyName = "zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        public Address()
        { }
    }
}
