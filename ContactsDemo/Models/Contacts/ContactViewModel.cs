using ContactsDemo.Services.Model.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsDemo.Api.Models.Contacts
{
    public class ContactViewModel
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<ContactNumberViewModel> ContactNumbers { get; set; }
    }

    public class ContactNumberViewModel
    {
        public Guid Id { get; set; }
        public ContactType Type { get; set; }
        public string Number { get; set; }
        public Guid ContactId { get; set; }
    }
}
