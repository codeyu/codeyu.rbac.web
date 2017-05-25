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