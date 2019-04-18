using System;
using System.Collections.Generic;
using System.Text;

namespace Farmers.UI
{
    public class FarmerUrl
    {
        public const string Prefix = "api/Farmer";

        public const string gets = "gets";
        public static string ApiGets => $"{Prefix}/{gets}";

        public const string get = "get";
        public static string ApiGet => $"{Prefix}/{get}";

        public const string getByUser = "get/byuser";
        public static string ApiGetByUser => $"{Prefix}/{getByUser}";

        public const string add = "add";
        public static string ApiAdd => $"{Prefix}/{add}";

        public const string update = "update";
        public static string ApiUpdate => $"{Prefix}/{update}";

        public const string delete = "delete";
        public static string ApiDelete => $"{Prefix}/{delete}";

        public const string getConfig = "config-app/get";
        public static string ApiGetConfig => $"{Prefix}/{getConfig}";

        public const string getProfile = "profile/get";
        public static string ApiGetProfile => $"{Prefix}/{getProfile}";

        public const string updateProfile = "profile/update";
        public static string ApiUpdateProfile => $"{Prefix}/{updateProfile}";
    }
}
