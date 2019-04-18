using Common;
using Common.Exceptions;
using Common.Helpers;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Geographical.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Admin.Commands.GeoCommand.Regions
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IRegionRepository regionRepository = null;
        private readonly IRegionQueries regionQueries = null;
        private readonly ICountryQueries countryQueries = null;
        public AddCommandHandler(IRegionRepository regionRepository, IRegionQueries regionQueries, ICountryQueries countryQueries)
        {
            this.regionRepository = regionRepository;
            this.regionQueries = regionQueries;
            this.countryQueries = countryQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.Region == null || string.IsNullOrEmpty(request.Region.Code))
            {
                throw new BusinessException("AddWrongInformation");
            }

            var region = (await regionQueries.Gets($"r.code = '{request.Region.Code}' and r.is_deleted = 0")).FirstOrDefault();
            if (region != null)
            {
                throw new BusinessException("Region.ExistedCode");
            }

            var country = await countryQueries.Get(request.Region.CountryId);
            if (country == null)
            {
                throw new BusinessException("Country.NotExisted");
            }

            request.Region = CreateBuild(request.Region, request.LoginSession);
            var rs = await regionRepository.Add(request.Region);

            return rs == 0 ? - 1 : 0;
        }
    }
}
