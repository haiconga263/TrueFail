using Common.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class CommonHelper
    {
        public static TResult Mapper<Tin, TResult>(Tin input) where TResult : Tin
        {
            TResult result = (TResult)Activator.CreateInstance(typeof(TResult));

            var destPro = result.GetType().GetProperties();
            foreach (PropertyInfo p in input.GetType().GetProperties())
            {
                var des = destPro.Where(d => d.Name == p.Name).FirstOrDefault();
                if (des != null)
                {
                    des.SetValue(result, p.GetValue(input));
                }
            }

            return result;
        }

        public static async Task<UserHttpResponse> HttpGet(string baseUri, string path, string accessToken = "", string lang = "")
        {
            //Create httpClient
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("AccessToken", accessToken);
            client.DefaultRequestHeaders.Add("Lang", lang);
            client.BaseAddress = new Uri(baseUri);

            //Request
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                return new UserHttpResponse()
                {
                    IsSuccess = true,
                    StatusCode = response.StatusCode,
                    Content = await response.Content.ReadAsStringAsync()
                };
            }

            return new UserHttpResponse()
            {
                IsSuccess = false,
                StatusCode = response.StatusCode
            };
        }

        public static async Task<UserHttpResponse> HttpPost<T>(string baseUri, string path, T data, string accessToken = "", string lang = "")
        {
            //Create httpClient
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("AccessToken", accessToken);
            client.DefaultRequestHeaders.Add("Lang", lang);
            client.BaseAddress = new Uri(baseUri);

            //Request
            HttpResponseMessage response = await client.PostAsync(path, new StringContent(JsonConvert.SerializeObject(data),
                                                                                          System.Text.Encoding.UTF8,
                                                                                          "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return new UserHttpResponse()
                {
                    IsSuccess = true,
                    StatusCode = response.StatusCode,
                    Content = await response.Content.ReadAsStringAsync()
                };
            }

            return new UserHttpResponse()
            {
                IsSuccess = false,
                StatusCode = response.StatusCode
            };
        }

        public static string GenerateRandomString(int length, int numberOfNonAlphanumericCharacters)
        {
            PasswordOptions opts = new PasswordOptions()
            {
                RequiredLength = length,
                RequiredUniqueChars = length - numberOfNonAlphanumericCharacters,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = false,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return Convert.ChangeType(value, t);
        }

        public static void CheckAndCreateDirectory(string directory)
        {
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        }
        public static string SaveImage(string prefix, string id, string type, string data)
        {
            string imageLink = string.Empty;

            string pathImage = GlobalConfiguration.ImageDataStoredPath + prefix + "/";
            CheckAndCreateDirectory(pathImage);
            File.WriteAllBytes(pathImage + id + "." + type, Convert.FromBase64String(data));
            imageLink = prefix + id + "." + type;

            return imageLink;
        }

        public static void DeleteImage(string pathFileName)
        {
            try
            {
                File.Delete(GlobalConfiguration.ImageDataStoredPath + pathFileName);
            }
            catch { }
        }

        public static string GetFileExtension(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf(".") + 1);
        }
        public static string GetImageType(byte[] ImageData)
        {
            string result = string.Empty;

            try
            {
                bool Istype = false;
                for (int i = 0; i < ImageData.Length; i++)
                {
                    if (Istype == false)
                    {
                        if ((char)ImageData[i] == '/')
                        {
                            Istype = true;
                        }
                    }
                    else
                    {
                        if ((char)ImageData[i] == ';')
                        {
                            break;
                        }
                        else
                        {
                            result += (char)ImageData[i];
                        }
                    }
                }
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }
        public static bool IsImageType(string type)
        {
            bool result = false;

            foreach (var item in Constant.ListImageType)
            {
                if (type == item)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static EventResultModel RunEvent(Assembly assembly, string method, Type baseType, object[] param)
        {
            if (assembly == null || string.IsNullOrEmpty(method))
                return new EventResultModel();

            var classStr = method.Substring(0, method.LastIndexOf('.'));
            var functionStr = method.Substring(method.LastIndexOf('.') + 1);
            Type type = assembly.GetType(classStr);
            if (type != null)
            {
                if (baseType == null || type.BaseType == baseType)
                {
                    object instance = Activator.CreateInstance(type);
                    MethodInfo theMethod = type.GetMethod(functionStr);
                    if (theMethod != null)
                    {
                        return (EventResultModel)theMethod.Invoke(instance, param);
                    }
                }
            }
            return new EventResultModel();
        }

        public static string CreateMD5(this string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static double DistanceBetween2Points(double la1, double lo1, double la2, double lo2)
        {
            double dLat = (la2 - la1) * (Math.PI / 180);
            double dLon = (lo2 - lo1) * (Math.PI / 180);
            double la1ToRad = la1 * (Math.PI / 180);
            double la2ToRad = la2 * (Math.PI / 180);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(la1ToRad) * Math.Cos(la2ToRad) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = Constant.REarth * c;
            return d;
        }
    }
}
