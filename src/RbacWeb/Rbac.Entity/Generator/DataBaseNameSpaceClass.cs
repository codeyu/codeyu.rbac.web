using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rbac.Entity
{
    public class DataBaseNameSpaceClass
    {
        /// <summary>
        /// 实体
        /// </summary>
        public string Entity { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string Dal { get; set; }
        /// <summary>
        /// 业务
        /// </summary>
        public string Business { get; set; }
        /// <summary>
        /// 接口
        /// </summary>
        public string IDal { get; set; }
    }
}
