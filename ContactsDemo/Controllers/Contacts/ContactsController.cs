using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsDemo.Api.Models.Contacts;
using ContactsDemo.Services.Interfaces;
using ContactsDemo.Services.Model.Contacts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ContactsDemo.Controllers.Contacts
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService = null;
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<List<ContactViewModel>> Get()
        {
            var contacts = await _contactService.GetAll();
            List<ContactViewModel> list = null;

            if (contacts != null && contacts.Count() > 0)
            {
                list = new List<ContactViewModel>();

                foreach (var contact in contacts)
                {
                    list.Add(GetContactViewModel(contact));
                }
            }

            return list;
        }

        // GET: api/Contacts/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ContactViewModel> Get(Guid id)
        {
            var contact = await _contactService.GetById(id);

            return GetContactViewModel(contact);
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task Post([FromBody] ContactViewModel contact)
        {
            if (contact != null)
            {
                Contact newContact = new Contact
                {
                    Address = contact.Address,
                    City = contact.City,
                    Country = contact.Country,
                    DateOfBirth = contact.DateOfBirth,
                    Firstname = contact.Firstname,
                    Lastname = contact.Lastname,
                    PostalCode = contact.PostalCode,
                    Status = Services.Model.Status.Active,
                    ContactNumbers = SetContactNumbers(contact.ContactNumbers)
                };

                await _contactService.Save(newContact);
            }
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] Contact contact)
        {
            if (id != Guid.Empty)
            {
                var oldContact = await _contactService.GetById(id);

                if (oldContact != null)
                {
                    oldContact.Firstname = contact.Firstname;
                    oldContact.Lastname = contact.Lastname;
                    oldContact.PostalCode = contact.PostalCode;
                    oldContact.Address = contact.Address;
                    oldContact.City = contact.City;
                    oldContact.Country = contact.Country;
                    oldContact.DateOfBirth = contact.DateOfBirth;

                    if (contact.ContactNumbers != null && contact.ContactNumbers.Count() > 0)
                    {
                        foreach (var contactNumber in contact.ContactNumbers)
                        {
                            var oldContactNumber = oldContact.ContactNumbers.FirstOrDefault(x => x.Id == contactNumber.Id);
                            if (oldContactNumber != null)
                            {
                                oldContactNumber.Number = contactNumber.Number;
                                oldContactNumber.Type = contactNumber.Type;
                            }
                            else
                            {
                                if (oldContact.ContactNumbers == null)
                                {
                                    oldContact.ContactNumbers = new List<ContactNumber>();
                                }

                                oldContact.ContactNumbers.Add(new ContactNumber
                                {
                                    Number = contactNumber.Number,
                                    Status = Services.Model.Status.Active,
                                    Type = contactNumber.Type
                                });
                            }
                        }
                    }
                    else
                    {
                        oldContact.ContactNumbers = null;
                    }
                }

                await _contactService.Save();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                var contact = await _contactService.GetById(id);

                if (contact != null)
                {
                    contact.Status = Services.Model.Status.Deleted;
                    await _contactService.Save();
                }
            }
        }

        private ContactViewModel GetContactViewModel(Contact contact)
        {
            if(contact != null)
            {
                return new ContactViewModel
                {
                    Address = contact.Address,
                    City = contact.City,
                    ContactNumbers = GetContactNumbersViewModel(contact.ContactNumbers),
                    Country = contact.Country,
                    DateOfBirth = contact.DateOfBirth,
                    Firstname = contact.Firstname,
                    Lastname = contact.Lastname,
                    PostalCode = contact.PostalCode,
                    Id = contact.Id
                };
            }
            else
            {
                return null;
            }
        }

        private List<ContactNumberViewModel> GetContactNumbersViewModel(List<ContactNumber> contactNumbers)
        {
            List<ContactNumberViewModel> list = null;

            if (contactNumbers != null && contactNumbers.Count() > 0)
            {
                list = new List<ContactNumberViewModel>();

                foreach (var contactNumber in contactNumbers)
                {
                    list.Add(new ContactNumberViewModel
                    {
                        Id = contactNumber.Id,
                        ContactId = contactNumber.ContactId,
                        Number = contactNumber.Number,
                        Type = contactNumber.Type
                    });
                }
            }

            return list;
        }

        private List<ContactNumber> SetContactNumbers(List<ContactNumberViewModel> contactNumbers)
        {
            List<ContactNumber> list = null;

            if (contactNumbers != null && contactNumbers.Count() > 0)
            {
                list = new List<ContactNumber>();

                foreach (var contactNumber in contactNumbers)
                {
                    list.Add(new ContactNumber
                    {
                        Number = contactNumber.Number,
                        Status = Services.Model.Status.Active,
                        Type = contactNumber.Type
                    });
                }
            }

            return list;
        }
    }
}
