using Administrator.Commands.CountryCommands;
using MDM.UI.Companies.Interfaces;
using MDM.UI.Countries.Interfaces;
using MDM.UI.Districts.Interfaces;
using MDM.UI.Provinces.Interfaces;
using MDM.UI.UnitOfMeasurements.Interfaces;
using MDM.UI.Wards.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Administrator.Controllers
{
    [Route("api/common")]
    [AuthorizeInUserService]
    public class CommonsController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IUnitOfMeasurementQueries uomQueries = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;


        public CommonsController(IMediator mediator,
                                    IUnitOfMeasurementQueries uomQueries,
                                    IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.uomQueries = uomQueries;
            this.accessTokenQueries = accessTokenQueries;
        }

        
        [HttpGet]
        [Route("uom")]
        public async Task<APIResult> GetUOMs()
        {
            var rs = (await uomQueries.GetsAsync()).Select(x => new SelectListItem()
            {
                Text = x.Code,
                Value = x.Id.ToString()
            });
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }
    }
}
