using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Constant
    {
        public const int CachingTimeout = 5000; //5s

        public const int MaxImageLength = 5242880;

        public static string[] ListImageType = { "jpg", "JPG", "png", "PNG", "jpeg", "JPEG", "gif", "GIF" };

        public const string AccessTokenHeaderKey = "AccessToken";
        public const string LanguageHeaderKey = "lang";

        public const string AssetsLanguageFolder = "./assets/lang";

        public const double REarth = 6371;
    }
}
