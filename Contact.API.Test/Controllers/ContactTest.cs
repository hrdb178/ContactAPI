using System.Net;
using System.Threading.Tasks;
using Contact.API.Controllers;
using Contact.API.Models;
using DB.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using System.Runtime.Serialization;
using System.Net.Http;
using System;

namespace Contact.API.Test.Controllers
{
    public class ContactTest
    {
        public Mock<IRepository<Contacts>> _contact;

        public ContactTest()
        {
            _contact = new Mock<IRepository<Contacts>>();
        }

        [Fact]
        public async Task AddNewContactSuccessTest()
        {
            var ControllerTest = new ContactController(_contact.Object);
            _contact.Setup(c => c.Insert(It.IsAny<Contacts>()))
                .Returns(Task.CompletedTask);

            var result = await ControllerTest.Post(TestData.TestData.Contacts()).ConfigureAwait(false);
            var status = result.Should().BeOfType<StatusCodeResult>().Subject;
            status.StatusCode.Equals(HttpStatusCode.Created);
            _contact.Verify(v => v.Insert(It.IsAny<Contacts>()));
        }

        [Fact]
        public async Task AddNewContactExceptionTest()
        {
            var sqlException = FormatterServices.GetUninitializedObject(typeof(SqlException)) as SqlException;
            var ControllerTest = new ContactController(_contact.Object);
            _contact.Setup(c => c.Insert(It.IsAny<Contacts>()))
                .ThrowsAsync(sqlException);

            var result = await ControllerTest.Post(TestData.TestData.Contacts()).ConfigureAwait(false);
            var status = result.Should().BeOfType<ObjectResult>().Subject;
            status.StatusCode.Equals(HttpStatusCode.InternalServerError);
            _contact.Verify(v => v.Insert(It.IsAny<Contacts>()));
           
        }
    }
}
