using SampleContact.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleContact.Data.Repositories.Contracts
{
    /// <summary>
    /// The interface for the <see cref="ContactRepository"/>
    /// </summary>
    public interface IContactRepository
    {
        /// <summary>
        /// Returns all the existing contacts
        /// </summary>
        /// <returns>A collections of all existing contacts</returns>
        Task<IEnumerable<Contact>> GetAllContacts();

        /// <summary>
        /// Creates a new contact record
        /// </summary>
        /// <param name="contact">A <see cref="Contact"/> instance</param>
        /// <returns>The newly created <see cref="Contact"/></returns>
        Task<Contact> CreateContact(Contact contact);

        /// <summary>
        /// Updates a single contact
        /// </summary>
        /// <param name="contact">The <see cref="Contact"/> instance with the changes</param>
        /// <returns>The changed <see cref="Contact"/></returns>
        Task<Contact> UpdateContact(Contact contact);

        /// <summary>
        /// Returns a single contact
        /// </summary>
        /// <param name="contactId">The id of the contact for which to search</param>
        /// <returns>The <see cref="Contact"/></returns>
        Task<Contact> GetContact(int contactId);

        /// <summary>
        /// Removes a <see cref="Contact"/> single contact
        /// </summary>
        /// <param name="contactId">The id of the contact to remove</param>
        Task DeleteContact(int contactId);

        /// <summary>
        /// Returns a call list based on existing contacts
        /// </summary>
        /// <returns>A collection of <see cref="CallListContact"/></returns>
        Task<IList<CallListContact>> GetCallList();
    }
}
