using Contact.API.Models;
using DB.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactDetailUpdate : ControllerBase
    {
        private IRepository<ContactDetail> _contactDetail;

        public ContactDetailUpdate(IRepository<ContactDetail> contactDetail)
        {
            _contactDetail = contactDetail;
        }

        [HttpPut("{contactid}/Email")]
        public async Task<IActionResult> Email([FromRoute] int contactid, [FromBody] string email)
        {
            try
            {
                var contactlist = await _contactDetail.Get(contactid).ConfigureAwait(false);

                // If doesn't exists
                if (contactlist == null)
                    return StatusCode((int)HttpStatusCode.NotFound);

                contactlist.Value = email;

                await _contactDetail.Update(contactlist).ConfigureAwait(false);

                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

        }
    }
}
