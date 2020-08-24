using Newtonsoft.Json;

namespace Customer.API.Models
{
    public class AddressModel
    {
        [JsonProperty(PropertyName = "line")]
        public string Line { get; set; }

        [JsonProperty(PropertyName = "zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        // This is needed for serialization
        public AddressModel()
        { }
    }
}