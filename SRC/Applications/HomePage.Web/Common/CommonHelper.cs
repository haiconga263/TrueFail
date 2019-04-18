using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace HomePage.Web.Common
{
	public static class CommonHelper
	{
		public static string ToFormatCurrency(this decimal value)
		{
			var c = ((int)value).ToString();
			CultureInfo cul = CultureInfo.CurrentCulture; //GetCultureInfo("vi-VN");// try with "en-US" vi-VN
			string res = double.Parse(c).ToString("#,##0" + cul.NumberFormat.CurrencySymbol, cul.NumberFormat);
			return res;
		}
		/// <summary>
		/// Uses regex to strip HTML from a string
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string StripHtmlFromString(string input)
		{
			if (!string.IsNullOrEmpty(input))
			{
				input = Regex.Replace(input, @"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>", string.Empty, RegexOptions.Singleline);
				input = Regex.Replace(input, @"\[[^]]+\]", "");
			}
			return input;
		}
		/// <summary>
		/// Returns safe plain text using XSS library
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string SafePlainText(string input)
		{
			if (!string.IsNullOrEmpty(input))
			{
				input = StripHtmlFromString(input);
				//input = GetSafeHtml(input, true);
			}
			return input;
		}
	}
}
