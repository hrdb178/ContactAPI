using Contact.API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contact.API.Models
{ 
    public class Contacts : BaseClassFields
    {       
        public string Title { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Company { get; set; }

        public ICollection<ContactDetail> ContactDetails { get; set; }

        public AddressDetails Address { get; set; }

       
    }
}
