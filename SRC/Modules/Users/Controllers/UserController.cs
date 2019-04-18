using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Users.Commands;
using Users.UI.Interfaces;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class UserController : BaseController
    {
        private readonly IMediator mediator = null;
        private readonly IUserQueries userQueries = null;
        private readonly IAccessTokenQueries accessTokenQueries = null;
        public UserController(IMediator mediator, IUserQueries userQueries, IAccessTokenQueries accessTokenQueries)
        {
            this.mediator = mediator;
            this.userQueries = userQueries;
            this.accessTokenQueries = accessTokenQueries;
        }

        #region Authen

        [HttpPost]
        [Route("login")]
        public async Task<APIResult> Login([FromBody]LoginCommand command)
        {
            var rs = await mediator.Send(command);
            if (rs.LoginResult == 0)
            {
                SetUserSession(rs);
            }
            return new APIResult()
            {
                Result = rs.LoginResult,
                Data = rs.LoginResult == 0 ? rs : null,
                ErrorMessage = rs.LoginResult == 0 ? null : GetCaption(rs.LoginCaptionMessage)
            };
        }

        [HttpPost]
        [Route("loginanotheruser")]
        [AuthorizeInUserService]
        public async Task<APIResult> LoginAnotherUser([FromBody]LoginAnotherCommand command)
        {
            var rs = await mediator.Send(command);
            if (rs.LoginResult == 0)
            {
                SetUserSession(rs);
                RemoveUserSession(LoginSession);
            }
            return new APIResult()
            {
                Result = rs.LoginResult,
                Data = rs.LoginResult == 0 ? rs : null,
                ErrorMessage = rs.LoginResult == 0 ? null : GetCaption(rs.LoginCaptionMessage)
            };
        }

        [HttpPost]
        [Route("logout")]
        [AuthorizeInUserService]
        public async Task<APIResult> Logout([FromBody]LogoutCommand command)
        {
            var rs = await mediator.Send(command);
            if (rs == 0)
            {
                RemoveUserSession(LoginSession);
            }
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpGet]
        [Route("checklogin")]
        public async Task<APIResult> CheckLogin(string accessToken)
        {
            var userSession = GetUserSession(accessToken);
            if(userSession == null)
            {
                userSession = await accessTokenQueries.Get(accessToken);
                if(userSession != null)
                {
                    SetUserSession(userSession);
                }
            }
            return new APIResult()
            {
                Result = userSession != null ? 0 : -1,
                Data = userSession
            };
        }


        [HttpPost]
        [Route("changepassword")]
        [AuthorizeInUserService]
        public async Task<APIResult> ChangePassword([FromBody]ChangePasswordCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("changeemail")]
        [AuthorizeInUserService]
        public async Task<APIResult> ChangeEmail([FromBody]ChangeEmailCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("registerresetpassword")]
        public async Task<APIResult> RegisterResetPassword([FromBody]RegisterResetPasswordCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("resetpassword")]
        public async Task<APIResult> ResetPassword([FromBody]ResetPasswordCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("active")]
        public async Task<APIResult> ActiveUser([FromBody]ActiveUserPasswordCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        #endregion

        #region UserAction

        [HttpPost]
        [Route("reset-password")]
        [AuthorizeInUserService("Administrator")]
        public async Task<APIResult> ResetPassword([FromBody]ResetPasswordByAdminCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("add")]
        [AuthorizeInUserService("Another")]
        public async Task<APIResult> AddUser([FromBody]AddUserCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpPost]
        [Route("update")]
        [AuthorizeInUserService("Administrator")]
        public async Task<APIResult> UpdateUser([FromBody]UpdateUserCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }



        [HttpPost]
        [Route("remove")]
        [AuthorizeInUserService("Administrator")]
        public async Task<APIResult> RemoveUser([FromBody]RemoveUserCommand command)
        {
            return new APIResult()
            {
                Result = 0,
                Data = await mediator.Send(command)
            };
        }

        [HttpPost]
        [Route("changeroles")]
        [AuthorizeInUserService("Administrator")]
        public async Task<APIResult> ChangeRoles([FromBody]ChangeRolesCommand command)
        {
            var rs = await mediator.Send(command);
            return new APIResult()
            {
                Result = rs
            };
        }

        [HttpGet]
        [Route("get/roles")]
        [AuthorizeInUserService("Administrator")]
        public async Task<APIResult> GetRoles(int type = 2)
        {
            var rs = await userQueries.GetRoles(type);
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("get/user")]
        [AuthorizeInUserService("Administrator")]
        public async Task<APIResult> GetUser(int userId)
        {
            var rs = await userQueries.GetUser(userId);
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("get/users")]
        [AuthorizeInUserService("Administrator,DeliverySupervisor,Collector")]
        public async Task<APIResult> GetUsers(int userType = 0)
        {
            //0 : all
            //1 : externalUser
            //2 : InternalUser
            string cmd = string.Empty;
            if(userType == 1)
            {
                cmd = "is_external_user = 1";
            }
            else if(userType == 2)
            {
                cmd = "is_external_user = 0";
            }

            var rs = await userQueries.GetUsers(cmd);
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("gets/userwithrole")]
        [AuthorizeInUserService("Administrator,DeliverySupervisor,Collector")]
        public async Task<APIResult> GetUsersWithRoles(int userType = 0)
        {
            //0 : all
            //1 : externalUser
            //2 : InternalUser
            string cmd = string.Empty;
            if (userType == 1)
            {
                cmd = "is_external_user = 1";
            }
            else if (userType == 2)
            {
                cmd = "is_external_user = 0";
            }

            var rs = await userQueries.GetUsersWithRole(cmd);
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("get/users/not-assign/by")]
        [AuthorizeInUserService("Administrator")]
        public async Task<APIResult> GetUsersNotAssignBy(bool isExternalUser, string roleName = "")
        {
            var rs = await userQueries.GetUsersNotAssignBy(isExternalUser, roleName);
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("get/userswithrole")]
        [AuthorizeInUserService("Administrator")]
        public async Task<APIResult> GetUsersWithRole()
        {
            var rs = await userQueries.GetUsersWithRole();
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("get/userwithrole")]
        [AuthorizeInUserService("Administrator")]
        public async Task<APIResult> GetUserWithRole(int userId)
        {
            var rs = await userQueries.GetUserWithRole(userId);
            return new APIResult()
            {
                Result = 0,
                Data = rs
            };
        }

        #endregion
    }
}
