using Murphy.Core;
using Murphy.Entity;
using Murphy.IData;
using Murphy.Utils;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Business
{
    public class BaseStorageBll
    {
        protected IBaseStorageDal dal = null;
        public BaseStorageBll()
        {
            IApplicationContext springContext = ContextRegistry.GetContext();
            dal = springContext.GetObject("BaseStorageDal") as IBaseStorageDal;
        }

        /// <summary>
        /// 获取操作按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BaseButtonPermission> GetButtonPermission(string userId)
        {
            return dal.GetButtonPermission(userId);
        }
        /// <summary>
        /// 获取字段权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BaseFieldPermission> GetFieldPermission(string userId)
        {
            return dal.GetFieldPermission(userId);
        }
        /// <summary>
        /// 获取功能模块权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BaseModulePermission> GetModulePermission(string userId)
        {
            return dal.GetModulePermission(userId);
        }

         /// <summary>
        /// 获取数据权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetDataPermission(CacheUser user)
        {
            return dal.GetDataPermission(user);
        }
    }
}
