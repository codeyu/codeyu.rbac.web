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
    public class BaseItemDetailBll : BaseBll<BaseItemDetail>
    {
        protected IBaseItemDetailDal dal = null;


         public BaseItemDetailBll()
        {
            IApplicationContext springContext = ContextRegistry.GetContext();
            dal = springContext.GetObject("BaseItemDetailDal") as IBaseItemDetailDal;
            base.idal = dal;
        }
    }
}
