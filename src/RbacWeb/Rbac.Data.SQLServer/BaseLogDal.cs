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
    public class BaseLogDal : BaseDAL<BaseLog>, IBaseLogDal
    {
        /// <summary>
        /// 单例模式
        /// </summary>
        private static BaseLogDal item;
        public static BaseLogDal Instance
        {
            get
            {
                if (item == null)
                {
                    item = new BaseLogDal();
                }
                return item;
            }
        }

        /// <summary>
        /// 创建公共方法
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        protected override string CreateSQL(string where, string orderBy)
        {
            where = where.IsNullOrEmpty() ? " 1=1 " : where;
            orderBy = orderBy.IsNullOrEmpty() ? " order by Date desc" : " order by " + orderBy + "";
            return string.Format(@"
                                    SELECT *,CASE WHEN Type=1 THEN '新增' WHEN Type=2 THEN '修改' WHEN Type='3' THEN '删除' ELSE '其他' END as TypeName FROM base_log
                                     where {0} {1} ", where, orderBy);
        }

        /// <summary>
        /// 添加创建日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void AddCreateLog<T>(T entity)
        {
            BaseLog baseLog = new BaseLog();
            baseLog.Id = Guid.NewGuid().ToString();
            baseLog.UserId = RequestCache.GetCacheUser().UserId;
            baseLog.UserName = RequestCache.GetCacheUser().UserName;
            baseLog.RealName = RequestCache.GetCacheUser().RealName;
            baseLog.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            baseLog.IPAddress = RequestHelper.GetIPAddress();
            baseLog.NavigationUrl = RequestHelper.GetScriptName;
            baseLog.Type = 1;
            baseLog.Description = entity.SerializeXml();
            lock (db)
            {
                db.Insert(baseLog);
            }
        }

        /// <summary>
        /// 添加修改日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldEntity"></param>
        /// <param name="newEntity"></param>
        public void AddUpdateLog<T>(T oldEntity, T newEntity)
        {
            BaseLog baseLog = new BaseLog();
            baseLog.Id = Guid.NewGuid().ToString();
            baseLog.UserId = RequestCache.GetCacheUser().UserId;
            baseLog.UserName = RequestCache.GetCacheUser().UserName;
            baseLog.RealName = RequestCache.GetCacheUser().RealName;
            baseLog.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            baseLog.IPAddress = RequestHelper.GetIPAddress();
            baseLog.NavigationUrl = RequestHelper.GetScriptName;
            baseLog.Type = 2;
            baseLog.OldXml = oldEntity.SerializeXml();
            baseLog.NewXml = newEntity.SerializeXml();
            lock (db)
            {
                db.Insert(baseLog);
            }
        }

        /// <summary>
        /// 添加删除日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void AddDeleteLog<T>(T entity)
        {
            BaseLog baseLog = new BaseLog();
            baseLog.Id = Guid.NewGuid().ToString();
            baseLog.UserId = RequestCache.GetCacheUser().UserId;
            baseLog.UserName = RequestCache.GetCacheUser().UserName;
            baseLog.RealName = RequestCache.GetCacheUser().RealName;
            baseLog.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            baseLog.IPAddress = RequestHelper.GetIPAddress();
            baseLog.NavigationUrl = RequestHelper.GetScriptName;
            baseLog.Type = 3;
            baseLog.Description = entity.SerializeXml();
            lock (db)
            {
                db.Insert(baseLog);
            }
        }

        /// <summary>
        /// 添加其他日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void AddOtherLog<T>(T entity)
        {
            BaseLog baseLog = new BaseLog();
            baseLog.Id = Guid.NewGuid().ToString();
            baseLog.UserId = RequestCache.GetCacheUser().UserId;
            baseLog.UserName = RequestCache.GetCacheUser().UserName;
            baseLog.RealName = RequestCache.GetCacheUser().RealName;
            baseLog.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            baseLog.IPAddress = RequestHelper.GetIPAddress();
            baseLog.NavigationUrl = RequestHelper.GetScriptName;
            baseLog.Type = 4;
            baseLog.Description = entity.SerializeXml();
            lock (db)
            {
                db.Insert(baseLog);
            }
        }

    }
}
