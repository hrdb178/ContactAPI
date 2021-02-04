using Contact.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.API.Test.TestData
{
    public static class TestData
    {
        public static Contacts Contacts()
        {
            return new Contacts
            {
                FirstName = "xyz",
                LastName = "abc",
                Company = "mnp",
                Title = "Mr",
                Address = new AddressDetails
                {
                    City = "City1",
                    Country = "Country1",
                    Postal = 000001,
                    Province = "Province1",
                    Street = "Street1"
                },
                ContactDetails = new List<ContactDetail>
                {
                    new ContactDetail
                    {
                        ContactType = Enum.TypeOfContact.Email,
                        Value = "Test@test.com"
                    }
                }

            };
        }

        public static Contacts ContactsTwo()
        {
            return new Contacts
            {
                FirstName = "Hriday",
                LastName = "Boro",
                Company = "xyz",
                Title = "Mr",
                Address = new AddressDetails
                {
                    City = "New York",
                    Country = "USA",
                    Postal = 111111,
                    Province = "white House",
                    Street = "street 2"
                },
                ContactDetails = new List<ContactDetail>
                {
                    new ContactDetail
                    {
                        ContactType =  Enum.TypeOfContact.Email,
                        Value = "test1@test1.com"
                    }
                }
            };
        }

        public static Contacts ContactThree()
        {
            return new Contacts
            {
                FirstName = "Hriday",
                LastName = "Boro",
                Company = "xyz",
                Title = "Mr",
                Address = new AddressDetails
                {
                    City = "New York",
                    Country = "USA",
                    Postal = 111111,
                    Province = "white House",
                    Street = "street 2"
                },
                ContactDetails = new List<ContactDetail>
                {
                    new ContactDetail
                    {
                        ContactType =  Enum.TypeOfContact.Phone,
                        Value = "8453647892"
                    }
                }
            };
        }
    }
}
