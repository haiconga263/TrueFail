using Common.Exceptions;
using DAL;
using Production.UI.CultureFields.Interfaces;
using Production.UI.MaterialHistories.Interfaces;
using Production.UI.MaterialHistories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Production.MaterialHistories.Commands
{
    public class InsertMaterialHistoryCommand : BaseCommand<int>
    {
        public MaterialHistory Model { set; get; }
        public InsertMaterialHistoryCommand(MaterialHistory materialHistory)
        {
            Model = materialHistory;
        }
    }

    public class InsertMaterialHistoryCommandHandler : BaseCommandHandler<InsertMaterialHistoryCommand, int>
    {
        private readonly IMaterialHistoryRepository _materialHistoryRepository = null;
        private readonly IMaterialHistoryQueries _materialHistoryQueries = null;

        private readonly ICultureFieldQueries _cultureFieldQueries = null;

        public InsertMaterialHistoryCommandHandler(IMaterialHistoryRepository materialHistoryRepository, IMaterialHistoryQueries materialHistoryQueries, ICultureFieldQueries cultureFieldQueries)
        {
            this._materialHistoryRepository = materialHistoryRepository;
            this._materialHistoryQueries = materialHistoryQueries;
            this._cultureFieldQueries = cultureFieldQueries;
        }
        public override async Task<int> HandleCommand(InsertMaterialHistoryCommand request, CancellationToken cancellationToken)
        {
            var id = 0;
            var cultureField = await _cultureFieldQueries.GetByIdAsync(request.Model.CultureFieldId);
            if (cultureField == null)
                throw new BusinessException("CultureField.NotExist");

            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        switch (cultureField.DataType)
                        {
                            case "text":
                                break;
                            case "number":
                                break;
                            case "boolean":
                                break;
                            case "date":
                                {
                                    DateTime dtmp;
                                    if (DateTime.TryParse(request.Model.Value, out dtmp))
                                        request.Model.Value = dtmp.ToString("MM/dd/yyyy");
                                }
                                break;
                            case "time":
                                {
                                    DateTime dtmp;
                                    if (DateTime.TryParse(request.Model.Value, out dtmp))
                                        request.Model.Value = dtmp.ToString("hh:mm tt");
                                }
                                break;
                            case "datetime":
                                {
                                    DateTime dtmp;
                                    if (DateTime.TryParse(request.Model.Value, out dtmp))
                                        request.Model.Value = dtmp.ToString("MM/dd/yyyy hh:mm tt");
                                }
                                break;
                            case "list":
                            default:
                                break;
                        }

                        request.Model.CreatedDate = DateTime.Now;
                        request.Model.CreatedBy = request.LoginSession.Id;
                        request.Model.DeletedDate = null;
                        request.Model.DeletedBy = null;

                        id = await _materialHistoryRepository.AddAsync(request.Model);
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
