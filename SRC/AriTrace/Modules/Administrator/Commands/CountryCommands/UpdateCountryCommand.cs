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
    public class UpdateCountryCommand : BaseCommand<int>
    {
        public Country Model { set; get; }
        public UpdateCountryCommand(Country country)
        {
            Model = country;
        }
    }

    public class UpdateCountryCommandHandler : BaseCommandHandler<UpdateCountryCommand, int>
    {
        private readonly ICountryRepository countryRepository = null;
        private readonly ICountryQueries countryQueries = null;
        public UpdateCountryCommandHandler(ICountryRepository countryRepository, ICountryQueries countryQueries)
        {
            this.countryRepository = countryRepository;
            this.countryQueries = countryQueries;
        }
        public override async Task<int> HandleCommand(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            Country country = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Country.NotExisted");
            }
            else
            {
                country = await countryQueries.GetByIdAsync(request.Model.Id);
                if (country == null)
                {
                    throw new BusinessException("Country.NotExisted");
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
                        if (await countryRepository.UpdateAsync(request.Model) > 0)
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
