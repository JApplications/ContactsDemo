using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsDemo.Services.Model.Contacts
{
    public class ContactNumber : BaseModel
    {
        public ContactType Type { get; set; }
        public string Number { get; set; }
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
    }

    public enum ContactType
    {
        Undefined = 0,
        Fixed = 1,
        Mobile = 2,
        Fax = 3
    }
}
