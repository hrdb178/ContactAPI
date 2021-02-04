using Contact.API.DB.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Contact.API.Models
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {

        }

        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<AddressDetails> Addresses { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ContactConfiguration(modelBuilder.Entity<Contacts>());
            new AddressConfiguration(modelBuilder.Entity<AddressDetails>());
            new ContactDetailConfiguration(modelBuilder.Entity<ContactDetail>());
        }
    }

}
