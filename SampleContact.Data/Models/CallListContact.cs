
namespace SampleContact.Data.Models
{
    /// <summary>
    /// This model represents a contact in the call list
    /// </summary>
    public class CallListContact
    {
        /// <summary>
        /// The name of the contact
        /// </summary>
        public ContactName Name { get; set; }

        /// <summary>
        /// The home phone associated with the contact
        /// </summary>
        public string Phone { get; set; }
    }
}
