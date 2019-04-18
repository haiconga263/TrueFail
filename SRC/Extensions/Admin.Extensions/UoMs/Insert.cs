using System.Data;
using Common.Abstracts;
using Common.Models;
using MDM.UI.UoMs.Models;

namespace Admin.Extensions.UoMs
{
    public class Insert : BaseEvent<UoM>
    {
        public override EventResultModel BeforeRecord(object sender, UoM value, IDbConnection connection, IDbTransaction transaction, UserSession user)
        {
            return base.BeforeRecord(sender, value, connection, transaction, user);
        }
    }
}
