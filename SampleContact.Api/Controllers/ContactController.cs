using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleContact.Data.Models;
using SampleContact.Data.Services.Contracts;

namespace SampleContact.Controllers
{
    /// <summary>
    /// This class represents the entry points for the API
    /// </summary>
    [Route("api/contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="contactService">The business tier services are obtained by dependency injection</param>
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        /// <summary>
        /// Retrieves a list of all contacts
        /// </summary>
        /// <returns>A collection of <see cref="Contact"/></returns>
        [HttpGet]
        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            var results = await _contactService.GetAllContacts();
            return results;
        }

        /// <summary>
        /// Adds a new <see cref="Contact"/>
        /// </summary>
        /// <returns>The <see cref="Contact"/> that was created.</returns>
        [HttpPost]
        public async Task<Contact> CreateContact(Contact contact)
        {
            return await _contactService.CreateContact(contact);
        }

        /// <summary>
        /// Updates a contact
        /// </summary>
        /// <param name="contactId">The unique id of the contact</param>
        /// <returns>The updated <see cref="Contact"/></returns>
        [HttpPut, Route("{contactId}")]
        public async Task<Contact> UpdateContact(Contact contact)
        {
            return await _contactService.UpdateContact(contact);
        }

        /// <summary>
        /// Retrieves a single contact
        /// </summary>
        /// <param name="contactId">The unique id of the contact</param>
        /// <returns>The requested <see cref="Contact"/></returns>
        [HttpGet, Route("{contactId}")]
        public async Task<Contact> GetContact(int contactId)
        {
            return await _contactService.GetContact(contactId);
        }

        /// <summary>
        /// Removes a contact
        /// </summary>
        /// <param name="contactId">The unique id of the contact</param>
        /// <returns>200 on success, 404 if not found</returns>
        [HttpDelete, Route("{contactId}")]
        public async Task<IActionResult> DeleteContact(int contactId)
        {
            var deleted = await _contactService.DeleteContact(contactId);
            
            if (deleted)
            {
                return Ok();
            }
            else
            { 
                return NotFound();
            }
        }

        /// <summary>
        /// Retrieves a call list based on existing contacts
        /// </summary>
        /// <returns>A collection of <see cref="CallListContact"/></returns>
        [HttpGet, Route("call-list")]
        public async Task<IList<CallListContact>> GetCallList()
        {
            return await _contactService.GetCallList();
        }
    }
}