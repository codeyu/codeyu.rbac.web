using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rbac.Business
{

    /// <summary>
    /// 生成数据访问层
    /// </summary>
    public class BuilderDalBll
    {
        /// <summary>
        /// 生成数据访问接口层
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetIDalClass(string dataBaseName, string tableName, DataBaseParam param)
        {
            StringBuilder strDal = new StringBuilder();
            strDal.Append("//=====================================================================================\r\n");
            strDal.Append("// All Rights Reserved , Copyright © " + param.Company + " " + param.CreateYear + "\r\n");
            strDal.Append("//=====================================================================================\r\n\r\n");
            strDal.Append("using Rbac.Entity;\r\n");
            strDal.Append("using Rbac.IData;\r\n");
            strDal.Append("using Rbac.Core;\r\n");
            strDal.Append("using Newtonsoft.Json;\r\n");
            strDal.Append("using Spring.Context;\r\n");
            strDal.Append("using Spring.Context.Support;\r\n");
            strDal.Append("using System;\r\n");
            strDal.Append("using System.Collections.Generic;\r\n");
            strDal.Append("using System.Linq;\r\n");
            strDal.Append("using System.Text;\r\n");
            strDal.Append("using System.Threading.Tasks;\r\n\r\n");
            strDal.Append("namespace " + param.NameSpaceClass + "\r\n");
            strDal.Append("{\r\n");
            strDal.Append("    /// <summary>\r\n");
            strDal.Append("    /// " + param.TableExplain + "\r\n");
            strDal.Append("    /// <author>\r\n");
            strDal.Append("    ///		<name>" + param.Author + "</name>\r\n");
            strDal.Append("    ///		<date>" + param.CreateDate + "</date>\r\n");
            strDal.Append("    /// </author>\r\n");
            strDal.Append("    /// </summary>\r\n");
            strDal.Append("    public class " + param.ClassName + "Dal : BaseDal<" + param.ClassName + ">,I" + param.ClassName + "Dal\r\n");
            strDal.Append("    {\r\n");
            strDal.Append("            /// <summary>\r\n");
            strDal.Append("            /// 创建公共方法\r\n");
            strDal.Append("            /// </summary>\r\n");
            strDal.Append("            /// <param name=\"where\"></param>\r\n");
            strDal.Append("            /// <param name=\"orderBy\"></param>\r\n");
            strDal.Append("            /// <returns></returns>\r\n");
            strDal.Append("            /// </summary>\r\n");
            strDal.Append("              protected override string CreateSQL(string where, string orderBy)\r\n");
            strDal.Append("              {\r\n");
            strDal.Append("                    where = where.IsNullOrEmpty() ? \" 1=1 \" : where;");
            strDal.Append("                    orderBy = orderBy.IsNullOrEmpty() ? \"order by SortCode\" : \" order by \" + orderBy;");
            strDal.Append("              }\r\n");
            strDal.Append("    }\r\n");
            strDal.Append("}");
            return strDal.ToString();
        }
    }
}
