using ContactsDemo.Services.Interfaces;
using ContactsDemo.Services.Model.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDemo.Services.Services
{
    public class ContactService : BaseService, IContactService
    {

        private readonly ContactsDbContext _context;

        public ContactService(ContactsDbContext context, ILogger<ContactService> logger) : base(logger)
        {
            _context = context;
        }

        public Task<List<Contact>> GetAll()
        {   
            return _context.Contacts.Include(c => c.ContactNumbers).ToListAsync();
        }

        public async Task<Contact> GetById(Guid id)
        {
            return await _context.Contacts.Include(c => c.ContactNumbers).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save(Contact contact = null)
        {
            if (contact != null)
            {
                _context.Contacts.Add(contact);
            }

            try
            {
               await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
