using Collections.UI.POActivities.Interfaces;
using Collections.UI.POActivities.ViewModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Collections.POActivities.Commands
{
    public class GetListPOActivityCommand : BaseCommand<IEnumerable<POActivityInformation>>
    {
        public int POItemId { get; set; }

        public GetListPOActivityCommand(int poItemId)
        {
            POItemId = poItemId;
        }
    }

    public class GetListPOActivityCommandHandler : BaseCommandHandler<GetListPOActivityCommand, IEnumerable<POActivityInformation>>
    {
        private readonly IPOActivityQueries _poActivityQueries = null;

        public GetListPOActivityCommandHandler(IPOActivityQueries poActivityQueries)
        {
            this._poActivityQueries = poActivityQueries;
        }

        public override async Task<IEnumerable<POActivityInformation>> HandleCommand(GetListPOActivityCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<POActivityInformation> rs = null;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        rs = await _poActivityQueries.GetByPOIdAsync(request.POItemId);
                    }
                    finally
                    {
                        if (rs != null)
                        {
                            trans.Commit();
                        }
                        else
                        {
                            try
                            {
                                trans.Rollback();
                            }
                            catch { }
                        }
                    }
                }
            }

            return rs;
        }
    }
}

