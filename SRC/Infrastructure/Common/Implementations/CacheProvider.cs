using Common.Helpers;
using Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Autofac;

namespace Common.Implementations
{
    public class CacheProvider : ICacheProvider
    {
        public static ICacheProvider ICacheProviderInstance = GlobalConfiguration.Container.Resolve<ICacheProvider>();

        // for global caching
        private Dictionary<string, Dictionary<string, object>> MemoryCache { set; get; }
        public CacheProvider()
        {
            this.MemoryCache = new Dictionary<string, Dictionary<string, object>>();
        }

        public bool TryGetValue<T>(string key, out T value, CachingType cachingType = CachingType.GlobalMemoryCaching, HttpContext instance = null)
        {
            switch (cachingType)
            {
                case CachingType.GlobalMemoryCaching:
                    return TryGetValueGlobalMemory<T>(key, out value);
                case CachingType.SessionCaching:
                    return TryGetValueSession<T>(key, out value, instance);
                case CachingType.SqlCaching:
                    return TryGetValueSql<T>(key, out value);
                default:
                    value = default(T);
                    return false;
            }
        }

        public bool TryRemoveCaching<T>(string key, CachingType cachingType = CachingType.GlobalMemoryCaching, HttpContext instance = null)
        {
            switch (cachingType)
            {
                case CachingType.GlobalMemoryCaching:
                    return TryRemoveCachingGlobalMemory<T>(key);
                case CachingType.SessionCaching:
                    return TryRemoveCachingSession<T>(key, instance);
                case CachingType.SqlCaching:
                    return TryRemoveCachingSql<T>(key);
                default:
                    return false;
            }
        }

        public bool TrySetValue<T>(string key, T value, CachingType cachingType = CachingType.GlobalMemoryCaching, HttpContext instance = null)
        {
            switch (cachingType)
            {
                case CachingType.GlobalMemoryCaching:
                    return TrySetValueGlobalMemory<T>(key, value);
                case CachingType.SessionCaching:
                    return TrySetValueSession<T>(key, value, instance);
                case CachingType.SqlCaching:
                    return TrySetValueSql<T>(key, value);
                default:
                    return false;
            }
        }

        #region private methods helper
        private bool TryGetValueGlobalMemory<T>(string key, out T value)
        {
            bool result = false;
            T returnValue = default(T);
            Thread thRun = new Thread(new ThreadStart(() =>
            {
                lock (MemoryCache)
                {
                    string typevalue = typeof(T).GUID.ToString();
                    if (!MemoryCache.TryGetValue(typevalue, out Dictionary<string, object> cache))
                    {
                        result = false;
                        return;
                    }
                    if (!cache.TryGetValue(key, out object output))
                    {
                        result = false;
                        return;
                    }
                    else
                    {
                        returnValue = (T)output;
                        result = true;
                    }
                }
            }));
            thRun.Start();
            thRun.Join(Constant.CachingTimeout);
            value = returnValue;
            return result;
        }
        private bool TryGetValueSql<T>(string key, out T value)
        {
            throw new NotImplementedException();
        }
        private bool TryGetValueSession<T>(string key, out T value, HttpContext instance)
        {
            bool result = false;
            value = default(T);
            try
            {
                string valueStr = instance?.Session?.GetString(key);
                if (!string.IsNullOrEmpty(valueStr))
                {
                    value = JsonConvert.DeserializeObject<T>(valueStr);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.GetLogger().Error(ex.Message);
            }
            return result;
        }
        private bool TryRemoveCachingGlobalMemory<T>(string key)
        {
            bool result = false;
            Thread thRun = new Thread(new ThreadStart(() =>
            {
                lock (MemoryCache)
                {
                    string typevalue = typeof(T).GUID.ToString();
                    if (!MemoryCache.TryGetValue(typevalue, out Dictionary<string, object> cache))
                    {
                        // no data. not need to remove
                        result = true;
                        return;
                    }
                    if (!cache.TryGetValue(key, out object output))
                    {
                        // no data. not need to remove
                        result = true;
                        return;
                    }
                    else
                    {
                        cache.Remove(key);
                        result = true;
                    }
                }
            }));
            thRun.Start();
            thRun.Join(Constant.CachingTimeout);
            return result;
        }
        private bool TryRemoveCachingSql<T>(string key)
        {
            throw new NotImplementedException();
        }
        private bool TryRemoveCachingSession<T>(string key, HttpContext instance)
        {
            bool result = true;
            try
            {
                instance.Session.Remove(key);
            }
            catch (Exception ex)
            {
                LogHelper.GetLogger().Error(ex.Message);
                return false;
            }
            return result;
        }
        private bool TrySetValueGlobalMemory<T>(string key, T value)
        {
            bool result = false;
            Thread thRun = new Thread(new ThreadStart(() =>
            {
                lock (MemoryCache)
                {
                    string typevalue = typeof(T).GUID.ToString();
                    if (!MemoryCache.TryGetValue(typevalue, out Dictionary<string, object> cache))
                    {
                        cache = new Dictionary<string, object>();
                        MemoryCache.Add(typevalue, cache);
                    }
                    if (!cache.TryGetValue(key, out object output))
                    {
                        cache.Add(key, value);
                    }
                    else
                    {
                        cache[key] = value;
                    }
                }
                result = true;
            }));
            thRun.Start();
            thRun.Join(Constant.CachingTimeout);
            return result;
        }
        private bool TrySetValueSql<T>(string key, T value)
        {
            throw new NotImplementedException();
        }
        private bool TrySetValueSession<T>(string key, T value, HttpContext instance)
        {
            bool result = true;
            try
            {
                instance.Session.SetString(key, JsonConvert.SerializeObject(value));
            }
            catch (Exception ex)
            {
                LogHelper.GetLogger().Error(ex.Message);
                return false;
            }
            return result;
        }
        #endregion
    }
}
