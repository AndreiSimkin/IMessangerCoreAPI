using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMessangerCoreAPI.Queries
{
    public class GetDiaglogsByClientListQuery : IRequest<IEnumerable<Guid>>
    {
        public GetDiaglogsByClientListQuery(IEnumerable<Guid> clientIds)
        {
            ClientIds = clientIds;
        }

        public IEnumerable<Guid> ClientIds { get; set; }
    }
}
