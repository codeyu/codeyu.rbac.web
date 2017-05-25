using Murphy.IData;
using Murphy.Utils;
using Murphy.Core;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Business
{
    /// <summary>
    /// 业务逻辑层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseBll<T> where T : class
    {
        /// <summary>
        /// 数据接口层对象 等待被实例化
        /// </summary>
        protected IBaseDal<T> idal = null;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Create(T entity)
        {
            return idal.Create(entity);
        }

        /// <summary>
        /// 创建 无Log
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool CreateNoLog(T entity)
        {
            return idal.CreateNoLog(entity);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(T entity)
        {
            return idal.Update(entity);
        }

        /// <summary>
        /// 编辑 无Log
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateNoLog(T entity)
        {
            return idal.UpdateNoLog(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int Delete(string keyValue)
        {
            return idal.Delete(keyValue);
        }

        /// <summary>
        /// 删除 无Log
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int DeleteNoLog(string keyValue)
        {
            return idal.DeleteNoLog(keyValue);
        }

        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public T GetEntity(string keyValue)
        {
            return idal.GetEntity(keyValue);
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<T> GetListWhere(string where, string orderBy)
        {
            return idal.GetListWhere(where, orderBy);
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
        public Page<T> GetPageListWhere(out string pager, string where,string query, string orderBy)
        {

            return idal.GetPageListWhere(out pager, PublicMethod.GetPageNumber(), PublicMethod.GetPageSize(), where, query, orderBy);
        }
    }
}
