using Contact.API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.DB.Configuration
{
    public class AddressConfiguration
    {
        public AddressConfiguration(EntityTypeBuilder<AddressDetails> entityBuilder)
        {
            entityBuilder.HasKey(t => t.UniqueId);
            entityBuilder.HasOne(e => e.Contact).WithOne(e => e.Address);
        }
    }

    public class ContactConfiguration
    {
        public ContactConfiguration(EntityTypeBuilder<Contacts> entityBuilder)
        {
            entityBuilder.HasKey(t => t.UniqueId);
            entityBuilder.HasIndex(p => new { p.FirstName, p.LastName }).IsUnique();
        }
    }

    public class ContactDetailConfiguration
    {
        public ContactDetailConfiguration(EntityTypeBuilder<ContactDetail> entityBuilder)
        {
            entityBuilder.HasKey(t => t.UniqueId);
            entityBuilder.HasOne(e => e.Contact).WithMany(e => e.ContactDetails).HasForeignKey(e => e.ContactId);

        }
    }
}
