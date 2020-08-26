using System;

namespace Customer.DataAccess.BusinessObject
{
    public class PersonalDetails
    {
        public PersonalDetails() { }
        public DateTimeOffset DOB { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
    }
}