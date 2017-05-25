using Murphy.Entity;
using Murphy.IData;
using Murphy.Core;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Murphy.Business
{
    /// <summary>
    /// 生成实体层
    /// </summary>
    public class BuilderEntityBll
    {

        protected IDataBaseDal dataBaseDal = null;

        public BuilderEntityBll()
        {
            IApplicationContext springContext = ContextRegistry.GetContext();
            dataBaseDal = springContext.GetObject("DataBaseDal") as IDataBaseDal;
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
            List<DataBaseField> dataBaseFieldList = dataBaseDal.GetFieldListWhere(dataBaseName, tableName);
            string primaryKeyColumns = dataBaseDal.GetPrimaryKeyColumns(dataBaseName, tableName);
            StringBuilder strEntity = new StringBuilder();
            strEntity.Append("//=====================================================================================\r\n");
            strEntity.Append("// All Rights Reserved , Copyright © " + param.Company + " " + param.CreateYear + "\r\n");
            strEntity.Append("//=====================================================================================\r\n\r\n");
            strEntity.Append("using Murphy.Core;\r\n");
            strEntity.Append("using PetaPoco;\r\n");
            strEntity.Append("using System;\r\n");
            strEntity.Append("using System.Collections.Generic;\r\n");
            strEntity.Append("using System.ComponentModel;\r\n");
            strEntity.Append("using System.Linq;\r\n");
            strEntity.Append("using System.Text;\r\n");
            strEntity.Append("using System.Threading.Tasks;\r\n\r\n");
            strEntity.Append("namespace " + param.NameSpaceClass + "\r\n");
            strEntity.Append("{\r\n");
            strEntity.Append("    /// <summary>\r\n");
            strEntity.Append("    /// " + param.TableExplain + "\r\n");
            strEntity.Append("    /// <author>\r\n");
            strEntity.Append("    ///		<name>" + param.Author + "</name>\r\n");
            strEntity.Append("    ///		<date>" + param.CreateDate + "</date>\r\n");
            strEntity.Append("    /// </author>\r\n");
            strEntity.Append("    /// </summary>\r\n");
            strEntity.Append("    [Serializable]\r\n");
            strEntity.Append("    [TableName(\"" + tableName + "\")]\r\n");
            strEntity.Append("    [PrimaryKey(\"" + primaryKeyColumns + "\")]\r\n");
            strEntity.Append("    [Description(\"" + param.TableExplain + "\")]\r\n");
            strEntity.Append("    [Key(\"" + primaryKeyColumns + "\")]\r\n");
            strEntity.Append("    public class " + param.ClassName + "\r\n");
            strEntity.Append("    {\r\n");
            foreach (var dataBaseField in dataBaseFieldList)
            {
                strEntity.Append("        private " + dataBaseField.datatype + " _" + dataBaseField.column + " = null;\r\n");
                strEntity.Append("        /// <summary>\r\n");
                strEntity.Append("        /// " + dataBaseField.remark + "\r\n");
                strEntity.Append("        /// </summary>\r\n");
                strEntity.Append("        /// <returns></returns>\r\n");
                strEntity.Append("        [Description(\"" + dataBaseField.remark + "\")]\r\n");
                strEntity.Append("        public " + dataBaseField.datatype + " " + dataBaseField.column + "\r\n");
                strEntity.Append("        {\r\n");
                strEntity.Append("            get\r\n");
                strEntity.Append("            {\r\n");
                strEntity.Append("                return this._" + dataBaseField.column + ";\r\n");
                strEntity.Append("            }\r\n");
                strEntity.Append("            set\r\n");
                strEntity.Append("            {\r\n");
                strEntity.Append("                this._" + dataBaseField.column + " = value;\r\n");
                strEntity.Append("            }\r\n");
                strEntity.Append("        }\r\n");
            }
            strEntity.Append("    }\r\n");
            strEntity.Append("}");
            return strEntity.ToString();
        }
    }
}
