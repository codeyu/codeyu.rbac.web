using Murphy.Core;
using Murphy.Entity;
using Murphy.IData;
using Murphy.Utils;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Data.SQLServer
{
    /// <summary>
    /// 权限存储信息
    /// </summary>
    public class BaseStorageDal : IBaseStorageDal
    {

        Database db = new Database("SQLConnectionString");

        /// <summary>
        /// 获取操作按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BaseButtonPermission> GetButtonPermission(string userId)
        {
            string sql = string.Format(@"select * from VM_GetButtonList  where UserId='{0}'", userId);
            lock (db)
            {
                return db.Fetch<BaseButtonPermission>(sql);
            }
        }
        /// <summary>
        /// 获取字段权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BaseFieldPermission> GetFieldPermission(string userId)
        {
            string sql = string.Format(@"select * from VM_GetFieldList where UserId='{0}'", userId);
            lock (db)
            {
                return db.Fetch<BaseFieldPermission>(sql);
            }
        }
        /// <summary>
        /// 获取功能模块权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BaseModulePermission> GetModulePermission(string userId)
        {
            string sql = string.Format(@"select * from VM_GetModuleList where UserId='{0}' Order By SortCode  ", userId);
            lock (db)
            {
                return db.Fetch<BaseModulePermission>(sql);
            }
        }
        /// <summary>
        /// 获取数据权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetDataPermission(CacheUser user)
        {
            string constraint = string.Empty; //约束
            string condition = string.Empty; //条件
            List<string> conditionList = new List<string>(); //条件列表
            List<string> idList = new List<string>(); //
            string objectValue = string.Empty;

            //用户对应的数据权限
            string baseUserPermissionSQL = string.Format(@"select * from Base_PermissionScope where PermissionMaster='BaseUser'  AND PermissionMasterValue='{0}' AND PermissionAccess='BaseOrganize' ", user.UserId);
            BasePermissionScope baseUserPermissionScope = db.Fetch<BasePermissionScope>(baseUserPermissionSQL).FirstOrDefault();
            if (baseUserPermissionScope != null)
            {
                constraint = baseUserPermissionScope.PermissionConstraint;
            }
            switch (constraint)
            {
                case "Company":
                    condition = " CompanyId='" + user.CompanyId + "'";
                    conditionList.Add(condition);
                    break;
                case "SubCompany":
                    condition = " SubCompanyId='" + user.SubCompanyId + "'";
                    conditionList.Add(condition);
                    break;
                case "Department":
                    condition = " DepartmentId='" + user.DepartmentId + "'";
                    conditionList.Add(condition);
                    break;
                case "WorkGroup":
                    condition = " WorkGroupId='" + user.WorkGroupId + "'";
                    conditionList.Add(condition);
                    break;
                case "ItSelf":
                    condition = " CreateUserId='" + user.UserId + "'";
                    conditionList.Add(condition);
                    break;
                case "Details":
                    condition = string.Format(@"select * from dbo.Base_PermissionScope where PermissionMaster='BaseUser' AND  PermissionMasterValue='{1}' AND PermissionAccess='BaseOrganize'", user.UserId);
                    idList = db.Fetch<BasePermissionScope>(condition).Select(u => u.PermissionAccessValue).ToList();
                    foreach (var id in idList)
                    {
                        objectValue += "'" + id + "'" + ",";
                    }
                    if (objectValue.Length != 0)
                    {
                        condition = " DepartmentId IN (" + objectValue.Substring(0, objectValue.Length - 1) + ") Or WorkGroupId IN (" + objectValue.Substring(0, objectValue.Length - 1) + ") ";
                        conditionList.Add(condition);
                    }
                    break;
                default:
                    break;
            }

            //角色对应的数据权限
            string getRoleIdListSQL = string.Format(@"select RoleId from (SELECT
	                                                        Id AS UserId,
	                                                        RoleId
                                                        FROM Base_User UNION SELECT
	                                                        UserId,
	                                                        RoleId
                                                        FROM Base_UserRole) as t where t.UserId='{0}'", user.UserId);
            //用户对应的角色集合
            List<string> roleIdList = db.Fetch<string>(getRoleIdListSQL);
            foreach (var roleId in roleIdList)
            {
                string baseRolePermissionSQL = string.Format(@"select * from Base_PermissionScope where PermissionMaster='BaseRole'  AND PermissionMasterValue='{0}' AND PermissionAccess='BaseOrganize' ", roleId);
                BasePermissionScope baseRolePermissionScope = db.Fetch<BasePermissionScope>(baseRolePermissionSQL).FirstOrDefault();
                if (baseRolePermissionScope != null)
                {
                    constraint = baseRolePermissionScope.PermissionConstraint;
                }
                switch (constraint)
                {
                    case "Company":
                        condition = " CompanyId='" + user.CompanyId + "'";
                        conditionList.Add(condition);
                        break;
                    case "SubCompany":
                        condition = " SubCompanyId='" + user.SubCompanyId + "'";
                        conditionList.Add(condition);
                        break;
                    case "Department":
                        condition = " DepartmentId='" + user.DepartmentId + "'";
                        conditionList.Add(condition);
                        break;
                    case "WorkGroup":
                        condition = " WorkGroupId='" + user.WorkGroupId + "'";
                        conditionList.Add(condition);
                        break;
                    case "ItSelf":
                        condition = " CreateUserId='" + user.UserId + "'";
                        conditionList.Add(condition);
                        break;
                    case "Details":
                        condition = string.Format(@"select * from dbo.Base_PermissionScope where PermissionMaster='BaseRole' AND  PermissionMasterValue='{1}' AND PermissionAccess='BaseOrganize'", roleId);
                        idList = db.Fetch<BasePermissionScope>(condition).Select(u => u.PermissionAccessValue).ToList();
                        foreach (var id in idList)
                        {
                            objectValue += "'" + id + "'" + ",";
                        }
                        if (objectValue.Length != 0)
                        {
                            condition = " DepartmentId IN (" + objectValue.Substring(0, objectValue.Length - 1) + ") Or WorkGroupId IN (" + objectValue.Substring(0, objectValue.Length - 1) + ") ";
                            conditionList.Add(condition);
                        }
                        break;
                    default:
                        break;
                }
            }

            string where = " where ";
            for (int i = 0; i < conditionList.Count; i++)
            {
                if (i == conditionList.Count - 1)
                {
                    where += conditionList[i];
                }
                else
                {
                    where += conditionList[i] + " OR";
                }
            }
            return where;
        }
    }
}
