using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Core
{  
    /// <summary>
    ///  存 缓存对象
    /// </summary>
    public class CacheSystem
    {
        /// <summary>
        /// 是否开启缓存
        /// </summary>
        public string OpenCache { get; set; }
        /// <summary>
        /// 缓存时长
        /// </summary>
        public string CacheMinute { get; set; }
        /// <summary>
        /// 是否开启操作日志
        /// </summary>
        public string OpenLog { get; set; }
        /// <summary>
        /// 是否开启登陆日志
        /// </summary>
        public string OpenAccessLog { get; set; }

        /// <summary>
        /// 徽标地址
        /// </summary>
        public string SoftwareLogo { get; set; }

        /// <summary>
        /// 版本类型
        /// </summary>
        public string SoftwareEdition { get; set; }

        /// <summary>
        /// 平台版权信息
        /// </summary>
        public string SoftwareCopyright { get; set; }
      
        /// <summary>
        /// 功能模块权限
        /// </summary>
        public Object ModulePermissionObj { get; set; }
        /// <summary>
        /// 操作按钮权限
        /// </summary>
        public Object ButtonPermissionObj { get; set; }
        /// <summary>
        /// 字段权限
        /// </summary>
        public Object FieldPermissionObj { get; set; }
        /// <summary>
        /// 数据权限
        /// </summary>
        public Object DataPermissionObj { get; set; }
    }
}
