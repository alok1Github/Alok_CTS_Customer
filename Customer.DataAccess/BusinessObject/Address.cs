namespace Customer.DataAccess.BusinessObject
{
    public class Address : BusinessObjectBase
    {
        public string Line { get; set; }
        public string ZipCode { get; set; }
        public string city { get; set; }
        public Address()
        { }
    }
}
