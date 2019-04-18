using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Users.Commands
{
    public class LoginCommand : BaseCommand<UserSession>
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsRememberMe { get; set; }

        public string AppName { get; set; }
        public string DeviceInfo { get; set; }
        public string MessagingToken { get; set; }

        public LoginCommand()
        {
        }

        public LoginCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public LoginCommand(string userName, string password, bool isRememberMe)
        {
            UserName = userName;
            Password = password;
            IsRememberMe = isRememberMe;
        }

        public LoginCommand(string userName, string password, bool isRememberMe, string appName, string deviceInfo, string messagingToken)
        {
            UserName = userName;
            Password = password;
            IsRememberMe = isRememberMe;
            AppName = appName;
            DeviceInfo = deviceInfo;
            MessagingToken = messagingToken;
        }
    }
}
