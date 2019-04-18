using Homepage.UI.ViewModels;
using MDM.UI.Categories;
using MDM.UI.Categories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePage.Web.Helpers
{
    public static class RoutingHelper
    {
        public static string GetUrl(this ProductHomepageViewModel product)
        {
            if (product == null)
                return string.Empty;

            return string.Format("/{0}-p{1}.html", product.Name?.Editor(), product.Id);
        }

        public static string GetUrl(this Category category)
        {
            if (category == null)
                return string.Empty;

            return string.Format("/{0}-c{1}", category.Name?.Editor(), category.Id);
        }

		public static string GetUrl(this PostHomepageViewModel post)
		{
			if (post == null)
				return string.Empty;

			return string.Format("/{0}-b{1}.html", post.Title?.Editor(), post.Id);
		}
		private static string Editor(this string str)
        {
            return str.ToLower().Trim().Replace(" ", "-").Replace("/", "-").Replace(@"\", "-").stripVN();
        }

        private static string stripVN(this string str)
        {
            char[] aChars = new char[] { 'à', 'á', 'ạ', 'ả', 'ã', 'â', 'ầ', 'ấ', 'ậ', 'ẩ', 'ẫ', 'ă', 'ằ', 'ắ', 'ặ', 'ẳ', 'ẵ' };
            char[] eChars = new char[] { 'è', 'é', 'ẹ', 'ẻ', 'ẽ', 'ê', 'ề', 'ế', 'ệ', 'ể', 'ễ' };
            char[] iChars = new char[] { 'ì', 'í', 'ị', 'ỉ', 'ĩ' };
            char[] oChars = new char[] { 'ò', 'ó', 'ọ', 'ỏ', 'õ', 'ô', 'ồ', 'ố', 'ộ', 'ổ', 'ỗ', 'ơ', 'ờ', 'ớ', 'ợ', 'ở', 'ỡ' };
            char[] uChars = new char[] { 'ù', 'ú', 'ụ', 'ủ', 'ũ', 'ư', 'ừ', 'ứ', 'ự', 'ử', 'ữ' };
            char[] yChars = new char[] { 'ỳ', 'ý', 'ỵ', 'ỷ', 'ỹ' };
            char[] dChars = new char[] { 'đ' };
            char[] AChars = new char[] { 'À', 'Á', 'Ạ', 'Ả', 'Ã', 'Â', 'Ầ', 'Ấ', 'Ậ', 'Ẩ', 'Ẫ', 'Ă', 'Ằ', 'Ắ', 'Ặ', 'Ẳ', 'Ẵ' };
            char[] EChars = new char[] { 'È', 'É', 'Ẹ', 'Ẻ', 'Ẽ', 'Ê', 'Ề', 'Ế', 'Ệ', 'Ể', 'Ễ' };
            char[] IChars = new char[] { 'Ì', 'Í', 'Ị', 'Ỉ', 'Ĩ' };
            char[] OChars = new char[] { 'Ò', 'Ó', 'Ọ', 'Ỏ', 'Õ', 'Ô', 'Ồ', 'Ố', 'Ộ', 'Ổ', 'Ỗ', 'Ơ', 'Ờ', 'Ớ', 'Ợ', 'Ở', 'Ỡ' };
            char[] UChars = new char[] { 'Ù', 'Ú', 'Ụ', 'Ủ', 'Ũ', 'Ư', 'Ừ', 'Ứ', 'Ự', 'Ử', 'Ữ' };
            char[] YChars = new char[] { 'Ỳ', 'Ý', 'Ỵ', 'Ỷ', 'Ỹ' };
            char[] DChars = new char[] { 'Đ' };

            str = str.stripVNProvider('a', aChars);
            str = str.stripVNProvider('e', eChars);
            str = str.stripVNProvider('i', iChars);
            str = str.stripVNProvider('o', oChars);
            str = str.stripVNProvider('u', uChars);
            str = str.stripVNProvider('y', yChars);
            str = str.stripVNProvider('d', dChars);
            str = str.stripVNProvider('A', AChars);
            str = str.stripVNProvider('E', EChars);
            str = str.stripVNProvider('I', IChars);
            str = str.stripVNProvider('O', OChars);
            str = str.stripVNProvider('U', UChars);
            str = str.stripVNProvider('Y', YChars);
            str = str.stripVNProvider('D', DChars);

            return str;
        }

        private static string stripVNProvider(this string str, char latin, char[] chars)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                str = str.Replace(chars[i], latin);
            }
            return str;
        }
    }
}
