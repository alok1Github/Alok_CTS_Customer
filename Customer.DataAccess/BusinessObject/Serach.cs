using Newtonsoft.Json;
using System;

namespace Customer.DataAccess.BusinessObject
{
    public class Serach
    {
        [JsonProperty(PropertyName = "dob")]
        public DateTimeOffset? DOB { get; set; }

        [JsonProperty(PropertyName = "zipCode")]
        public string ZipCode { get; set; }
        public Serach()
        { }
    }
}
