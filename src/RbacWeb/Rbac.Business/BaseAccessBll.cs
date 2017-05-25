using Murphy.Entity;
using Murphy.IData;
using Murphy.Core;
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
    /// 登陆日志
    /// </summary>
    public class BaseAccessBll : BaseBll<BaseAccess>
    {
        protected IBaseAccessDal baseAccessDal = null;

        public BaseAccessBll()
        {
            IApplicationContext springContext = ContextRegistry.GetContext();
            baseAccessDal = springContext.GetObject("BaseAccessDal") as IBaseAccessDal;
            base.idal = baseAccessDal;
        }

        /// <summary>
        /// 统计登录日志
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BaseAccess> GetHighchartAccessLog(string userId)
        {
            return baseAccessDal.GetHighchartAccessLog(userId);
        }
    }
}
