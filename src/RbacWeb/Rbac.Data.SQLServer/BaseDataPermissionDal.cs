using Rbac.IData;
using Rbac.Utils;
using Rbac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Data.SQLServer
{  
    /// <summary>
    /// 数据权限基类
    /// </summary>
    public class BaseDataPermissionDal : IBaseDataPermissionDal
    {
      
        BaseStorageDal baseStorageDal = new BaseStorageDal();
        //单利模式
        private static BaseDataPermissionDal baseDataPermissionDal;
        public static BaseDataPermissionDal Instance
        {
            get
            {
                if (baseDataPermissionDal == null)
                {
                    baseDataPermissionDal = new BaseDataPermissionDal();
                }
                return baseDataPermissionDal;
            }
        }

         /// <summary>
        /// 构造函数
        /// </summary>
        public BaseDataPermissionDal()
        {
            string where = baseStorageDal.GetDataPermission(RequestCache.GetCacheUser());
            if (!string.IsNullOrEmpty(where))
            {
                //这里从缓存中取数据
                this.Organization = ""+RequestCache.GetCacheSystem().DataPermissionObj.ToString()+"";
            }
        }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string Organization { get; set; }

    }
}
