using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleContact.Controllers;
using SampleContact.Data;
using SampleContact.Data.Repositories;
using System.Threading.Tasks;
using System.Linq;
using SampleContact.Data.Models;
using System;

namespace SampleContacts
{
    [TestClass]
    public class ContactPostTests
    {
        //  Returns an expected value
        //  Throws an exception under the tested condition
        //  Changes the state of the system
        //  Calls another function

        private readonly ContactRepository _contactRepository;
        private readonly ContactService _contactService;
        private readonly ContactController _contactController;
        

        public ContactPostTests()
        {
            _contactRepository = new ContactRepository();
            _contactService = new ContactService(_contactRepository);
            _contactController = new ContactController(_contactService);
        }

        [TestMethod]
        public async Task ValidContactSavesToDb()
        {
            var contact = new Contact()
            {
                Email = "something"
            };

            var results = await _contactController.CreateContact(contact);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Id > 0);
        }


        [TestMethod]
        public async Task InvalidContactReturnsArgumentException()
        {
            var contact = new Contact()
            {
                Email = null
            };

            await Assert.ThrowsExceptionAsync<ArgumentException>( async () =>
            {
                await _contactController.CreateContact(contact);
            });
        }
    }
}
