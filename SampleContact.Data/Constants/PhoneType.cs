using System.Text.Json.Serialization;

namespace SampleContact.Data.Constants
{
    /// <summary>
    /// This enum is used to identify the type of phone number
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))] // added to get names vs numbers per spec
    public enum PhoneType
    {
        home,
        work,
        mobile
    }
}
