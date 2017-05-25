using Murphy.Entity;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.IData
{
    public interface IDataBaseDal
    {
           /// <summary>
        /// 得到服务器所有数据库
        /// </summary>
        /// <returns></returns>
        List<string> GetDataBaseListWhere();

          /// <summary>
        /// 得到数据库所有表
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        List<DataBaseTable> GetTableListWhere(string dataBaseName);

        /// <summary>
        /// 得到一个表中的所有字段    带字段类型转换
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        List<DataBaseField> GetFieldListWhere(string dataBaseName, string tableName);

        /// <summary>
        /// 得到一个表中的所有字段
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <param name="query"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        Page<DataBaseField> GetPageFieldListWhere(out string pager, int pageNumber, int pageSize, string dataBaseName, string tableName, string query, string orderBy);
        
         /// <summary>
        /// 获取某一个表的主键字段
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
       string GetPrimaryKeyColumns(string dataBaseName, string tableName);

         /// <summary>
        /// 得到实体数据类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetEntityType(string name);
    }
}
