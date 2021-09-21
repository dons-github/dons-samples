using SampleContact.Data.Models;
using SampleContact.Data.Repositories.Contracts;
using SampleContact.Data.Services.Contracts;
using SampleContact.Data.Services.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleContact.Data
{
    /// <summary>
    /// This service contractt defines an interface for managing <see cref="Contact"/>
    /// </summary>
    public sealed class ContactService: IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(
            IContactRepository contactRepository
        )
        {
            _contactRepository = contactRepository;
        }

        // <inheritdoc />
        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            var results = await _contactRepository.GetAllContacts();
            return results;
        }

        // <inheritdoc />
        public async Task<Contact> CreateContact(Contact contact)
        {
            var contactValidator = new ContactValidator(); // TODO - move to constructor and consider DI ???
            var validationResult = await contactValidator.ValidateAsync(contact);

            if (validationResult.IsValid)
            {
                return await _contactRepository.CreateContact(contact);
            }
            else
            {
                throw new ArgumentException("Contact is invalid!");
            }
        }

        // <inheritdoc />
        public async Task<Contact> UpdateContact(Contact contact)
        {
            return await _contactRepository.UpdateContact(contact);
        }

        // <inheritdoc />
        public async Task<Contact> GetContact(int contactId)
        {
            return await _contactRepository.GetContact(contactId);
        }

        // <inheritdoc />
        public async Task DeleteContact(int contactId)
        {
            await _contactRepository.DeleteContact(contactId);
        }

        // <inheritdoc />
        public async Task<IList<CallListContact>> GetCallList()
        {
            return await _contactRepository.GetCallList();
        }
    }
}
