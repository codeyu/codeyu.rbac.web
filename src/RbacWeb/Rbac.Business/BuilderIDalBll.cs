using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rbac.Business
{
    /// <summary>
    /// 生成数据访问接口层
    /// </summary>
    public class BuilderIDalBll
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
            StringBuilder strIDal = new StringBuilder();
            strIDal.Append("//=====================================================================================\r\n");
            strIDal.Append("// All Rights Reserved , Copyright © " + param.Company + " " + param.CreateYear + "\r\n");
            strIDal.Append("//=====================================================================================\r\n\r\n");
            strIDal.Append("using Rbac.Entity;\r\n");
            strIDal.Append("using Rbac.IData;\r\n");
            strIDal.Append("using Rbac.Core;\r\n");
            strIDal.Append("using Newtonsoft.Json;\r\n");
            strIDal.Append("using Spring.Context;\r\n");
            strIDal.Append("using Spring.Context.Support;\r\n");
            strIDal.Append("using System;\r\n");
            strIDal.Append("using System.Collections.Generic;\r\n");
            strIDal.Append("using System.Linq;\r\n");
            strIDal.Append("using System.Text;\r\n");
            strIDal.Append("using System.Threading.Tasks;\r\n\r\n");
            strIDal.Append("namespace " + param.NameSpaceClass + "\r\n");
            strIDal.Append("{\r\n");
            strIDal.Append("    /// <summary>\r\n");
            strIDal.Append("    /// " + param.TableExplain + "\r\n");
            strIDal.Append("    /// <author>\r\n");
            strIDal.Append("    ///		<name>" + param.Author + "</name>\r\n");
            strIDal.Append("    ///		<date>" + param.CreateDate + "</date>\r\n");
            strIDal.Append("    /// </author>\r\n");
            strIDal.Append("    /// </summary>\r\n");
            strIDal.Append("    public interface I" + param.ClassName + "Dal : IBaseDal<" + param.ClassName + ">\r\n");
            strIDal.Append("    {\r\n");
            strIDal.Append("    }\r\n");
            strIDal.Append("}");
            return strIDal.ToString();
        }
    }
}
