using DAL;
using MDM.UI.Settings.Enumerations;
using MDM.UI.Settings.Interfaces;
using MDM.UI.Settings.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Collections.Configurations.Commands
{
    public class GetConfigsCommand : BaseCommand<Dictionary<string, string>>
    {
        public GetConfigsCommand()
        {
        }
    }

    public class GetConfigsCommandHandler : BaseCommandHandler<GetConfigsCommand, Dictionary<string, string>>
    {
        private readonly ISettingQueries _settingQueries = null;

        public GetConfigsCommandHandler(ISettingQueries settingQueries)
        {
            _settingQueries = settingQueries;
        }

        public override async Task<Dictionary<string, string>> HandleCommand(GetConfigsCommand request, CancellationToken cancellationToken)
        {
            Dictionary<string, string> rs = new Dictionary<string, string>();
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var settings = (await _settingQueries.GetsByPrefixAsync(SettingKeys.PrefixAppAriCollector)).ToList();

                        if (settings == null) settings = new List<Setting>();

                        settings.ForEach(x => rs.Add(x.Name.Replace(SettingKeys.PrefixAppAriCollector + ".", ""), x.Value));
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

