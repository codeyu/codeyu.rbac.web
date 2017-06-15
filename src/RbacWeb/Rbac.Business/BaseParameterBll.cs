using Rbac.Entity;
using Rbac.IData;
using Rbac.Utils;
using Rbac.Core;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Rbac.Business
{
    public class BaseParameterBll : BaseBll<BaseParameter>
    {
        protected IBaseParameterDal dal = null;
        public BaseParameterBll()
        {
            IApplicationContext springContext = ContextRegistry.GetContext();
            dal = springContext.GetObject("BaseParameterDal") as IBaseParameterDal;
            base.idal = dal;
        }

      
        /// <summary>
        /// 保存参数信息
        /// </summary>
        /// <param name="insertParameters"></param>
        /// <param name="updateParameters"></param>
        /// <param name="deleteParameters"></param>
        /// <returns></returns>
        public bool SaveParameter(List<BaseParameter> insertParameters, List<BaseParameter> updateParameters, List<BaseParameter> deleteParameters)
        {
            using (TransactionScope tsScope = new TransactionScope())
            {
                //插入
                foreach (var insertParameter in insertParameters)
                {
                    if (insertParameter != null)
                    {
                        Create(insertParameter);
                    }
                }
                //更新
                foreach (var updateParameter in updateParameters)
                {
                    if (updateParameter != null)
                    {
                        Update(updateParameter);
                    }
                }
                //删除
                foreach (var deleteParameter in deleteParameters)
                {
                    if (deleteParameter != null)
                    {
                        Delete(deleteParameter.Id);
                    }
                }
                tsScope.Complete();
            }
            return true;
        }
    }
}
