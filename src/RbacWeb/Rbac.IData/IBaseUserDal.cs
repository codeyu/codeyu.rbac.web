using Murphy.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.IData
{
    public interface IBaseUserDal : IBaseDal<BaseUser>
    {
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        BaseUser LoginUser(string userName, string userPassword);

        /// <summary>
        /// 获取用户组织机构树
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        List<BaseOrganize> GetUserOrganizeListWhere(string where, string orderBy);

        /// <summary>
        /// 得到部门所在的单位
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        BaseOrganize GetUserCompanyId(string departmentId);

        /// <summary>
        /// 得到部门所在的分部
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        BaseOrganize GetUserSubCompanyId(string departmentId);

        /// <summary>
        /// 获取用户数据权限类型标识
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetUserDataPermissionConstraint(string userId);

        /// <summary>
        /// 获取用户对应的功能模块权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<BaseModule> GetUserModuleListByUserId(string userId);

        /// <summary>
        /// 获取用户对应的操作按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<BasePermissionItem> GetUserButtonListByUserId(string userId);


        /// <summary>
        /// 获取用户对应的字段权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<BasePermissionItem> GetUserFieldListByUserId(string userId);

        /// <summary>
        /// 获取用户对应的明细数据权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<BaseOrganize> GetUserDataDetailsListByUserId(string userId);

        /// <summary>
        /// 保存用户对应对应的模块权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        bool UpdateUserModuleList(string id, string accessValueArray);

        /// <summary>
        /// 保存用户对应对应的操作按钮权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        bool UpdateUserButtonList(string id, string accessValueArray);

        /// <summary>
        /// 保存用户对应对应的字段权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        bool UpdateUserFieldList(string id, string accessValueArray);

        /// <summary>
        /// 保存用户对应对应的数据权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        bool UpdateUserDataList(string id, string accessValueArray, string constraint);

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<BaseRole> GetUserRoleListWhere(string userId);

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        bool SaveAllotRole(string userId, string roleIds);
    }
}
