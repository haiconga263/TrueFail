using Common.Exceptions;
using Common.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Controls;

namespace Web.Helpers
{
    public class WebHelper
    {
        public static async Task<T> HttpGet<T>(string baseUri, string path, string accessToken = "", string lang = "")
        {
            var httpRs = await CommonHelper.HttpGet(baseUri, path, accessToken, lang);

            if (httpRs.IsSuccess)
            {
                if (httpRs.Content != null)
                {
                    var rs = ToAPIDataResult<T>(httpRs.Content);
                    if (rs.Result == 0)
                    {
                        return rs.Data;
                    }
                    else if (rs.Result > 0)
                    {
                        throw new BusinessWarningException(rs.ErrorMessage);
                    }
                    else
                    {
                        throw new BusinessException(rs.ErrorMessage);
                    }
                }
            }
            throw new BusinessException("APIGateway.FailedNetWork");
        }

        public static async Task<T> HttpPost<T>(string baseUri, string path, object data, string accessToken = "", string lang = "")
        {
            var httpRs = await CommonHelper.HttpPost(baseUri, path, data, accessToken, lang);

            if (httpRs.IsSuccess)
            {
                if (httpRs.Content != null)
                {
                    var rs = ToAPIDataResult<T>(httpRs.Content);
                    if (rs.Result == 0)
                    {
                        return rs.Data;
                    }
                    else if (rs.Result > 0)
                    {
                        throw new BusinessWarningException(rs.ErrorMessage);
                    }
                    else
                    {
                        throw new BusinessException(rs.ErrorMessage);
                    }
                }
            }
            throw new BusinessException("APIGateway.FailedNetWork");
        }

        public static APIDataResult<T> ToAPIDataResult<T>(string content)
        {
            return JsonConvert.DeserializeObject<APIDataResult<T>>(content);
        }
        public static APIResult ToAPIResult(string content)
        {
            return JsonConvert.DeserializeObject<APIResult>(content);
        }
    }
}
