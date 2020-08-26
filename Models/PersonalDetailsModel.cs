using Newtonsoft.Json;
using System;

namespace Customer.API.Models
{
    public class PersonalDetailsModel
    {
        public PersonalDetailsModel() { }

        [JsonProperty(PropertyName = "dob")]
        public DateTimeOffset DOB { get; set; }
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
}