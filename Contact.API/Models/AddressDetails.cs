
namespace Contact.API.Models
{
    public class AddressDetails : BaseClassFields
    {       
        public string City { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string Street { get; set; }
        public int Postal { get; set; }
        public int ContactId { get; set; }
        public Contacts Contact { get; set; }
    }
}
