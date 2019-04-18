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
    public class DeleteSettingCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteSettingCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteSettingCommandHandler : BaseCommandHandler<DeleteSettingCommand, int>
    {
        private readonly ISettingRepository settingRepository = null;
        private readonly ISettingQueries settingQueries = null;
        public DeleteSettingCommandHandler(ISettingRepository settingRepository, ISettingQueries settingQueries)
        {
            this.settingRepository = settingRepository;
            this.settingQueries = settingQueries;
        }
        public override async Task<int> HandleCommand(DeleteSettingCommand request, CancellationToken cancellationToken)
        {
            Setting setting = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Setting.NotSelected");
            }
            else
            {
                setting = await settingQueries.GetByIdAsync(request.Model);
                if (setting == null)
                    throw new BusinessException("Setting.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (await settingRepository.DeleteAsync(setting.Id) > 0)
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
