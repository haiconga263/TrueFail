using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Categories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using MDM.UI.Storages.Interfaces;
using MDM.UI.Storages.Enumerations;

namespace Admin.Commands.CategoryCommand
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly ICategoryRepository categoryRepository = null;
        private readonly ICategoryQueries categoryQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public AddCommandHandler(ICategoryRepository categoryRepository, ICategoryQueries categoryQueries, IStorageQueries storageQueries)
        {
            this.categoryRepository = categoryRepository;
            this.categoryQueries = categoryQueries;
            this.storageQueries = storageQueries;
        }

        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        categoryRepository.JoinTransaction(conn, trans);
                        categoryQueries.JoinTransaction(conn, trans);

                        if (request.Category == null)
                        {
                            throw new BusinessException("AddWrongInformation");
                        }
                        
                        request.Category.Code = await storageQueries.GenarateCodeAsync(StorageKeys.CategoryCode);
                        request.Category = CreateBuild(request.Category, request.LoginSession);
                        var categoryId = await categoryRepository.Add(request.Category);

                        // languages
                        foreach (var item in request.Category.Languages)
                        {
                            item.CategoryId = categoryId;
                            await categoryRepository.AddOrUpdateLanguage(item);
                        }

                        rs = 0;
                    }
                    finally
                    {
                        if (rs == 0)
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
