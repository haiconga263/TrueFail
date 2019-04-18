using System;
using System.Collections.Generic;
using System.Text;

namespace GS1.UI.Constansts
{
    public class AppUrls
    {
        public static readonly string Domain = "localhost";
        public static readonly string UrlRoot = "api/gtin";
        public static readonly string UrlGenerateGTIN = UrlRoot + "/generate";
        public static readonly string UrlCheckNewGTIN = UrlRoot + "/checknew";
        public static readonly string UrlCalculateCheckDigitGTIN = UrlRoot + "/calculatecheckdigit";
        public static readonly string UrlInsertOrUpdateGTIN = UrlRoot + "/insertorupdate";
    }
}
