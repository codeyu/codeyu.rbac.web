//=====================================================================================
// All Rights Reserved , Copyright © Murphy 
//=====================================================================================

using Rbac.Entity;
using Rbac.IData;
using Rbac.Core;
using Rbac.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Data.SQLServer
{
    /// <summary>
    /// 
    /// <author>
    ///		<name>lixinyu</name>
    ///		<date></date>
    /// </author>
    /// </summary>
    public class WorkFlow_BillDal : BaseDAL<WorkFlow_Bill>, IWorkFlow_BillDal
    {
        /// <summary>
        /// 创建公共方法
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        /// </summary>
        protected override string CreateSQL(string where, string orderBy)
        {
            where = where.IsNullOrEmpty() ? " 1=1 " : where;
            orderBy = orderBy.IsNullOrEmpty() ? "order by SortCode" : " order by " + orderBy;
            return string.Format(@"select * from WorkFlow_Bill where {0} {1}", where, orderBy);
        }
    }
}