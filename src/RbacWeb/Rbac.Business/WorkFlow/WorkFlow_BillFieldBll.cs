//=====================================================================================
// All Rights Reserved , Copyright © Murphy 
//=====================================================================================

using Murphy.Entity;
using Murphy.IData;
using Murphy.Core;
using Newtonsoft.Json;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Business
{
    /// <summary>
    /// 表单字段
    /// <author>
    ///		<name>lixinyu</name>
    ///		<date></date>
    /// </author>
    /// </summary>
    public class WorkFlow_BillFieldBll : BaseBll<WorkFlow_BillField>
    {
        protected IWorkFlow_BillFieldDal dal = null;
        public  WorkFlow_BillFieldBll() 
        {
             IApplicationContext springContext = ContextRegistry.GetContext();
             dal = springContext.GetObject("WorkFlow_BillFieldDal") as IWorkFlow_BillFieldDal;
             base.idal = dal;
        }
    }
}