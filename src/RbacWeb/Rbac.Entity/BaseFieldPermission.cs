using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Entity
{
    /// <summary>
    /// 字段权限实体
    /// </summary>
    public class BaseFieldPermission
    {
        public string UserId { get; set; }
        public string FieldCode { get; set; }
        public string FieldId { get; set; }
        public string FieldName { get; set; }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
        public string ParentCode { get; set; }
    }
}
