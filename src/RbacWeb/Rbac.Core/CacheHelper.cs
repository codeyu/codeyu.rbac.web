using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Rbac.Core
{
    /// <summary>
    /// 缓存帮助类
    /// </summary>
    public class CacheHelper
    {
        private static System.Web.Caching.Cache cache = HttpContext.Current.Cache;

        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Insert(string key, object obj)
        {
            if (obj == null) return false;
            object lockObj = new object();
            lock (lockObj)
            {
                int expiry = 120;
                //取出服务器缓存保存时长
                if (RequestCache.GetCacheSystem().CacheMinute != null)
                {
                    expiry = Convert.ToInt32(RequestCache.GetCacheSystem().CacheMinute);
                }
                Insert(key, obj, expiry);
            }
            return true;
        }

        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static bool Insert(string key, object obj, int expiry)
        {
            if (obj == null) return false;
            object lockObj = new object();
            lock (lockObj)
            {
                cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, expiry, 0));
            }
            return true;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            //是否开启服务器缓存
            if (RequestCache.GetCacheSystem().OpenCache== "Open")
            {
                return cache.Get(key);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 移出缓存
        /// </summary>
        /// <param name="key"></param>
        public static bool Remove(string key)
        {
            object lockObj = new object();
            lock (lockObj)
            {
                cache.Remove(key);
            }
            return true;
        }
        /// <summary>
        /// 移出所有缓存
        /// </summary>
        /// <returns></returns>
        public static void RemoveAll()
        {
            IDictionaryEnumerator CacheEnum = cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }
}
