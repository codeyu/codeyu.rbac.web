using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.IData
{
    /// <summary>
    /// 数据层父接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseDal<T> where T : class
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Create(T entity);

        /// <summary>
        /// 创建 无Log
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool CreateNoLog(T entity);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(T entity);

        /// <summary>
        /// 编辑 无Log
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int UpdateNoLog(T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        int Delete(string keyValue);

        /// <summary>
        /// 删除 无Log
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        int DeleteNoLog(string keyValue);

        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        T GetEntity(string keyValue);

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        List<T> GetListWhere(string where, string orderBy);

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
        Page<T> GetPageListWhere(out string pager, int pageNumber, int pageSize, string where, string query,string orderBy);
    }
}
