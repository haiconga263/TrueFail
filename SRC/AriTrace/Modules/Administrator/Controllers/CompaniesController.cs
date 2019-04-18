using Administrator.Commands.CompanyCommands;
using MDM.UI.Companies.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Administrator.Controllers
{
    [Route("api/company")]
    [AuthorizeInUserService]
    public class CompaniesController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly ICompanyQueries companyQueries = null;
        private readonly ICompanyRepository companyRepository = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;

        public CompaniesController(IMediator mediator, ICompanyQueries companyQueries, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.companyQueries = companyQueries;
            this.accessTokenQueries = accessTokenQueries;
        }

        [HttpGet]
        [Route("common")]
        public async Task<APIResult> Gets()
        {
            var rs = await companyQueries.GetsAsync();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("getbyid")]
        public async Task<APIResult> GetById(int id)
        {
            var rs = await companyQueries.GetByIdAsync(id);
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("getbyuserid")]
        public async Task<APIResult> GetByUserId(int id)
        {
            var rs = await companyQueries.GetByUserIdAsync(id);
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpPost]
        [Route("all")]
        public async Task<APIResult> GetAll()
        {
            var rs = await companyQueries.GetAllAsync();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpPost]
        [Route("insert")]
        public async Task<APIResult> Insert([FromBody]InsertCompanyCommand command)
        {
            var id = await mediator.Send(command);
            return new APIResult()
            {
                Data = new { id = (id > 0) ? id : (int?)null },
                Result = (id > 0) ? 0 : -1,
            };
        }

        [HttpPost]
        [Route("update")]
        public async Task<APIResult> Update([FromBody]UpdateCompanyCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<APIResult> Delete([FromBody]DeleteCompanyCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
