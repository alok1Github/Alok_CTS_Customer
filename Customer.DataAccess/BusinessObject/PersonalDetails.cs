using Newtonsoft.Json;
using System;

namespace Customer.DataAccess.BusinessObject
{
    public class PersonalDetails
    {
        public PersonalDetails() { }
        [JsonProperty(PropertyName = "dob")]
        public DateTimeOffset DOB { get; set; }
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}