using Rbac.Core;
using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.IData
{
    public interface IBaseStorageDal
    {
        /// <summary>
        /// 获取操作按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<BaseButtonPermission> GetButtonPermission(string userId);
        /// <summary>
        /// 获取字段权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<BaseFieldPermission> GetFieldPermission(string userId);
        /// <summary>
        /// 获取功能模块权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<BaseModulePermission> GetModulePermission(string userId);
        /// <summary>
        /// 获取数据权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetDataPermission(CacheUser user);
    }
}
