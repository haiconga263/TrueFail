using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    public interface ICacheProvider
    {
        bool TryGetValue<T>(string key, out T value, CachingType cachingType = CachingType.GlobalMemoryCaching, HttpContext instance = null);
        bool TrySetValue<T>(string key, T value, CachingType cachingType = CachingType.GlobalMemoryCaching, HttpContext instance = null);
        bool TryRemoveCaching<T>(string key, CachingType cachingType = CachingType.GlobalMemoryCaching, HttpContext instance = null);
    }
}
