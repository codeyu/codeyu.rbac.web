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
    public class BaseAccessDal : BaseDAL<BaseAccess>, IBaseAccessDal
    {
        /// <summary>
        /// 创建SQL
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        protected override string CreateSQL(string where, string orderBy)
        {
            where = where.IsNullOrEmpty() ? " 1=1 " : where;
            orderBy = orderBy.IsNullOrEmpty() ? " order by Date desc" : " order by " + orderBy + "";
            return string.Format(@"select * from Base_Access where {0} {1}", where, orderBy);
        }

        /// <summary>
        /// 统计登录日志
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BaseAccess> GetHighchartAccessLog(string userId)
        {
            string sql = string.Format(@"SELECT  DAY(Date) AS day ,
                                    COUNT(1) AS count
                            FROM    Base_Access
                            WHERE   MONTH(Date) = MONTH(GETDATE()) and UserId='{0}'
                            GROUP BY DAY(Date)", userId);
            return db.Fetch<BaseAccess>(sql);
        }
    }
}
