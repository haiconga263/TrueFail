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

namespace Admin.Commands.GeoCommand.Provinces
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IProvinceRepository provinceRepository = null;
        private readonly IProvinceQueries provinceQueries = null;
        private readonly IRegionQueries regionQueries = null;
        private readonly ICountryQueries countryQueries = null;
        public UpdateCommandHandler(IProvinceRepository provinceRepository, IProvinceQueries provinceQueries, IRegionQueries regionQueries, ICountryQueries countryQueries)
        {
            this.provinceRepository = provinceRepository;
            this.provinceQueries = provinceQueries;
            this.regionQueries = regionQueries;
            this.countryQueries = countryQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if(request.Province == null || request.Province.Id == 0)
            {
                throw new BusinessException("Region.NotExisted");
            }

            var province = await provinceQueries.Get(request.Province.Id);
            if (province == null)
            {
                throw new BusinessException("Province.NotExisted");
            }

            var checkingProvince = (await provinceQueries.Gets($"p.code = '{request.Province.Code}' and p.id <> {province.Id} and p.is_deleted = 0")).FirstOrDefault();
            if (checkingProvince != null)
            {
                throw new BusinessException("Province.ExistedCode");
            }

            var country = await countryQueries.Get(request.Province.CountryId);
            if (country == null)
            {
                throw new BusinessException("Country.NotExisted");
            }

            if (request.Province.RegionId != null)
            {
                var region = await regionQueries.Get(request.Province.RegionId.Value);
                if (region == null || region.CountryId != request.Province.CountryId)
                {
                    throw new BusinessException("Region.NotExisted");
                }
            }

            province = UpdateBuild(province, request.LoginSession);
            province.Code = request.Province.Code;
            province.Name = request.Province.Name;
            province.PhoneCode = request.Province.PhoneCode;
            province.CountryId = request.Province.CountryId;
            province.RegionId = request.Province.RegionId;
            province.IsUsed = request.Province.IsUsed;
            var rs = await provinceRepository.Update(province);

            return rs;
        }
    }
}
