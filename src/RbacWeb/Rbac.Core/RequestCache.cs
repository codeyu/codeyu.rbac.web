using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Rbac.Core
{
    public class RequestCache
    {
        public RequestCache()
        {

        }

        private static string CACHE_USER = "CACHE_USER";
        private static string CACHE_SYSTEM = "CACHE_SYSTEM";
        private static string CACHE_TOPMODULE = "CACHE_TOPMODULE";
        /// <summary>
        /// 添加用户信息到缓存中
        /// </summary>
        /// <param name="user"></param>
        public static void AddCacheUser(CacheUser user)
        {
            HttpContext rq = HttpContext.Current;
            rq.Cache[CACHE_USER] = user;
        }

        /// <summary>
        /// 取出用户信息
        /// </summary>
        /// <returns></returns>
        public static CacheUser GetCacheUser()
        {
            HttpContext rq = HttpContext.Current;
            if (rq.Cache[CACHE_USER] == null)
            {
                HttpContext.Current.Response.Redirect("/Home/LoginUser");
            }
            return (CacheUser)rq.Cache[CACHE_USER];

        }

        /// <summary>
        /// 添加系统参数信息到缓存中
        /// </summary>
        /// <param name="system"></param>
        public static void AddCacheSystem(CacheSystem system)
        {
            HttpContext rq = HttpContext.Current;
            rq.Cache[CACHE_SYSTEM] = system;
        }

        /// <summary>
        /// 取出系统参数信息
        /// </summary>
        /// <returns></returns>
        public static CacheSystem GetCacheSystem()
        {
            HttpContext rq = HttpContext.Current;
            if (rq.Cache[CACHE_SYSTEM] == null)
            {
                HttpContext.Current.Response.Redirect("/Home/LoginUser");
            }
            return (CacheSystem)rq.Cache[CACHE_SYSTEM];

        }

        /// <summary>
        /// 添加顶部菜单Id到缓存中
        /// </summary>
        /// <param name="topModule"></param>
        public static void AddCacheTopModuleId(CacheTopModule topModule)
        {
            HttpContext rq = HttpContext.Current;
            rq.Cache[CACHE_TOPMODULE] = topModule;
        }

        /// <summary>
        /// 取出顶部菜单Id
        /// </summary>
        /// <returns></returns>
        public static CacheTopModule GetCacheTopModuleId()
        {
            HttpContext rq = HttpContext.Current;
            if (rq.Cache[CACHE_TOPMODULE] == null)
            {
                HttpContext.Current.Response.Redirect("/Home/LoginUser");
            }
            return (CacheTopModule)rq.Cache[CACHE_TOPMODULE];

        }
    }
}
