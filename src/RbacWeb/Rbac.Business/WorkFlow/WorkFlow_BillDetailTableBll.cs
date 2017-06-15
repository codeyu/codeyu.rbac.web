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
    /// 表单明细表
    /// <author>
    ///		<name>lixinyu</name>
    ///		<date></date>
    /// </author>
    /// </summary>
    public class WorkFlow_BillDetailTableBll : BaseBll<WorkFlow_BillDetailTable>
    {
        protected IWorkFlow_BillDetailTableDal dal = null;
        public  WorkFlow_BillDetailTableBll() 
        {
             IApplicationContext springContext = ContextRegistry.GetContext();
             dal = springContext.GetObject("WorkFlow_BillDetailTableDal") as IWorkFlow_BillDetailTableDal;
             base.idal = dal;
        }
    }
}