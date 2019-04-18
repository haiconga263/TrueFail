using MDM.UI.Settings.Interfaces;
using MDM.UI.Settings.Models;
using Common.Exceptions;
using DAL;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.SettingCommands
{
    public class UpdateSettingCommand : BaseCommand<int>
    {
        public Setting Model { set; get; }
        public UpdateSettingCommand(Setting setting)
        {
            Model = setting;
        }
    }

    public class UpdateSettingCommandHandler : BaseCommandHandler<UpdateSettingCommand, int>
    {
        private readonly ISettingRepository settingRepository = null;
        private readonly ISettingQueries settingQueries = null;
        public UpdateSettingCommandHandler(ISettingRepository settingRepository, ISettingQueries settingQueries)
        {
            this.settingRepository = settingRepository;
            this.settingQueries = settingQueries;
        }
        public override async Task<int> HandleCommand(UpdateSettingCommand request, CancellationToken cancellationToken)
        {
            Setting setting = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Setting.NotExisted");
            }
            else
            {
                setting = await settingQueries.GetByIdAsync(request.Model.Id);
                if (setting == null)
                {
                    throw new BusinessException("Setting.NotExisted");
                }
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;
                        if (await settingRepository.UpdateAsync(request.Model) > 0)
                            rs = 0;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (rs == 0) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }
            return rs;
        }
    }
}
