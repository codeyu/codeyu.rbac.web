using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Entity
{
    /// <summary>
    /// 功能模块权限实体
    /// </summary>
    public class BaseModulePermission
    {
        public string UserId { get; set; }
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleIcon { get; set; }
        public string ModuleUrl { get; set; }
        public string SortCode { get; set; }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
    }
}
