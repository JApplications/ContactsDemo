using ContactsDemo.Api.Models.Contacts;
using ContactsDemo.Services.Interfaces;
using ContactsDemo.Services.Model.Contacts;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsDemo.Api.Hubs
{
    public class ContactHub : Hub
    {
        private readonly IContactService _contactService;
        public ContactHub(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task UpdateContactList()
        {
            var data = await _contactService.GetAll();

            var viewModelData = SetViewModel(data);

            await Clients.All.SendAsync("ContactsRefresh", viewModelData);
        }

        private List<ContactViewModel> SetViewModel(List<Contact> contacts)
        {
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

        private ContactViewModel GetContactViewModel(Contact contact)
        {
            if (contact != null)
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
    }
}
