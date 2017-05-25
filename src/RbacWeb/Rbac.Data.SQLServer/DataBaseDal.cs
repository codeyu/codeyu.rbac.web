using Murphy.Entity;
using Murphy.IData;
using Murphy.Utils;
using Murphy.Core;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Data.SQLServer
{
    /// <summary>
    /// 操作数据库
    /// </summary>
    public class DataBaseDal : IDataBaseDal
    {
        protected Database db = new Database("GeneratorConnectionString");

        /// <summary>
        /// 得到服务器所有数据库
        /// </summary>
        /// <returns></returns>
        public List<string> GetDataBaseListWhere()
        {
            string sql = "select name from sysdatabases";
            return db.Fetch<string>(sql);
        }

        /// <summary>
        /// 得到数据库所有表
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        public List<DataBaseTable> GetTableListWhere(string dataBaseName)
        {
            string connectionString = string.Format(@"Data Source=.;Initial Catalog={0};User Id=sa;Password=sa;", dataBaseName);
            db = new Database(connectionString, "System.Data.SqlClient");
            string sql = "SELECT name FROM sysobjects WHERE xtype='u' order by name";
            return db.Fetch<DataBaseTable>(sql);
        }


        /// <summary>
        /// 得到一个表中的所有字段    带字段类型转换
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<DataBaseField> GetFieldListWhere(string dataBaseName, string tableName)
        {
            string connectionString = string.Format(@"Data Source=.;Initial Catalog={0};User Id=sa;Password=sa;", dataBaseName);
            db = new Database(connectionString, "System.Data.SqlClient");
            string sql = string.Format(@"SELECT
                                         [number]=a.colorder,
                                         [column] =a.name,
	                                     [datatype]=b.name,
	                                     [length]=COLUMNPROPERTY(a.id,a.name,'PRECISION'),
                                         [key]=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name in (
                                         SELECT name FROM sysindexes WHERE indid in(
                                         SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid
                                         ))) then '√' else '' end,
                                         [isnullable]=case when a.isnullable=1 then '√'else '' end,
                                         [remark]=isnull(g.[value],'')
                                         FROM syscolumns a
                                         left join systypes b on a.xusertype=b.xusertype
                                         inner join sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
                                         left join syscomments e on a.cdefault=e.id
                                         left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id 
                                         left join sys.extended_properties f on d.id=f.major_id and f.minor_id=0
                                         where d.name='{0}' order by a.id,a.colorder", tableName);
            List<DataBaseField> dataBaseFieldList = new List<DataBaseField>();
            var fieldList = db.Fetch<DataBaseField>(sql);
            foreach (var item in fieldList)
            {
                dataBaseFieldList.Add(new DataBaseField()
                            {
                                column = item.column,
                                remark = item.remark,
                                //转换成实体类型
                                datatype = GetEntityType(item.datatype)
                            });
            }
            return dataBaseFieldList;
        }

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
        public Page<DataBaseField> GetPageFieldListWhere(out string pager, int pageNumber, int pageSize, string dataBaseName, string tableName, string query, string orderBy)
        {
            string connectionString = string.Format(@"Data Source=.;Initial Catalog={0};User Id=sa;Password=sa;", dataBaseName);
            db = new Database(connectionString, "System.Data.SqlClient");
            string sql = string.Format(@"SELECT
                                         [number]=a.colorder,
                                         [column] =a.name,
	                                     [datatype]=b.name,
	                                     [length]=COLUMNPROPERTY(a.id,a.name,'PRECISION'),
                                         [key]=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name in (
                                         SELECT name FROM sysindexes WHERE indid in(
                                         SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid
                                         ))) then '√' else '' end,
                                         [isnullable]=case when a.isnullable=1 then '√'else '' end,
                                         [remark]=isnull(g.[value],'')
                                         FROM syscolumns a
                                         left join systypes b on a.xusertype=b.xusertype
                                         inner join sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
                                         left join syscomments e on a.cdefault=e.id
                                         left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id 
                                         left join sys.extended_properties f on d.id=f.major_id and f.minor_id=0
                                         where d.name='{0}' order by a.id,a.colorder", tableName);
            var pageResult = db.Page<DataBaseField>(pageNumber, pageSize, sql);
            pager = PageHelper.GetPageHtml(pageResult.TotalItems, pageNumber, pageSize, query);
            return pageResult;
        }

        /// <summary>
        /// 获取某一个表的主键字段
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetPrimaryKeyColumns(string dataBaseName, string tableName)
        {
            string connectionString = string.Format(@"Data Source=.;Initial Catalog={0};User Id=sa;Password=sa;", dataBaseName);
            db = new Database(connectionString, "System.Data.SqlClient");
            string sql = string.Format(@" SELECT
	                                        a.name
                                        FROM SYSCOLUMNS A
                                        INNER JOIN SYSOBJECTS D
	                                        ON A.ID = D.ID
	                                        AND D.XTYPE = 'U'
	                                        AND D.NAME <> 'DTPROPERTIES'
                                        WHERE d.name = '{0}' AND EXISTS (SELECT
	                                        1
                                        FROM SYSOBJECTS
                                        WHERE XTYPE = 'PK' AND PARENT_OBJ = A.ID AND NAME IN (SELECT
	                                        NAME
                                        FROM SYSINDEXES
                                        WHERE INDID IN (SELECT
	                                        INDID
                                        FROM SYSINDEXKEYS
                                        WHERE ID = A.ID AND COLID = A.COLID)
                                        )
                                        )", tableName);
            return db.Fetch<string>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 得到实体数据类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetEntityType(string name)
        {
            if (name == "int" || name == "smallint")
            {
                return "int?";
            }
            else if (name == "tinyint")
            {
                return "byte?";
            }
            else if (name == "numeric" || name == "real" || name == "float")
            {
                return "Single?";
            }
            else if (name == "float")
            {
                return "float?";
            }
            else if (name == "decimal")
            {
                return "decimal?";
            }
            else if (name == "char" || name == "varchar" || name == "text" || name == "nchar" || name == "nvarchar" || name == "ntext")
            {
                return "string";
            }
            else if (name == "bit")
            {
                return "bool?";
            }
            else if (name == "datetime" || name == "smalldatetime")
            {
                return "DateTime?";
            }
            else if (name == "money" || name == "smallmoney")
            {
                return "double?";
            }
            else
            {
                return "string";
            }
        }
    }
}
