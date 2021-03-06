using LiteDB;
using SampleContact.Data.Models;
using SampleContact.Data.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleContact.Data.Repositories
{
    /// <inheritdoc />
    public sealed class ContactRepository : IContactRepository
    {
        // TODO - Pull out all the LiteDB details and consolidate into a single client in the Clients folder
        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            using (var db = new LiteDatabase(@"SampleContacts.db"))
            {
                var contacts = db.GetCollection<Contact>("contacts");
                var results = await Task.Run(() => contacts.FindAll());
                return results.ToList();
            }
        }

        /// <inheritdoc />
        public async Task<Contact> CreateContact(Contact contact)
        {
            using (var db = new LiteDatabase(@"SampleContacts.db"))
            {
                var contacts = db.GetCollection<Contact>("contacts");
                await Task.Run(() => contacts.Insert(contact));
                return contact;
            }
        }

        /// <inheritdoc />
        public async Task<Contact> UpdateContact(Contact contact)
        {
            using (var db = new LiteDatabase(@"SampleContacts.db"))
            {
                var contacts = db.GetCollection<Contact>("contacts");
                await Task.Run(() => contacts.Update(contact));
                return contact;
            }
        }

        /// <inheritdoc />
        public async Task<Contact> GetContact(int contactId)
        {
            using (var db = new LiteDatabase(@"SampleContacts.db"))
            {
                var contacts = db.GetCollection<Contact>("contacts");
                var contact = await Task.Run(() => contacts.FindById(contactId));
                return contact;
            }
        }

        /// <inheritdoc />
        public async Task<bool> DeleteContact(int contactId)
        {
            using (var db = new LiteDatabase(@"SampleContacts.db"))
            {
                var contacts = db.GetCollection<Contact>("contacts");
                return await Task.Run(() => contacts.Delete(contactId));
            }
        }

        // <inheritdoc />
        public async Task<IList<CallListContact>> GetCallList()
        {
            using (var db = new LiteDatabase(@"SampleContacts.db"))
            {
                var contacts = db.GetCollection<Contact>("contacts");

                var c = await Task.Run(() => contacts.Query()
                    .Where(c => c.Phone.Type == Constants.PhoneType.home)
                    .ToList());

                return c.Select(c => new CallListContact 
                            { 
                                Name = c.Name, 
                                Phone = c.Phone.Number 
                            })
                    .OrderBy(c => c.Name?.Last)
                    .ThenBy(c => c.Name?.First)
                    .ToList();
            }
        }
    }
}
