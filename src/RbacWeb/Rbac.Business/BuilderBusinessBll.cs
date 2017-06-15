using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rbac.Business
{
    /// <summary>
    /// 生成业务逻辑层
    /// </summary>
    public class BuilderBusinessBll
    {
        /// <summary>
        /// 生成业务逻辑层
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetBusinessClass(string dataBaseName, string tableName, DataBaseParam param)
        {
            StringBuilder strBusiness = new StringBuilder();
            strBusiness.Append("//=====================================================================================\r\n");
            strBusiness.Append("// All Rights Reserved , Copyright © " + param.Company + " " + param.CreateYear + "\r\n");
            strBusiness.Append("//=====================================================================================\r\n\r\n");
            strBusiness.Append("using Rbac.Entity;\r\n");
            strBusiness.Append("using Rbac.IData;\r\n");
            strBusiness.Append("using Rbac.Core;\r\n");
            strBusiness.Append("using Newtonsoft.Json;\r\n");
            strBusiness.Append("using Spring.Context;\r\n");
            strBusiness.Append("using Spring.Context.Support;\r\n");
            strBusiness.Append("using System;\r\n");
            strBusiness.Append("using System.Collections.Generic;\r\n");
            strBusiness.Append("using System.Linq;\r\n");
            strBusiness.Append("using System.Text;\r\n");
            strBusiness.Append("using System.Threading.Tasks;\r\n\r\n");
            strBusiness.Append("namespace " + param.NameSpaceClass + "\r\n");
            strBusiness.Append("{\r\n");
            strBusiness.Append("    /// <summary>\r\n");
            strBusiness.Append("    /// " + param.TableExplain + "\r\n");
            strBusiness.Append("    /// <author>\r\n");
            strBusiness.Append("    ///		<name>" + param.Author + "</name>\r\n");
            strBusiness.Append("    ///		<date>" + param.CreateDate + "</date>\r\n");
            strBusiness.Append("    /// </author>\r\n");
            strBusiness.Append("    /// </summary>\r\n");
            strBusiness.Append("    public class " + param.ClassName + "Bll : BaseBll(" + param.ClassName + ")\r\n");
            strBusiness.Append("    {\r\n");
            strBusiness.Append("        protected I" + param.ClassName + "Dal dal = null;\r\n");
            strBusiness.Append("        public  " + param.ClassName + "Bll() \r\n");
            strBusiness.Append("        {\r\n");
            strBusiness.Append("             IApplicationContext springContext = ContextRegistry.GetContext(); \r\n");
            strBusiness.Append("             dal = springContext.GetObject(" + param.ClassName + "Dal) as I" + param.ClassName + "Dal; \r\n");
            strBusiness.Append("             base.idal = " + param.ClassName.ToLower() + "Dal;\r\n");  
            strBusiness.Append("        }\r\n");
            strBusiness.Append("    }\r\n");
            strBusiness.Append("}");
            return strBusiness.ToString();
        }
    }
}
