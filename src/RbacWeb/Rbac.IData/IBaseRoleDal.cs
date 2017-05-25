using Murphy.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.IData
{
    public interface IBaseRoleDal : IBaseDal<BaseRole>
    {

        /// <summary>
        /// 获取指定部门下的角色
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        List<BaseRole> GetRoleListByDepartmentId(string departmentId);
         /// <summary>
        /// 删除角色（带权限信息）
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
       bool DeleteRole(string roleId);
         /// <summary>
        /// 获取角色数据权限类型标识
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        string GetRoleDataPermissionConstraint(string roleId);
        /// <summary>
        /// 获取角色对应的功能模块权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<BaseModule> GetRoleModuleListByRoleId(string roleId);
        /// <summary>
        /// 获取角色对应的操作按钮权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<BasePermissionItem> GetRoleButtonListByRoleId(string roleId);
        /// <summary>
        /// 获取角色对应的明细数据权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<BaseOrganize> GetRoleDataDetailsListByRoleId(string roleId);
        /// <summary>
        /// 获取用户对应的字段权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<BasePermissionItem> GetRoleFieldListByRoleId(string roleId);
        /// <summary>
        /// 保存角色对应的模块权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        bool UpdateRoleModuleList(string id, string accessValueArray);
        /// <summary>
        /// 保存角色对应的操作按钮权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        bool UpdateRoleButtonList(string id, string accessValueArray);
        /// <summary>
        /// 保存角色对应的字段权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        bool UpdateRoleFieldList(string id, string accessValueArray);
        /// <summary>
        /// 保存角色对应的数据权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        bool UpdateRoleDataList(string id, string accessValueArray, string constraint);
    }
}
