using Newtonsoft.Json;

namespace Customer.API.Models
{
    public class BankDetailsModel
    {
        public BankDetailsModel()
        { }
        [JsonProperty(PropertyName = "sortCode")]
        public string SortCode { get; set; }

        [JsonProperty(PropertyName = "accountName")]
        public string AccountName { get; set; }

        [JsonProperty(PropertyName = "accountNumber")]
        public string AccountNumber { get; set; }
    }
}