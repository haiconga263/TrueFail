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

namespace Admin.Commands.GeoCommand.Wards
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IWardRepository wardRepository = null;
        private readonly IWardQueries wardQueries = null;
        private readonly IDistrictQueries districtQueries = null;
        private readonly IProvinceQueries provinceQueries = null;
        private readonly IRegionQueries regionQueries = null;
        private readonly ICountryQueries countryQueries = null;
        public UpdateCommandHandler(IWardRepository wardRepository, IWardQueries wardQueries, IDistrictQueries districtQueries, IProvinceQueries provinceQueries, IRegionQueries regionQueries, ICountryQueries countryQueries)
        {
            this.wardRepository = wardRepository;
            this.wardQueries = wardQueries;
            this.districtQueries = districtQueries;
            this.provinceQueries = provinceQueries;
            this.regionQueries = regionQueries;
            this.countryQueries = countryQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if(request.Ward == null || request.Ward.Id == 0)
            {
                throw new BusinessException("Ward.NotExisted");
            }

            var ward = await wardQueries.Get(request.Ward.Id);
            if (ward == null)
            {
                throw new BusinessException("Ward.NotExisted");
            }

            var checkingWard = (await wardQueries.Gets($"w.code = '{request.Ward.Code}' and w.id <> {ward.Id} and w.is_deleted = 0")).FirstOrDefault();
            if (checkingWard != null)
            {
                throw new BusinessException("Ward.ExistedCode");
            }

            var country = await countryQueries.Get(request.Ward.CountryId);
            if (country == null)
            {
                throw new BusinessException("Country.NotExisted");
            }

            var province = await provinceQueries.Get(request.Ward.ProvinceId);
            if (province == null || province.CountryId != request.Ward.CountryId)
            {
                throw new BusinessException("Province.NotExisted");
            }

            var district = await districtQueries.Get(request.Ward.DistrictId);
            if (district == null || district.ProvinceId != request.Ward.ProvinceId)
            {
                throw new BusinessException("District.NotExisted");
            }

            ward = UpdateBuild(ward, request.LoginSession);
            ward.Code = request.Ward.Code;
            ward.Name = request.Ward.Name;
            ward.CountryId = request.Ward.CountryId;
            ward.ProvinceId = request.Ward.ProvinceId;
            ward.DistrictId = request.Ward.DistrictId;
            ward.IsUsed = request.Ward.IsUsed;
            var rs = await wardRepository.Update(ward);

            return rs;
        }
    }
}
