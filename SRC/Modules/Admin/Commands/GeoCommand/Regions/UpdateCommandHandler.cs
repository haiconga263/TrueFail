using Common;
using Common.Exceptions;
using Common.Helpers;
using MDM.UI.Employees.Interfaces;
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
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IRegionRepository regionRepository = null;
        private readonly IRegionQueries regionQueries = null;
        private readonly ICountryQueries countryQueries = null;
        public UpdateCommandHandler(IRegionRepository regionRepository, IRegionQueries regionQueries, ICountryQueries countryQueries)
        {
            this.regionRepository = regionRepository;
            this.regionQueries = regionQueries;
            this.countryQueries = countryQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if(request.Region == null || request.Region.Id == 0)
            {
                throw new BusinessException("Region.NotExisted");
            }

            var region = await regionQueries.Get(request.Region.Id);
            if (region == null)
            {
                throw new BusinessException("Region.NotExisted");
            }

            var checkingRegion = (await regionQueries.Gets($"r.code = '{request.Region.Code}' and r.id <> {region.Id} and r.is_deleted = 0")).FirstOrDefault();
            if (checkingRegion != null)
            {
                throw new BusinessException("Region.ExistedCode");
            }

            var country = await countryQueries.Get(request.Region.CountryId);
            if (country == null)
            {
                throw new BusinessException("Country.NotExisted");
            }

            region = UpdateBuild(region, request.LoginSession);
            region.Code = request.Region.Code;
            region.Name = request.Region.Name;
            region.CountryId = request.Region.CountryId;
            region.IsUsed = request.Region.IsUsed;
            var rs = await regionRepository.Update(region);

            return rs;
        }
    }
}
