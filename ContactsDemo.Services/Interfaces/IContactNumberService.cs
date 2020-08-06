using ContactsDemo.Services.Model.Contacts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDemo.Services.Interfaces
{
    public interface IContactNumberService
    {
        Task<List<ContactNumber>> GetAll(Guid contactId);
        Task<ContactNumber> GetById(Guid id);
        Task Save(ContactNumber contactNumber = null);
    }
}
