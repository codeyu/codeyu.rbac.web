using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.IData
{
    public interface IBaseOrganizeDal : IBaseDal<BaseOrganize>
    {
        /// <summary>
        /// 获取组织机构角色
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        List<BaseRole> GetOrganizeRoleListWhere(string departmentId);

          /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        bool SaveAllotRole(string departmentId, string roleIds);
    }
}
