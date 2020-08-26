using Newtonsoft.Json;
using System;

namespace Customer.API.Models
{
    public class SerachModel
    {
        [JsonProperty(PropertyName = "dob")]
        public DateTimeOffset? DOB { get; set; }

        [JsonProperty(PropertyName = "zipCode")]
        public string ZipCode { get; set; }
        public SerachModel()
        { }
    }
}