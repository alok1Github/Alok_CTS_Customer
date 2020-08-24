namespace Customer.API.Models
{
    public class AddressModel
    {
        public string Line { get; set; }
        public string ZipCode { get; set; }
        public string city { get; set; }

        // This is needed for serialization
        public AddressModel()
        { }
    }
}