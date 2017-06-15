using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Entity
{
    /// <summary>
    /// 数据权限实体
    /// </summary>
    public class BasePermissionScope
    {
        public string Id { get; set; }
        public string PermissionMaster { get; set; }
        public string PermissionMasterValue { get; set; }
        public string PermissionAccess { get; set; }
        public string PermissionAccessValue { get; set; }
        public string PermissionConstraint { get; set; }
    }
}
