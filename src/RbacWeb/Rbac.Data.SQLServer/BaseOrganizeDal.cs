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
    public class BaseOrganizeDal : BaseDAL<BaseOrganize>, IBaseOrganizeDal
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
            return string.Format(@"select * from VM_GetOrganizeList where {0} {1}", where, orderBy);
        }


        /// <summary>
        /// 获取组织机构角色
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<BaseRole> GetOrganizeRoleListWhere(string departmentId)
        {
            string sql = string.Format(@"select r.*,
								CASE
                                    WHEN m.ObjectId IS NOT NULL THEN 'true' ELSE 'false'
                                END
                                AS isChecked from Base_Role as  r
                           left join (
									select * from Base_OrganizeMember where DepartmentId='{0}' and ObjectType='BaseRole' 
									) as m
                           on m.ObjectId=r.Id order by SortCode", departmentId);
            return db.Fetch<BaseRole>(sql);
        }

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public bool SaveAllotRole(string departmentId, string roleIds)
        {
            lock (db)
            {
                using (var scope = db.GetTransaction())
                {
                    var deleteSQL = string.Format(@"delete from Base_OrganizeMember where departmentId='{0}' and objecttype='BaseRole'", departmentId);
                    db.Execute(deleteSQL);
                    foreach (var roleId in roleIds.Split(','))
                    {
                        var insertSQL = string.Format(@"insert into Base_OrganizeMember values(newid(),'{0}','{1}','BaseRole')", departmentId, roleId);
                        db.Execute(insertSQL);
                    }
                    scope.Complete();
                }
            }
            return true;
        }
    }
}
