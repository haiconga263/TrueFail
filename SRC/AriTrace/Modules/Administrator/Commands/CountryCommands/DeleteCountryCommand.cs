using Common.Exceptions;
using DAL;
using MDM.UI.Countries.Interfaces;
using MDM.UI.Countries.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.CountryCommands
{
    public class DeleteCountryCommand : BaseCommand<int>
    {
        public int Model { get; set; }
        public DeleteCountryCommand(int id)
        {
            Model = id;
        }
    }

    public class DeleteCountryCommandHandler : BaseCommandHandler<DeleteCountryCommand, int>
    {
        private readonly ICountryRepository countryRepository = null;
        private readonly ICountryQueries countryQueries = null;
        public DeleteCountryCommandHandler(ICountryRepository countryRepository, ICountryQueries countryQueries)
        {
            this.countryRepository = countryRepository;
            this.countryQueries = countryQueries;
        }
        public override async Task<int> HandleCommand(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            Country country = null;
            if (request.Model == 0)
            {
                throw new BusinessException("Country.NotSelected");
            }
            else
            {
                country = await countryQueries.GetByIdAsync(request.Model);
                if (country == null)
                    throw new BusinessException("Country.NotSelected");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        country.IsDeleted = true;
                        country.ModifiedDate = DateTime.Now;
                        country.ModifiedBy = request.LoginSession.Id;

                        if (await countryRepository.UpdateAsync(country) > 0)
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
