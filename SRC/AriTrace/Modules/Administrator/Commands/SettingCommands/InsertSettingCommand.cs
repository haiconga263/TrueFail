using MDM.UI.Settings.Interfaces;
using MDM.UI.Settings.Models;
using DAL;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.SettingCommands
{
    public class InsertSettingCommand : BaseCommand<int>
    {
        public Setting Model { set; get; }
        public InsertSettingCommand(Setting setting)
        {
            Model = setting;
        }
    }

    public class InsertSettingCommandHandler : BaseCommandHandler<InsertSettingCommand, int>
    {
        private readonly ISettingRepository settingRepository = null;
        private readonly ISettingQueries settingQueries = null;
        public InsertSettingCommandHandler(ISettingRepository settingRepository, ISettingQueries settingQueries)
        {
            this.settingRepository = settingRepository;
            this.settingQueries = settingQueries;
        }
        public override async Task<int> HandleCommand(InsertSettingCommand request, CancellationToken cancellationToken)
        {
            var id = 0;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Model.CreatedDate = DateTime.Now;
                        request.Model.CreatedBy = request.LoginSession.Id;
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;

                        id = await settingRepository.AddAsync(request.Model);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (id > 0) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }

            return id;
        }
    }
}
