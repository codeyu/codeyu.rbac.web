//=====================================================================================
// All Rights Reserved , Copyright © Murphy 
//=====================================================================================

using Rbac.Entity;
using Rbac.IData;
using Rbac.Core;
using Newtonsoft.Json;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Business
{
    /// <summary>
    /// 表单管理
    /// <author>
    ///		<name>lixinyu</name>
    ///		<date></date>
    /// </author>
    /// </summary>
    public class WorkFlow_BillBll : BaseBll<WorkFlow_Bill>
    {
        protected IWorkFlow_BillDal dal = null;
        public  WorkFlow_BillBll() 
        {
             IApplicationContext springContext = ContextRegistry.GetContext();
             dal = springContext.GetObject("WorkFlow_BillDal") as IWorkFlow_BillDal;
             base.idal = dal;
        }
    }
}