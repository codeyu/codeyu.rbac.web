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
    public class BaseLogBll:BaseBll<BaseLog>
    {  
        protected IBaseLogDal baseModuleDal = null;
        public BaseLogBll()
        {
            IApplicationContext springContext = ContextRegistry.GetContext();
            baseModuleDal = springContext.GetObject("BaseLogDal") as IBaseLogDal;
            base.idal = baseModuleDal;
        }
    }
}
