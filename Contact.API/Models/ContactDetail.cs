using Contact.API.Enum;

namespace Contact.API.Models
{

    public class ContactDetail : BaseClassFields
    {
        public string Value { get; set; }
        public TypeOfContact ContactType { get; set; }
        public int ContactId { get; set; }
        public Contacts Contact { get; set; }

    }
}
