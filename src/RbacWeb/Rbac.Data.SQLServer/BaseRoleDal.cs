using Murphy.Entity;
using Murphy.IData;
using Murphy.Utils;
using Murphy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Data.SQLServer
{
    public class BaseRoleDal : BaseDAL<BaseRole>, IBaseRoleDal
    {
        /// <summary>
        /// 创建公共方法
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        protected override string CreateSQL(string where, string orderBy)
        {
            where = where.IsNullOrEmpty() ? " 1=1 " : where;
            orderBy = orderBy.IsNullOrEmpty() ? " order by SortCode" : " order by " + orderBy + "";
            return string.Format(@"select * from Base_Role where {0} {1}", where, orderBy);
        }
        
        /// <summary>
        /// 删除角色（带权限信息）
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool DeleteRole(string roleId)
        {
            using (var ts = db.GetTransaction())
            {
                base.Delete(roleId);
                string deletePermissionSQL = string.Format(@"delete FROM Base_Permission where PermissionMaster='BaseRole' AND PermissionMasterValue='{0}'", roleId);
                db.Execute(deletePermissionSQL);
                string deletePermissionScopeSQL = string.Format(@"delete FROM Base_PermissionScope where PermissionMaster='BaseRole' AND PermissionMasterValue='{0}'", roleId);
                db.Execute(deletePermissionScopeSQL);
                ts.Complete();
            }
            return true;
        }

        /// <summary>
        /// 获取指定部门下的角色
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<BaseRole> GetRoleListByDepartmentId(string departmentId)
        {
            string sql = string.Format(@"select r.Id,r.Name from Base_OrganizeMember as m
                                        left join Base_Role as r
                                        on m.ObjectId=r.Id
                                        where m.ObjectType='BaseRole' and m.DepartmentId='{0}'", departmentId);
            return db.Fetch<BaseRole>(sql);
        }

          /// <summary>
        /// 获取角色数据权限类型标识
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public string GetRoleDataPermissionConstraint(string roleId)
        {
            string sql = string.Format(@"select PermissionConstraint from  Base_PermissionScope where PermissionMaster='BaseRole' and PermissionAccess='BaseOrganize' and PermissionMasterValue='{0}'", roleId);
            return db.Fetch<string>(sql).FirstOrDefault();
        }

         /// <summary>
        /// 获取角色对应的功能模块权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<BaseModule> GetRoleModuleListByRoleId(string roleId)
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
                                            WHERE PermissionMaster = 'BaseRole' AND PermissionAccess = 'BaseModule')
                                            AS p1

                                            WHERE p1.PermissionMasterValue = '{0}')
                                            AS t

                                        ON t.PermissionAccessValue = m.Id  WHERE  m.enabled=1 ", roleId);
            lock (db)
            {
                return db.Fetch<BaseModule>(sql);
            }
        }

         /// <summary>
        /// 获取角色对应的操作按钮权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<BasePermissionItem> GetRoleButtonListByRoleId(string roleId)
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
                                                WHERE PermissionMaster = 'BaseRole' AND PermissionAccess = 'BaseButton')
                                                AS p1
                                                WHERE p1.PermissionMasterValue = '{0}')
                                                AS t2
                                                  ON t2.PermissionAccessValue = t1.Id  ", roleId);
            lock (db)
            {
                return db.Fetch<BasePermissionItem>(sql);
            }
        }

        /// <summary>
        /// 获取角色对应的明细数据权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<BaseOrganize> GetRoleDataDetailsListByRoleId(string roleId)
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
                                        WHERE PermissionMaster = 'BaseRole' AND PermissionAccess = 'BaseOrganize' and PermissionConstraint='Details' AND PermissionMasterValue = '{0}')
                                        AS ps
                                            ON ps.PermissionAccessValue = tt.Id", roleId);
            lock (db)
            {
                return db.Fetch<BaseOrganize>(sql);
            }
        }

         /// <summary>
        /// 获取用户对应的字段权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<BasePermissionItem> GetRoleFieldListByRoleId(string roleId)
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
                                                WHERE PermissionMaster = 'BaseRole' AND PermissionAccess = 'BaseAccess')
                                                AS p1
                                                WHERE p1.PermissionMasterValue = '{0}')
                                                AS t2
                                                  ON t2.PermissionAccessValue = t1.Id  ", roleId);
            lock (db)
            {
                return db.Fetch<BasePermissionItem>(sql);
            }
        }

        /// <summary>
        /// 保存角色对应的模块权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateRoleModuleList(string id, string accessValueArray)
        {
            lock (db)
            {
                using (var scope = db.GetTransaction())
                {
                    string deleteSQL = string.Format(@"delete FROM Base_Permission WHERE PermissionMaster='BaseRole' AND PermissionAccess='BaseModule' AND PermissionMasterValue='{0}'", id);
                    db.Execute(deleteSQL);
                    foreach (var accessValue in accessValueArray.Split(','))
                    {
                        if (!string.IsNullOrEmpty(accessValue))
                        {
                            string insertSQL = string.Format(@"INSERT INTO Base_Permission (Id, PermissionMaster, PermissionMasterValue, PermissionAccess, PermissionAccessValue)
	VALUES (NEWID(), 'BaseRole', '{0}', 'BaseModule', '{1}')", id, accessValue);
                            db.Execute(insertSQL);
                        }
                    }
                    scope.Complete();
                }
            }
            return true;
        }

        /// <summary>
        /// 保存角色对应的操作按钮权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateRoleButtonList(string id, string accessValueArray)
        {
            lock (db)
            {
                using (var scope = db.GetTransaction())
                {
                    string deleteSQL = string.Format(@"delete FROM Base_Permission WHERE PermissionMaster='BaseRole' AND PermissionAccess='BaseButton' AND PermissionMasterValue='{0}'", id);
                    db.Execute(deleteSQL);
                    foreach (var accessValue in accessValueArray.Split(','))
                    {
                        if (!string.IsNullOrEmpty(accessValue))
                        {
                            string insertSQL = string.Format(@"INSERT INTO Base_Permission (Id, PermissionMaster, PermissionMasterValue, PermissionAccess, PermissionAccessValue)
	VALUES (NEWID(), 'BaseRole', '{0}', 'BaseButton', '{1}')", id, accessValue);
                            db.Execute(insertSQL);
                        }
                    }
                    scope.Complete();
                }
            }
            return true;
        }

        /// <summary>
        /// 保存角色对应的字段权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateRoleFieldList(string id, string accessValueArray)
        {
            lock (db)
            {
                using (var scope = db.GetTransaction())
                {
                    string deleteSQL = string.Format(@"delete FROM Base_Permission WHERE PermissionMaster='BaseRole' AND PermissionAccess='BaseAccess' AND PermissionMasterValue='{0}'", id);
                    db.Execute(deleteSQL);
                    foreach (var accessValue in accessValueArray.Split(','))
                    {
                        if (!string.IsNullOrEmpty(accessValue))
                        {
                            string insertSQL = string.Format(@"INSERT INTO Base_Permission (Id, PermissionMaster, PermissionMasterValue, PermissionAccess, PermissionAccessValue)
	VALUES (NEWID(), 'BaseRole', '{0}', 'BaseAccess', '{1}')", id, accessValue);
                            db.Execute(insertSQL);
                        }
                    }
                    scope.Complete();
                }
            }
            return true;
        }

        /// <summary>
        /// 保存角色对应的数据权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateRoleDataList(string id, string accessValueArray, string constraint)
        {
            lock (db)
            {
                using (var scope = db.GetTransaction())
                {
                    string deleteSQL = string.Format(@"DELETE FROM Base_PermissionScope WHERE PermissionMaster = 'BaseRole' AND PermissionAccess = 'BaseOrganize' AND PermissionMasterValue='{0}'", id);
                    db.Execute(deleteSQL);
                    foreach (var accessValue in accessValueArray.Split(','))
                    {
                        if (!string.IsNullOrEmpty(accessValue))
                        {
                            string insertSQL = string.Format(@"INSERT INTO Base_PermissionScope (Id, PermissionMaster, PermissionMasterValue, PermissionAccess, PermissionAccessValue, PermissionConstraint)
	VALUES (NEWID(), 'BaseRole', '{0}', 'BaseOrganize', '{1}', 'Details')", id, accessValue);
                            db.Execute(insertSQL);
                        }
                    }
                    //如果accessValueArray为空  则表示存储的不是明细数据权限
                    if (string.IsNullOrEmpty(accessValueArray))
                    {

                        string insertSQLSec = string.Format(@"INSERT INTO Base_PermissionScope (Id, PermissionMaster, PermissionMasterValue, PermissionAccess, PermissionAccessValue, PermissionConstraint)
	VALUES (NEWID(), 'BaseRole', '{0}', 'BaseOrganize', '', '{1}')", id, constraint);
                        db.Execute(insertSQLSec);
                    }

                    scope.Complete();
                }
            }
            return true;
        }
    }
}
