using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QC.UI.Interface;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace QC.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class QCStaffController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IQCStaff qcStaff = null;
        public QCStaffController(IMediator mediator, IQCStaff qcStaff)
        {
            this.mediator = mediator;
            this.qcStaff = qcStaff;
        }
        [HttpGet]
        [Route("get")]
        [AuthorizeUser("QCStaff")]
        public async Task<APIResult> Get()
        {
            return new APIResult()
            {
                Result = 0,
                Data = null
            };
        }
    }
}
