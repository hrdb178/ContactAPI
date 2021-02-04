using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Contact.API.Models;
using DB.Repository;
using System.Net;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private IRepository<Contacts> _contact;

        public ContactController(IRepository<Contacts> contact)
        {
            _contact = contact;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _contact.FindResult(include => include.Address,
                    include => include.ContactDetails
                ).ConfigureAwait(false);

                if (result == null)
                    return StatusCode((int)HttpStatusCode.NotFound);

                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

        }

        [HttpGet("{cityname}")]
        public async Task<IActionResult> Get([FromRoute] string city)
        {
            try
            {
                var result = await _contact.FindResult(c => c.Address.City == city,
                    include => include.Address,
                    include => include.ContactDetails
                ).ConfigureAwait(false);

                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contacts contactRequest)
        {
            try
            {
                var existingContact = await _contact.Result(c =>
                                        c.FirstName == contactRequest.FirstName
                                        && c.LastName == contactRequest.LastName).ConfigureAwait(false);

                if (existingContact != null)
                    return StatusCode((int)HttpStatusCode.Found);

                await _contact.Insert(contactRequest).ConfigureAwait(false);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

        }

        [HttpDelete("{contactid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] int contactId)
        {
            try
            {
                var contact = await _contact.Result(c => c.UniqueId == contactId).ConfigureAwait(false);


                // If doesn't exists
                if (contact == null)
                    return StatusCode((int)HttpStatusCode.NotFound);

                await _contact.Delete(contact).ConfigureAwait(false);

                return StatusCode((int)HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

        }

        [HttpPut("{contactid}/AddPhone")]
        public async Task<IActionResult> AddPhoneNumber([FromRoute] int contactId, [FromBody] ContactDetail contactDetail)
        {
            try
            {
                var contact = await _contact.Result(c => c.UniqueId == contactId,
                    include => include.Address,
                    include => include.ContactDetails
                ).ConfigureAwait(false);


                if (contact == null)
                    return StatusCode((int)HttpStatusCode.NotFound);

                contact.ContactDetails.Add(contactDetail);
                await _contact.Update(contact).ConfigureAwait(false);

                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }

}
