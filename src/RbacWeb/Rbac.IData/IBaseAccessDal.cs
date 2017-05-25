using Murphy.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.IData
{
    public interface IBaseAccessDal : IBaseDal<BaseAccess>
    {
         /// <summary>
        /// 统计登录日志
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<BaseAccess> GetHighchartAccessLog(string userId);
        
    }
}
