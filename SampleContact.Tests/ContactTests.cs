using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleContact.Controllers;
using SampleContact.Data;
using SampleContact.Data.Constants;
using SampleContact.Data.Models;
using SampleContact.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Tests
{
    [TestClass]
    public class ContactTests
    {
        // Basic concepts
        //  1. The API returns an expected value
        //  2. The API throws exceptions as expected
        //  3. The API changes the state of the system as expected

        // TODO - As the number of test grows, it will make sense to break
        // into smaller chunks and move the test data to a separate area.

        private readonly ContactRepository _contactRepository;
        private readonly ContactService _contactService;
        private readonly ContactController _contactController;
                
        public ContactTests()
        {
            _contactRepository = new ContactRepository();
            _contactService = new ContactService(_contactRepository);
            _contactController = new ContactController(_contactService);
        }

        [ClassInitialize]
        public static void SetupTest(TestContext context)
        {
        }

        [TestMethod]
        public async Task ValidContactSavesToDb()
        {
            // Arrange
            var contact = new Contact()
            {
                Email = "something"
            };

            // Act
            var results = await _contactController.CreateContact(contact);

            // Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Id > 0);
        }


        [TestMethod]
        public async Task ContactWithNullEmailReturnsArgumentException()
        {
            // Arrange
            var contact = new Contact()
            {
                Email = null
            };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await _contactController.CreateContact(contact);
            });
        }

        [TestMethod]
        public async Task ContactWithLongFirstNameReturnsArgumentException()
        {
            // Arrange
            var contact = new Contact()
            {
                Email = "not null",
                Name = new ContactName() { First = "This very long invalid first name exceeds 50 characters" }
            };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await _contactController.CreateContact(contact);
            });
        }

        [TestMethod]
        public async Task ContactWithLongLastNameReturnsArgumentException()
        {
            // Arrange
            var contact = new Contact()
            {
                Email = "not null",
                Name = new ContactName() { First = "This very long invalid last name exceeds 50 characters" }
            };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await _contactController.CreateContact(contact);
            });
        }

        [TestMethod]
        public async Task GetAllReturnsAllContacts()
        {
            // Arrange
            InitializeDb();

            // Act
            var allContacts = await _contactController.GetAllContacts();

            // Assert
            Assert.IsNotNull(allContacts);
            Assert.IsTrue(allContacts.ToList().Count == 6);
        }

        [TestMethod]
        public async Task GetContactReturnsContact()
        {
            // Arrange
            InitializeDb();
            // Act
            var contacts = await _contactController.GetContact(2);

            // Assert
            Assert.IsNotNull(contacts);
            Assert.IsTrue(contacts.Id == 2);
        }

        [TestMethod]
        public async Task GetCallListReturnsContactWithHomePhone()
        {
            // Arrange
            var validFirstNames = new List<string> { "George", "Thomas", "Marilyn", "James" };
            InitializeDb();

            // Act
            var contacts = await _contactController.GetCallList();

            // Assert
            Assert.IsNotNull(contacts);
            Assert.IsTrue(contacts.Count == 4);

            foreach(var contact in contacts)
            {
                // TODO This test could probably be more robust.
                Assert.IsTrue(validFirstNames.Contains(contact.Name.First));
            }
        }

        [TestMethod]
        public async Task GetCallListReturnsContactInCorrectOrder()
        {
            // Expectation: Order By LastName, FirstName

            // Arrange
            var allContacts = InitializeDb();
            var contactsWithHomePhoneCount = allContacts
                .Where(c => c.Phone.Type == PhoneType.home)
                .Count();

            // Act
            var contacts = await _contactController.GetCallList();

            // Assert
            Assert.IsNotNull(contacts);
            Assert.IsTrue(contacts.Count == contactsWithHomePhoneCount);
            
            Assert.IsTrue(ValidateContact(contacts[0], "Thomas", "Jefferson"));
            Assert.IsTrue(ValidateContact(contacts[1], "James", "Monroe"));
            Assert.IsTrue(ValidateContact(contacts[2], "Marilyn", "Monroe"));
            Assert.IsTrue(ValidateContact(contacts[3], "George", "Washington"));
        }

        [TestMethod]
        public async Task PutUpdatesValues()
        {
            // Arrange
            InitializeDb();

            // Act
            var contact = await _contactController.GetContact(1);
            contact.Email = "new@email.com";
            contact = await _contactController.UpdateContact(contact);

            // Assert
            Assert.IsNotNull(contact);
            Assert.IsTrue(contact.Email == "new@email.com");
        }

        [TestMethod]
        public async Task DeleteRemovesContact()
        {
            // Arrange
            InitializeDb();

            // Act
            var results = await _contactController.DeleteContact(1);
            var contact = await _contactController.GetContact(1);

            // Assert
            Assert.IsNull(contact);
        }

        private List<Contact> InitializeDb()
        {
            var contacts = new List<Contact>()
            {
                new Contact
                {
                    Email = "1@test.com",
                    Phone = new ContactPhone
                    {
                        Number = "111-111-1111",
                        Type = PhoneType.home
                    },
                    Name = new ContactName { First = "George", Last = "Washington" }
                },
                new Contact
                {
                    Email = "2@test.com",
                    Phone = new ContactPhone
                    {
                        Number = "222-222-2222",
                        Type = PhoneType.work
                    },
                    Name = new ContactName { First = "John", Last = "Adams" }
                },
                new Contact
                {
                    Email = "3@test.com",
                    Phone = new ContactPhone
                    {
                        Number = "333-333-3333",
                        Type = PhoneType.home
                    },
                    Name = new ContactName { First = "Thomas", Last = "Jefferson" }
                },
                new Contact
                {
                    Email = "4@test.com",
                    Phone = new ContactPhone
                    {
                        Number = "444-444-4444",
                        Type = PhoneType.mobile
                    },
                    Name = new ContactName { First = "James", Last = "Madison" }
                },
                new Contact
                {
                    Email = "456@test.com",
                    Phone = new ContactPhone
                    {
                        Number = "444-555-6666",
                        Type = PhoneType.home
                    },
                    Name = new ContactName { First = "Marilyn", Last = "Monroe" }
                },
                new Contact
                {
                    Email = "5@test.com",
                    Phone = new ContactPhone
                    {
                        Number = "555-555-5555",
                        Type = PhoneType.home
                    },
                    Name = new ContactName { First = "James", Last = "Monroe" }
                }
            };
        
            using (var db = new LiteDatabase(@"SampleContacts.db"))
            {
                var dbContacts = db.GetCollection<Contact>("contacts");
                dbContacts.DeleteAll();

                foreach(var contact in contacts)
                {
                    dbContacts.Insert(contact);
                }
            }

            return contacts;
        }
        private bool ValidateContact(CallListContact contact, string firstName, string lastName)
        {
            if (contact.Name.First == firstName && contact.Name.Last == lastName)
            {
                return true;
            }

            return false;
        }
    }
}
