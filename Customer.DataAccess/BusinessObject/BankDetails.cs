using Newtonsoft.Json;

namespace Customer.DataAccess.BusinessObject
{
    public class BankDetails
    {
        public BankDetails()
        { }
        [JsonProperty(PropertyName = "sortCode")]
        public string SortCode { get; set; }

        [JsonProperty(PropertyName = "accountName")]
        public string AccountName { get; set; }

        [JsonProperty(PropertyName = "accountNumber")]
        public string AccountNumber { get; set; }
    }
}