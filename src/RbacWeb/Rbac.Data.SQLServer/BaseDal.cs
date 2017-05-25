using Murphy.Core;
using Murphy.IData;
using Murphy.Utils;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Data.SQLServer
{

    public abstract class BaseDAL<T> : IBaseDal<T> where T : class
    {
        protected Database db = new Database("SQLConnectionString");

        /// <summary>
        /// 创建公共方法SQL
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        protected abstract string CreateSQL(string where, string orderBy);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Create(T entity)
        {
            lock (db)
            {
                object obj = db.Insert(entity);
                if (obj.ToString() == "True")
                {
                    if (RequestCache.GetCacheSystem().OpenLog == "Open")
                    {
                        BaseLogDal.Instance.AddCreateLog<T>(entity);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 创建 无Log
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool CreateNoLog(T entity)
        {
            lock (db)
            {
                object obj = db.Insert(entity);
                if (obj.ToString() == "True")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(T entity)
        {
            //获取KeyValue值
            string primaryKeyValue = PublicMethod.GetKeyFieldValue<T>(entity).ToString();
            var oldEntity = GetEntity(primaryKeyValue);
            lock (db)
            {
                var count = db.Update(entity);
                if (count > 0)
                {
                    if (RequestCache.GetCacheSystem().OpenLog == "Open")
                    {
                        BaseLogDal.Instance.AddUpdateLog<T>(oldEntity, entity);
                    }
                }
                return count;
            }
        }

        /// <summary>
        /// 编辑 无Log
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateNoLog(T entity)
        {
            lock (db)
            {
                return db.Update(entity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int Delete(string keyValue)
        {
            var pd = Database.PocoData.ForType(typeof(T));
            string tableName = pd.TableInfo.TableName;
            string primaryKey = pd.TableInfo.PrimaryKey;
            lock (db)
            {
                int count = db.Delete(tableName, primaryKey, null, keyValue);
                if (count > 0)
                {
                    if (RequestCache.GetCacheSystem().OpenLog == "Open")
                    {
                        BaseLogDal.Instance.AddDeleteLog<T>(GetEntity(keyValue));
                    }
                }
                return count;
            }
        }

        /// <summary>
        /// 删除 无Log
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int DeleteNoLog(string keyValue)
        {
            var pd = Database.PocoData.ForType(typeof(T));
            //得到表名
            string tableName = pd.TableInfo.TableName;
            //得到主键
            string primaryKey = pd.TableInfo.PrimaryKey;
            lock (db)
            {
                return db.Delete(tableName, primaryKey, null, keyValue);
            }
        }

        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public virtual T GetEntity(string keyValue)
        {
            var pd = Database.PocoData.ForType(typeof(T));
            //得到主键
            string primaryKey = pd.TableInfo.PrimaryKey;
            lock (db)
            {
                StringBuilder sbStr = new StringBuilder();
                sbStr.Append(CreateSQL("" + primaryKey + "='" + keyValue + "'", null));
                return db.SingleOrDefault<T>(sbStr.ToString());
            }
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public virtual List<T> GetListWhere(string where, string orderBy)
        {
            lock (db)
            {
                StringBuilder sbStr = new StringBuilder();
                sbStr.Append(CreateSQL(where, orderBy));
                return db.Fetch<T>(sbStr.ToString());
            }
        }

        /// <summary>
        /// 得到一页数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <param name="query"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public virtual Page<T> GetPageListWhere(out string pager, int pageNumber, int pageSize, string where, string query, string orderBy)
        {
            lock (db)
            {
                StringBuilder sbStr = new StringBuilder();
                sbStr.Append(CreateSQL(where, orderBy));
                var pageResult = db.Page<T>(pageNumber, pageSize, sbStr.ToString());
                pager = PageHelper.GetPageHtml(pageResult.TotalItems, pageNumber, pageSize, query);
                return pageResult;
            }
        }
    }
}
