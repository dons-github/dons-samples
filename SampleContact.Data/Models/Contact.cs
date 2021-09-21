using SampleContact.Data.Constants;

namespace SampleContact.Data.Models
{
    /// <summary>
    /// This class represents the contact details
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// The unique identifier for the contact
        /// </summary>
        public int Id { get; set; }
                
        /// <summary>
        /// The <see cref="ContactName"/> for the name parts
        /// </summary>
        public ContactName Name { get; set; }

        /// <summary>
        /// The <see cref="ContactAddress"/> for the address parts
        /// </summary>
        public ContactAddress Address { get; set; }

        /// <summary>
        /// The <see cref="ContactPhone"/> for the phone parts
        /// </summary>
        public ContactPhone Phone { get; set; }

        /// <summary>
        /// The email address for the contact
        /// </summary>
        public string Email { get; set; }
    }

    /// <summary>
    /// This class represents the contact name
    /// </summary>
    public class ContactName
    {
        /// <summary>
        /// The contacts first name
        /// </summary>
        public string First { get; set; }

        /// <summary>
        /// The contacts middle name
        /// </summary>
        public string Middle { get; set; }
        
        /// <summary>
        /// The contacts last name
        /// </summary>
        public string Last { get; set; }
    }

    /// <summary>
    /// This class represents a phone number for a contact
    /// </summary>
    public class ContactPhone
    {
        /// <summary>
        /// The phone number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The <see cref="PhoneType"/> type of phone
        /// </summary>
        public PhoneType Type { get; set; }
    }

    /// <summary>
    /// This class represents an address for a contact
    /// </summary>
    public class ContactAddress
    {
        /// <summary>
        /// The address street
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// The address city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The address state
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The address postal code
        /// </summary>
        public string Zip { get; set; }
    }
}
