using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IMessangerCoreAPI.Models;

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

    public class GetDiaglogsByClientListQueryHandler : IRequestHandler<GetDiaglogsByClientListQuery, IEnumerable<Guid>>
    {
        public async Task<IEnumerable<Guid>> Handle(GetDiaglogsByClientListQuery request, CancellationToken cancellationToken)
        {
            var result = new List<Guid>();
            var diialogs = new RGDialogsClients().Init(); // database query mock

            var uniqueDialogs = diialogs.GroupBy(c => c.IDRGDialog).Select(d => d.First());

            foreach (var dialog in uniqueDialogs)
            {
                var clients = diialogs.Where(c => c.IDRGDialog == dialog.IDRGDialog).Select(d => d.IDClient);
                var containsAll = request.ClientIds.All(c => clients.Contains(c));
                if (containsAll)
                    result.Add(dialog.IDRGDialog);
            }

            if (result.Count == 0)
                return new[] { Guid.Empty };

            return result;
        }
    }
}
