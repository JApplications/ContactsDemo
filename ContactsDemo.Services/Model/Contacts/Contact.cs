using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsDemo.Services.Model.Contacts
{
    public class Contact : BaseModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<ContactNumber> ContactNumbers { get; set; }
    }
}
