using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rbac.Entity
{
    public class DataBaseParam
    {
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        ///创建年份
        /// </summary>
        public string CreateYear { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 类命名空间
        /// </summary>
        public string NameSpaceClass { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 表说明
        /// </summary>
        public string TableExplain { get; set; }
    }
}
