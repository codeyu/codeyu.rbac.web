using Rbac.Entity;
using Rbac.IData;
using Rbac.Utils;
using Rbac.Core;
using Newtonsoft.Json;
using PetaPoco;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Business
{
    public class DataBaseBll
    {

        protected IDataBaseDal dataBaseDal = null;
        BuilderEntityBll builderEntityBll = new BuilderEntityBll();
        BuilderBusinessBll builderBusinessBll = new BuilderBusinessBll();
        BuilderDalBll builderDalBll = new BuilderDalBll();
        BuilderIDalBll builderIDalBll = new BuilderIDalBll();
        public DataBaseBll()
        {
            IApplicationContext springContext = ContextRegistry.GetContext();
            dataBaseDal = springContext.GetObject("DataBaseDal") as IDataBaseDal;
        }

        /// <summary>
        /// 获取数据表树形树
        /// </summary>
        /// <returns></returns>
        public string GetTableTree()
        {
            List<BaseTree> rootTreeList = new List<BaseTree>();
            List<BaseTree> dataBaseTreeList = new List<BaseTree>();
            //加载数据库
            List<string> dataBaseList = GetDataBaseListWhere();
            foreach (var dataBase in dataBaseList)
            {
                BaseTree dataBaseTree = new BaseTree();
                dataBaseTree.name = dataBase;
                dataBaseTree.type = 2;
                dataBaseTree.font = new font() { color = "#08c" };
              
                //加载数据表
                List<BaseTree> tableTreeList = new List<BaseTree>();
                List<DataBaseTable> tableList = GetTableListWhere(dataBase);
                foreach (var table in tableList)
                {
                    BaseTree baseTableTree = new BaseTree();
                    baseTableTree.name = table.name;
                    //数据库名称
                    baseTableTree.pname = dataBaseTree.name; 
                    baseTableTree.target = "rightFrame";
                    baseTableTree.url = "/Generator/DbServer/FieldList?dataBaseName=" + dataBaseTree.name + "&tableName=" + table.name;
                    baseTableTree.font = new font() { color = "#08c" };
                    tableTreeList.Add(baseTableTree);
                }
                dataBaseTree.children = tableTreeList;
                dataBaseTreeList.Add(dataBaseTree);
            }

            //设置根节点
            BaseTree rootTree = new BaseTree();
            rootTree.name = "数据库";
            rootTree.type = 1;
            rootTree.font = new font() { color = "#08c" };
            rootTree.open = true;
            rootTree.children = dataBaseTreeList;
            rootTreeList.Add(rootTree);
            return JsonConvert.SerializeObject(rootTreeList);
        }


        /// <summary>
        /// 得到服务器所有数据库
        /// </summary>
        /// <returns></returns>
        public List<string> GetDataBaseListWhere()
        {
            return dataBaseDal.GetDataBaseListWhere();
        }

        /// <summary>
        /// 得到数据库所有表
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        public List<DataBaseTable> GetTableListWhere(string dataBaseName)
        {
            return dataBaseDal.GetTableListWhere(dataBaseName);
        }

         /// <summary>
        /// 得到一个表中的所有字段    带字段类型转换
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<DataBaseField> GetFieldListWhere(string dataBaseName, string tableName)
        {
            return dataBaseDal.GetFieldListWhere(dataBaseName, tableName);
        }

        /// <summary>
        /// 得到一个表中的所有字段
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <param name="query"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public Page<DataBaseField> GetPageFieldListWhere(out string pager, string dataBaseName, string tableName, string query, string orderBy)
        {
            return dataBaseDal.GetPageFieldListWhere(out pager, PublicMethod.GetPageNumber(), PublicMethod.GetPageSize(), dataBaseName, tableName, query, orderBy);
        }

         /// <summary>
        /// 获取某一个表的主键字段
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetPrimaryKeyColumns(string dataBaseName, string tableName)
        {
            return dataBaseDal.GetPrimaryKeyColumns(dataBaseName, tableName);
        }

         /// <summary>
        /// 生成实体层
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetEntityClass(string dataBaseName, string tableName, DataBaseParam param)
        {
            return builderEntityBll.GetEntityClass(dataBaseName, tableName, param);
        }

        /// <summary>
        /// 生成业务逻辑层
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetBusinessClass(string dataBaseName, string tableName, DataBaseParam param)
        {
            return builderBusinessBll.GetBusinessClass(dataBaseName, tableName, param);
        }


        /// <summary>
        /// 生成数据库访问层
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetDalClass(string dataBaseName, string tableName, DataBaseParam param)
        {
            return builderDalBll.GetIDalClass(dataBaseName, tableName, param);
        }


        /// <summary>
        /// 生成数据访问接口层
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetIDalClass(string dataBaseName, string tableName, DataBaseParam param)
        {
            return builderIDalBll.GetIDalClass(dataBaseName, tableName, param);
        }
    }
}
