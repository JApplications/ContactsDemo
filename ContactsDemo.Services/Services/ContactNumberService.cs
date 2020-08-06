using ContactsDemo.Services.Interfaces;
using ContactsDemo.Services.Model.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDemo.Services.Services
{
    public class ContactNumberService : BaseService, IContactNumberService
    {
        private readonly ContactsDbContext _context;

        public ContactNumberService(ContactsDbContext context, ILogger<ContactNumberService> logger) : base(logger)
        {
            _context = context;
        }

        public Task<List<ContactNumber>> GetAll(Guid contactId)
        {
            return _context.ContactNumbers.Where(x => x.ContactId == contactId).ToListAsync();
        }

        public Task<ContactNumber> GetById(Guid id)
        {
            return _context.ContactNumbers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save(ContactNumber contactNumber = null)
        {
            if (contactNumber != null)
            {
                _context.ContactNumbers.Add(contactNumber);
            }

            await _context.SaveChangesAsync();
        }
    }
}
