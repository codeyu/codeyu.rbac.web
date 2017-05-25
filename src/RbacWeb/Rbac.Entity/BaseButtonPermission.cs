using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Entity
{
    /// <summary>
    /// 操作按钮权限实体
    /// </summary>
    public class BaseButtonPermission
    {
        public string UserId { get; set; }
        public string ButtonId { get; set; }
        public string ButtonName { get; set; }
        public string ButtonCode { get; set; }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
    }
}
