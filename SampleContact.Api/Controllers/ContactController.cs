using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleContact.Data.Models;
using SampleContact.Data.Services.Contracts;

namespace SampleContact.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/contacts")] //[controller] ???
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactService"></param>
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            var results = await _contactService.GetAllContacts();
            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<Contact> CreateContact(Contact contact)
        {
            return await _contactService.CreateContact(contact);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        [HttpPut, Route("{contactId}")]
        public async Task<Contact> UpdateContact(Contact contact)
        {
            return await _contactService.UpdateContact(contact);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        [HttpGet, Route("{contactId}")]
        public async Task<Contact> GetContact(int contactId)
        {
            return await _contactService.GetContact(contactId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        [HttpDelete, Route("{contactId}")]
        public async Task<IActionResult> DeleteContact(int contactId)
        {
            try
            {
                await _contactService.DeleteContact(contactId);
            }
            catch (System.Web.Http.HttpResponseException exception) when (exception.Response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("call-list")]
        public async Task<IList<CallListContact>> GetCallList()
        {
            return await _contactService.GetCallList();
        }
    }
}