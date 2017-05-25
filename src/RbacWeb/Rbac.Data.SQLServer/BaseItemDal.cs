using Murphy.Entity;
using Murphy.IData;
using Murphy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Data.SQLServer
{
    public class BaseItemDal : BaseDAL<BaseItem>, IBaseItemDal
    {
        /// <summary>
        /// 创建SQL
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        protected override string CreateSQL(string where, string orderBy)
        {
            where = string.IsNullOrEmpty(where) ? " 1=1 " : where;
            orderBy = string.IsNullOrEmpty(orderBy) ? " order by SortCode" : " order by " + orderBy + "";
            return string.Format(@"select * from Base_Item where {0} {1}", where, orderBy);
        }
    }
}
