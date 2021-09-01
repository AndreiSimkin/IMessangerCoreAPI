using IMessangerCoreAPI.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMessangerCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DialogsController : ControllerBase
    {
        private readonly ISender _sender;

        public DialogsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("/search")]
        public async Task<Guid> Search([FromBody] IEnumerable<Guid> clientIds)
        {
            var query = new GetDiaglogByClientListQuery(clientIds);
            return await _sender.Send(query);
        }
    }
}
