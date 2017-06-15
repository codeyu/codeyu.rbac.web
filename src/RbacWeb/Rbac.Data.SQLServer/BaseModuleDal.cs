using Rbac.Entity;
using Rbac.IData;
using Rbac.Utils;
using Rbac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Data.SQLServer
{
    public class BaseModuleDal : BaseDAL<BaseModule>, IBaseModuleDal
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
            return string.Format(@"select * FROM (select m.*,m2.Name as ParentName,m2.SortCode AS ParentSortCode,a.amount,
                                            CASE
                                                WHEN a.amount IS NULL THEN 'true' ELSE 'false'
                                            END AS isleaf FROM Base_Module as m
                                    LEFT JOIN Base_Module as m2
                                    ON m.ParentId=m2.Id
                                    LEFT JOIN (SELECT
                                                COUNT(1) AS amount,
                                                ParentId
                                            FROM Base_Module
                                            GROUP BY ParentId) a
                                                ON a.ParentId = m.Id) as t where {0} {1}", where, orderBy);
        }
    }
}
