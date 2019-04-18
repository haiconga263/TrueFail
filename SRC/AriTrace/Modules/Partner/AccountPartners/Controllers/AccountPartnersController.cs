using MDM.UI.Accounts.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Partner.AccountPartners.Commands;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Partner.AccountPartners.Controllers
{
    [Route("api/account-partner")]
    [AuthorizeInUserService]
    public class AccountPartnersController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IAccountQueries accountQueries = null;
        private readonly IAccountRepository accountRepository = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;


        public AccountPartnersController(IMediator mediator, IAccountQueries accountQueries, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.accountQueries = accountQueries;
            this.accessTokenQueries = accessTokenQueries;
        }

        [HttpGet]
        [Route("common")]
        public async Task<APIResult> Gets(GetCommonAccountPartnerCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }

        [HttpPost]
        [Route("getbyid")]
        public async Task<APIResult> GetById([FromBody]GetByIdAccountPartnerCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }

        [HttpPost]
        [Route("all")]
        public async Task<APIResult> GetAll([FromBody]GetAllAccountPartnerCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Data = rs,
                Result = 0
            };
        }

        [HttpPost]
        [Route("insert")]
        public async Task<APIResult> Insert([FromBody]InsertAccountPartnerCommand command)
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
        public async Task<APIResult> Update([FromBody]UpdateAccountPartnerCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<APIResult> Delete([FromBody]DeleteAccountPartnerCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }
    }
}
