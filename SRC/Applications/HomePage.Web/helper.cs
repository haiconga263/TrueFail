using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HomePage.Web
{
    public static class helper 
    {
        public static string ToFormatCurrency(this decimal value)
        {
            var c = ((int)value).ToString();
            CultureInfo cul = CultureInfo.CurrentCulture; //GetCultureInfo("vi-VN");// try with "en-US" vi-VN
            string res = double.Parse(c).ToString("#,##0 "+cul.NumberFormat.CurrencySymbol, cul.NumberFormat);
            return res;

        }
        
    }
    
}
