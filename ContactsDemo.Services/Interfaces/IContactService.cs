using ContactsDemo.Services.Model.Contacts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDemo.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<Contact>> GetAll();
        Task<Contact> GetById(Guid id);
        Task Save (Contact contact = null);
    }
}
