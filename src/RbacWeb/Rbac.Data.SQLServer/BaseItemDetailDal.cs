using Rbac.Entity;
using Rbac.IData;
using Rbac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Data.SQLServer
{
    public class BaseItemDetailDal : BaseDAL<BaseItemDetail>, IBaseItemDetailDal
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
            orderBy = string.IsNullOrEmpty(orderBy) ? " order by ItemName" : " order by " + orderBy + "";
            return string.Format(@"select * from (select id.*,i.Name as ItemName from Base_ItemDetail as id
                                    left join  Base_Item as i
                                    on i.Id=id.ItemId) as t where {0} {1}", where, orderBy);
        }
    }
}
