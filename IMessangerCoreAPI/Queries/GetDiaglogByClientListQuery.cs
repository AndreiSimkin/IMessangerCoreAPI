using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IMessangerCoreAPI.Models;

namespace IMessangerCoreAPI.Queries
{
    public class GetDiaglogByClientListQuery : IRequest<Guid>
    {
        public GetDiaglogByClientListQuery(IEnumerable<Guid> clientIds)
        {
            ClientIds = clientIds;
        }

        public IEnumerable<Guid> ClientIds { get; set; }
    }

    public class GetDiaglogsByClientListQueryHandler : IRequestHandler<GetDiaglogByClientListQuery, Guid>
    {
        public async Task<Guid> Handle(GetDiaglogByClientListQuery request, CancellationToken cancellationToken)
        {
            var query = new RGDialogsClients().Init(); // database query mock

            var dialogs = query.GroupBy(c => c.IDRGDialog); // get dialogs

            var result = dialogs.FirstOrDefault(d => request.ClientIds.All(c => d.Select(_d => _d.IDClient).Contains(c)))?.First(); // get dialog by query

            if (result == null)
                return Guid.Empty;

            return result.IDRGDialog;
        }
    }
}
