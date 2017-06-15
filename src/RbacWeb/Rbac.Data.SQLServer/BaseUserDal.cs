using Rbac.Entity;
using Rbac.IData;
using Rbac.Utils;
using Rbac.Core;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Data.SQLServer
{
    public class BaseUserDal : BaseDAL<BaseUser>, IBaseUserDal
    {
        protected override string CreateSQL(string where, string orderBy)
        {
            where = where.IsNullOrEmpty() ? " 1=1 " : where;
            orderBy = orderBy.IsNullOrEmpty() ? " order by UserName" : " order by " + orderBy + "";
            return string.Format(@"select * from VM_GetUserList where {0} {1}", where, orderBy);
        }

        /// <summary>
        /// 查询所有记录   重写
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public override List<BaseUser> GetListWhere(string where, string orderBy)
        {
            lock (db)
            {
                StringBuilder sbStr = new StringBuilder();
                sbStr.Append(CreateSQL(where, orderBy));
                var baseUserList = db.Fetch<BaseUser>(sbStr.ToString());
                //过滤字段
                return BaseFieldPermissionDal.Instance.FilterUser(baseUserList);
            }
        }



        /// <summary>
        /// 得到一页数据 重写
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <param name="query"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public override Page<BaseUser> GetPageListWhere(out string pager, int pageNumber, int pageSize, string where, string query, string orderBy)
        {
            lock (db)
            {
                StringBuilder sbStr = new StringBuilder();
                sbStr.Append(CreateSQL(where, orderBy));
                var pageResult = db.Page<BaseUser>(pageNumber, pageSize, sbStr.ToString());
                pager = PageHelper.GetPageHtml(pageResult.TotalItems, pageNumber, pageSize, query);
                //过滤字段
                //pageResult.Items = BaseFieldPermissionDal.Instance.FilterUser(pageResult.Items);
                return pageResult;
            }
        }


        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public BaseUser LoginUser(string userName, string userPassword)
        {
            string where = " UserName='" + userName + "' ";
            lock (db)
            {
                return db.SingleOrDefault<BaseUser>(CreateSQL(where, null));
            }
        }

        /// <summary>
        /// 获取用户组织机构树
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<BaseOrganize> GetUserOrganizeListWhere(string where, string orderBy)
        {
            where = where.IsNullOrEmpty() ? " 1=1 " : where;
            orderBy = orderBy.IsNullOrEmpty() ? " order by SortCode" : " order by " + orderBy + "";
            var sql = string.Format(@"select * from Base_Organize where {0} {1}", where, orderBy);
            lock (db)
            {
                return db.Fetch<BaseOrganize>(sql);
            }
        }

        /// <summary>
        /// 得到部门所在的单位
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public BaseOrganize GetUserCompanyId(string departmentId)
        {
            string sql = string.Format(@"select * from F_GetRecursiveParent('{0}') where type=1", departmentId);
            return db.Fetch<BaseOrganize>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 得到部门所在的分部
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public BaseOrganize GetUserSubCompanyId(string departmentId)
        {
            string sql = string.Format(@"select * from F_GetRecursiveParent('{0}') where type=2", departmentId);
            return db.Fetch<BaseOrganize>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取用户数据权限类型标识
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserDataPermissionConstraint(string userId)
        {
            string sql = string.Format(@"select PermissionConstraint from  Base_PermissionScope where PermissionMaster='BaseUser' and PermissionAccess='BaseOrganize' and PermissionMasterValue='{0}'",userId);
            return db.Fetch<string>(sql).FirstOrDefault();
        }
        /// <summary>
        /// 获取用户对应的功能模块权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BaseModule> GetUserModuleListByUserId(string userId)
        {
            string sql = string.Format(@"
                                        SELECT
                                                m.*,
                                                CASE
                                                    WHEN t.PermissionAccessValue IS NOT NULL THEN 'true' ELSE 'false'
                                                END
                                                AS isChecked
                                            FROM Base_Module AS m

                                            LEFT JOIN (SELECT
                                                *
                                            FROM (SELECT
                                                *
                                            FROM Base_Permission
                                            WHERE PermissionMaster = 'BaseUser' AND PermissionAccess = 'BaseModule')
                                            AS p1

                                            WHERE p1.PermissionMasterValue = '{0}')
                                            AS t

                                        ON t.PermissionAccessValue = m.Id  WHERE  m.enabled=1 ", userId);
            lock (db)
            {
                return db.Fetch<BaseModule>(sql);
            }
        }

        /// <summary>
        /// 获取用户对应的操作按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BasePermissionItem> GetUserButtonListByUserId(string userId)
        {
            var sql = string.Format(@";with t as
                                        (
                                        select Id, ParentId, Name,Code
                                        from Base_PermissionItem
                                        where Code = 'Operate_Item'
                                        union all
                                        select r1.Id,r1.ParentId,r1.Name,r1.Code
                                        from Base_PermissionItem r1 join t as r2 on r1.ParentId = r2.Id
                                        )
                                        select t1.*, 
                                            CASE
                                                WHEN t2.PermissionAccessValue IS NOT NULL THEN 'true' ELSE 'false'
                                            END
                                            AS isChecked from t as t1
                                          LEFT JOIN (SELECT
                                                    *
                                                FROM (SELECT
                                                    *
                                                FROM Base_Permission
                                                WHERE PermissionMaster = 'BaseUser' AND PermissionAccess = 'BaseButton')
                                                AS p1
                                                WHERE p1.PermissionMasterValue = '{0}')
                                                AS t2
                                                  ON t2.PermissionAccessValue = t1.Id  ", userId);
            lock (db)
            {
                return db.Fetch<BasePermissionItem>(sql);
            }
        }

        /// <summary>
        /// 获取用户对应的明细数据权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BaseOrganize> GetUserDataDetailsListByUserId(string userId)
        {
            var sql = string.Format(@"SELECT
    tt.*,
    CASE
        WHEN ps.PermissionAccessValue IS NOT NULL THEN 'true' ELSE 'false'
    END AS isChecked
FROM (SELECT
     *
FROM Base_Organize)
AS tt
LEFT JOIN (SELECT
    *
FROM Base_PermissionScope
WHERE PermissionMaster = 'BaseUser' AND PermissionAccess = 'BaseOrganize' and PermissionConstraint='Details' AND PermissionMasterValue = '{0}')
AS ps
    ON ps.PermissionAccessValue = tt.Id", userId);
            lock (db)
            {
                return db.Fetch<BaseOrganize>(sql);
            }
        }


        /// <summary>
        /// 获取用户对应的字段权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BasePermissionItem> GetUserFieldListByUserId(string userId)
        {
            var sql = string.Format(@";with t as
                                        (
                                        select Id, ParentId, Name,Code
                                        from Base_PermissionItem
                                        where Code = 'Access_Item'
                                        union all
                                        select r1.Id,r1.ParentId,r1.Name,r1.Code
                                        from Base_PermissionItem r1 join t as r2 on r1.ParentId = r2.Id
                                        )
                                        select t1.*, 
                                            CASE
                                                WHEN t2.PermissionAccessValue IS NOT NULL THEN 'true' ELSE 'false'
                                            END
                                            AS isChecked from t as t1
                                          LEFT JOIN (SELECT
                                                    *
                                                FROM (SELECT
                                                    *
                                                FROM Base_Permission
                                                WHERE PermissionMaster = 'BaseUser' AND PermissionAccess = 'BaseAccess')
                                                AS p1
                                                WHERE p1.PermissionMasterValue = '{0}')
                                                AS t2
                                                  ON t2.PermissionAccessValue = t1.Id  ", userId);
            lock (db)
            {
                return db.Fetch<BasePermissionItem>(sql);
            }
        }

        /// <summary>
        /// 保存用户对应的模块权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateUserModuleList(string id, string accessValueArray)
        {
            lock (db)
            {
                using (var scope = db.GetTransaction())
                {
                    string deleteSQL = string.Format(@"delete FROM Base_Permission WHERE PermissionMaster='BaseUser' AND PermissionAccess='BaseModule' AND PermissionMasterValue='{0}'", id);
                    db.Execute(deleteSQL);
                    foreach (var accessValue in accessValueArray.Split(','))
                    {
                        if (!string.IsNullOrEmpty(accessValue))
                        {
                            string insertSQL = string.Format(@"INSERT INTO Base_Permission (Id, PermissionMaster, PermissionMasterValue, PermissionAccess, PermissionAccessValue)
	VALUES (NEWID(), 'BaseUser', '{0}', 'BaseModule', '{1}')", id, accessValue);
                            db.Execute(insertSQL);
                        }
                    }
                    scope.Complete();
                }
            }
            return true;
        }

        /// <summary>
        /// 保存用户对应的操作按钮权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateUserButtonList(string id, string accessValueArray)
        {
            lock (db)
            {
                using (var scope = db.GetTransaction())
                {
                    string deleteSQL = string.Format(@"delete FROM Base_Permission WHERE PermissionMaster='BaseUser' AND PermissionAccess='BaseButton' AND PermissionMasterValue='{0}'", id);
                    db.Execute(deleteSQL);
                    foreach (var accessValue in accessValueArray.Split(','))
                    {
                        if (!string.IsNullOrEmpty(accessValue))
                        {
                            string insertSQL = string.Format(@"INSERT INTO Base_Permission (Id, PermissionMaster, PermissionMasterValue, PermissionAccess, PermissionAccessValue)
	VALUES (NEWID(), 'BaseUser', '{0}', 'BaseButton', '{1}')", id, accessValue);
                            db.Execute(insertSQL);
                        }
                    }
                    scope.Complete();
                }
            }
            return true;
        }

        /// <summary>
        /// 保存用户对应的字段权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateUserFieldList(string id, string accessValueArray)
        {
            lock (db)
            {
                using (var scope = db.GetTransaction())
                {
                    string deleteSQL = string.Format(@"delete FROM Base_Permission WHERE PermissionMaster='BaseUser' AND PermissionAccess='BaseAccess' AND PermissionMasterValue='{0}'", id);
                    db.Execute(deleteSQL);
                    foreach (var accessValue in accessValueArray.Split(','))
                    {
                        if (!string.IsNullOrEmpty(accessValue))
                        {
                            string insertSQL = string.Format(@"INSERT INTO Base_Permission (Id, PermissionMaster, PermissionMasterValue, PermissionAccess, PermissionAccessValue)
	VALUES (NEWID(), 'BaseUser', '{0}', 'BaseAccess', '{1}')", id, accessValue);
                            db.Execute(insertSQL);
                        }
                    }
                    scope.Complete();
                }
            }
            return true;
        }

         /// <summary>
        /// 保存用户对应的数据权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateUserDataList(string id, string accessValueArray, string constraint)
        {
            lock (db)
            {
                using (var scope = db.GetTransaction())
                {
                    string deleteSQL = string.Format(@"DELETE FROM Base_PermissionScope WHERE PermissionMaster = 'BaseUser' AND PermissionAccess = 'BaseOrganize' AND PermissionMasterValue='{0}'", id);
                    db.Execute(deleteSQL);
                    foreach (var accessValue in accessValueArray.Split(','))
                    {
                        if (!string.IsNullOrEmpty(accessValue))
                        {
                            string insertSQL = string.Format(@"INSERT INTO Base_PermissionScope (Id, PermissionMaster, PermissionMasterValue, PermissionAccess, PermissionAccessValue, PermissionConstraint)
	VALUES (NEWID(), 'BaseUser', '{0}', 'BaseOrganize', '{1}', 'Details')", id, accessValue);
                            db.Execute(insertSQL);
                        }
                    }
                    //如果accessValueArray为空  则表示存储的不是明细数据权限
                    if (string.IsNullOrEmpty(accessValueArray))
                    {

                        string insertSQLSec = string.Format(@"INSERT INTO Base_PermissionScope (Id, PermissionMaster, PermissionMasterValue, PermissionAccess, PermissionAccessValue, PermissionConstraint)
	VALUES (NEWID(), 'BaseUser', '{0}', 'BaseOrganize', '', '{1}')", id, constraint);
                        db.Execute(insertSQLSec);
                    }
                  
                    scope.Complete();
                }
            }
            return true;
        }


         /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BaseRole> GetUserRoleListWhere(string userId)
        {
            string sql = string.Format(@"SELECT
	                                        r.*,
	                                        CASE
		                                        WHEN m.RoleId IS NOT NULL THEN 'true' ELSE 'false'
	                                        END
	                                        AS isChecked
                                        FROM Base_Role AS r
                                        LEFT JOIN (SELECT
	                                        *
                                        FROM (SELECT
	                                        Id AS UserId,
	                                        RoleId
                                        FROM Base_User UNION SELECT
	                                        UserId,
	                                        RoleId
                                        FROM Base_UserRole)
                                        AS t
                                        WHERE t.UserId = '{0}')
                                        AS m
	                                        ON m.RoleId = r.Id", userId);
            return db.Fetch<BaseRole>(sql);
        }


        
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public bool SaveAllotRole(string userId, string roleIds)
        {
            lock (db)
            {
                using (var scope = db.GetTransaction())
                {
                    var deleteSQL = string.Format(@"delete from Base_UserRole where UserId='{0}'", userId);
                    db.Execute(deleteSQL);
                    foreach (var roleId in roleIds.Split(','))
                    {
                        if (!string.IsNullOrEmpty(roleId))
                        {
                            var insertSQL = string.Format(@"insert into Base_UserRole values(newid(),'{0}','{1}')", userId, roleId);
                            db.Execute(insertSQL);
                        }
                       
                    }
                    scope.Complete();
                }
            }
            return true;
        }
    }
}
