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
    public class BaseJobTitleDal : BaseDAL<BaseJobTitle>, IBaseJobTitleDal
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
            orderBy = orderBy.IsNullOrEmpty() ? " order by SortCode" : " order by " + orderBy;
            return string.Format(@"select * from Base_JobTitle where {0} {1}", where, orderBy);
        }
    }
}
